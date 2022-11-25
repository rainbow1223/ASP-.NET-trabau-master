$(document).ready(function () {
    $('[id*="lbtnConfirmation_MenuLink"]:contains("Send")').click(function () {
        HandlePopUp('1', 'divTrabau_SendJobToFriend');
        $('#hfFriendUserId').val('');
        $('#txtFriendName').val('');
        $('#txtFriendEmailAddress').val('');
        AutoCompleteTextBox('txtFriendName', 'hfFriendUserId', '', pathconfig + '/GetUsers', '::', 'SearchUserForSendToFriend');
        return false;
    });

    $('#btnSendJobToFriend').click(function () {
        var Name = $('#txtFriendName').val();
        var EmailAddress = $('#txtFriendEmailAddress').val();
        if (Name != '') {
            if (EmailAddress != '') {
                var userid = $('#hfFriendUserId').val();
                SendJobToFriend(Name, EmailAddress, userid);
            }
            else {
                toastr['error']('Enter Email Address for sending job to friend');
            }
        }
        else {
            toastr['error']('Enter Name for sending job to friend');
        }
        return false;
    });
});

function SearchUserForSendToFriend(id) {
    $('#txtFriendEmailAddress').val('***********');
}

function RegisterPreferEvent() {
    $('.btn-prefer-list').unbind('click');
    $('.btn-prefer-list').click(function () {
        debugger;
        var userid = $(this).parent('div').parent('div').attr('data');
        var action_control = $(this).parent('div').parent('div').attr('id');
        var action_method = $(this).attr('action-method');
        var action_name = $(this).attr('action-name');
        var Type = 'A';
        if ($(this).attr('class').indexOf('disabled') > -1) {
            Type = 'D';
        }
        AddTo_PreferList(userid, Type, action_control, action_method, action_name);
    });
}


function SendJobToFriend(Name, EmailAddress, userid) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/SendJobToFriend',
        data: "{Name:'" + Name + "',EmailAddress:'" + EmailAddress + "',userid:'" + userid + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            var response = json_data[0].response;
            if (response == 'ok') {
                var action_response = json_data[0].action_response;
                var action_message = json_data[0].action_message;
                if (action_response == 'success') {
                    $('#hfFriendUserId').val('');
                    $('#txtFriendName').val('');
                    $('#txtFriendEmailAddress').val('');

                    HandlePopUp('0', 'divTrabau_SendJobToFriend');
                }
                toastr[action_response](action_message);
            }
            else {
                toastr['error']('Error while sending job to ' + EmailAddress + ', please refresh and try again');
            }
        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function AddTo_PreferList(UserId, Type, action_control, action_method, action_name) {
    $('#' + action_control).find('a[action-method="' + action_method + '"]').html('<img src="../../assets/uploads/' + (Type == 'A' ? 'loading-green-back.svg' : 'loading-gray-back.svg') + '" class="loading-request"/> ' + (Type == 'A' ? 'Adding' : 'Removing'));
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig_hire + '/' + action_name,
        data: "{UserId:'" + UserId + "',Type:'" + Type + "',action_method:'" + action_method + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            var message = json_data[0].message;
            var message_response = json_data[0].message_response;
            var redirect_url = json_data[0].redirect_url;
            toastr[message_response](message);

            if (json_data[0].response == 'ok') {
                if (message_response == 'success') {
                    if (Type == 'A') {
                        $('#' + action_control).find('a[action-method="' + action_method + '"]').html('<i class="fa fa-check" aria-hidden="true"></i> Prefer List');
                        $('#' + action_control).find('a[action-method="' + action_method + '"]').addClass('disabled');
                        $('#' + action_control).find('a[action-method="' + action_method + '"]').attr('data-content', 'Remove Freelancer from preferred list');
                    }
                    else {
                        $('#' + action_control).find('a[action-method="' + action_method + '"]').html('Add to Prefer');
                        $('#' + action_control).find('a[action-method="' + action_method + '"]').removeClass('disabled');
                        $('#' + action_control).find('a[action-method="' + action_method + '"]').attr('data-content', 'Add Freelancer to preferred list');
                    }
                }
            }
            else {
                if (redirect_url != undefined && redirect_url != '') {
                    setTimeout(function () { window.location.href = redirect_url; }, 1000);
                }
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}
function RegisterMenuChangeEvent() {
    $('select[id*="ddlJobPostingMenu"]').change(function () {
        var MenuName = $(this).val();
        if (MenuName == 'VJP') {
            window.location.href = $('a[id*="lbtnTabViewJobPost"]').attr('href');;
        }
        else if (MenuName == 'IF') {
            window.location.href = $('a[id*="lbtnTabInvite"]').attr('href');;
        }
        else if (MenuName == 'RP') {
            $('a[id="proposals-tab"]').click();
        }
        else if (MenuName == 'H') {
            window.location.href = $('a[id*="lbtnTabHire"]').attr('href');;
        }
    });
}

function RemovalConfirmation(id) {
    Swal.fire({
        title: 'Are you finish hiring for this job?',
        text: "This job will be closed and you will be not able to hire more people.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, finished hiring!',
    }).then((result) => {
        if (result.value) {
            var click_event = $(id).parent('li').find('a[id*="lbtnConfirmation_MenuLink"]').attr('href');
            window.location.href = click_event;
            return true;
        }
    });
    return false;
}

function ActivateTooltip() {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover'
    });
}

