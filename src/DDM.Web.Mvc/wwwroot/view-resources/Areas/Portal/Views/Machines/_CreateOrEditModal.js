(function ($) {
    app.modals.CreateOrEditMachineModal = function () {

        var _machinesService = abp.services.app.machines;

        var _modalManager;
        var _$machineInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$machineInformationForm = _modalManager.getModal().find('form[name=MachineInformationsForm]');
            _$machineInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$machineInformationForm.valid()) {
                return;
            }

            var machine = _$machineInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _machinesService.createOrEdit(
				machine
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditMachineModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);