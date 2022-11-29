<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSearchJobRightSide.ascx.cs" Inherits="Jobs_searchjobs_UserControls_wucSearchJobRightSide" %>
<div class="nav-details">
    <div id="div_right_nav" runat="server" visible="false">
        <div class="right-nav-main">
            <h4>Visibility<asp:LinkButton ID="lbtnEditProfileVisibility" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>"
                CssClass="edit-pencil-button edit-profile-visibility" Visible="false"></asp:LinkButton></h4>
            <div class="right-nav-content">
                <strong><i id="profile_visibility_icon_class" runat="server" class="fa fa-id-badge" aria-hidden="true"></i></strong>
                <span>
                    <asp:Literal ID="ltrlProfileVisibility" runat="server"></asp:Literal>
                    <asp:Literal ID="lblProfileVisibility" runat="server" Visible="false"></asp:Literal>
                </span>

            </div>
        </div>
        <div class="right-nav-main">
            <h4>Availability<asp:LinkButton ID="lbtnEditAvailability" runat="server" Text="<i class='fa fa-pencil' aria-hidden='true'></i>"
                CssClass="edit-pencil-button edit-profile-availability" Visible="false"></asp:LinkButton></h4>
            <div class="right-nav-content">
                <asp:Literal ID="ltrlProfileAvailability" runat="server" Visible="false"></asp:Literal>
                <asp:Literal ID="ltrlVacationDate" runat="server" Visible="false"></asp:Literal>
                <strong><i class="fa fa-clock-o" aria-hidden="true"></i></strong>
                <asp:Label ID="lblProfileAvailability" runat="server"></asp:Label>

            </div>
        </div>
        <div class="right-nav-main">
            <a id="aProfile" runat="server"><i class="fa fa-street-view" aria-hidden="true"></i>View Profile</a>
        </div>
        <div class="right-nav-main">
            <h4>Proposals </h4>
            <a id="aProposals" runat="server"></a>
        </div>
    </div>
</div>
