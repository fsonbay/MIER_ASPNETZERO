﻿(function () {
    $(function () {

        var _$machinesTable = $('#MachinesTable');
        var _machinesService = abp.services.app.machines;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Machines.Create'),
            edit: abp.auth.hasPermission('Pages.Machines.Edit'),
            'delete': abp.auth.hasPermission('Pages.Machines.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'Portal/Machines/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/Machines/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditMachineModal'
                });
                   

		 var _viewMachineModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/Machines/ViewmachineModal',
            modalClass: 'ViewMachineModal'
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

        var dataTable = _$machinesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _machinesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#MachinesTableFilter').val()
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
                                    _viewMachineModal.open({ id: data.record.machine.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.machine.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteMachine(data.record.machine);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "machine.name",
						 name: "name"   
					},
					{
						targets: 3,
						 data: "machine.description",
						 name: "description"   
					}
            ]
        });

        function getMachines() {
            dataTable.ajax.reload();
        }

        function deleteMachine(machine) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _machinesService.delete({
                            id: machine.id
                        }).done(function () {
                            getMachines(true);
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

        $('#CreateNewMachineButton').click(function () {
         //   alert(1);
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditMachineModalSaved', function () {
            getMachines();
        });

        $('#GetMachinesButton').click(function (e) {
            alert(1);
            e.preventDefault();
            getMachines();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getMachines();
		  }
		});
		
		
		
    });
})();
