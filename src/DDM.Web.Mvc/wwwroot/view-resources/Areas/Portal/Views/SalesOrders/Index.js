(function () {
    $(function () {

        var _$salesOrdersTable = $('#SalesOrdersTable');
        var _salesOrdersService = abp.services.app.salesOrders;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.SalesOrders.Create'),
            edit: abp.auth.hasPermission('Pages.SalesOrders.Edit'),
            'delete': abp.auth.hasPermission('Pages.SalesOrders.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/SalesOrders/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/SalesOrders/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditSalesOrderModal'
        });

        var _viewSalesOrderModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/SalesOrders/ViewsalesOrderModal',
            modalClass: 'ViewSalesOrderModal'
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

        var dataTable = _$salesOrdersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _salesOrdersService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#SalesOrdersTableFilter').val(),
                        numberFilter: $('#NumberFilterId').val(),
                        minDateFilter: getDateFilter($('#MinDateFilterId')),
                        maxDateFilter: getMaxDateFilter($('#MaxDateFilterId')),
                        minDeadlineFilter: getDateFilter($('#MinDeadlineFilterId')),
                        maxDeadlineFilter: getMaxDateFilter($('#MaxDeadlineFilterId')),
                        customerNameFilter: $('#CustomerNameFilterId').val()
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
                                    _viewSalesOrderModal.open({ id: data.record.salesOrder.id });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                iconStyle: 'far fa-edit mr-2',
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.salesOrder.id });
                                }
                            },
                            {
                                text: app.localize('Delete'),
                                iconStyle: 'far fa-trash-alt mr-2',
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteSalesOrder(data.record.salesOrder);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "salesOrder.number",
                    name: "number"
                },
                {
                    targets: 3,
                    data: "salesOrder.date",
                    name: "date",
                    render: function (date) {
                        if (date) {
                            return moment(date).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 4,
                    data: "salesOrder.deadline",
                    name: "deadline",
                    render: function (deadline) {
                        if (deadline) {
                            return moment(deadline).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 5,
                    data: "customerName",
                    name: "customerFk.name"
                },
                {
                    targets: 6,
                    data: "salesOrder.notes",
                    name: "notes",
                },
                {
                    targets: 7,
                    data: "productionStatus",
                    name: "productionStatusFK.name",
                },
                {
                    targets: 8,
                    data: null,
                    defaultContent: "<button id='OpenProductionStatus' class='btn btn-outline-primary'>Edit Status</button>"
                }

            ]
        });

        $('#SalesOrdersTable tbody').on('click', 'button', function () {
            alert(1);
            var data = dataTable.row($(this).parents('tr')).data();
            alert(data[1] + "'s salary is: " + data[5]);
        });

        //$('#OpenSalesOrderLookupTableButton').click(function () {

        //    var salesInvoice = _$salesInvoiceInformationForm.serializeFormToObject();

        //    _SalesInvoicesalesOrderLookupTableModal.open({ id: salesInvoice.salesOrderId, displayName: salesInvoice.salesOrderNumber }, function (data) {
        //        _$salesInvoiceInformationForm.find('input[name=salesOrderNumber]').val(data.displayName);
        //        _$salesInvoiceInformationForm.find('input[name=salesOrderId]').val(data.id);
        //    });
        //});

        function getSalesOrders() {
            dataTable.ajax.reload();
        }

        function deleteSalesOrder(salesOrder) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _salesOrdersService.delete({
                            id: salesOrder.id
                        }).done(function () {
                            getSalesOrders(true);
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

        $('#CreateNewSalesOrderButton').click(function () {
            _createOrEditModal.open();
        });

        abp.event.on('app.createOrEditSalesOrderModalSaved', function () {
            getSalesOrders();
        });

        $('#GetSalesOrdersButton').click(function (e) {
            e.preventDefault();
            getSalesOrders();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getSalesOrders();
            }
        });



    });
})();
