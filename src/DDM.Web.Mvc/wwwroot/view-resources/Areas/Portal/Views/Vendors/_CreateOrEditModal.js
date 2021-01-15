(function ($) {
    app.modals.CreateOrEditVendorModal = function () {

        var _vendorsService = abp.services.app.vendors;

        var _modalManager;
        var _$vendorInformationForm = null;

		        var _VendorvendorCategoryLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/Vendors/VendorCategoryLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/Vendors/_VendorVendorCategoryLookupTableModal.js',
            modalClass: 'VendorCategoryLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$vendorInformationForm = _modalManager.getModal().find('form[name=VendorInformationsForm]');
            _$vendorInformationForm.validate();
        };

		          $('#OpenVendorCategoryLookupTableButton').click(function () {

            var vendor = _$vendorInformationForm.serializeFormToObject();

            _VendorvendorCategoryLookupTableModal.open({ id: vendor.vendorCategoryId, displayName: vendor.vendorCategoryName }, function (data) {
                _$vendorInformationForm.find('input[name=vendorCategoryName]').val(data.displayName); 
                _$vendorInformationForm.find('input[name=vendorCategoryId]').val(data.id); 
            });
        });
		
		$('#ClearVendorCategoryNameButton').click(function () {
                _$vendorInformationForm.find('input[name=vendorCategoryName]').val(''); 
                _$vendorInformationForm.find('input[name=vendorCategoryId]').val(''); 
        });
		


        this.save = function () {
            if (!_$vendorInformationForm.valid()) {
                return;
            }
            if ($('#Vendor_VendorCategoryId').prop('required') && $('#Vendor_VendorCategoryId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('VendorCategory')));
                return;
            }

            var vendor = _$vendorInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _vendorsService.createOrEdit(
				vendor
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditVendorModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);