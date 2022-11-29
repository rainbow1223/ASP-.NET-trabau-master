<%@ Page Title="Proposal details - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="viewproposal.aspx.cs" Inherits="Jobs_proposals_viewproposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="problem-heading p-80 pt-0">
        <h3>Proposal details </h3>
    </div>
    <div class="proposal-content">
        <div class="row mar-0">
            <div class="col-sm-9 proposal-left pad-0">
                <div class="profile-card profile-card-no-shadow">
                    <div class="airCardHeader d-flex align-items-center">
                        <h2>Job details</h2>
                    </div>
                    <div class="airCardBody padding-20">
                        <div class="card-body-content">
                            <div class="row">
                                <div class="col-sm-9">
                                    <h6 class="apply-job-content-header">
                                        <asp:Literal ID="ltrlJobTitle" runat="server"></asp:Literal></h6>
                                    <div class="apply-tags">
                                        <ul class="tags">
                                            <li>
                                                <asp:Literal ID="ltrlJobCategory" runat="server"></asp:Literal>
                                            </li>
                                        </ul>
                                        <p class="apply-posted-on">
                                            <asp:Literal ID="ltrlJobPostedOn" runat="server"></asp:Literal>
                                        </p>
                                    </div>
                                    <p>
                                        <asp:Literal ID="ltrlJobDescription" runat="server"></asp:Literal>
                                    </p>
                                    <a class="view-job-posting" target="_blank" id="aViewJobPosting" runat="server">View job posting </a>
                                </div>
                                <div class="col-sm-3 apply-right">
                                    <div>
                                        <i class="fa fa-flask" aria-hidden="true"></i>
                                        <div class="apply-right-info">
                                            <b>Experience Level</b>

                                        </div>
                                        <div class="apply-right-info-bottom">
                                            <asp:Literal ID="ltrlExperienceLevel" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                    <br />
                                    <div>
                                        <i class="flaticon-null-4" aria-hidden="true"></i>
                                        <div class="apply-right-info">
                                            <b>Budget Type</b><br />
                                            <asp:Literal ID="ltrlPaymentType" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />

                            <div class="row">
                                <div class="col-sm-12">
                                    <h6 class="apply-job-content-header">Skills and expertise</h6>
                                    <ul class="tags">
                                        <asp:Repeater ID="rAllSkillsExpertise" runat="server">
                                            <ItemTemplate>
                                                <li class="ticked"><%#Eval("ExpertiseValue") %></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="proposed-clause">
                                        <p>Your proposed terms</p>
                                        <p class="clause-right">
                                            Client's budget:
                                        <asp:Literal ID="ltrlBudgetValue" runat="server"></asp:Literal>
                                        </p>
                                    </div>
                                </div>
                                <div class="col-sm-12 proposed-clause-2">
                                    <p><b>Bid/Budget</b> </p>
                                    <p>Total amount the client will see on your proposal </p>
                                    <p>
                                        <asp:Literal ID="ltrlProposedBID" runat="server"></asp:Literal>
                                    </p>

                                    <hr />
                                </div>
                                <div class="col-sm-12 proposed-clause-2">
                                    <p><b>You'll Receive</b>  </p>
                                    <p>The estimated amount you'll receive after service fees  </p>
                                    <p>
                                        <asp:Literal ID="ltrlFinalAmount" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="profile-card profile-card-no-shadow">
                    <div class="airCardHeader d-flex align-items-center">
                        <h2>Cover Letter</h2>
                    </div>

                    <div class="airCardBody padding-20">
                        <div class="card-body-content">
                            <p>
                                <asp:Literal ID="ltrlCoverLetter" runat="server"></asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">

                <div class="about-client">
                    <h5>About the client</h5>
                    <br />

                    <p>
                        <b>
                            <asp:Literal ID="ltrlClientCountryName" runat="server"></asp:Literal></b>
                    </p>
                    <p>
                        <asp:Literal ID="ltrlClientCityName" runat="server"></asp:Literal>
                    </p>
                    <br />

                    <p>
                        <b>
                            <asp:Literal ID="ltrlClientJobPostedCount" runat="server"></asp:Literal>
                            jobs posted</b>
                    </p>
                    <p>
                        <asp:Literal ID="ltrlClientHireRate" runat="server"></asp:Literal>
                        hire rate,
                    <asp:Literal ID="ltrlClientJobOpenCount" runat="server"></asp:Literal>
                        open job
                    </p>
                    <br />

                    <p>
                        <small>Member since
                    <asp:Literal ID="ltrlClientRegDate" runat="server"></asp:Literal></small>
                    </p>
                </div>
                <div class="submit-proposal">
                    <a class="save-job" id="aSaveJob" runat="server" visible="false" data-toggle="popover" data-trigger="focus" data-content="Save Job"><i class="fa fa-heart" aria-hidden="true"></i>Save Job</a>
                    <a class="save-job mt-3" id="aAddToPreferList" runat="server" data-toggle="popover" data-trigger="focus" data-content="Add Client to your preferred list">Add Client to Preferred List</a>
                </div>
            </div>
        </div>
    </div>
    <script src='<%= Page.ResolveUrl("~/assets/js/view-proposal.js?version=1.1") %>' type="text/javascript"></script>
</asp:Content>

