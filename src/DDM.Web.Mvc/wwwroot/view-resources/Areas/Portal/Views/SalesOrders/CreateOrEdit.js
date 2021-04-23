
/*SalesOrders/CreateOrEdit.js*/

(function () {
    $(function () {

        var _salesOrdersService = abp.services.app.salesOrders;
        var _$newCustBtn = $('#CreateNewCustomerButton');

        var _$1Btn = $('#1Button');
        var _$2Btn = $('#2Button');
        var _$3Btn = $('#3Button');
        var _$4Btn = $('#4Button');
        var _$5Btn = $('#5Button');
        var _$deadline = $('#SalesOrder_Deadline');

        var _$addLineBtn = $('#AddLineButton');

        var _createCustomerModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Portal/Customers/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/Portal/Views/SalesOrders/_CreateCustomerModal.js',
            modalClass: 'CreateCustomerModal'
        });

        SetDefaultDate();

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'DD/MM/YYYY'
            
        });
      
        var KTWizard1 = function () {
            // Base elements
            var wizardEl;
            var formEl;
            var validator;
            var wizard;

            // Private functions
            var initWizard = function () {
                // Initialize form wizard
                wizard = new KTWizard('kt_wizard', {
                    startStep: 1
                });

                // Validation before going to next page
                wizard.on('beforeNext', function (wizardObj) {
                    if (validator.form() !== true) {
                      
                        wizardObj.stop();  // don't go to the next step
                    }
                });

                wizard.on('beforePrev', function (wizardObj) {
                    if (validator.form() !== true) {
                        wizardObj.stop();  // don't go to the next step
                    }
                });

                // Change event
                wizard.on('change', function (wizard) {
                    setTimeout(function () {
                        KTUtil.scrollTop();
                    }, 500);
                });
            }

            var initValidation = function () {
                validator = formEl.validate({
                    // Validate only visible fields
                    ignore: ":hidden",

                    // Validation rules
                    rules: {

                        //= Step 1
                        CustomerId: {
                            required: true
                        },
                        Date: {
                            required: true
                        },
                        Deadline: {
                            required: true
                        },

                        //= Step 2
                        package: {
                            required: true
                        },
                        weight: {
                            required: true
                        },
                        width: {
                            required: true
                        },
                        height: {
                            required: true
                        },
                        length: {
                            required: true
                        },

                        //= Step 3
                        delivery: {
                            required: true
                        },
                        packaging: {
                            required: true
                        },
                        preferreddelivery: {
                            required: true
                        },

                        //= Step 4
                        locaddress1: {
                            required: true
                        },
                        locpostcode: {
                            required: true
                        },
                        loccity: {
                            required: true
                        },
                        locstate: {
                            required: true
                        },
                        loccountry: {
                            required: true
                        },
                    },

                    // Display error  
                    invalidHandler: function (event, validator) {
                        KTUtil.scrollTop();

                        //swal.fire({
                        //	"title": "",
                        //	"text": "There are some errors in your submission. Please correct them.",
                        //	"type": "error",
                        //	"confirmButtonClass": "btn btn-secondary"
                        //});
                    },

                    // Submit valid form
                    submitHandler: function (form) {

                    }
                });
            }
            var initSubmit = function () {
                var btn = formEl.find('[data-ktwizard-type="action-submit"]');

                btn.on('click', function (e) {
                    e.preventDefault();

                    if (validator.form()) {
                        // See: src\js\framework\base\app.js
                        KTApp.progress(btn);
                        //KTApp.block(formEl);

                        // See: http://malsup.com/jquery/form/#ajaxSubmit
                        formEl.ajaxSubmit({
                            success: function () {
                                KTApp.unprogress(btn);
                                //KTApp.unblock(formEl);

                                //swal.fire({
                                //	"title": "",
                                //	"text": "The application has been successfully submitted!",
                                //	"type": "success",
                                //	"confirmButtonClass": "btn btn-secondary"
                                //});
                            }
                        });
                    }
                });
            }
            return {
                // public functions
                init: function () {
                    wizardEl = KTUtil.getById('kt_wizard');
                    formEl = $('#SalesOrderInformationsForm');

                    initWizard();
                    initValidation();
                    initSubmit();
                }
            };
        }();
        jQuery(document).ready(function () {
            KTWizard1.init();
        });

        _$newCustBtn.click(function () {
            _createCustomerModal.open();
        });

        _$1Btn.click(function () {
            var deadlineDt = FormatStringToDate(_$deadline.val());
            deadlineDt.setDate(deadlineDt.getDate() + 1);
            _$deadline.val(FormatDateToString(deadlineDt));
        });

        _$2Btn.click(function () {
            var deadlineDt = FormatStringToDate(_$deadline.val());
            deadlineDt.setDate(deadlineDt.getDate() + 2);
            _$deadline.val(FormatDateToString(deadlineDt));
        });

        _$3Btn.click(function () {
            var deadlineDt = FormatStringToDate(_$deadline.val());
            deadlineDt.setDate(deadlineDt.getDate() + 3);
            _$deadline.val(FormatDateToString(deadlineDt));
        });

        _$4Btn.click(function () {
            var deadlineDt = FormatStringToDate(_$deadline.val());
            deadlineDt.setDate(deadlineDt.getDate() + 4);
            _$deadline.val(FormatDateToString(deadlineDt));
        });

        _$5Btn.click(function () {
            var deadlineDt = FormatStringToDate(_$deadline.val());
            deadlineDt.setDate(deadlineDt.getDate() + 5);
            _$deadline.val(FormatDateToString(deadlineDt));
        });

        //Wrappper
        var wrapper = $('.subcat-sets');

        _$addLineBtn.click(function (e) {

            //Cancel default postback
            e.preventDefault();

            //Create new item and show (to override case style display none)
            var newItem = $(".subcat-set:last-child").clone(true);
            newItem.show();

            //Input Values
            newItem.find(':input').each(function () {

                //Texbox
                $(this).val('');

                //mark-for-delete
                if ($(this).hasClass("mark-for-delete")) {
                    $(this).val('False');
                }

                //ID
                if ($(this).hasClass("ID")) {
                    $(this).val('0');
                }
            });

            //Add clone
            wrapper.append(newItem);

            //Focus
            $('.subcat-set:last-child :input:enabled:visible:first').focus();

            ////Reorder index
            //reorderIndex();

            ////Button
            //ButtonVisibility();

            ////Reset validator
            //resetValidator();

        });

        function SetDefaultDate() {
            var today = FormatDateToString(new Date())
            $('#SalesOrder_Date').val(today);
            $('#SalesOrder_Deadline').val(today);
        }

        //Format date to string DD/MM/YYYY
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



    });
})();
