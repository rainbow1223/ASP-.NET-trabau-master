function LoadPostJobEvents() {
    $(document).ready(function () {
        $('form').attr('enctype', 'multipart/form-data');

        $('input[id*="afuProjectFiles"][type="file"]').parent('div').append('<label id="fileLabel">Choose or drag and drop file</label>');

        $('input[id*="rbtnlJobCategory"][type="radio"]').click(function () {
            var selected = $(this).val();
            var html1 = '<div class="panel-collapse in collapse" style=""><div class="panel-body"><p>HTML stands for HyperText Markup Language. HTML is the main markup language for describing the structure of Web pages. </p></div></div>';
            var html2 = '<div class="panel-collapse in collapse" style=""><div class="panel-body"><p>Bootstrap is a powerful front-end framework for faster and easier web development. It is a collection of CSS and HTML conventions.</p></div></div>';

            if ($(this).parent('td').html().indexOf('panel-collapse') == -1) {
                $(this).parent('td').append((selected == "FE" ? html1 : html2));
            }

            $(this).parent('td').find('div[class*="panel-collapse"]').slideToggle(300);
        });

        $('textarea[id*="txtJobDescription"]').on('inputchange', function () {
            CountCharacters();
        });

        $('#ContentPlaceHolder1_afuProjectFiles').click(function () {
            $('input[type="file"]').click();
        });

        $('.business-radio-btn input[type="radio"]').click(function () {
            $('.business-radio-btn').find('td').removeClass('selected');
            $(this).parent('td').addClass('selected');
        });

        $('.special-radio-btn input[type="radio"]').click(function () {
            $(this).closest('table').find('td').removeClass('special-radio-btn-selected');
            $(this).closest('td').addClass('special-radio-btn-selected');
        });

        $('.special-btn input[type="checkbox"]').click(function () {
            if ($(this).prop('checked') == true) {
                $(this).parent('td').addClass('btn-selected');
                //
            }
            else {
                $(this).parent('td').removeClass('btn-selected');
            }

        });


        //$('html').on('dragenter', function (e) {
        //    $('.file-upload').find('label').text('Drag here');
        //    e.stopPropagation();
        //    e.preventDefault();
        //});
        //$('.file-upload').on('dragover', function (e) {
        //    $('.file-upload').find('label').text('Drop here');
        //    e.stopPropagation();
        //    e.preventDefault();
        //});

        //$('html').on('dragleave', function (e) {
        //    $('.file-upload').find('label').text('Choose or drag and drop file');
        //    e.stopPropagation();
        //    e.preventDefault();
        //});


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