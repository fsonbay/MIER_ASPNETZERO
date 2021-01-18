(function ($) {
    app.modals.CreateOrEditSalesInvoiceModal = function () {

        var _salesInvoicesService = abp.services.app.salesInvoices;

        var _modalManager;
        var _$salesInvoiceInformationForm = null;

		        var _SalesInvoicesalesOrderLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/SalesInvoices/SalesOrderLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/SalesInvoices/_SalesInvoiceSalesOrderLookupTableModal.js',
            modalClass: 'SalesOrderLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$salesInvoiceInformationForm = _modalManager.getModal().find('form[name=SalesInvoiceInformationsForm]');
            _$salesInvoiceInformationForm.validate();
        };

		          $('#OpenSalesOrderLookupTableButton').click(function () {

            var salesInvoice = _$salesInvoiceInformationForm.serializeFormToObject();

            _SalesInvoicesalesOrderLookupTableModal.open({ id: salesInvoice.salesOrderId, displayName: salesInvoice.salesOrderNumber }, function (data) {
                _$salesInvoiceInformationForm.find('input[name=salesOrderNumber]').val(data.displayName); 
                _$salesInvoiceInformationForm.find('input[name=salesOrderId]').val(data.id); 
            });
        });
		
		$('#ClearSalesOrderNumberButton').click(function () {
                _$salesInvoiceInformationForm.find('input[name=salesOrderNumber]').val(''); 
                _$salesInvoiceInformationForm.find('input[name=salesOrderId]').val(''); 
        });
		


        this.save = function () {
            if (!_$salesInvoiceInformationForm.valid()) {
                return;
            }
            if ($('#SalesInvoice_SalesOrderId').prop('required') && $('#SalesInvoice_SalesOrderId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('SalesOrder')));
                return;
            }

            var salesInvoice = _$salesInvoiceInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _salesInvoicesService.createOrEdit(
				salesInvoice
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditSalesInvoiceModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);