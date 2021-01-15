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

            alert(abp.localization.currentLanguage.name);

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
            });
          
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

        $('#AddOrderLineButton').click(function (e) {
            e.preventDefault();
           
            //Create new item and show (to override case style display none)
            var newItem = $(".subcat-set:last-child").clone(true);
            newItem.show();

            //Add clone
            wrapper.append(newItem);

            reorderIndex();

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
                    abp.event.trigger('app.createOrEditSalesOrderModalSaved');})
                .always(function ()
                {
                 _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);
