(function () {
    $(function () {

        var _$customersTable = $('#CustomersTable');
        var _customersService = abp.services.app.customers;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Customers.Create'),
            edit: abp.auth.hasPermission('Pages.Customers.Edit'),
            'delete': abp.auth.hasPermission('Pages.Customers.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/Customers/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/Customers/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditCustomerModal'
        });


        var _viewCustomerModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/Customers/ViewcustomerModal',
            modalClass: 'ViewCustomerModal'
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

        var dataTable = _$customersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _customersService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#CustomersTableFilter').val(),
                        nameFilter: $('#NameFilterId').val(),
                        companyFilter: $('#CompanyFilterId').val(),
                        phoneFilter: $('#PhoneFilterId').val(),
                        emailFilter: $('#EmailFilterId').val(),
                        addressFilter: $('#AddressFilterId').val(),
                        customerCategoryNameFilter: $('#CustomerCategoryNameFilterId').val()
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
                                    _viewCustomerModal.open({ id: data.record.customer.id });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                iconStyle: 'far fa-edit mr-2',
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.customer.id });
                                }
                            },
                            {
                                text: app.localize('Delete'),
                                iconStyle: 'far fa-trash-alt mr-2',
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteCustomer(data.record.customer);
                                }
                            }]
                    }
                },
                {
                    targets: 2,
                    data: "customer.name",
                    name: "name"
                },
                {
                    targets: 3,
                    data: "customer.company",
                    name: "company"
                },
                {
                    targets: 4,
                    data: "customer.phone",
                    name: "phone"
                },
                {
                    targets: 5,
                    data: "customer.email",
                    name: "email"
                },
                {
                    targets: 6,
                    data: "customer.address",
                    name: "address"
                },
                {
                    targets: 7,
                    data: "customerCategoryName",
                    name: "customerCategoryFk.name"
                },
                {
                    targets: 8,
                    data:  "customer.creationTime",
                    name: "creationTime",
                    render: function (data) {

                        var y = new Date(data);
                        var z = y.toISOString();
                       // var x = data.date().format("YYYY-MM-DDT23:59:59Z");
                        return data;

                        //return y..format("YYYY-MM-DD");

                        //var res = new Date(dat1).getTime() > new Date(dat2).getTime()
                    }
                    
                }

            ]
        });

        function test() {
            return 'aaa';
        };

        function getCustomers() {
            dataTable.ajax.reload();
        }

        function deleteCustomer(customer) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _customersService.delete({
                            id: customer.id
                        }).done(function () {
                            getCustomers(true);
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

        $('#CreateNewCustomerButton').click(function () {
            _createOrEditModal.open();
        });



        abp.event.on('app.createOrEditCustomerModalSaved', function () {
            getCustomers();
        });

        $('#GetCustomersButton').click(function (e) {
            e.preventDefault();
            getCustomers();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getCustomers();
            }
        });



    });
})();
