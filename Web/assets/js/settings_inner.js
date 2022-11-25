Sys.Application.add_load(LoadProfileEvents);

function ValidateFileUploadExtension() {
    $('#UpdateProgress1').show();
    var fupData = document.getElementById('fu_profilepic_upload');

    var FileUploadPath = fupData.value;

    if (FileUploadPath == '') {

        // There is no file selected
        Swal.fire({ type: 'error', title: 'Please select file to upload', showConfirmButton: true, timer: 1500 });
        $('#UpdateProgress1').hide();
    }
    else {

        var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

        if (Extension == "jpeg" || Extension == "jpg" || Extension == "png") {
            readURL(fupData);
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

function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#pic_browse').hide();
            $('#pic_crop').show();
            $('#img_profile_pic_crop').attr('src', e.target.result);

            document.getElementById('btndestroy_crop').click();

            StartCrop();

        }

        reader.readAsDataURL(input.files[0]);
    }
    $('#UpdateProgress1').hide();
}


function StartCrop(data_uri) {
    var image = document.querySelector('#img_profile_pic_crop');
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

    document.getElementById('btndestroy_crop').onclick = function () {
        cropper.destroy();
    };

    $('input[id*="btnSaveProfilePicture"]').click(function () {
        $('#UpdateProgress1').show();
        try {
            var imageData = cropper.getCroppedCanvas();
            $('#hfCropped_Picture').val(imageData.toDataURL());
            $('#pic_browse img').attr('src', imageData.toDataURL());
            $('#pic_browse').show();
            $('#pic_crop').hide();
            $('#UpdateProgress1').hide();
            return true;
        } catch (e) {
            $('#UpdateProgress1').hide();
        }
        return false;
    });

}