(function ($) {
    app.modals.CreateOrEditCustomerModal = function () {

        var _customersService = abp.services.app.customers;

        var _modalManager;
        var _$customerInformationForm = null;

		        var _CustomercustomerCategoryLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/Customers/CustomerCategoryLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/Customers/_CustomerCustomerCategoryLookupTableModal.js',
            modalClass: 'CustomerCategoryLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$customerInformationForm = _modalManager.getModal().find('form[name=CustomerInformationsForm]');
            _$customerInformationForm.validate();
        };

		          $('#OpenCustomerCategoryLookupTableButton').click(function () {

            var customer = _$customerInformationForm.serializeFormToObject();

            _CustomercustomerCategoryLookupTableModal.open({ id: customer.customerCategoryId, displayName: customer.customerCategoryName }, function (data) {
                _$customerInformationForm.find('input[name=customerCategoryName]').val(data.displayName); 
                _$customerInformationForm.find('input[name=customerCategoryId]').val(data.id); 
            });
        });
		
		$('#ClearCustomerCategoryNameButton').click(function () {
                _$customerInformationForm.find('input[name=customerCategoryName]').val(''); 
                _$customerInformationForm.find('input[name=customerCategoryId]').val(''); 
        });
		


        this.save = function () {
            if (!_$customerInformationForm.valid()) {
                return;
            }
            if ($('#Customer_CustomerCategoryId').prop('required') && $('#Customer_CustomerCategoryId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('CustomerCategory')));
                return;
            }

            var customer = _$customerInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _customersService.createOrEdit(
				customer
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditCustomerModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);