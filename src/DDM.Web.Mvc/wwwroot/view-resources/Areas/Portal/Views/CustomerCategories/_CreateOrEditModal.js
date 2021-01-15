(function ($) {
    app.modals.CreateOrEditCustomerCategoryModal = function () {

        var _customerCategoriesService = abp.services.app.customerCategories;

        var _modalManager;
        var _$customerCategoryInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$customerCategoryInformationForm = _modalManager.getModal().find('form[name=CustomerCategoryInformationsForm]');
            _$customerCategoryInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$customerCategoryInformationForm.valid()) {
                return;
            }

            var customerCategory = _$customerCategoryInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _customerCategoriesService.createOrEdit(
				customerCategory
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditCustomerCategoryModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);