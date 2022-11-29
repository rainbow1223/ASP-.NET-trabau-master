<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSearchJobs.ascx.cs" Inherits="Jobs_searchjobs_UserControls_wucSearchJobs" %>

<div class="jobs-result-data">
    <asp:Repeater ID="rjobs" runat="server">
        <ItemTemplate>
            <div class="job-result-main">
                <div class="job-actions">
                    <a class='<%#"btn-save-search-job"+(Eval("SavedText").ToString()=="Saved"?" saved-job":"") %>' id="a_save_job" runat="server" visible='<%#Eval("CanSaveJob") %>'
                        data-toggle="popover" tabindex="0" data-trigger="focus" data-content="Save Job"><i class='<%#Eval("SavedStatus") %>' aria-hidden="true"></i>&nbsp;<%#Eval("SavedText") %></a>
                    <a class="btn-view-search-job" data-toggle="popover" tabindex="0" data-trigger="focus"
                        data-content="View Job Details">View Job Details</a>
                </div>
                <div class="job-result">
                    <div class="already-applied" runat="server" visible='<%#Eval("AlreadyApplied") %>'>
                        <i class="fa fa-check-circle-o" aria-hidden="true"></i>
                        Applied
                    </div>
                    <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lbl_JobId" runat="server" Text='<%#Eval("JobId") %>' Style="display: none"></asp:Label>
                    <p class="job-result-top">
                        <a><%#Eval("JobTitle") %></a>

                    </p>
                    <div class="job-result-middle">
                        <span><%#Eval("JobBudgetType") %></span> - 
                                        <span><%#Eval("JobLevelOfExperience") %></span> - 
                                        <span><%#Eval("JobBudgetValue") %></span> - 
                                        <span><%#Eval("PostedOn") %></span>
                    </div>
                    <div class="job-result-desc">
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
        </ItemTemplate>
    </asp:Repeater>
    <div class="no-jobs-found text-center" id="div_nojobs" runat="server" visible="false">No More Jobs</div>
    <asp:Label ID="lblPageNumber_Request" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblSearchText_Request" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblSearchType_Request" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblSavedJobs_Request" runat="server" Visible="false"></asp:Label>

    <asp:Label ID="lblJobType" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblExpLevel" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblClientHistory" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblNoOfProposals" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblBudget" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblHoursPerWeek" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblProjectLength" runat="server" Visible="false"></asp:Label>
</div>
