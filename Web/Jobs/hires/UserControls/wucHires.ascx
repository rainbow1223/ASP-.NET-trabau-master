<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucHires.ascx.cs" Inherits="Jobs_hires_UserControls_wucHires" %>
<div class="hire-result-data">
    <asp:Repeater ID="rHires" runat="server">
        <ItemTemplate>
            <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
            <div class="hire-result-main profile-row">
                <div class="profile-actions hire-actions">
                    <a class="btn-view-search-profile" href='<%#"../../profile/user/userprofile.aspx?profile="+Eval("profile_id").ToString() %>' data-toggle="popover" tabindex="0" data-trigger="focus"
                        data-content="View Profile">View Profile</a>
                    <a class='<%#"btn-preferlist btn-prefer-list"+Eval("Preferred_ListClass").ToString() %>' id="aAddToPreferList" runat="server" data='<%#Eval("profile_id") %>' data-toggle="popover"
                        tabindex="0" data-trigger="focus" data-content="Add freelancer to your preferred list"><%#Eval("Preferred_List") %></a>
                </div>
                <div class="hire-result">
                    <p class="hire-result-top">
                        <b><%#Eval("JobTitle") %></b>
                    </p>
                    <div class="row">
                        <div class="col-4 col-lg-3">
                            <div class="profilefoto" id="div_profile_photo" runat="server">
                                <img alt="user" runat="server" id="imgFL_ProfilePic" src="" />
                            </div>
                        </div>
                        <div class="col-8 col-lg-9">
                            <div class="profile-inner-l">
                                <div class="ellipsis"><b><%#Eval("Name") %></b></div>
                                <div class="ellipsis"><%#Eval("Title") %></div>
                                <div class="ellipsis"><%#Eval("LocalTime") %></div>
                            </div>
                            <div class="col-sm-12 proposal-terms">
                                <div class="col-xs-6 col-md-3">
                                    <b>$<%#Eval("HourlyRate") %> <span>/ hr</span></b>
                                </div>
                                <div class="col-xs-6 col-md-3">
                                    <b>$<%#Eval("TotalEarning") %> <span>earned</span></b>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            No active function
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
