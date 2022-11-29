<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSearchFreelancer.ascx.cs" Inherits="Jobs_Posting_UserControls_wucSearchFreelancer" %>
<div class="freelancers-result-data">
    <asp:Repeater ID="rProposals" runat="server">
        <ItemTemplate>
            <div class="col-12 proposal-row">
                <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' Visible="false"></asp:Label>
                <div class="proposal-inner-row" id='<%#Eval("ProposalId") %>'>
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
                <div class="profile-inner-r proposal-actions-top" data='<%#Eval("ProposalId") %>' runat="server">
                    <a id="aPropsalDetails_top" class="cta-btn-sm">View Proposal</a>
                    <a id="aHire_top" class='<%# "cta-btn-sm "+Eval("HiredClass").ToString() %>' runat="server" visible='<%# !Convert.ToBoolean(Eval("Declined").ToString()) %>'><%#Eval("HiredText") %></a>
                    <a id="aDecline_top" class='<%# "cta-btn-sm "+Eval("DeclinedClass").ToString() %>' runat="server" visible='<%# !Convert.ToBoolean(Eval("Hired").ToString()) %>'><%#Eval("DeclinedText") %></a>
                </div>
                <div class="proposal-actions-bottom" data='<%#Eval("ProposalId") %>' runat="server">
                    <a id="aPropsalDetails" class="cta-btn-sm">View Proposal</a>
                    <a id="aHire" class='<%# "cta-btn-sm "+Eval("HiredClass").ToString() %>' runat="server" visible='<%# !Convert.ToBoolean(Eval("Declined").ToString()) %>'><%#Eval("HiredText") %></a>
                    <a id="aDecline" class='<%# "cta-btn-sm "+Eval("DeclinedClass").ToString() %>' runat="server" visible='<%# !Convert.ToBoolean(Eval("Hired").ToString()) %>'><%#Eval("DeclinedText") %></a>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div id="divTrabau_proposal_details" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4>Proposal details</h4>
                    <button class="close" onclick="HandlePopUp('0', 'divTrabau_proposal_details');return false;">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body proposaldetails-content">
                </div>
            </div>
        </div>
    </div>
    <div id="divTrabau_Interview_details" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4>Interview details</h4>
                    <button class="close" onclick="HandlePopUp('0', 'divTrabau_Interview_details');HandlePopUp('1', 'divTrabau_proposal_details');return false;">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body interviewdetails-content">

                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblJobId" runat="server" Visible="false" />
</div>
