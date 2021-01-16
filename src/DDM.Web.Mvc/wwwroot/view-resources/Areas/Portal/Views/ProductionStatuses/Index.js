(function () {
    $(function () {

        var _$productionStatusesTable = $('#ProductionStatusesTable');
        var _productionStatusesService = abp.services.app.productionStatuses;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.ProductionStatuses.Create'),
            edit: abp.auth.hasPermission('Pages.ProductionStatuses.Edit'),
            'delete': abp.auth.hasPermission('Pages.ProductionStatuses.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'Portal/ProductionStatuses/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/ProductionStatuses/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditProductionStatusModal'
                });
                   

		 var _viewProductionStatusModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/ProductionStatuses/ViewproductionStatusModal',
            modalClass: 'ViewProductionStatusModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }
        
        var getMaxDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT23:59:59Z"); 
        }

        var dataTable = _$productionStatusesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _productionStatusesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ProductionStatusesTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					descriptionFilter: $('#DescriptionFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
						{
                                text: app.localize('View'),
                                iconStyle: 'far fa-eye mr-2',
                                action: function (data) {
                                    _viewProductionStatusModal.open({ id: data.record.productionStatus.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.productionStatus.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteProductionStatus(data.record.productionStatus);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "productionStatus.name",
						 name: "name"   
					},
					{
						targets: 3,
						 data: "productionStatus.description",
						 name: "description"   
					}
            ]
        });

        function getProductionStatuses() {
            dataTable.ajax.reload();
        }

        function deleteProductionStatus(productionStatus) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _productionStatusesService.delete({
                            id: productionStatus.id
                        }).done(function () {
                            getProductionStatuses(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

		$('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewProductionStatusButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditProductionStatusModalSaved', function () {
            getProductionStatuses();
        });

		$('#GetProductionStatusesButton').click(function (e) {
            e.preventDefault();
            getProductionStatuses();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getProductionStatuses();
		  }
		});
		
		
		
    });
})();
