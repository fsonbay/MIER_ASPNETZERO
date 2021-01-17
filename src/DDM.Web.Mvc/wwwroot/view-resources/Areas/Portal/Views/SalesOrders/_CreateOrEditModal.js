(function ($) {
    app.modals.CreateOrEditSalesOrderModal = function () {


        var _salesOrdersService = abp.services.app.salesOrders;

        var _modalManager;
        var _$salesOrderInformationForm = null;

        var _SalesOrdercustomerLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/SalesOrders/CustomerLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/SalesOrders/_SalesOrderCustomerLookupTableModal.js',
            modalClass: 'CustomerLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();

            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            //FS: Datetimepicker format and default value
            //modal.find('.date-picker').datetimepicker({
            //    format: 'DD/MM/YYYY' // format you want to show on datetimepicker
            //});

            var d = new Date();
            var today = d.getDate() + "/" + "0" + (d.getMonth() + 1) + "/" + d.getFullYear(); //January = 0.
            modal.find('.date-picker').val(today);

            modal.find('#CustomerId').select2({
                width: '100%'
            }).on('change', function () {
                $(this).valid();
            });

            //$("#vedit-filter").select2({
            //    // your options here
            //}).on('change', function () {
            //    $(this).valid();
            //});

            _$salesOrderInformationForm = _modalManager.getModal().find('form[name=SalesOrderInformationsForm]');
            _$salesOrderInformationForm.validate();

            //FS : Popup modal size
            modal.find(".modal-dialog").addClass("modal-xxl");

        };

        $('#OpenCustomerLookupTableButton').click(function () {

            var salesOrder = _$salesOrderInformationForm.serializeFormToObject();

            _SalesOrdercustomerLookupTableModal.open({ id: salesOrder.customerId, displayName: salesOrder.customerName }, function (data) {
                _$salesOrderInformationForm.find('input[name=customerName]').val(data.displayName);
                _$salesOrderInformationForm.find('input[name=customerId]').val(data.id);
            });
        });

        $('#ClearCustomerNameButton').click(function () {
            _$salesOrderInformationForm.find('input[name=customerName]').val('');
            _$salesOrderInformationForm.find('input[name=customerId]').val('');
        });

        //Wrappper
        var wrapper = $('.subcat-sets');

        buttonVisibility();

        $('#AddOrderLine').click(function (e) {
            e.preventDefault();

            //Create new item and show (to override case style display none)
            var newItem = $(".subcat-set:last-child").clone(true);
            newItem.show();


            //Input Values
            newItem.find(':input').each(function () {

                //Texbox
                $(this).val('');

                //mark-for-delete
                if ($(this).hasClass("mark-for-delete")) {
                    $(this).val('False');
                }

                //ID
                if ($(this).hasClass("Id")) {
                    $(this).val('0');
                }
            });

            //Add clone
            wrapper.append(newItem);

            reorderIndex();

            buttonVisibility();

        });

        this.save = function () {

            if (!_$salesOrderInformationForm.valid()) {

                return;
            }


            if ($('#SalesOrder_CustomerId').prop('required') && $('#SalesOrder_CustomerId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Customer')));
                return;
            }

            var salesOrder = _$salesOrderInformationForm.toObject({ mode: 'first' });

            

            _modalManager.setBusy(true);

            _salesOrdersService.createOrEdit(salesOrder)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditSalesOrderModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });
        };

        //FS : Helper methods
        $(".calc").on("keyup", function () {
            
            var i = $(this).attr('name');
            var start_pos = i.indexOf('[') + 1;
            var end_pos = i.indexOf(']', start_pos);
            var index = i.substring(start_pos, end_pos);

            var quantityName = 'input[name="SalesOrderLines[' + index + '].Quantity"';
            var unitPriceName = 'input[name="SalesOrderLines[' + index + '].UnitPrice"';
            var amountName = 'input[name="SalesOrderLines[' + index + '].LineAmount"';

            var quantity = $(quantityName).val();
            var unitPrice = $(unitPriceName).val();

            quantity = quantity.replace(/\./g, '');
            unitPrice = unitPrice.replace(/\./g, '');

            var amount = addSeparatorsNF((unitPrice * quantity).toFixed(0), '.', ',', '.');
            $(amountName).val(amount);

            calculateTotalAmount();
        });
        $('.number-format').keyup(function (event) {

            // skip for arrow keys
            if (event.which >= 37 && event.which <= 40) {
                event.preventDefault();
            }

            $(this).val(function (index, value) {

                //clean previously added dot
                value = value.replace(/\./g, '');

                //reformat
                return addSeparatorsNF(value, '.', ',', '.');
            });
        });
        $('.delete-subcat').click(function (e) {

            //Cancel default postback
            e.preventDefault();

            //Set hidden value
            $(this).parents('.subcat-set').find('.mark-for-delete').val("true");

            //Check Id, if ID = 0 ==> new set, remove. else hide.
            var id = $(this).parents('.subcat-set').find('.Id').val();

            if (id === '0') {
                $(this).parents('.subcat-set').remove();
            }
            else {
                $(this).parents('.subcat-set').hide();
            }

            //Reorder index
            reorderIndex();

            //Buttons
            buttonVisibility();

            //Calculation
            calculateTotalAmount();
        });

        function reorderIndex() {
            $(".subcat-set").each(function () {

                //Current index
                var index = $(this).index();

                //Rename inputs
                $(":input", this).each(function () {
                    this.name = this.name.replace(/[0-9]+/, index);
                    this.id = this.id.replace(/[0-9]+/, index);
                });

                //Rename spans
                $(this).find('.field-validation-valid, .field-validation-error').each(function () {
                    var oldName = $(this).attr('data-valmsg-for');
                    var newName = $(this).attr('data-valmsg-for').replace(/[0-9]+/, index);
                    $(this).attr("data-valmsg-for", newName);

                });

            });
        }
        function addSeparatorsNF(nStr, inD, outD, sep) {
            nStr += '';
            var dpos = nStr.indexOf(inD);
            var nStrEnd = '';
            if (dpos !== -1) {
                nStrEnd = outD + nStr.substring(dpos + 1, nStr.length);
                nStr = nStr.substring(0, dpos);
            }
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(nStr)) {
                nStr = nStr.replace(rgx, '$1' + sep + '$2');
            }
            return nStr + nStrEnd;
        }
        function calculateTotalAmount() {
            var sum = 0;

            //iterate through each textboxes and add the values
            $(".line-amount").each(function () {

                var lineAmount = this.value.replace(/\./g, '');

                //add only if the value is number and visible
                if (!isNaN(lineAmount) && lineAmount.length !== 0) {
                    var parent = $(this).parents('.subcat-set');
                    if (parent.is(':visible')) {
                        sum += parseFloat(lineAmount);
                        $(this).css("background-color", "#E0E0E0");
                        $(this).css("color", "#003300");
                    }
                }
                else if (lineAmount.length !== 0) {
                    $(this).css("background-color", "red");
                }
            });

            var totalAmount = addSeparatorsNF(sum.toFixed(0), '.', ',', '.');
            $("#Amount").val(totalAmount);
            $("#Amount").css("background-color", "#E0E0E0");
            $("#Amount").css("color", "#003300");
        }
        function buttonVisibility() {

            var counter = $(".subcat-set:visible").length;

            if (counter < 2) {
                $('.subcat-set .delete-subcat').hide();
            }
            else {
                $('.subcat-set .delete-subcat').show();
            }
        }
    };
})(jQuery);
