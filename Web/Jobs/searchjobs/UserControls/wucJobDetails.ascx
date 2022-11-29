<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucJobDetails.ascx.cs" Inherits="Jobs_searchjobs_UserControls_wucJobDetails" %>
<div class="job-details">
    <div class="already-applied-top" id="div_alreadyapplied" runat="server" visible="false">
        <span class="already-applied-left">
            <i class="fa fa-check" aria-hidden="true"></i>
        </span>
        <div class="already-applied-right">
            <asp:Label ID="lblAlreadyApplied" runat="server"></asp:Label>
            <a id="a_view_proposal" runat="server">View Proposal</a>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-9 job-details-left">
            <asp:HiddenField ID="hfJobId" runat="server" Visible="false" />
            <div class="dt-block">
                <h2>
                    <asp:Literal ID="ltrlJobTitle" runat="server"></asp:Literal></h2>
            </div>
            <hr />
            <div class="dt-block">
                <h3>Job Details</h3>
                <div class="job-description">
                    <asp:Literal ID="ltrlJobDescription" runat="server"></asp:Literal>
                </div>


                <div class="dt-block additional-files">
                </div>

                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <h3 class="no-margin-bottom">Job, Budget, and Experience</h3>
                    </div>
                    <div class="col-md-5">
                        <div class="jd-opt">
                            <i class="flaticon-null-11"></i>
                            <div class="jd-heading">
                                <h6>
                                    <asp:Literal ID="ltrlJobCategory" runat="server"></asp:Literal></h6>
                                <p>
                                    <asp:Literal ID="ltrlJobPostedOn" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="jd-opt">
                            <i class="flaticon-null-4"></i>
                            <div class="jd-heading">
                                <h6>
                                    <asp:Literal ID="ltrlBudgetValue" runat="server"></asp:Literal></h6>
                                <p>
                                    <asp:Literal ID="ltrlBudgetType" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="jd-opt">
                            <i class="flaticon-null-5"></i>
                            <div class="jd-heading">
                                <h6>
                                    <asp:Literal ID="ltrlExperienceLevel" runat="server"></asp:Literal></h6>
                                <p>
                                    <asp:Literal ID="ltrlExperienceInfo" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="jd-opt">
                            <i class="fa fa-map-marker" aria-hidden="true"></i>
                            <div class="jd-heading">
                                <h6>Location</h6>
                                <p>
                                    <asp:Literal ID="ltrlLocationInfo" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="jd-opt">
                            <i class="fa fa-credit-card" aria-hidden="true"></i>
                            <div class="jd-heading">
                                <h6>Payment Type</h6>
                                <p>
                                    <asp:Literal ID="ltrlPaymentType" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="jd-opt">
                            <i class="fa fa-tasks" aria-hidden="true"></i>
                            <div class="jd-heading">
                                <h6>Type Of Work</h6>
                                <p>
                                    <asp:Literal ID="ltrlTypeOfWork" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />

                <div class="dt-block">
                    <h3>Skills and expertise</h3>
                    <div class="row">
                        <asp:Repeater ID="rSkills" runat="server">
                            <ItemTemplate>
                                <div class="col-md-6">
                                    <div class="jd-opt">
                                        <div class="jd-heading">
                                            <h6>
                                                <asp:Literal ID="ltrlExpertiseType" runat="server" Text='<%#Eval("ExpertiseType") %>'></asp:Literal>
                                            </h6>
                                            <ul class="tags">
                                                <asp:Repeater ID="rFE" runat="server">
                                                    <ItemTemplate>
                                                        <li class="ticked"><%#Eval("ExpertiseValue") %></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
                <hr />

                <div class="dt-block">
                    <h3>Activity on this job</h3>
                    <div class="job-activity">
                        <ul>
                            <li>Proposals: <b>
                                <asp:Literal ID="ltrlProposalsCount" runat="server"></asp:Literal></b></li>
                            <li>Interviewing: <b>
                                <asp:Literal ID="ltrlInterviewingCount" runat="server"></asp:Literal></b></li>
                            <li>Invites sent: <b>
                                <asp:Literal ID="ltrlInvitesCount" runat="server"></asp:Literal></b></li>
                            <li>Unanswererd invites: <b>
                                <asp:Literal ID="ltrlUnanswererdInvitesCount" runat="server"></asp:Literal></b></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="submit-proposal" id="div_proposal_submission" runat="server" visible="false">
                <a class="cta-btn-sm" id="aApply" runat="server" data-toggle="popover" data-trigger="focus" data-content="Submit Proposal">Submit a Proposal</a>
                <a class="save-job" id="aSaveJob" runat="server" data-toggle="popover" data-trigger="focus" data-content="Save Job"><i class="fa fa-heart" aria-hidden="true"></i>Save Job</a>
                <a class="save-job" id="aAddToPreferList" runat="server" data-toggle="popover" data-trigger="focus" data-content="Add Client to your preferred list">Add Client to Preferred List</a>
                <a class="send-job" id="aSendJob" runat="server" data-toggle="popover" data-trigger="focus" data-content="Send Job to your Friend">Send Job to your Friend</a>
                <hr />
            </div>

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
        </div>
    </div>
    <div class="client-other-jobs">
        <hr />
        <h4>Other Jobs posted by Client</h4>
        <hr />
        <asp:Repeater ID="rotherjobs" runat="server">
            <ItemTemplate>
                <div class="job-result-main other-posted-jobs">
                    <div class="job-actions">
                        <a class="btn-view-search-job" data-toggle="popover" tabindex="0" data-trigger="focus"
                            data-content="View Job Details">View Job Details</a>
                    </div>
                    <div class="job-result">
                        <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lbl_JobId" runat="server" Text='<%#Eval("JobId") %>' Style="display: none"></asp:Label>
                        <p class="job-result-top">
                            <i class="fa fa-angle-down opj-arrow" aria-hidden="true"></i><a><%#Eval("JobTitle") %></a>
                        </p>
                        <div class="job-result-full-details">
                            <div class="job-result-middle">
                                <span><%#Eval("JobBudgetType") %></span> - 
                                        <span><%#Eval("JobLevelOfExperience") %></span> - 
                                        <span><%#Eval("JobBudgetValue") %></span> - 
                                        <span><%#Eval("PostedOn") %></span>
                            </div>
                            <div class="job-result-desc job-result-desc-other">
                                <%#Eval("JobDescription") %>
                            </div>
                            <ul class="tags">
                                <asp:Repeater ID="rExpertise" runat="server">
                                    <ItemTemplate>
                                        <li><%#Eval("SkillName") %></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                            <p class="job-result-prop-count">
                                Proposals: <b>
                                    <%#Eval("ProposalsCount") %></b>
                            </p>
                            <p class="job-result-location">
                                <i class="fa fa-map-marker" aria-hidden="true"></i>
                                <%#Eval("JobLocationInfo") %>
                            </p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