function ActivateInviteTab(TabName) {
    $('.nav-pills').find('li a').removeClass('active');

    $('#myTabContent').find('.tab-pane').removeClass('active');
    $('#myTabContent').find('.tab-pane').removeClass('show');

    if (TabName == 'I') {
        $('.nav-pills').find('a[id*=lbtnTabInvite]').addClass('active');
        $('#myTabContent').find('div[id=invite]').addClass('active show');
    } else if (TabName == 'JP') {
        $('.nav-pills').find('a[id*=lbtnTabViewJobPost]').addClass('active');
        $('#myTabContent').find('div[id=home]').addClass('active show');
    }
    else if (TabName == 'H') {
        $('.nav-pills').find('a[id*=lbtnTabHire]').addClass('active');
        $('#myTabContent').find('div[id=invite]').addClass('active show');
    }


}

function ActivateChild_InviteTab(TabName) {
    $('#search-f-tab').addClass('active show');
    $('.nav-tabs').find('a[id*="nav-link"]').removeClass('active');

    if (TabName == 'S') {
        $('.nav-tabs').find('a[id*="lbtnChildSearchTab"]').addClass('active');
    } else if (TabName == 'I') {
        $('.nav-tabs').find('a[id*="lbtnChildInviteTab"]').addClass('active');
    }
    else if (TabName == 'H') {
        $('.nav-tabs').find('a[id*="lbtnChildHireTab"]').addClass('active');
    }
    else if (TabName == 'Saved') {
        $('.nav-tabs').find('a[id*="lbtnChildSavedTab"]').addClass('active');
    }


}


function ChangeDropDown(id, input) {
    $(id).parent('li').parent('ul').find('a[class*="search-selected"]').removeClass('search-selected');
    $(id).addClass('search-selected');
    $('#' + input).attr('placeholder', $(id).text());
}

function openURL(myurl) {
    window.open(myurl, '_blank');
}


var loading_data = '<div class="loading-linear-background"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';
var loading_data_small = '<div class="loading-linear-back-small"><div class="loading-top-1" ></div><div class="loading-top-2"></div></div>';

