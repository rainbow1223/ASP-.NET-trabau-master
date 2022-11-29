<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucProposals.ascx.cs" Inherits="Jobs_proposals_UserControls_wucProposals" %>
<div class="proposals-result-data">
    <asp:Repeater ID="rProposals" runat="server">
        <ItemTemplate>
            <div class="col-12 proposal-row">
                <div class="job-desc">
                    <b>Job Title: </b><%#Eval("JobTitle") %>
                    <hr style="border-color: #eceaea" />
                </div>
                <div class="proposal-actions">
                    <a class="btn-view-jobposting" id='<%#Eval("JobId") %>' data-toggle="popover" tabindex="0" data-trigger="focus"
                        data-content="View Job Posting" data-original-title="" title="">View Job Posting</a>
                </div>
                <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' Visible="false"></asp:Label>
                <div class="proposal-inner-row">
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
                                    <b>Bid Amount: <span>$<%#Eval("BIDAmount") %></span></b>
                                </div>
                                <div class="col-xs-6 col-md-3">
                                    <b>$<%#Eval("TotalEarning") %> <span>earned</span></b>
                                </div>
                                <div class="col-xs-6 col-md-3">
                                </div>
                            </div>
                        </div>
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
                    <div class="col-sm-12 profile-cover-letter">
                        <b>Cover Letter: </b><%#Eval("CoverLetter") %>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
