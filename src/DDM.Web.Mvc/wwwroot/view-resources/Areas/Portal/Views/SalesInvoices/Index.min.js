(function () {
    $(function () {

        var _$salesInvoicesTable = $('#SalesInvoicesTable');
        var _salesInvoicesService = abp.services.app.salesInvoices;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.SalesInvoices.Create'),
            edit: abp.auth.hasPermission('Pages.SalesInvoices.Edit'),
            'delete': abp.auth.hasPermission('Pages.SalesInvoices.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'Portal/SalesInvoices/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/SalesInvoices/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditSalesInvoiceModal'
                });
                   

		 var _viewSalesInvoiceModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/SalesInvoices/ViewsalesInvoiceModal',
            modalClass: 'ViewSalesInvoiceModal'
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

        var dataTable = _$salesInvoicesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _salesInvoicesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#SalesInvoicesTableFilter').val(),
					numberFilter: $('#NumberFilterId').val(),
					minDateFilter:  getDateFilter($('#MinDateFilterId')),
					maxDateFilter:  getMaxDateFilter($('#MaxDateFilterId')),
					dueDateFilter: $('#DueDateFilterId').val(),
					minAmountFilter: $('#MinAmountFilterId').val(),
					maxAmountFilter: $('#MaxAmountFilterId').val(),
					minPaidFilter: $('#MinPaidFilterId').val(),
					maxPaidFilter: $('#MaxPaidFilterId').val(),
					minOutstandingFilter: $('#MinOutstandingFilterId').val(),
					maxOutstandingFilter: $('#MaxOutstandingFilterId').val(),
					salesOrderNumberFilter: $('#SalesOrderNumberFilterId').val()
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
                                    _viewSalesInvoiceModal.open({ id: data.record.salesInvoice.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.salesInvoice.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteSalesInvoice(data.record.salesInvoice);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "salesInvoice.number",
						 name: "number"   
					},
					{
						targets: 3,
						 data: "salesInvoice.date",
						 name: "date" ,
					render: function (date) {
						if (date) {
							return moment(date).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 4,
						 data: "salesInvoice.dueDate",
						 name: "dueDate"   
					},
					{
						targets: 5,
						 data: "salesInvoice.amount",
						 name: "amount"   
					},
					{
						targets: 6,
						 data: "salesInvoice.paid",
						 name: "paid"   
					},
					{
						targets: 7,
						 data: "salesInvoice.outstanding",
						 name: "outstanding"   
					},
					{
						targets: 8,
						 data: "salesOrderNumber" ,
						 name: "salesOrderFk.number" 
					}
            ]
        });

        function getSalesInvoices() {
            dataTable.ajax.reload();
        }

        function deleteSalesInvoice(salesInvoice) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _salesInvoicesService.delete({
                            id: salesInvoice.id
                        }).done(function () {
                            getSalesInvoices(true);
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

        $('#CreateNewSalesInvoiceButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditSalesInvoiceModalSaved', function () {
            getSalesInvoices();
        });

		$('#GetSalesInvoicesButton').click(function (e) {
            e.preventDefault();
            getSalesInvoices();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getSalesInvoices();
		  }
		});
		
		
		
    });
})();
