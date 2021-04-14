
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

            var deadlineString = _$deadline.val();
            var deadlineParts = deadlineString.split("/");
            var deadlineObject = new Date(+deadlineParts[2], deadlineParts[1] - 1, +deadlineParts[0]);

            deadlineObject.setDate(deadlineObject.getDate() + 1);
            _$deadline.val(deadlineObject);
        });

        _$2Btn.click(function () {
            var deadline = _$deadline.val();
            var date = parseInt(deadline.substring(0, 2));
            var date_2 = date + 2;
            var updatedDate = deadline.replaceAt(0, date_2.toString());
            _$deadline.val(updatedDate);
        });

        _$3Btn.click(function () {
            var deadline = _$deadline.val();
            var date = parseInt(deadline.substring(0, 2));
            var date_3 = date + 3;
            var updatedDate = deadline.replaceAt(0, date_3.toString());
            _$deadline.val(updatedDate);
        });

        _$4Btn.click(function () {
            var deadline = _$deadline.val();
            var date = parseInt(deadline.substring(0, 2));
            var date_4 = date + 4;
            var updatedDate = deadline.replaceAt(0, date_4.toString());
            _$deadline.val(updatedDate);
        });

        _$5Btn.click(function () {
            var deadline = _$deadline.val();
            var date = parseInt(deadline.substring(0, 2));
            var date_5 = date + 5;
            var updatedDate = deadline.replaceAt(0, date_5.toString());
            _$deadline.val(updatedDate);
        });

        String.prototype.replaceAt = function (index, replacement) {
            return this.substr(0, index) + replacement + this.substr(index + replacement.length);
        }

        function SetDefaultDate() {
            var currentYear = (new Date).getFullYear();
            var currentMonth = (new Date).getMonth() + 1;
            var currentDate = (new Date).getDate();

            var today = currentDate + '/' + currentMonth + '/' + currentYear;

            $('#SalesOrder_Date').val(today);
            $('#SalesOrder_Deadline').val(today);

        }

    });
})();
