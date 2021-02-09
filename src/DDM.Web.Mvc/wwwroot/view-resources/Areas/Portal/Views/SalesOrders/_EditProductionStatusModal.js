
/*SalesOrder/_EditProductionStatusModal.js*/

(function ($) {

    app.modals.EditProductionStatusModal = function () {

        var _salesOrdersService = abp.services.app.salesOrders;

        var _modalManager;
        var _$productionStatusInformationForm = null;

        this.init = function (modalManager) {

            _modalManager = modalManager;
            var modal = _modalManager.getModal();

            _$productionStatusInformationForm = _modalManager.getModal().find('form[name=ProductionStatusInformationsForm]');
            _$productionStatusInformationForm.validate();
        };
        this.save = function () {
            if (!_$productionStatusInformationForm.valid()) {

                return;
            }

            var productionStatus = _$productionStatusInformationForm.toObject({ mode: 'first' });
            _modalManager.setBusy(true);

            _salesOrdersService.updateProductionStatus(productionStatus)
                .done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditSalesOrderModalSaved');
                })
                .always(function () {
                    _modalManager.setBusy(false);
                });


        };

    };
})(jQuery);