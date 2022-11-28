<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucsearch_profiles.ascx.cs" Inherits="profile_usercontrols_wucsearch_profiles" %>
<div class="freelancers-result-data">
    <asp:Repeater ID="rFreelancer_Profiles" runat="server">
        <ItemTemplate>
            <div class="col-12 profile-row">
                <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' Visible="false"></asp:Label>
                <div class="profile-actions">
                    <a class="btn-view-search-profile" href='<%#"user/userprofile.aspx?profile="+Eval("profile_id").ToString() %>' data-toggle="popover" tabindex="0" data-trigger="focus"
                        data-content="View Profile">View Profile</a>
                    <a class='<%#"btn-preferlist btn-prefer-list"+Eval("Preferred_ListClass").ToString() %>' id="aAddToPreferList" runat="server" data='<%#Eval("profile_id") %>' data-toggle="popover"
                        tabindex="0" data-trigger="focus" data-content='<%#Eval("Preferred_ListTooltip") %>' visible='<%#Eval("CanAdd_PreferList") %>'><%#Eval("Preferred_List") %></a>
                </div>
                <div class="profile-inner-row">
                    <div class="row">
                        <div class="col-sm-2">
                            <div class="profilefoto" id="div_profile_photo" runat="server">
                                <img alt="user" runat="server" id="imgFL_ProfilePic" src="" />
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="profile-inner-l">
                                <div class="ellipsis"><b><%#Eval("Name") %></b></div>
                                <div class="ellipsis"><b><%#Eval("Title") %></b></div>
                                <div class="ellipsis"><%#Eval("CountryName") %></div>
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
                    </div>
                    <div class="col-sm-12 profile-overview">
                        <%#Eval("Overview") %>
                    </div>
                    <div class="col-sm-12 profile-skills">
                        <ul class="tags">
                            <asp:Repeater ID="rFreelancers_Skills" runat="server">
                                <ItemTemplate>
                                    <li><%#Eval("SkillName") %></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="no-result-found" id="div_no_result" runat="server" visible="false">
        <div><i class="flaticon-not-found" aria-hidden="true"></i></div>
        <h3>Sorry we couldn't find any result matching your search</h3>
        <p>You needs to your change  your search keywords or filters. </p>
    </div>
    <div class="no-more-profiles-found text-center" id="div_more_profiles" runat="server" visible="false">No more profiles</div>
    <asp:Label ID="lblPageNumber" runat="server" Visible="false" />
    <asp:Label ID="lblSearchText" runat="server" Visible="false" />
    <asp:Label ID="lblCategory" runat="server" Visible="false" />
    <asp:Label ID="lblHourlyRate" runat="server" Visible="false" />
    <asp:Label ID="lblJobSuccess" runat="server" Visible="false" />
    <asp:Label ID="lblEarnedAmount" runat="server" Visible="false" />
    <asp:Label ID="lblLanguage" runat="server" Visible="false" />
    <asp:Label ID="lblProfileType" runat="server" Visible="false" />
</div>
