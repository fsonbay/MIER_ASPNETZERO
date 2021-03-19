
/*NewSalesOrderStats.js*/

$(function () {

    var _tenantDashboardService = abp.services.app.tenantDashboard;

    var _$Container = $('.NewSalesOrderStatsContainer');

    var initNewSalesOrderStats = function () {

        var refreshinitNewSalesOrderStats = function () {
            _tenantDashboardService
                .getNewSalesOrderStats({})
                .done(function (result) {
                    for (var i = 0; i < _$Container.length; i++) {
                        var container = $(_$Container[i]);
                        var $tableBody = container.find('#newSalesOrder_statistics_content table tbody');

                        for (var rowIndex = 0; rowIndex < result.stats.length; rowIndex++) {
                            var stat = result.stats[rowIndex];
                            var $tr = $('<tr></tr>').append(
                                $(
                                    '<td class="m-datatable__cell--center m-datatable__cell m-datatable__cell--check">' +
                                    '<label class="checkbox checkbox-outline checkbox-outline-2x checkbox-success">' +
                                    '<input type = "checkbox" > <span></span>' +
                                    '</label>' +
                                    '</td>'
                                ),
                                $('<td>' + stat.customerName + '</td>'),
                                $('<td>$' + stat.customerName + '</td>'),
                                $(
                                    '<td>' +
                                    '<div class="m-widget11__chart" style="height:50px; width: 100px">' +
                                    '<iframe class="chartjs-hidden-iframe" tabindex="-1" style="display: block; overflow: hidden; border: 0px; margin: 0px; top: 0px; left: 0px; bottom: 0px; right: 0px; height: 100%; width: 100%; position: absolute; pointer-events: none; z-index: -1;"></iframe>' +
                                    '<canvas class="m_chart_sales_by_region" style="display: block; width: 100px; height: 50px;" width="100" height="50"></canvas>' +
                                    '</div>' +
                                    '</td>'
                                ),
                                $('<td>$' + stat.customerName + '</td>'),
                                $('<td>$' + stat.customerName + '</td>')
                            );

                            $tableBody.append($tr);
                        }
                       
                    }
                



                });
        };
        refreshinitNewSalesOrderStats();

    };

initNewSalesOrderStats();

});


