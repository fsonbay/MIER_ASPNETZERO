(function () {
    $(function () {

        var _$materialsTable = $('#MaterialsTable');
        var _materialsService = abp.services.app.materials;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Materials.Create'),
            edit: abp.auth.hasPermission('Pages.Materials.Edit'),
            'delete': abp.auth.hasPermission('Pages.Materials.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'Portal/Materials/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/Materials/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditMaterialModal'
                });
                   

		 var _viewMaterialModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/Materials/ViewmaterialModal',
            modalClass: 'ViewMaterialModal'
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

        var dataTable = _$materialsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _materialsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#MaterialsTableFilter').val()
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
                                    _viewMaterialModal.open({ id: data.record.material.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.material.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteMaterial(data.record.material);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "material.name",
						 name: "name"   
					},
					{
						targets: 3,
						 data: "material.description",
						 name: "description"   
					}
            ]
        });

        function getMaterials() {
            dataTable.ajax.reload();
        }

        function deleteMaterial(material) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _materialsService.delete({
                            id: material.id
                        }).done(function () {
                            getMaterials(true);
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

        $('#CreateNewMaterialButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditMaterialModalSaved', function () {
            getMaterials();
        });

		$('#GetMaterialsButton').click(function (e) {
            e.preventDefault();
            getMaterials();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getMaterials();
		  }
		});
		
		
		
    });
})();
