<%@ Page Title="Proposals" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Jobs_proposals_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        var pathconfig = '<%= Page.ResolveUrl("index.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/proposal.js?version=1.0") %>'></script>
    <div class="myCard-heading">
        <h4>Proposals </h4>
    </div>
    <div class="proposals-content">
        <div class="proposals-result no-background ad-search">
            <div id="div_proposals_result">
                <asp:Repeater ID="rProposals" runat="server">
                    <ItemTemplate>
                        <div class="my-proposals row">
                            <asp:Label ID="lblProposalId" runat="server" Visible="false" Text='<%# Eval("ProposalId") %>'></asp:Label>
                            <div class="col-sm-3">
                                <b><%# Eval("ProposalDate") %></b>
                            </div>
                            <div class="col-sm-3">
                                <a href="index.aspx" id="a_viewjob" runat="server"><%# Eval("JobTitle") %></a>
                            </div>
                            <div class="col-sm-3">
                                <%# Eval("JobBudgetType") %>
                            </div>
                            <div class="col-sm-3">
                                <a id="btnRequestForInterview" class="btn-interview-request" runat="server" visible='<%# Eval("CanRequestInterview") %>'>Request for Interview</a>
                                <span runat="server" visible='<%# !Convert.ToBoolean(Eval("CanRequestInterview").ToString()) %>'><%# Eval("RequestStatus") %></span>
                                <a class="btn-view-interview-sch" href="~/jobs/interview/myinterviews.aspx" runat="server" visible='<%# Eval("CanViewSchedule") %>'>View Interview Schedule</a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>

