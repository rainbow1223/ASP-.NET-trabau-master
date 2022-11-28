<%@ Page Title="Prefer List - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="profile_preferlist_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/search-profile.css?version=1.3") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("search.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/trabau-picture.js?version=1.0") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/preferlist.js?version=1.0") %>'></script>

    <div class="myCard-heading">
        <h4 id="h4_heading" runat="server">My Prefer List </h4>
    </div>
    <div class="proposals-content">
        <div class="proposals-result no-background ad-search">
            <div id="div_proposals_result">
                <asp:Repeater ID="rFreelancer_Profiles" runat="server">
                    <ItemTemplate>
                        <div class="col-12 profile-row">
                            <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' Visible="false"></asp:Label>
                            <div class="profile-actions">
                                <a class="btn-view-search-profile" href='<%#"user/userprofile.aspx?profile="+Eval("profile_id").ToString() %>' data-toggle="popover" tabindex="0" data-trigger="focus"
                                    data-content="View Profile" runat="server" visible='<%#Eval("CanView") %>'>View Profile</a>
                                <a class="btn-remove-prefer" data='<%#Eval("profile_id") %>' data-toggle="popover" tabindex="0" data-trigger="focus" data-content='<%#Eval("Preferred_ListTooltip") %>'>Remove</a>
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
                                            <div class="col-xs-6 col-md-3" runat="server" visible='<%#Eval("CanView") %>'>
                                                <b>$<%#Eval("HourlyRate") %> <span>/ hr</span></b>
                                            </div>
                                            <div class="col-xs-6 col-md-3" runat="server" visible='<%#Eval("CanView") %>'>
                                                <b>$<%#Eval("TotalEarning") %> <span>earned</span></b>
                                            </div>
                                            <div class="col-xs-12 col-md-6 website-url" runat="server" visible='<%# !Convert.ToBoolean(Eval("CanView").ToString()) %>'>
                                                Website URL: <a target="_blank" href='<%#Eval("CompanyWebsite")%>'><%#Eval("CompanyWebsite") %></a>
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
                    <h3>Sorry we couldn't find anyone in your preferred list</h3>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