function LoadProposals() {
    $('#div_proposals').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/ViewProposals',
        data: "{}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var Proposals_html = json_data[0].Proposals_html;
                Proposals_html = Proposals_html.substring(Proposals_html.indexOf('<div class="freelancers-result-data">'), Proposals_html.indexOf('</form>'));
                $('#div_proposals').html(Proposals_html);

                $('.proposal-inner-row').click(function () {
                    var proposalid = $(this).attr('id');
                    ViewProposal_Details(proposalid);
                });

                $('a[id*="aHire"]').click(function () {
                    if ($(this).attr('class').indexOf('disabled') == -1) {
                        var data = $(this).parent('div').attr('data');
                        Proposal_Action(data, 'H', this);
                    }
                });

                $('a[id*="aDecline"]').click(function () {
                    if ($(this).attr('class').indexOf('disabled') == -1) {
                        var data = $(this).parent('div').attr('data');
                        Proposal_Action(data, 'D', this);
                    }
                });

                $('a[id*="aPropsalDetails"]').click(function () {
                    var proposalid = $(this).parent('div').attr('data');
                    ViewProposal_Details(proposalid);
                });

                GetTrabau_PicInfo('1000', $('.freelancers-result-data .proposal-row').length + 1000);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function ViewProposal_Details(proposalid) {
    HandlePopUp('1', 'divTrabau_proposal_details');
    $('.proposaldetails-content').html(loading_data);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/ViewProposalDetails',
        data: "{ProposalId:'" + proposalid + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var Proposal_details_html = json_data[0].Proposal_details_html;
                Proposal_details_html = Proposal_details_html.substring(Proposal_details_html.indexOf('<div class="proposal-details-result-data">'), Proposal_details_html.indexOf('</form>'));
                $('.proposaldetails-content').html(Proposal_details_html);

                $('#img_profile_picture').attr('src', $('#' + proposalid).find('img[id*="imgFL_ProfilePic"]').attr('src'));

                $('a[id*="lbtnHire"]').click(function () {
                    if ($(this).attr('class').indexOf('disabled') == -1) {
                        var data = $(this).parent('div').attr('data');
                        Proposal_Action(data, 'H', this);
                    }
                });

                $('a[id*="lbtnDecline"]').click(function () {
                    if ($(this).attr('class').indexOf('disabled') == -1) {
                        var data = $(this).parent('div').attr('data');
                        Proposal_Action(data, 'D', this);
                    }
                });

                $('a[id*="lbtnFlagForInterview"]').click(function () {
                    if ($(this).attr('class').indexOf('disabled') == -1) {
                        var data = $(this).parent('div').attr('data');
                        FlagForInterview(data, this);
                        //HandlePopUp('0', 'divTrabau_proposal_details');
                        //HandlePopUp('1', 'divTrabau_Interview_details');

                        //GetInterview_Content();
                    }
                });

                GetProposalAdditionalFileDetails(proposalid);

            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}




function GetProposalAdditionalFileDetails(ProposalId) {
    $('.additional-files').html(loading_data_small);
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/GetProposalAdditionalFiles',
        data: "{ProposalId:'" + ProposalId + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var additional_files = json_data[0].jobAF_html;
                additional_files = additional_files.substring(additional_files.indexOf('<div class="proposal-additional-files">'), additional_files.indexOf('</form>'));
                $('.additional-files').html(additional_files);
            }
            else {

            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
        }
    });
}


function DownloadAdditionalFile(id) {
    var URL = $(id).attr('download-url');
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/DownloadAdditionalFile',
        data: "{URL:'" + URL + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var download_url = json_data[0].DownloadURL;
                window.open(download_url, '_blank');
            }
            //else {

            //}

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}



function Proposal_Action(proposalId, action, id) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/Proposal_Action',
        data: "{proposalId:'" + proposalId + "',action:'" + action + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {

                    if (action == 'H') {
                        $('div[data="' + proposalId + '"]').find('a[id*="Decline"]').remove();

                        $('div[data="' + proposalId + '"]').find('a[id*="Hire"]').addClass('disabled');
                        $('div[data="' + proposalId + '"]').find('a[id*="Hire"]').html('<i class="fa fa-check" aria-hidden="true"></i> Hired');
                    }
                    else {
                        $('div[data="' + proposalId + '"]').find('a[id*="Hire"]').remove();

                        $('div[data="' + proposalId + '"]').find('a[id*="Decline"]').addClass('disabled');
                        $('div[data="' + proposalId + '"]').find('a[id*="Decline"]').html('<i class="fa fa-check" aria-hidden="true"></i> Declined');
                    }
                }
                toastr[action_response](action_message);
            }
            else {
                toastr["error"]("Error while taking action on proposal, please refresh and try again");
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function FlagForInterview(proposalId, id) {
    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: pathconfig + '/FlagForInterview',
        data: "{ProposalId:'" + proposalId + "'}",
        success: function (msg) {
            var data = msg.d;
            var json_data = JSON.parse(data);

            if (json_data[0].response == 'ok') {
                var action_message = json_data[0].action_message;
                var action_response = json_data[0].action_response;
                if (action_response == 'success') {


                    $('div[data="' + proposalId + '"]').find('a[id*="lbtnFlagForInterview"]').addClass('disabled');
                    $('div[data="' + proposalId + '"]').find('a[id*="lbtnFlagForInterview"]').html('<i class="fa fa-check" aria-hidden="true"></i> Flag For Interview');
                }
                toastr[action_response](action_message);
            }
            else {
                toastr["error"]("Error while taking action on proposal, please refresh and try again");
            }

        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}