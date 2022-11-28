<%@ Page Title="Profile Update - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="profile-updation.aspx.cs" Inherits="Signup_profile_updation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/keywords.css") %>' rel="stylesheet" type="text/css" />
    <link href="https://cdn.jsdelivr.net/npm/select2@4.0.12/dist/css/select2.min.css" rel="stylesheet" />
    <style>
        .close {
            background: none;
            border: none;
        }

        label[for*="chkServiceType"] {
            margin-left: 10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/plugins/select2/css/select2.min.css") %>' rel="stylesheet" type="text/css" />
    <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select2.min.js") %>'></script>

    <%--*********************cropping***********************************************--%>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/camera/js/cropper.js") %>'></script>
    <link href='<%= Page.ResolveUrl("~/assets/plugins/camera/css/cropper.css") %>' rel="stylesheet" type="text/css" />
    <%--*********************cropping***********************************************--%>
     <script src='<%= Page.ResolveUrl("~/assets/js/profile-updation_1.js") %>' type="text/javascript"></script>
    <div class="Profile-Updation">
        <asp:UpdatePanel ID="upSignUp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select-custom.js") %>'></script>
                <script src='<%= Page.ResolveUrl("~/assets/js/profile-updation_2.js") %>' type="text/javascript"></script>

                <div id="div_profile_pic_popup" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">

                            <!-- Modal Header -->
                            <div class="modal-header">
                                <h4 class="modal-title" id="H1" runat="server">
                                    <asp:Literal ID="ltrlProfilePic_Header" runat="server"></asp:Literal></h4>
                                <asp:Button ID="btnCloseProfilePic_popup" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseProfilePic_popup_Click" />
                            </div>

                            <!-- Modal body -->
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-body" id="div_profile_pic_content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group text-center" id="pic_browse">
                                                    <asp:Image ID="imgProfilePic_Upload" runat="server" ImageUrl="~/assets/images/default-avatar.png" Width="300px" />
                                                </div>
                                                <div class="form-group text-center" id="pic_crop">
                                                    <asp:Image ID="img_profile_pic_crop" runat="server" ClientIDMode="Static" Width="100%" />
                                                    <asp:HiddenField ID="hfCropped_Picture" runat="server" ClientIDMode="Static" />
                                                    <input type="button" id="btndestroy_crop" style="display: none" />
                                                </div>
                                                <div class="form-group text-center">
                                                    <asp:LinkButton ID="lbtnUploadProfilePicture" runat="server" Text="<i class='fa fa-plus-circle' aria-hidden='true'></i> Add New Photo"
                                                        OnClick="lbtnUploadProfilePicture_Click" CssClass="profile-picture-add-btn" Style="display: block;" />
                                                    <asp:FileUpload ID="fu_profilepic_upload" runat="server" ClientIDMode="Static" Style="display: none" onchange="ValidateFileUploadExtension()" />
                                                </div>

                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnSaveProfilePicture" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveProfilePicture_Click" />
                                        <asp:Button ID="btnClose_Profile_Pic_Popup" runat="server" Text="Close" OnClick="btnCloseProfilePic_popup_Click" CssClass="cta-btn-sm" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div id="divTrabau_AddEducation" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">

                            <!-- Modal Header -->
                            <div class="modal-header">
                                <h4 class="modal-title" id="popHeader" runat="server">
                                    <asp:Literal ID="lblAddEducation_Header" runat="server"></asp:Literal></h4>
                                <asp:Button ID="btnCloseEducation_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseEducation_Click" />
                            </div>

                            <!-- Modal body -->
                            <asp:UpdatePanel ID="UpnlAddEducation" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-body" id="div_education_content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="txtSchoolName">School</label>
                                                    <asp:TextBox ID="txtSchoolName" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtSchoolName"
                                                        FilterMode="ValidChars" ValidChars="qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM ." />
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtSchoolName" SetFocusOnError="true" ErrorMessage="Enter School Name" ValidationGroup="SaveEducation" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="ddlEducationYearFrom">Education Attended</label>
                                                    <asp:DropDownList ID="ddlEducationYearFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="ddlEducationYearTo">&nbsp;</label>
                                                    <asp:DropDownList ID="ddlEducationYearTo" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="txtEducationDegree">Degree</label>
                                                    <asp:TextBox ID="txtEducationDegree" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtEducationDegree"
                                                        FilterMode="ValidChars" ValidChars="qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM ." />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="txtEducationAreaOfStudy">Area of Study</label>
                                                    <asp:TextBox ID="txtEducationAreaOfStudy" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtEducationAreaOfStudy"
                                                        FilterMode="ValidChars" ValidChars="qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM ." />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="txtEducationDescription">Description</label>
                                                    <asp:TextBox ID="txtEducationDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="70px"
                                                        Style="resize: none;" autocomplete="Off"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtEducationDescription"
                                                        FilterMode="ValidChars" ValidChars="qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM ." />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnSaveEducation" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveEducation_Click" CommandName="save" ValidationGroup="SaveEducation" />
                                        <asp:Button ID="btnSaveEducationAndClose" runat="server" Text="Save and Close" CssClass="cta-btn-sm" OnClick="btnSaveEducation_Click" CommandName="save_close" ValidationGroup="SaveEducation" />
                                        <asp:Button ID="btnCloseEducation" runat="server" Text="Close" OnClick="btnCloseEducation_Click" CssClass="cta-btn-sm" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div id="divTrabau_AddEmployment" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">

                            <!-- Modal Header -->
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <asp:Literal ID="ltrlEmployment_Header" runat="server"></asp:Literal></h4>
                                <asp:Button ID="btnClose_employment_top" runat="server" Text="&times;" OnClick="btnCloseEmployment_Click" CssClass="close" />
                            </div>

                            <!-- Modal body -->
                            <asp:UpdatePanel ID="UpEmployment" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-body" id="div_employment_content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="txtCompanyName">Company</label>
                                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtCompanyName" SetFocusOnError="true" ErrorMessage="Enter Company Name" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="txtCityName">City Name</label>
                                                    <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtCityName" SetFocusOnError="true" ErrorMessage="Enter City Name" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="ddlCountry">Country</label>
                                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" autocomplete="Off"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlCountry" SetFocusOnError="true" ErrorMessage="Select Country" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="txtEmploymentTitle">Title</label>
                                                    <asp:TextBox ID="txtEmploymentTitle" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtEmploymentTitle" SetFocusOnError="true" ErrorMessage="Enter Title" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="ddlRole">Role</label>
                                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlRole" SetFocusOnError="true" ErrorMessage="Select Role" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="ddlPeriodFrom_Month">Period From</label>
                                                    <asp:DropDownList ID="ddlPeriodFrom_Month" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodFrom_Month" SetFocusOnError="true" ErrorMessage="Select Period From Month" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="ddlPeriodFrom_Year">&nbsp;</label>
                                                    <asp:DropDownList ID="ddlPeriodFrom_Year" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodFrom_Year" SetFocusOnError="true" ErrorMessage="Select Period From Year" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6" id="div_Period_To_Month" runat="server">
                                                <div class="form-group">
                                                    <label for="ddlPeriodTo_Month">Period To</label>
                                                    <asp:DropDownList ID="ddlPeriodTo_Month" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodTo_Month" SetFocusOnError="true" ErrorMessage="Select Period To Month" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6" id="div_Period_To_Year" runat="server">
                                                <div class="form-group">
                                                    <label for="ddlPeriodTo_Year">&nbsp;</label>
                                                    <asp:DropDownList ID="ddlPeriodTo_Year" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodTo_Year" SetFocusOnError="true" ErrorMessage="Select Period To Year" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chkWorkingStatus" runat="server" Text="I currently working here" AutoPostBack="true" OnCheckedChanged="chkWorkingStatus_CheckedChanged" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="txtEmploymentDescription">Description (Optional)</label>
                                                    <asp:TextBox ID="txtEmploymentDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="70px"
                                                        Style="resize: none;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnSaveEmployment" runat="server" Text="Save" OnClick="btnSaveEmployment_Click" CssClass="cta-btn-sm" CommandName="save" ValidationGroup="SaveEmployment" />
                                        <asp:Button ID="btnSaveCloseEmployment" runat="server" Text="Save and Close" CssClass="cta-btn-sm" OnClick="btnSaveEmployment_Click" CommandName="save_close" ValidationGroup="SaveEmployment" />
                                        <asp:Button ID="btnCloseEmployment" runat="server" Text="Close" OnClick="btnCloseEmployment_Click" CssClass="cta-btn-sm" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row" id="div_signup_step1" runat="server" visible="false">
                    <div class="col-sm-12 text-center">
                        <h2>Update your Profile</h2>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group">
                            <label>Select Services</label>
                            <asp:DropDownList ID="ddlServices" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlServices_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlServices" SetFocusOnError="true" ErrorMessage="Select Services" InitialValue="0" ValidationGroup="Profile_Update" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="col-sm-12" id="div_service_type" runat="server" visible="false">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label>Select upto 4 types</label>
                                <div style="height: 300px; overflow-y: scroll;">
                                    <asp:Repeater ID="rServiceType" runat="server">
                                        <ItemTemplate>
                                            <div>
                                                <asp:CheckBox ID="chkServiceType" runat="server" Text='<%#Eval("Text") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>What skills do you have ?</label>
                                <div class="skills">
                                    <asp:DropDownList ID="ddlSkills" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlSkills" SetFocusOnError="true" ErrorMessage="Select your skills" InitialValue="0" ValidationGroup="Profile_Update" Display="Dynamic" />
                                    <%--  <asp:TextBox ID="txtSkills" runat="server" CssClass="form-control" placeholder="Enter your skills"></asp:TextBox>--%>
                                    <asp:HiddenField ID="hfSkills" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label>What is your experience level ?</label>
                                <div class="skills">
                                    <asp:DropDownList ID="ddlExperienceLevel" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlExperienceLevel" SetFocusOnError="true" ErrorMessage="Select Experience Level" InitialValue="0" ValidationGroup="Profile_Update" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="cta-btn-sm" OnClick="btnContinue_Click" ValidationGroup="Profile_Update" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="div_signup_step2" runat="server" visible="false">
                    <h2 class="m-heading">Tell us more about you</h2>
                    <div class="ab-profile">
                        <div class="form-group">
                            <label>Please upload a professional portrait that clearly shows your face</label>
                            <asp:Image ID="imgProfilePic" runat="server" ImageUrl="~/assets/images/default-avatar.png" class="profile-picture" />
                            <asp:LinkButton ID="lbtnAddProfilePic" runat="server" Text="<i class='fa fa-plus-circle' aria-hidden='true'></i> Add New Photo" OnClick="lbtnAddProfilePic_Click" CssClass="profile-picture-add-btn" />
                        </div>
                        <div class="form-group">
                            <label>Add Professional Title</label>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter Professional Title" autocomplete="Off"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtTitle" SetFocusOnError="true" ErrorMessage="Enter Professional Title" ValidationGroup="Profile_Update2" Display="Dynamic" />
                        </div>
                        <div class="form-group">
                            <label>Write Professional Overview</label>
                            <asp:TextBox ID="txtOverview" runat="server" CssClass="form-control" TextMode="MultiLine" Height="300px"
                                placeholder="Use this space to show clients you have the skills and experience they're looking for" Style="resize: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtOverview" SetFocusOnError="true" ErrorMessage="Enter Professional Overview" ValidationGroup="Profile_Update2" Display="Dynamic" />
                        </div>
                        <div class="form-group">
                            <div>
                                <label>Education</label>
                                <asp:LinkButton ID="btnAddEducation" runat="server" Text="<i class='fa fa-plus-circle' aria-hidden='true'></i>" OnClick="btnAddEducation_Click" />
                            </div>

                            <asp:Repeater ID="rEducation" runat="server">

                                <ItemTemplate>
                                    <div class="row" style="padding: 10px;">
                                        <asp:Label ID="lblEducationId" runat="server" Visible="false" Text='<%#Eval("EducationId") %>'></asp:Label>
                                        <div class="col-sm-10" style="display: inline-block;">
                                            <%#Eval("FullDetails") %>
                                            <br />
                                            <%#Eval("YearDetails") %>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lbtnEditEducation" runat="server" ClientIDMode="AutoID" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditEducation_Click"
                                                CssClass="edit-pencil-button"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <hr />
                                </ItemTemplate>

                            </asp:Repeater>
                        </div>
                        <div class="form-group">
                            <div>
                                <label>Employment History</label>
                                <asp:LinkButton ID="lbtnAddEmploymentHistory" runat="server" Text="<i class='fa fa-plus-circle' aria-hidden='true'></i>" OnClick="lbtnAddEmploymentHistory_Click" />
                            </div>

                            <asp:Repeater ID="rEmployment" runat="server">

                                <ItemTemplate>
                                    <div class="row" style="padding: 10px;">
                                        <asp:Label ID="lblEmploymentId" runat="server" Visible="false" Text='<%#Eval("EmploymentId") %>'></asp:Label>
                                        <div class="col-sm-10" style="display: inline-block;">
                                            <%#Eval("FullDetails") %>
                                            <br />
                                            <%#Eval("YearDetails") %>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="lbtnEditEmployment" runat="server" ClientIDMode="AutoID" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditEmployment_Click"
                                                CssClass="edit-pencil-button"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <hr />
                                </ItemTemplate>

                            </asp:Repeater>
                        </div>

                        <div class="form-group">
                            <label>What is your English proficiency?</label>
                            <asp:DropDownList ID="ddlEnglishProficiency" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select your proficiency" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Basic" Value="Basic"></asp:ListItem>
                                <asp:ListItem Text="Conversational" Value="Conversational"></asp:ListItem>
                                <asp:ListItem Text="Fluent" Value="Fluent"></asp:ListItem>
                                <asp:ListItem Text="Native or Bilingual" Value="Native or Bilingual"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlEnglishProficiency" SetFocusOnError="true" ErrorMessage="Select your English proficiency" InitialValue="0" ValidationGroup="Profile_Update2" Display="Dynamic" />
                        </div>
                        <div class="form-group">
                            <label>Address</label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" Height="70px" Style="resize: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtAddress" SetFocusOnError="true" ErrorMessage="Enter Address" ValidationGroup="Profile_Update2" Display="Dynamic" />
                        </div>
                        <div class="form-group">
                            <label>City</label>
                            <p class="autocomplete_loading_main">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="Enter City and select from the list" ClientIDMode="Static" autocomplete="Off"></asp:TextBox>
                                <asp:HiddenField ID="hfCityId" runat="server" ClientIDMode="Static" />
                            </p>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtCity" SetFocusOnError="true" ErrorMessage="Enter City" ValidationGroup="Profile_Update2" Display="Dynamic" />
                        </div>
                        <div class="form-group">
                            <label>Postal Code</label>
                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" placeholder="Enter Postal Code" autocomplete="Off"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtPostalCode" SetFocusOnError="true" ErrorMessage="Enter Postal Code" ValidationGroup="Profile_Update2" Display="Dynamic" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="cta-btn-sm" OnClick="btnUpdateProfile_Click" ValidationGroup="Profile_Update2" />
                            <asp:Button ID="btnGoToSettings" runat="server" Text="Go to Settings" OnClick="btnGoToSettings_Click" CssClass="cta-btn-sm" Visible="false" />
                        </div>
                    </div>


                </div>

                <script src='<%= Page.ResolveUrl("~/assets/js/keywords_autocomplete_1.0.js") %>'></script>
                <script src='<%= Page.ResolveUrl("~/assets/plugins/AutoComplete/js/AutoCompleteText.js?version=1.0") %>' type="text/javascript"></script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

