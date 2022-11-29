<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucProposalDetails.ascx.cs" Inherits="Jobs_Posting_UserControls_wucProposalDetails" %>
<div class="proposal-details-result-data">
    <asp:Label ID="lblProposalId" runat="server" Visible="false"></asp:Label>
    <div class="row proposal-top">
        <div class="col-sm-5">
            <div class="profile-content">
                <div class="my-profile-picture">
                    <img class="profile-avatar avatar" id="img_profile_picture" src="../assets/uploads/avtar.jpg" />
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
                </div>
            </div>
        </div>
        <div class="col-sm-7 text-right" id="divActions" runat="server" clientidmode="static">
            <a id="lbtnFlagForInterview" class="cta-btn-sm" runat="server" visible="false">Identify For Interview</a>
            <a id="lbtnHire" class="cta-btn-sm" runat="server">Hire</a>
            <a id="lbtnDecline" class="cta-btn-sm" runat="server">Decline</a>
        </div>
    </div>
    <div class="row job-proposal-terms">
        <div class="col-sm-12">
            <div class="proposed-clause">
                <p>Your proposed terms</p>
                <p class="clause-right">
                    Job budget:
                    <asp:Literal ID="ltrlBudgetValue" runat="server"></asp:Literal>
                </p>
            </div>
        </div>
        <div class="col-sm-12 proposed-clause-2">
            <p><b>Bid/Budget</b> </p>
            <p>Total amount the freelancer Bid </p>
            <p>
                <b>
                    <asp:Literal ID="ltrlProposedBID" runat="server"></asp:Literal></b>
            </p>

        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 border-bottom pad-t-10 pad-b-10">
            <b>Cover Letter: </b>
            <div id="sCoverLetter" runat="server"></div>
        </div>

        <div class="col-sm-12 border-bottom pad-t-10 pad-b-10" id="div_screening" runat="server" visible="false">
            <label><b>Screening Questions:</b></label>
            <asp:Repeater ID="rScreenQuestions" runat="server">
                <ItemTemplate>
                    <div>
                        <b><%# Container.ItemIndex + 1 %>. 
                        <%#Eval("QuestionName") %></b>
                        <div>
                            <%#Eval("Answer") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="col-sm-12 additional-files pad-t-10 pad-b-10">
        </div>
    </div>


</div>
