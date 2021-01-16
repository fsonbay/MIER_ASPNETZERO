(function ($) {
    app.modals.CreateOrEditProductionStatusModal = function () {

        var _productionStatusesService = abp.services.app.productionStatuses;

        var _modalManager;
        var _$productionStatusInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$productionStatusInformationForm = _modalManager.getModal().find('form[name=ProductionStatusInformationsForm]');
            _$productionStatusInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$productionStatusInformationForm.valid()) {
                return;
            }

            var productionStatus = _$productionStatusInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _productionStatusesService.createOrEdit(
				productionStatus
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditProductionStatusModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);