<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucSearchJobs_Op.ascx.cs" Inherits="UserControls_wucSearchJobs_Op" %>
<div class="jobs-result-data">
    <asp:Repeater ID="rjobs" runat="server">
        <ItemTemplate>
            <div class="job-result-main">
                <div class="job-actions">
                    <a class="btn-view-search-job" data-toggle="popover" tabindex="0" data-trigger="focus"
                        data-content="View Job Details" href="Login.aspx">View Job Details</a>
                </div>
                <div class="job-result">
                    <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
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
    <div class="no-jobs-found text-center register-for-more" id="div_Register" runat="server" visible="false"><a href="signup/index.aspx">Register to view more jobs</a></div>
</div>
