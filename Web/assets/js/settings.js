function LoadProfileEvents() {
    $(document).ready(function () {
        $('a[id*="lbtnUploadProfilePicture"]').click(function () {
            document.getElementById("fu_profilepic_upload").click();
            return false;
        });
    });
}

function LoadCompanyEvents() {
    $(document).ready(function () {
        $('a[id*="lbtnUploadCompanyLogo"]').click(function () {
            document.getElementById("fu_companylogo_upload").click();
            return false;
        });
    });
}

function LoadDocsEvents() {
    $(document).ready(function () {
        //CheckOtherDoc();
        //           $('#btnUpload').click(function () {
        //    $('select[id*="ddlDocumentType"]').attr('disabled', true);
        //    var DocType = $('select[id*="ddlDocumentType"] option:selected').text();

        //    if (DocType == 'Youtube Video') {
        //        return true;
        //    }
        //    else {
        //        var fupData = document.getElementById('<%= fu_profile_document.ClientID %>');
        //        var FileUploadPath = fupData.value;

        //        if (FileUploadPath == '') {
        //            // There is no file selected
        //            toastr['error']('Please select a file and upload *.doc, *.docx, *.pdf, *.jpg, *.png, *.gif, *.tif file only');
        //            setTimeout(function () {
        //                $('select[id*="ddlDocumentType"]').attr('disabled', false);
        //            }, 3000);
        //        }
        //        else {
        //            var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

        //            if (Extension == "jpeg" || Extension == "jpg" || Extension == "png" ||
        //                Extension == "docx" || Extension == "doc" || Extension == "pdf" ||
        //                Extension == "tif" || Extension == "gif") {
        //                return true;
        //            }
        //            else {
        //                toastr['error']('Please select a file and upload *.doc, *.docx, *.pdf, *.jpg, *.png, *.gif, *.tif file only');
        //                setTimeout(function () {
        //                    $('select[id*="ddlDocumentType"]').attr('disabled', false);
        //                }, 3000);
        //                return false; // Not valid file type
        //            }
        //        }
        //    }

        //    setTimeout(function () {
        //        $('select[id*="ddlDocumentType"]').attr('disabled', false);
        //    }, 3000);

        //    return false;
        //}); 

        $('a[id*="lbtnRemoveDocument"]').click(function () {
            Swal.fire({
                title: 'Are you sure you remove this document?',
                text: "Removal Confirmation",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, remove it!'
            }).then((result) => {
                if (result.value) {
                    window.location.href = $(this).attr('href');
                }
            });
            return false;
        });

        $('.file-upload').click(function () {
            $('#fu_profile_document').click();
            return false;
        });


    });
}
        //function CheckOtherDoc() {
        //    var DocType = $('select[id*="ddlDocumentType"] option:selected').text();
        //    $('.file-upload').show();
        //    $('.youtube-video').hide();
        //    ValidatorEnable($('span[id*="rfvDocumentFile"]')[0], true);
        //    ValidatorEnable($('span[id*="rfvYoutubeVideoName"]')[0], false);
        //    if (DocType == 'Other') {
        //        $('.other-document').show();
        //        $('.other-document label').text('Enter Other Document Name');
        //        $('input[id*="txtOtherDocumentType"]').attr('placeholder', 'Enter Other Document Name');
        //        $('span[id*="rfvOtherDocType"]').text('Enter Other Document Name');
        //        ValidatorEnable($('span[id*="rfvOtherDocType"]')[0], true);
        //    }
        //    else if (DocType == 'Youtube Video') {
        //        $('.other-document').show();
        //        $('.youtube-video').show();
        //        $('.other-document label').text('Enter Youtube Video URL');
        //        $('input[id*="txtOtherDocumentType"]').attr('placeholder', 'Enter Youtube Video URL');
        //        $('span[id*="rfvOtherDocType"]').text('Enter Youtube Video URL');
        //        ValidatorEnable($('span[id*="rfvDocumentFile"]')[0], false);
        //        $('.file-upload').hide();
        //        ValidatorEnable($('span[id*="rfvOtherDocType"]')[0], true);
        //        ValidatorEnable($('span[id*="rfvYoutubeVideoName"]')[0], true);
        //    }
        //    else {
        //        $('.other-document').hide();
        //        ValidatorEnable($('span[id*="rfvOtherDocType"]')[0], false);
        //        $('input[id*="txtOtherDocumentType"]').val('');
        //    }
        //}

