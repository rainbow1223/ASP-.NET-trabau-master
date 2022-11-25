Sys.Application.add_load(LoadCompanyEvents);

function ValidateCompanyLogoFileUploadExtension() {
    $('#UpdateProgress1').show();
    var fupData = document.getElementById('fu_companylogo_upload');

    var FileUploadPath = fupData.value;

    if (FileUploadPath == '') {

        // There is no file selected
        Swal.fire({ type: 'error', title: 'Please select file to upload', showConfirmButton: true, timer: 1500 });
        $('#UpdateProgress1').hide();
    }
    else {

        var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

        if (Extension == "jpeg" || Extension == "jpg" || Extension == "png") {
            CompanyLogo_readURL(fupData);
            //$('#UpdateProgress1').hide();
            return false;
            //args.IsValid = true; // Valid file type
        }
        else {
            Swal.fire({ type: 'error', title: 'Please select a valid image(jpeg, jpg, png)', showConfirmButton: true, timer: 1500 });
            $('#UpdateProgress1').hide();
            return false; // Not valid file type
        }
    }
}

function CompanyLogo_readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#company_logo_browse').hide();
            $('#companylogo_crop').show();
            $('#img_company_logo_crop').attr('src', e.target.result);

            document.getElementById('btn_companylogo_destroy_crop').click();

            StartCrop_CompanyLogo();

        }

        reader.readAsDataURL(input.files[0]);
    }
    $('#UpdateProgress1').hide();
}


function StartCrop_CompanyLogo(data_uri) {
    var image = document.querySelector('#img_company_logo_crop');
    var minAspectRatio = 0.5;
    var maxAspectRatio = 1.5;
    var cropper = new Cropper(image, {
        ready: function () {
            var cropper = this.cropper;

            var containerData = cropper.getContainerData();
            var cropBoxData = cropper.getCropBoxData();
            var aspectRatio = cropBoxData.width / cropBoxData.height;
            var newCropBoxWidth;

            if (aspectRatio < minAspectRatio || aspectRatio > maxAspectRatio) {
                newCropBoxWidth = cropBoxData.height * ((minAspectRatio + maxAspectRatio) / 2);

                cropper.setCropBoxData({
                    left: (containerData.width - newCropBoxWidth) / 2,
                    width: newCropBoxWidth
                });
            }

        },
        cropmove: function () {
            var cropper = this.cropper;
            var cropBoxData = cropper.getCropBoxData();
            var aspectRatio = cropBoxData.width / cropBoxData.height;

            if (aspectRatio < minAspectRatio) {
                cropper.setCropBoxData({
                    width: cropBoxData.height * minAspectRatio
                });
            } else if (aspectRatio > maxAspectRatio) {
                cropper.setCropBoxData({
                    width: cropBoxData.height * maxAspectRatio
                });
            }
        }
    });

    document.getElementById('btn_companylogo_destroy_crop').onclick = function () {
        cropper.destroy();
    };

    $('input[id*="btnSaveCompanyLogo"]').click(function () {
        $('#UpdateProgress1').show();
        try {
            var imageData = cropper.getCroppedCanvas();
            $('#hf_CompanyLogo_Cropped').val(imageData.toDataURL());
            $('#company_logo_browse img').attr('src', imageData.toDataURL());
            $('#company_logo_browse').show();
            $('#companylogo_crop').hide();
            $('#UpdateProgress1').hide();
            return true;
        } catch (e) {
            $('#UpdateProgress1').hide();
        }
        return false;
    });

}