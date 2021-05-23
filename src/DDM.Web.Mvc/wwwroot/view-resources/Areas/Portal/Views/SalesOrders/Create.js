
/*SalesOrders/Create.js*/


(function () {

    var _$form = $('form[name = SalesOrderForm]');
    var $datePicker = $('.date-picker');
    var _$1Btn = $('#1Button');
    var _$2Btn = $('#2Button');
    var _$3Btn = $('#3Button');
    var _$4Btn = $('#4Button');
    var _$5Btn = $('#5Button');
    var _$deadline = $('#SalesOrder_Deadline');

    //FORMAT DATE TO DD/MM/YYY
    $datePicker.datetimepicker({
        locale: abp.localization.currentLanguage.name,
        format: 'DD/MM/YYYY'
    });
    function FormatDateToString(dt) {
        var year = dt.getFullYear();
        var month = (1 + dt.getMonth()).toString();
        month = month.length > 1 ? month : '0' + month;
        var day = dt.getDate().toString();
        day = day.length > 1 ? day : '0' + day;
        return day + '/' + month + '/' + year;
    }
    function FormatStringToDate(str) {
        var parts = str.split("/");
        var dt = new Date(+parts[2], parts[1] - 1, +parts[0]);
        return dt;
    }


    _$1Btn.click(function (e) {
        e.preventDefault();
        var deadlineDt = FormatStringToDate(_$deadline.val());
        deadlineDt.setDate(deadlineDt.getDate() + 1);
        _$deadline.val(FormatDateToString(deadlineDt));
    });
    _$2Btn.click(function (e) {
        e.preventDefault();
        var deadlineDt = FormatStringToDate(_$deadline.val());
        deadlineDt.setDate(deadlineDt.getDate() + 2);
        _$deadline.val(FormatDateToString(deadlineDt));
    });
    _$3Btn.click(function (e) {
        e.preventDefault();
        var deadlineDt = FormatStringToDate(_$deadline.val());
        deadlineDt.setDate(deadlineDt.getDate() + 3);
        _$deadline.val(FormatDateToString(deadlineDt));
    });
    _$4Btn.click(function (e) {
        e.preventDefault();
        var deadlineDt = FormatStringToDate(_$deadline.val());
        deadlineDt.setDate(deadlineDt.getDate() + 4);
        _$deadline.val(FormatDateToString(deadlineDt));
    });
    _$5Btn.click(function (e) {
        e.preventDefault();
        var deadlineDt = FormatStringToDate(_$deadline.val());
        deadlineDt.setDate(deadlineDt.getDate() + 5);
        _$deadline.val(FormatDateToString(deadlineDt));
    });




})();

