<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile_user_profile" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Src="~/profile/usercontrols/wucEmploymentHistory.ascx" TagPrefix="uc1" TagName="wucEmploymentHistory" %>
<%@ Register Src="~/profile/usercontrols/wucEducationHistory.ascx" TagPrefix="uc1" TagName="wucEducationHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/profile/usercontrols/wucProfilePicture.ascx" TagPrefix="uc1" TagName="wucProfilePicture" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/profile_style-1.1.css?version=1.0") %>' rel="stylesheet" type="text/css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select-custom.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/AutoComplete/js/AutoCompleteText.js") %>' type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script>
        function ProfileLoad_Events() {
            $(document).ready(function () {
                $('.profile-details-top h3').mouseover(function () {
                    $('.profile-details-top h3 .profile-details-title-edit').show();
                });
                $('.profile-details-top h3').mouseout(function () {
                    $('.profile-details-top h3 .profile-details-title-edit').hide();
                });


                $('.profile-details-bottom p').mouseover(function () {
                    $('.profile-details-bottom p .profile-details-overview-edit').show();
                });
                $('.profile-details-bottom p').mouseout(function () {
                    $('.profile-details-bottom p .profile-details-overview-edit').hide();
                });


                $('.profile-rate-item-top').mouseover(function () {
                    $('.profile-rate-item-top .edit-pencil-button').css('display', 'inline-block');
                });
                $('.profile-rate-item-top').mouseout(function () {
                    $('.profile-rate-item-top .edit-pencil-button').hide();
                });

                $('.my-profile-picture').mouseover(function () {
                    $('.my-profile-picture .edit-pencil-button').css('display', 'inline-block');
                });
                $('.my-profile-picture').mouseout(function () {
                    $('.my-profile-picture .edit-pencil-button').hide();
                });

                $('a[id*="lbtnUploadProfilePicture"]').click(function () {
                    document.getElementById("fu_profilepic_upload").click();
                    return false;
                });
            });
        }


    </script>
    <asp:UpdatePanel ID="upParent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script>
                Sys.Application.add_load(ProfileLoad_Events);


                function ActivateDatePicker() {
                    $('input[id*="txtVacationTillDated"]').datepicker({
                        minDate: 0
                    })
                }
                function RadioButtonSelection_Override() {
                    $('.special-radio-btn').find('td').addClass('col-lg-4 col-md-4 col-sm-4');
                }

                function ProfileAvailabilityEvents() {
                    $(document).ready(function () {
                        $('.special-radio-btn input[type="radio"]').click(function () {
                            $(this).closest('table').find('td').removeClass('special-radio-btn-selected');
                            $(this).closest('td').addClass('special-radio-btn-selected');
                        });
                    });
                }

                function CheckRadioButton() {
                    $('input[type="radio"]:checked').closest('td').addClass('special-radio-btn-selected');

                }

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
            </script>
            <div class="row">
                <div class="col-xs-12 col-lg-9 my-profile-left">
                    <div class="profile-card padding-20">
                        <div class="row profile-top">
                            <div class="col-xs-12 col-sm-8 col-md-9">
                                <div class="profile-content">
                                    <div class="my-profile-picture">
                                        <img class="profile-avatar avatar" id="img_profile_picture" runat="server" src="../assets/uploads/avtar.jpg" />
                                        <asp:LinkButton ID="lbtnEditProfilePicture" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditProfilePicture_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </div>
                                    <div class="profile-text">
                                        <h2 class="profile-name">
                                            <asp:Literal ID="ltrl_profile_name" runat="server"></asp:Literal>
                                        </h2>
                                        <div class="location">
                                            <i class="fa fa-map-marker" aria-hidden="true"></i>
                                            <span class="location-main">
                                                <asp:Literal ID="ltrl_profile_cityname" runat="server"></asp:Literal></span>
                                            <span class="location-timezone">
                                                <asp:Literal ID="ltrlLocalTime" runat="server"></asp:Literal></span>
                                        </div>
                                        <div class="profile-progress-bar progress-bar-mobile mt-4">
                                            <div>
                                                <h3 class="progress-bar-percentage">
                                                    <asp:Literal ID="ltrlProgessPercentageMobile" runat="server"></asp:Literal></h3>
                                                <div class="progress-bar-parent">
                                                    <div class="progress-bar progress-bar-override" id="divProgessBarMobile" runat="server"></div>
                                                </div>
                                                <small>Job Success
                                                </small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3 profile-progress-block">
                                <div class="profile-progress-bar progress-bar-big mt-3">
                                    <div>
                                        <h3 class="progress-bar-percentage">
                                            <asp:Literal ID="ltrlProgessPercentage" runat="server"></asp:Literal></h3>
                                        <div class="progress-bar-parent">
                                            <div class="progress-bar progress-bar-override" id="divProgessBar" runat="server"></div>
                                        </div>
                                        <small>Job Success
                                        </small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="profile-details">
                            <div class="profile-details-top">
                                <h3>
                                    <asp:Literal ID="ltrl_profile_title" runat="server"></asp:Literal>
                                    <span class="profile-details-title-edit">
                                        <asp:LinkButton ID="lbtnEditTitle" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditTitle_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </span>
                                </h3>
                            </div>
                            <div class="profile-details-bottom">
                                <p>
                                    <asp:Literal ID="ltrl_profile_overview" runat="server"></asp:Literal>
                                    <span class="profile-details-overview-edit">
                                        <asp:LinkButton ID="lbtnEditOverview" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditOverview_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </span>
                                </p>
                            </div>
                        </div>
                        <hr />
                        <div class="profile-rate-details">
                            <ul>
                                <li>
                                    <div class="profile-rate-item-top">
                                        <span>
                                            <asp:Literal ID="ltrlHourlyRate" runat="server"></asp:Literal></span>
                                        <asp:LinkButton ID="lbtnEditHourlyRate" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditHourlyRate_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </div>
                                    <div class="profile-rate-item-bottom">Hourly rate</div>
                                </li>
                                <li>
                                    <div class="profile-rate-item-top">
                                        <span>
                                            <asp:Literal ID="ltrlTotalEarned" runat="server"></asp:Literal></span>
                                    </div>
                                    <div class="profile-rate-item-bottom">Total earned</div>
                                </li>
                                <li>
                                    <div class="profile-rate-item-top">
                                        <span>
                                            <asp:Literal ID="ltrlTotalJobs" runat="server"></asp:Literal></span>
                                    </div>
                                    <div class="profile-rate-item-bottom">Total jobs</div>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="profile-card">
                        <div class="airCardHeader d-flex align-items-center">
                            <h2>Portfolio</h2>
                            <div class="ml-auto editCard-btn">
                                <asp:LinkButton ID="lbtnAddPortfolio" runat="server" Text="<i class='fa fa-plus' aria-hidden='true'></i>" OnClick="lbtnAddPortfolio_Click"
                                    CssClass="edit-pencil-button"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="airCardBody padding-20">
                            <div class="card-body-content row">
                                <asp:Repeater ID="rPortfolio" runat="server">
                                    <ItemTemplate>
                                        <div class="col-sm-6">
                                            <asp:Label ID="lblPortfolioId" runat="server" Visible="false" Text='<%#Eval("PortfolioId") %>'></asp:Label>
                                            <div class="portfolio-items">
                                                <img src='<%#Eval("GalleryItemPreview") %>' />
                                                <asp:LinkButton ID="lbtnEditPortfolio" runat="server" ClientIDMode="AutoID" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditPortfolio_Click"
                                                    CssClass="edit-pencil-button media-remove-item"></asp:LinkButton>
                                            </div>
                                            <h5><%#Eval("PortfolioTitle") %></h5>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="profile-card">
                        <div class="airCardHeader d-flex align-items-center">
                            <h2>Skills</h2>
                            <div class="ml-auto editCard-btn">
                                <asp:LinkButton ID="lbtnEditSkills" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditSkills_Click"
                                    CssClass="edit-pencil-button"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="airCardBody padding-20">
                            <div class="card-body-content">
                                <asp:Repeater ID="rSkills" runat="server">
                                    <ItemTemplate>
                                        <span class="skills"><%#Eval("SkillName") %></span>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="profile-card">
                        <uc1:wucEmploymentHistory runat="server" ID="wucEmploymentHistory" />
                    </div>
                    <div class="profile-card">
                        <uc1:wucEducationHistory runat="server" ID="wucEducationHistory" />
                    </div>
                </div>
                <div class="col-lg-3 profile-sidebar">
                    <div class="btn-profile-settings">
                        <a href="../../profile/settings/">Profile Settings</a>
                    </div>
                    <div class="right-profile-main">
                        <h4>Visibility<asp:LinkButton ID="lbtnEditProfileVisibility" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditProfileVisibility_Click"
                            CssClass="edit-pencil-button edit-profile-visibility"></asp:LinkButton></h4>
                        <div class="right-profile-content">
                            <strong><i id="profile_visibility_icon_class" runat="server" class="fa fa-id-badge" aria-hidden="true"></i></strong>
                            <span>
                                <asp:Literal ID="ltrlProfileVisibility" runat="server"></asp:Literal>
                                <asp:Literal ID="lblProfileVisibility" runat="server" Visible="false"></asp:Literal>
                            </span>

                        </div>
                    </div>
                    <div class="right-profile-main">
                        <h4>Availability
                            <asp:LinkButton ID="lbtnEditAvailability" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditAvailability_Click"
                                CssClass="edit-pencil-button edit-profile-availability"></asp:LinkButton></h4>
                        <div class="right-profile-content">
                            <asp:Literal ID="ltrlProfileAvailability" runat="server" Visible="false"></asp:Literal>
                            <asp:Literal ID="ltrlVacationDate" runat="server" Visible="false"></asp:Literal>
                            <strong><i class="fa fa-clock-o" aria-hidden="true"></i></strong>
                            <asp:Label ID="lblProfileAvailability" runat="server"></asp:Label>

                        </div>
                    </div>
                </div>
            </div>
            <div id="divTrabau_Popup_EditTitle" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title" id="popHeader" runat="server">Edit your title</h4>
                            <asp:Button ID="btnCloseEditTitlePopup_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseEditTitlePopup_top_Click" />
                        </div>

                        <!-- Modal body -->
                        <asp:UpdatePanel ID="UpnlEditTitle" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-body" id="div_edit_title_content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txtProfileTitle">Your title</label>
                                                <asp:TextBox ID="txtProfileTitle" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtProfileTitle" SetFocusOnError="true"
                                                    ErrorMessage="Enter Profile Title" ValidationGroup="SaveTitle" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSaveTitle" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveTitle_Click" CommandName="save" ValidationGroup="SaveTitle" />
                                    <asp:Button ID="btnCloseEditTitlePopup" runat="server" Text="Close" OnClick="btnCloseEditTitlePopup_top_Click" CssClass="cta-btn-sm" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="divTrabau_Popup_EditOverview" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title" id="H1" runat="server">Overview</h4>
                            <asp:Button ID="btnCloseEditOverviewPopup_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseEditOverviewPopup_top_Click" />
                        </div>

                        <!-- Modal body -->
                        <asp:UpdatePanel ID="upEditOverview" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-body" id="div_edit_overview_content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txtProfileOverview">Use this space to show clients you have the skills and experience they're looking for. </label>
                                                <asp:TextBox ID="txtProfileOverview" runat="server" CssClass="form-control" autocomplete="Off" TextMode="MultiLine" Height="300px"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtProfileOverview" SetFocusOnError="true"
                                                    ErrorMessage="Enter Profile Overview" ValidationGroup="SaveOverview" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSaveOverview" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveOverview_Click" ValidationGroup="SaveOverview" />
                                    <asp:Button ID="btnCloseOverview_bottom" runat="server" Text="Close" OnClick="btnCloseEditOverviewPopup_top_Click" CssClass="cta-btn-sm" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="divTrabau_Popup_EditSkills" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title" id="H2" runat="server">My skills</h4>
                            <asp:Button ID="btnCloseEditSkillsPopup_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseEditSkillsPopup_Click" />
                        </div>

                        <!-- Modal body -->
                        <asp:UpdatePanel ID="upSkills" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="modal-body" id="div_edit_skills_content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txtSkills">Enter your skills</label>
                                                <asp:DropDownList ID="ddlSkills" runat="server" CssClass="form-control" autocomplete="Off"></asp:DropDownList>
                                                <asp:HiddenField ID="hfSkills" runat="server" ClientIDMode="Static" />
                                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlSkills" SetFocusOnError="true"
                                                    ErrorMessage="Enter your skills" ValidationGroup="SaveSkills" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSaveSkills" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveSkills_Click" ValidationGroup="SaveSkills" />
                                    <asp:Button ID="btnCloseEditSkillsPopup_bottom" runat="server" Text="Close" OnClick="btnCloseEditSkillsPopup_Click" CssClass="cta-btn-sm" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div id="divTrabau_Popup_HourlyRate" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Change hourly rate</h4>
                            <asp:Button ID="btnCloseHourlyRatePopup_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseHourlyRatePopup_Click" />
                        </div>

                        <!-- Modal body -->
                        <asp:UpdatePanel ID="upHourlyRate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12 margin-bottom-10">
                                            Your profile rate:
                                            <b>
                                                <asp:Literal ID="ltrlCurrentProfileRate" runat="server"></asp:Literal></b>
                                        </div>
                                        <div class="col-sm-12">
                                            <label for="txtHourlyRate">Hourly Rate (Total amount the client will see)</label>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtHourlyRate" runat="server" CssClass="form-control" autocomplete="Off" MaxLength="10"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fteHourlyRate" runat="server" TargetControlID="txtHourlyRate" FilterMode="ValidChars"
                                                    ValidChars="0123456789." />
                                                <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                                <span class="hourly-rate-units">/hr</span>
                                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtHourlyRate" SetFocusOnError="true"
                                                    ErrorMessage="Enter Hourly Rate" ValidationGroup="SaveHourlyRate" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSaveHourlyRate" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveHourlyRate_Click" CommandName="save" ValidationGroup="SaveHourlyRate" />
                                    <asp:Button ID="btnCloseHourlyRatePopup" runat="server" Text="Close" OnClick="btnCloseHourlyRatePopup_Click" CssClass="cta-btn-sm" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div id="divTrabau_Profile_Visibility" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Profile Visibility</h4>
                            <asp:Button ID="btnCloseProfileVisibilityPopup_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseProfileVisibilityPopup_top_Click" />
                        </div>

                        <!-- Modal body -->
                        <asp:UpdatePanel ID="upProfileVisibility" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="txtProfileTitle">Your visibility</label>
                                                <asp:DropDownList ID="ddlProfileVisibility" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Public" Value="Public"></asp:ListItem>
                                                    <asp:ListItem Text="Trabau Users Only" Value="Trabau"></asp:ListItem>
                                                    <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlProfileVisibility" SetFocusOnError="true"
                                                    ErrorMessage="Select Profile Visibility" ValidationGroup="UpdateProfileVisibility" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnUpdateProfileVisibility" runat="server" Text="Update" CssClass="cta-btn-sm" OnClick="btnUpdateProfileVisibility_Click" ValidationGroup="UpdateProfileVisibility" />
                                    <asp:Button ID="btnCloseProfileVisibilityPopup_bottom" runat="server" Text="Close" OnClick="btnCloseProfileVisibilityPopup_top_Click" CssClass="cta-btn-sm" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div id="divTrabau_Profile_Availability" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Profile Availability</h4>
                            <asp:Button ID="btnCloseProfile_Availability_popup_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseProfile_Availability_popup_top_Click" />
                        </div>

                        <!-- Modal body -->
                        <asp:UpdatePanel ID="upProfileAvailability" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label for="rbtnProfileAvailability">
                                                    Your availability
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="rbtnProfileAvailability" SetFocusOnError="true"
                                                        ErrorMessage="Select Profile Availability" ValidationGroup="UpdateProfileAvailability" Display="Dynamic" /></label>
                                                <asp:RadioButtonList ID="rbtnProfileAvailability" runat="server" CssClass="special-radio-btn" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" OnSelectedIndexChanged="rbtnProfileAvailability_SelectedIndexChanged">
                                                    <asp:ListItem Text="<h6>Available</h6>" Value="Available"></asp:ListItem>
                                                    <asp:ListItem Text="<h6>Not Available</h6>" Value="Not Available"></asp:ListItem>
                                                    <asp:ListItem Text="<h6>On Vacation</h6>" Value="Vacation"></asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>
                                        <div class="col-sm-12" id="div_vacation_date" runat="server" visible="false">
                                            <div class="form-group">
                                                <label for="txtVacationTillDated">
                                                    When your vacation will be ended?
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtVacationTillDated" SetFocusOnError="true"
                                                        ErrorMessage="Select Vacation end date" ValidationGroup="UpdateProfileAvailability" Display="Dynamic" />
                                                </label>
                                                <asp:TextBox ID="txtVacationTillDated" runat="server" CssClass="form-control">
                                                </asp:TextBox>
                                            </div>
                                    </div>
                                </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnUpdateProfileAvailability" runat="server" Text="Update" CssClass="cta-btn-sm" OnClick="btnUpdateProfileAvailability_Click" ValidationGroup="UpdateProfileAvailability" />
                                    <asp:Button ID="btnCloseProfile_Availability_popup" runat="server" Text="Close" OnClick="btnCloseProfile_Availability_popup_top_Click" CssClass="cta-btn-sm" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <uc1:wucProfilePicture runat="server" ID="wucProfilePicture" />

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

