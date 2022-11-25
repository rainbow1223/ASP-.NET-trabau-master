function RegisterProfileLoadEvents() {
    $(document).ready(function () {
        $('input[id*="chkServiceType"]').click(function () {
            var total_checked = $('div[id*="div_service_type"]').find('input[type="checkbox"]:checked').length;
            if (total_checked == 4) {
                $('div[id*="div_service_type"]').find('input[type="checkbox"]:not(:checked)').prop('disabled', 'disabled');
            }
            else {
                $('div[id*="div_service_type"]').find('input[type="checkbox"]:not(:checked)').prop('disabled', '');
            }
        });

        $('a[id*="lbtnUploadProfilePicture"]').click(function () {
            document.getElementById("fu_profilepic_upload").click();
            return false;
        });
    });
}