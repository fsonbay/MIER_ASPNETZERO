(function ($) {
    app.modals.SalesOrderLookupTableModal = function () {

        var _modalManager;

        var _salesInvoicesService = abp.services.app.salesInvoices;
        var _$salesOrderTable = $('#SalesOrderTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$salesOrderTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _salesInvoicesService.getAllSalesOrderForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#SalesOrderTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" + app.localize('Select') + "' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#SalesOrderTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getSalesOrder() {
            dataTable.ajax.reload();
        }

        $('#GetSalesOrderButton').click(function (e) {
            e.preventDefault();
            getSalesOrder();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getSalesOrder();
            }
        });

    };
})(jQuery);

