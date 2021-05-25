
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
    var _$newCustBtn = $('#CreateNewCustomerButton');

    var _$addLineBtn = $('#AddLineButton');
    var _$delLineBtn = $('.delete-line');

    var $sets = $('.line-sets');
    var $set = $('.line-set');

    //START
    ButtonVisibility();


    //MODAL POPUP
    var _createCustomerModal = new app.ModalManager({
        viewUrl: abp.appPath + 'Portal/Customers/CreateOrEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/SalesOrders/_CreateCustomerModal.js',
        modalClass: 'CreateCustomerModal'
    });
    _$newCustBtn.click(function (e) {
        e.preventDefault();
        _createCustomerModal.open();
    });

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

    //SALES ORDER LINES
    var wrapper = $sets;
    _$addLineBtn.click(function (e) {
        alert(1);
        //Cancel default postback
        e.preventDefault();

        //Create new item and show (to override case style display none)
        var newItem = $(".line-set:last").clone(true);
        // newItem.show();

        //Input Values
        newItem.find(':input').each(function () {

            //Texbox
            $(this).val('');

            //mark-for-delete
            if ($(this).hasClass("mark-for-delete")) {
                $(this).val('False');
            }

            //ID
            if ($(this).hasClass("line-id")) {
                $(this).val('0');
            }
        });

        //Add clone
        wrapper.append(newItem);

        //Focus
        $('.line-set:last-child :input:enabled:visible:first').focus();

        //Reorder index
        ReorderIndex();

        //Button
        ButtonVisibility();

    });
    _$delLineBtn.click(function (e) {

        //Cancel default postback
        e.preventDefault();

        //Set hidden value
        $(this).parents('.line-set').find('.mark-for-delete').val("true");

        //Check Id, if ID = 0 ==> new set, remove. else hide.
        var id = $(this).parents('.line-set').find('.line-id').val();

        if (id === '0') {
            $(this).parents('.line-set').remove();
        }
        else {
            $(this).parents('.line-set').hide();
        }

        ReorderIndex();
        ButtonVisibility();
    });

    function ReorderIndex() {
        $(".line-set").each(function () {

            //Current index
            var index = $(this).index();

            //Rename inputs
            $(":input", this).each(function () {
                this.name = this.name.replace(/[0-9]+/, index);
                this.id = this.id.replace(/[0-9]+/, index);
            });

            ////Rename spans
            //$(this).find('.field-validation-valid, .field-validation-error').each(function () {
            //    var oldName = $(this).attr('data-valmsg-for');
            //    var newName = $(this).attr('data-valmsg-for').replace(/[0-9]+/, index);
            //    $(this).attr("data-valmsg-for", newName);

            //});

        });
    }
    function ButtonVisibility() {

        var counter = $(".line-set").length;

        if (counter === 1) {
            $('.line-set .delete-line').hide();
        }
        else {
            $('.line-set .delete-line').show();
        }
    }


})();

