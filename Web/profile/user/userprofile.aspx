<%@ Page Title="" Language="C#" MasterPageFile="~/index.master" AutoEventWireup="true" CodeFile="userprofile.aspx.cs" Inherits="profile_user_userprofile" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Src="~/profile/usercontrols/wucEmploymentHistory.ascx" TagPrefix="uc1" TagName="wucEmploymentHistory" %>
<%@ Register Src="~/profile/usercontrols/wucEducationHistory.ascx" TagPrefix="uc1" TagName="wucEducationHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/profile/usercontrols/wucProfilePicture.ascx" TagPrefix="uc1" TagName="wucProfilePicture" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/profile_style-1.1.css?version=1.1") %>' rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select-custom.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/AutoComplete/js/AutoCompleteText.js") %>' type="text/javascript"></script>
    <script>
        function ProfileLoad_Events() {
            $(document).ready(function () {

            });
        }
    </script>
    <div class="layout-page-content">
        <div class="container">

            <div id="div_profile_not_avail" runat="server" visible="false" class="text-center profile-not-avail"></div>
            <asp:UpdatePanel ID="upParent" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <script>
                        Sys.Application.add_load(ProfileLoad_Events);
                    </script>
                    <div class="row">
                        <div class="col-xs-12 col-lg-9 my-profile-left">
                            <div class="profile-card padding-20">
                                <div class="row profile-top">
                                    <div class="col-xs-12 col-sm-8 col-md-9">
                                        <div class="profile-content">
                                            <div class="my-profile-picture">
                                                <img class="profile-avatar avatar" id="img_profile_picture" runat="server" src="../assets/uploads/avtar.jpg" />
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

                                        </h3>
                                    </div>
                                    <div class="profile-details-bottom">
                                        <p>
                                            <asp:Literal ID="ltrl_profile_overview" runat="server"></asp:Literal>

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

                                </div>
                                <div class="airCardBody padding-20">
                                    <div class="card-body-content row">
                                        <asp:Repeater ID="rPortfolio" runat="server">
                                            <ItemTemplate>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblPortfolioId" runat="server" Visible="false" Text='<%#Eval("PortfolioId") %>'></asp:Label>
                                                    <div class="portfolio-items">
                                                        <img src='<%#Eval("GalleryItemPreview") %>' />
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

                            <div class="right-profile-main">
                                <h4>Availability</h4>
                                <div class="right-profile-content">
                                    <strong><i class="fa fa-clock-o" aria-hidden="true"></i></strong>
                                    <span>- As needed - open to offers</span>
                                    <br />
                                    <strong><i class="fa fa-clock-o" aria-hidden="true"></i></strong>
                                    <asp:Label ID="lblProfileAvailability" runat="server" Style="margin-top: 10px !important; display: inline-block;"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                    <uc1:wucProfilePicture runat="server" ID="wucProfilePicture" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

