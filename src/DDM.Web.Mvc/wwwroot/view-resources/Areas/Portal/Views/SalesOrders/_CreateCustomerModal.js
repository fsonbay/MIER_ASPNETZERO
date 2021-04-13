
/*SalesOrders/_CreateCustomerModal.js*/

(function ($) {

    app.modals.CreateCustomerModal = function () {

        var _modalManager;
        var _customersService = abp.services.app.customers;
        var _$customerInformationForm = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _modalManager.getModal()
                .find('#CustomerCategoryEdit')
                .selectpicker({
                    iconBase: "fa",
                    tickIcon: "fa fa-check"
                });

            _$customerInformationForm = _modalManager.getModal().find('form[name=CustomerInformationsForm]');
            _$customerInformationForm.validate();
        };
        this.save = function () {
            if (!_$customerInformationForm.valid()) {
                return;
            }
            if ($('#CustomerCategoryEdit').prop('required') && $('#CustomerCategoryEdit').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('CustomerCategory')));
                return;
            }

            var customer = _$customerInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _customersService.createNewCustomer(
                customer
            ).done(function (result) {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createCustomerModalSaved');

                alert(customer.name + '-' + customer.company);

                $('#CustomerId').append($('<option>', {
                    value: 1,
                    text: 'My option',
                    selected: true
                   
                }));

                //Refresh page
               // alert(result);
               //window.top.location.reload();

               // $('#CustomerId').val(5)
                //$("#CustomerId").val(result).change();

            //  alert($('#CustomerId').val());
              

            }).always(function () {
                _modalManager.setBusy(false);

            });
        };



    };

})(jQuery);