<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="profile_settings_index" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/profile/usercontrols/wucProfilePicture.ascx" TagPrefix="uc1" TagName="wucProfilePicture" %>
<%@ Register Src="~/profile/usercontrols/wucCompanyLogo.ascx" TagPrefix="uc1" TagName="wucCompanyLogo" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/settings_style.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/select2/css/select2.min.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select2.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/settings.js?version=1.0") %>'></script>
    <!-- put content here -->
    <div class="row">
        <div class="col-md-3 d-none d-md-block leftSideBar">
            <asp:Repeater ID="rNavigation_main" runat="server">
                <ItemTemplate>
                    <h4 class="m-sm-bottom"><%#Eval("NavigationName") %></h4>
                    <asp:Label ID="lblNavigationId_main" runat="server" Visible="false" Text='<%#Eval("NavigationId") %>'></asp:Label>
                    <ul>
                        <asp:Repeater ID="rNavigation" runat="server">
                            <ItemTemplate>
                                <li class='<%#Eval("NavigationClass") %>' id="li_nav" runat="server">
                                    <asp:Label ID="lblNavigationId" runat="server" Visible="false" Text='<%#Eval("NavigationId") %>'></asp:Label>
                                    <asp:Label ID="lblNavigation_Action" runat="server" Visible="false" Text='<%#Eval("Action") %>'></asp:Label>
                                    <asp:LinkButton ID="lbtnNavigation" runat="server" Text='<%#Eval("NavigationName") %>' OnClick="lbtnNavigation_Click"></asp:LinkButton>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <div class="col-md-9 data-container">
            <div class="d-block d-md-none">
                <div class="mobile-link-nav">
                    <h1>Settings</h1>
                    <asp:DropDownList ID="ddlNavigation" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNavigation_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="ui-application">
                <h1 id="h1_Header" runat="server">Contact Info</h1>
                <div id="div_contact_info" runat="server" visible="false">
                    <div class="air-card">
                        <asp:UpdatePanel ID="upContactDetails" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <script src='<%= Page.ResolveUrl("~/assets/js/settings_inner.js?version=1.0") %>'></script>
                                <div class="airCardHeader d-flex align-items-center">
                                    <h2>Account</h2>
                                    <div class="ml-auto editCard-btn">
                                        <asp:LinkButton ID="lbtnEdit_ContactInfo" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEdit_ContactInfo_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="airCardBody">
                                    <div class="row account-picture" id="div_account_picture" runat="server" visible="false">
                                        <div class="col-md-12 text-center">
                                            <asp:ImageButton ID="imgProfilePic" runat="server" CssClass="avatar avatar-editable" OnClick="imgProfilePic_Click" />
                                            <uc1:wucProfilePicture runat="server" ID="wucProfilePicture1" />
                                        </div>
                                    </div>
                                    <div class="row account-name" id="div_userid" runat="server" visible="false">
                                        <div class="col-md-3">
                                            <label>User ID</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:Literal ID="ltrlUserId" runat="server"></asp:Literal>
                                                <asp:Literal ID="ltrlUserType" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtUserId" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvUserId" runat="server" ControlToValidate="txtUserId"
                                                    ErrorMessage="Enter UserId" ValidationGroup="AccountUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-md-3">
                                            <label>First Name</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:Literal ID="ltrlFName" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtFName" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtFName"
                                                    ErrorMessage="Enter First Name" ValidationGroup="AccountUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </p>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Last Name</label>
                                        </div>
                                        <div class="col-md-12">
                                            <p>
                                                <asp:Literal ID="ltrlLName" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtLName" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Email Address</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:Literal ID="ltrlEmailId" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtEmailAddress" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                                    ErrorMessage="Enter Email Address" ValidationGroup="AccountUpdate" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" CssClass="text-danger custom-validator" ControlToValidate="txtEmailAddress"
                                                    SetFocusOnError="true" ErrorMessage="Enter valid email address" ValidationGroup="AccountUpdate" Display="Dynamic"
                                                    ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$">
                                                </asp:RegularExpressionValidator>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="div_save_contact_details" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnSubmitContactDetails" runat="server" Text="Update" ValidationGroup="AccountUpdate" OnClick="btnSubmitContactDetails_Click" CssClass="cta-btn-sm" />
                                            <asp:LinkButton ID="lbtnCancelContactDetails" runat="server" Text="Cancel" OnClick="lbtnCancelContactDetails_Click" CssClass="cta-btn-sm-cancel"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="air-card" id="div_company_details" runat="server" visible="false">
                        <asp:UpdatePanel ID="upCompanyDetails" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <script src='<%= Page.ResolveUrl("~/assets/js/settings_company.js?version=1.0") %>'></script>
                                <div class="airCardHeader d-flex align-items-center">
                                    <h2>Company Details</h2>
                                    <div class="ml-auto editCard-btn">
                                        <asp:LinkButton ID="lbtnEditCompanyDetails" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditCompanyDetails_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="airCardBody company-details">
                                    <div class="row company-logo" id="company_logo" runat="server">
                                        <div class="col-md-12 text-center">
                                            <asp:ImageButton ID="imgCompanyLogo" runat="server" CssClass="avatar avatar-editable" OnClick="imgCompanyLogo_Click" />
                                            <uc1:wucCompanyLogo runat="server" ID="wucCompanyLogo1" />
                                        </div>

                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-md-3">
                                            <label>Company Name</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:Literal ID="ltrlCompanyName" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Enter Company Name" Visible="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyName"
                                                    ErrorMessage="Enter Company Name" ValidationGroup="CompanyUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Website</label>
                                        </div>
                                        <div class="col-md-12">
                                            <p>
                                                <asp:Literal ID="ltrlCompanyWebsite" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtCompanyWebsite" runat="server" CssClass="form-control" placeholder="Enter Company Website" Visible="false"></asp:TextBox>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Tagline</label>
                                        </div>
                                        <div class="col-md-12">
                                            <p>
                                                <asp:Literal ID="ltrlCompanyTagline" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtCompanyTagline" runat="server" CssClass="form-control" placeholder="Enter Company Tagline" Visible="false"></asp:TextBox>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Description</label>
                                        </div>
                                        <div class="col-md-12">
                                            <p>
                                                <asp:Literal ID="ltrlCompanyDescription" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtCompanyDescription" runat="server" CssClass="form-control" placeholder="Enter Company Description"
                                                    Height="70px" TextMode="MultiLine" Style="resize: none;" Visible="false"></asp:TextBox>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="div_update_company_details" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnUpdateCompanyDetails" runat="server" Text="Update" OnClick="btnUpdateCompanyDetails_Click" CssClass="cta-btn-sm" ValidationGroup="CompanyUpdate" />
                                            <asp:LinkButton ID="lbtnCancelCompanyDetails" runat="server" Text="Cancel" OnClick="lbtnCancelCompanyDetails_Click" CssClass="cta-btn-sm-cancel"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="air-card">
                        <asp:UpdatePanel ID="upLocation" runat="server">
                            <ContentTemplate>
                                <script src='<%= Page.ResolveUrl("~/assets/plugins/AutoComplete/js/AutoCompleteText.js") %>' type="text/javascript"></script>
                                <div class="airCardHeader d-flex align-items-center">
                                    <h2 id="h2_location_header" runat="server"></h2>
                                    <div class="ml-auto editCard-btn">
                                        <asp:LinkButton ID="lbtnEditLocation" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditLocation_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="airCardBody">
                                    <div class="row" id="divVATID" runat="server" visible="false">
                                        <div class="col-md-3">
                                            <label>VAT ID</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:Literal ID="ltrlVATID" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtVATID" runat="server" CssClass="form-control" Visible="false" placeholder="Enter your VAT ID"></asp:TextBox>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Time Zone</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:Literal ID="ltrlTimeZone" runat="server"></asp:Literal>
                                                <asp:DropDownList ID="ddlTimeZone" runat="server" Visible="false" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTimeZone" runat="server" ControlToValidate="ddlTimeZone"
                                                    ErrorMessage="Select Time Zone" ValidationGroup="LocationUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="div_Country" runat="server" visible="false">
                                        <div class="col-md-3">
                                            <label>Country</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                    ErrorMessage="Select Country" ValidationGroup="LocationUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="div_address" runat="server" visible="false">
                                        <div class="col-md-3">
                                            <label>Address</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Height="70px" Style="resize: none" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                                    ErrorMessage="Enter Address" ValidationGroup="LocationUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="divCity" runat="server" visible="false">
                                        <div class="col-md-6">
                                            <div class="row">
                                                <div class="col-md-12 required">
                                                    <label>City</label>
                                                    <p class="autocomplete_loading_main">
                                                        <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                                        <asp:HiddenField ID="hfCityId" runat="server" ClientIDMode="Static" />
                                                    </p>
                                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCityName"
                                                        ErrorMessage="Enter City" ValidationGroup="LocationUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="row">
                                                <div class="col-md-12 required">
                                                    <label>Postal Code</label>
                                                    <p>
                                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPostalCode" runat="server" ControlToValidate="txtPostalCode"
                                                            ErrorMessage="Enter Postal Code" ValidationGroup="LocationUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row google-maps-link">
                                                <div class="col-md-12">
                                                    <label>Google Maps Link</label>
                                                    <p>
                                                        <asp:CheckBox ID="chkGoogleMapsLink" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="chkGoogleMapsLink_CheckedChanged" />
                                                        <span>
                                                            <asp:Literal ID="ltrlGoogleMapsLink" runat="server"></asp:Literal></span>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="div_address_view" runat="server">
                                        <div class="col-md-3">
                                            <label>Address</label>
                                        </div>
                                        <div class="col-md-12">
                                            <p>
                                                <asp:Literal ID="ltrlAddress" runat="server"></asp:Literal><br />
                                                <asp:Literal ID="ltrlCityName" runat="server"></asp:Literal>,
                                                        <asp:Literal ID="ltrlState" runat="server"></asp:Literal>,
                                                        <asp:Literal ID="ltrlPostalCode" runat="server"></asp:Literal>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="div_Phone_View" runat="server">
                                        <div class="col-md-3">
                                            <label>Phone No</label>
                                        </div>
                                        <div class="col-md-12">
                                            <p>
                                                <asp:Literal ID="ltrlPhoneNo" runat="server"></asp:Literal>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="div_Phone" runat="server">
                                        <div class="col-md-12">
                                            <label>Phone No</label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="row">
                                                <div class="col-md-12 required">
                                                    <p>
                                                        <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCountryCode" runat="server" ControlToValidate="ddlCountryCode"
                                                            ErrorMessage="Select Country Code" ValidationGroup="LocationUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-9">
                                            <div class="row">
                                                <div class="col-md-12 required">
                                                    <p>
                                                        <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftePhoneNo" runat="server" TargetControlID="txtPhoneNo" FilterMode="ValidChars"
                                                            ValidChars="0123456789" />
                                                        <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" ControlToValidate="txtPhoneNo"
                                                            ErrorMessage="Enter Phone No" ValidationGroup="LocationUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="div_location_details" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnUpdateLocationDetails" runat="server" Text="Update" OnClick="btnUpdateLocationDetails_Click" CssClass="cta-btn-sm" ValidationGroup="LocationUpdate" />
                                            <asp:LinkButton ID="btnCancelLocationDetails" runat="server" Text="Cancel" OnClick="btnCancelLocationDetails_Click" CssClass="cta-btn-sm-cancel"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="air-card" id="div_profiletype" runat="server" visible="false">
                        <asp:UpdatePanel ID="upProfileType" runat="server">
                            <ContentTemplate>
                                <div class="airCardHeader d-flex align-items-center">
                                    <h2>Profile Type</h2>
                                    <div class="ml-auto editCard-btn">
                                        <asp:LinkButton ID="lbtnEditProfileType" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditProfileType_Click"
                                            CssClass="edit-pencil-button"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="airCardBody">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <label>Profile Type</label>
                                        </div>
                                        <div class="col-md-12 required">
                                            <p>
                                                <asp:Literal ID="ltrlProfileType" runat="server"></asp:Literal>
                                                <asp:DropDownList ID="ddlProfileType" runat="server" CssClass="form-control" Visible="false">
                                                    <asp:ListItem Text="Freelancer" Value="Freelancer"></asp:ListItem>
                                                    <asp:ListItem Text="Contractor" Value="Contractor"></asp:ListItem>
                                                    <asp:ListItem Text="Agency" Value="Agency"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvProfileType" runat="server" ControlToValidate="ddlProfileType"
                                                    ErrorMessage="Select Profile Type" ValidationGroup="ProfileTypeUpdate" CssClass="text-danger"></asp:RequiredFieldValidator>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row" id="div_update_profiletype" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnUpdateProfileType" runat="server" Text="Update" OnClick="btnUpdateProfileType_Click" CssClass="cta-btn-sm" ValidationGroup="ProfileTypeUpdate" />
                                            <asp:LinkButton ID="lbtnCancelProfileType" runat="server" Text="Cancel" OnClick="lbtnCancelProfileType_Click" CssClass="cta-btn-sm-cancel"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="air-card" id="divNewAccount" runat="server" visible="false">
                        <asp:UpdatePanel ID="upNewAccount" runat="server">
                            <ContentTemplate>
                                <div class="airCardBody new-account">
                                    <div class="row">
                                        <div class="col-md-12">
                                            This is <b>
                                                <asp:Literal ID="lblAccountType" runat="server"></asp:Literal></b> Account
                                            <asp:Label ID="lblDefaultAccountStatus" runat="server" Style="color: #0bbc56; font-weight: bold;"></asp:Label>
                                            <div style="padding-top: 10px;">
                                                <asp:Button ID="btnSetAsDefaultAccount" runat="server" Text="Set as Default Account" OnClick="btnSetAsDefaultAccount_Click" CssClass="cta-btn-sm" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row btn-new-account">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnNewAccount" runat="server" Text="Create New Account" OnClick="btnNewAccount_Click" CssClass="cta-btn-sm" />
                                        </div>
                                    </div>
                                </div>


                                <div id="div_newaccount_popup" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">
                                    <div class="modal-dialog modal-lg" role="document">
                                        <div class="modal-content">
                                            <!-- Modal Header -->
                                            <div class="modal-header new-user-account-header">
                                                <h4 class="modal-title">Create New Account</h4>
                                                <asp:Button ID="btnCloseNewAccount_popup" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseNewAccount_popup_Click" />
                                            </div>

                                            <!-- Modal body -->

                                            <div class="modal-body new-user-account">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>Creating a new account allows you to use Trabau in different ways, while still having just one login</label>
                                                    </div>
                                                </div>
                                                <div class="row account-options" id="divNewClientAccount" runat="server" visible="false">
                                                    <div class="col-sm-12">
                                                        <label>Hire, manage and pay as a different company. Each client company has its own freelancers, payment methods and reports</label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:Button ID="lbtnNewClientAccount" runat="server" Text="+ New Client Account" OnClick="lbtnNewClientAccount_Click" CssClass="btn-create-new-account"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="row account-options" id="divNewFreelancerAccount" runat="server" visible="false">
                                                    <div class="col-sm-12">
                                                        <label>Find jobs and earn money as a freelancer</label>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <asp:Button ID="lbtnNewFreelancerAccount" runat="server" Text="+ New Freelancer Account" OnClick="lbtnNewFreelancerAccount_Click" CssClass="btn-create-new-account"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div id="div_Docs_Upload" runat="server" visible="false">
                    <div class="air-card">
                        <asp:UpdatePanel ID="upDocUpload" runat="server">
                            <ContentTemplate>
                                <script src='<%= Page.ResolveUrl("~/assets/js/settings_doc_upload.js?version=1.0") %>'></script>
                                <div class="airCardHeader d-flex align-items-center">
                                    <h2>Upload Documents</h2>
                                </div>
                                <div class="col-sm-12 text-center">
                                    <div class="row">
                                        <div class="col-sm-12 alert alert-info">
                                            extensions e.g.
                                    <asp:Literal ID="ltrlValidTypes" runat="server" Text="*.doc, *.docx, *.pdf, *.jpg, *.png, *.gif, *.tif"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <div class="row document-upload-header">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Select Document Type</label>
                                                    <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDocumentType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" CssClass="file-type text-danger" ControlToValidate="ddlDocumentType" SetFocusOnError="true" ErrorMessage="Select Document Type"
                                                        ValidationGroup="SaveDocument" Display="Dynamic" InitialValue="0" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6 other-document" id="div_other_document" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Literal ID="ltrlOtherDocLabel" runat="server" Text="Enter Other Document Name"></asp:Literal></label>
                                                    <asp:TextBox ID="txtOtherDocumentType" runat="server" MaxLength="50" CssClass="form-control"
                                                        placeholder="Enter Other Document Name"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvOtherDocType" runat="server" CssClass="file-type text-danger" ControlToValidate="txtOtherDocumentType" SetFocusOnError="true" ErrorMessage="Enter Other Document Name"
                                                        ValidationGroup="SaveDocument" Display="Dynamic" Enabled="false" />
                                                </div>
                                                <p id="pYoutubeVideoExample" runat="server" visible="false">e.g. https://www.youtube.com/embed/********* or https://www.youtube.com/watch?v=*********</p>
                                            </div>
                                            <div class="col-sm-6 youtube-video" id="div_youtube_video" runat="server" visible="false">
                                                <div class="form-group">
                                                    <label>Enter Youtube Video Name</label>
                                                    <asp:TextBox ID="txtYouTubeVideoName" runat="server" CssClass="form-control" placeholder="Enter Youtube Video Name"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvYoutubeVideoName" runat="server" CssClass="file-type text-danger" ControlToValidate="txtYouTubeVideoName" SetFocusOnError="true" ErrorMessage="Enter Youtube Video Name"
                                                        ValidationGroup="SaveDocument" Display="Dynamic" Enabled="false" />
                                                </div>

                                            </div>
                                            <div class="col-sm-6 align-self-center" style="text-align: left;">
                                                <div class="form-group file-upload-main">
                                                    <input type="button" class="file-upload" value="Upload a file" id="btnUploadFile" runat="server" clientidmode="static" />
                                                    <asp:FileUpload ID="fu_profile_document" runat="server" class="file-upload-control" ClientIDMode="Static" onchange="GetFileInfo()" />
                                                    <asp:RequiredFieldValidator ID="rfvDocumentFile" runat="server" CssClass="file-req text-danger" ControlToValidate="fu_profile_document" SetFocusOnError="true" ErrorMessage="Choose file"
                                                        ValidationGroup="SaveDocument" Display="Dynamic" />
                                                    <span id="sfileinfo" class="file-info"></span>
                                                </div>
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="cta-btn-sm" ClientIDMode="Static" ValidationGroup="SaveDocument" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 document_viewer">
                                        <table class="table table-bordered text-left">
                                            <tr>
                                                <th>Document Name</th>
                                                <th>Document Type</th>
                                                <th>Size</th>
                                                <th>Visibility</th>
                                                <th></th>
                                            </tr>

                                            <asp:Repeater ID="rDocuments" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblDocumentExtension" runat="server" Text='<%#Eval("DocumentExtension") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblUploadedDocId" runat="server" Text='<%#Eval("UploadedDocId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblOtherDocType" runat="server" Text='<%#Eval("OtherDocType") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblDocumentId" runat="server" Text='<%#Eval("DocumentId") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblDocumentVisibility" runat="server" Text='<%#Eval("Visibility") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblDocumentName" runat="server" Text='<%#Eval("DocumentName") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%#Eval("DocumentType") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDocumentSize" runat="server" Text='<%#Eval("DocumentSize") %>'></asp:Label>
                                                        </td>
                                                        <%--                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                </td>--%>
                                                        <td>
                                                            <asp:RadioButtonList ID="rbtnlVisibility" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnlVisibility_SelectedIndexChanged">
                                                                <asp:ListItem Text="Public" Value="Public" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lbtnDownloadDocument" runat="server" Text="<i class='fa fa-download' aria-hidden='true'></i>" OnClick="lbtnDownloadDocument_Click" Visible='<%#!Convert.ToBoolean(Eval("IsYoutubeVideo").ToString()) %>'></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnViewYouTubeVideo" runat="server" Text="<i class='fa fa-youtube' aria-hidden='true'></i>" OnClick="lbtnViewYouTubeVideo_Click" Visible='<%#Eval("IsYoutubeVideo") %>'></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnViewDocument" runat="server" Text="View Document" OnClick="lbtnViewDocument_Click" Visible="false"></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnRemoveDocument" runat="server" Text="<i class='fa fa-trash-o' aria-hidden='true'></i>" OnClick="lbtnRemoveDocument_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <asp:HiddenField ID="hfError" runat="server" />
                                    </div>

                                    <div id="divTrabau_DocumentView" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

                                        <div class="modal-dialog modal-lg" role="document">
                                            <div class="modal-content">

                                                <!-- Modal Header -->
                                                <div class="modal-header">
                                                    <h4 class="modal-title">
                                                        <asp:Literal ID="ltrlEmployment_Header" runat="server"></asp:Literal></h4>
                                                    <asp:Button ID="btnClose_document_top" runat="server" Text="&times;" OnClick="btnClose_document_top_Click" CssClass="close" />
                                                </div>

                                                <!-- Modal body -->

                                                <div class="modal-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div id="div_doc_data" runat="server" class="doc_content"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <asp:Button ID="btnClose_document_bottom" runat="server" Text="Close" OnClick="btnClose_document_top_Click" CssClass="cta-btn-sm" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div id="div_newaccount" runat="server" visible="false">
                    <div class="air-card">
                        <asp:UpdatePanel ID="upNewAccountCreation" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="airCardHeader d-flex align-items-center">
                                    <h2 class="newaccount-header" id="h2_newacc_header" runat="server"></h2>
                                </div>
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-12 new-account-info" id="div_newacc_info" runat="server">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 create-newaccount">
                                            <asp:Button ID="btnCreateNewAccount" runat="server" Text="Become a Freelancer" OnClick="btnCreateNewAccount_Click"
                                                CssClass="cta-btn-sm" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

