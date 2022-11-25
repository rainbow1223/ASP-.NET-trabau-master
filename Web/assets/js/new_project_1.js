function LoadNewProjectEvents() {
    $(document).ready(function () {
        ActivateHelpText();
        CheckOtherCommFunction();
        ExpandProjectType_RadioButton();
        ExpandProjectTypeStep2_RadioButton();
        $('form').attr('enctype', 'multipart/form-data');
        $('input[id*="afuProjectFiles"][type="file"]').parent('div').append('<label id="fileLabel">Choose or drag and drop file</label>');
        //$('select[id*="ddlCompany"]').select2();
        //$('select[id*="ddlCommunicationFunction"]').select2();


        $('select[id*="ddlCommunicationFunction"]').change(function () {
            CheckOtherCommFunction();
        });

        $('input[id*="rbtnlProjectType"][type="radio"]').click(function () {
            ExpandProjectType_RadioButton();
        });


        $('input[id*="rbtnlOtherProject_Step2"][type="radio"]').click(function () {
            ExpandProjectTypeStep2_RadioButton();
        });


        $('input[id*="txtStartDate"]').datepicker({
            changeMonth: true,
            changeYear: true,
            minDate: 0
        });

        $('input[id*="txtEndDate"]').datepicker({
            changeMonth: true,
            changeYear: true,
            minDate: 0
        });

        var times;
        var currentdate = new Date();
        var StartTime = $('input[id*="hfStartTime"]').val();
        if (StartTime != '' && StartTime != undefined) {
            times = new Date(currentdate.getFullYear(), currentdate.getMonth(), currentdate.getDay(), StartTime.split(':')[0], StartTime.split(':')[1]);
        }
        else {
            times = new Date();
        }

        //var minute = (times.getMinutes() > 0 && times.getMinutes() <= 30 ? 30 : 0);

        //$('input[id*="txtStartTime"]').timepicker({
        //    step: 1,
        //    setTime: new Date()
        //});

        $('input[id*="txtStartTime"]').timepicker({
            timeFormat: 'h:mm p',
            interval: 1,
            defaultTime: times.getHours() + ':' + times.getMinutes()
        });

        //$('input[id*="txtStartTime"]').timepicker(
        //    {
        //        minTime: times.getHours() + ':' + (minute.toString().length == 1 ? '0' + minute.toString() : minute.toString()),
        //        maxTime: '22:00',
        //        interval: 30
        //    });

        $('input[id*="txtEndTime"]').timepicker({
            timeFormat: 'h:mm p',
            interval: 30,
            minTime: times.getHours() + ':' + times.getMinutes(),
            dynamic: false,
            dropdown: true,
            scrollbar: true
        });
        // $('input[id*="txtEndTime"]').timepicker();


        $('input[id*="txtStartDate"]').keypress(function (event) {
            event.preventDefault();
        });

        $('input[id*="txtEndDate"]').keypress(function (event) {
            event.preventDefault();
        });

        $('input[id*="txtStartTime"]').keypress(function (event) {
            event.preventDefault();
        });

        $('input[id*="txtEndTime"]').keypress(function (event) {
            event.preventDefault();
        });


        $('input[id*="btnNext"]').click(function () {
            var error_message = '';
            var inline_errors = $('input[id*="chkWarningInlineConfig"]').prop('checked');
            if (!inline_errors) {
                var rfvProjectName = $('span[id*="rfvProjectName"]').attr('style').indexOf('hidden') == -1 ? '0' : '1';
                if (rfvProjectName == '0') {
                    error_message = "Anything that we do to solve an identified problem is being viewed as a project. " +
                        "Anything that we undertake to solve an identified problem is being viewed as a project " +
                        "For instance, if we produce an entity where the usage of that entity is to solve " +
                        "a problem, then the production of that entity is being viewed as a project.  In this " +
                        "a project is being viewed as the step we take to produce that entity.  Since the " +
                        "existence of an entity enables that entity to have a name, the existence of our " +
                        "project enables our project to have a name as well.  Here I will need to provide " +
                        "a name for the project.";
                    toastr['error'](error_message);
                    return false;
                }

                var rfvCompanyName = $('span[id*="rfvCompanyName"]').attr('style').indexOf('hidden') == -1 ? '0' : '1';
                if (rfvCompanyName == '0') {
                    error_message = "The company name is being viewed as the name of the organization " +
                        "or the name of the group of people that execute the function of " +
                        "the project.  For instance, let's assume that a group of people " +
                        "get together to produce an entity and they give the group a name " +
                        "in relationship to the production of that entity, then that name " +
                        "is being viewed as the company name.  As well as, an organization " +
                        "that is formed by a group of people with a name, that names is being " +
                        "viewed as the company name.  Here I must provide a name for the company.";
                    toastr['error'](error_message);
                    return false;
                }

                var rfvApplicationName = $('span[id*="rfvApplicationName"]').attr('style').indexOf('hidden') == -1 ? '0' : '1';
                if (rfvApplicationName == '0') {
                    error_message = "Within a project itself, the name of the application " +
                        "provides us with the idea of what we need to do or the " +
                        "idea of the project.  The application name is simply the " +
                        "name of the application.  Here I must enter that name.";
                    toastr['error'](error_message);
                    return false;
                }

                var rfvCommunicationFunction = $('span[id*="rfvCommunicationFunction"]').attr('style').indexOf('hidden') == -1 ? '0' : '1';
                if (rfvCommunicationFunction == '0') {
                    error_message = "If a project exists, that project must have a function. " +
                        "The function of a project which is being viewed as a " +
                        "communication function, simply the function that needs " +
                        "to be executed for that project.  For instance, if our " +
                        "project is to produce an entity, then the function of " +
                        "producing that entity is the communication function of that " +
                        "of that project or simply the function of the project.  Here " +
                        "I must enter the name of the communication function.";
                    toastr['error'](error_message);
                    return false;
                }

                var rfvDescription = $('span[id*="rfvDescription"]').attr('style').indexOf('hidden') == -1 ? '0' : '1';
                if (rfvDescription == '0') {
                    error_message = "If a project exists, that project must have a description. " +
                        "Having a description for our project helps us to be more " +
                        "organize in term of what we need to do the execute the " +
                        "function of our application.  Here I need to provide " +
                        "a description for the project.";
                    toastr['error'](error_message);
                    return false;
                }
            }
        });


        $(".file-upload").on('dragover', function (e) {
            e.stopPropagation();
            e.preventDefault();
            $('.file-upload').find('label').text('Drag here');
        }).on('dragleave dragend', function (e) {
            e.stopPropagation();
            e.preventDefault();
            $('.file-upload').find('label').text('Choose or drag and drop file');
        }).on('drop', function (e) {
            // e.preventDefault();     // Stops some browsers from redirecting.

        });
    });

}