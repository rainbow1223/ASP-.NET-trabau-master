<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucInterviews.ascx.cs" Inherits="Jobs_interview_UserControls_wucInterviews" %>
<div class="interview-data">
    <div class="row">
        <div class="col-sm-12">
            <label>Filter</label>
            <asp:HiddenField ID="hfInterviewFilter" runat="server" Visible="false" />
            <asp:DropDownList ID="ddlInterviewFilter" runat="server" CssClass="form-control">
                <asp:ListItem Text="All Interviews" Value="All"></asp:ListItem>
                <asp:ListItem Text="Identify by me" Value="I"></asp:ListItem>
                <asp:ListItem Text="Requested by Freelancer" Value="R"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div>
        <asp:Repeater ID="rInterviews" runat="server">
            <ItemTemplate>
                <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                <asp:Label ID="lblInterviewId" runat="server" Text='<%#Eval("InterviewId") %>' Visible="false"></asp:Label>
                <div class="hire-result-main profile-row interview-result-main">
                    <div class="profile-actions hire-actions interview-actions">
                        <a class="btn-select-interview" data-toggle="popover" tabindex="0" data-trigger="focus" data='<%#Eval("InterviewId_Enc") %>'
                            data-content="Select to Interview" runat="server" visible='<%#Eval("CanSchedule") %>'>Schedule New</a>
                        <a class="btn-view-search-profile" href='<%#"../../profile/user/userprofile.aspx?profile="+Eval("profile_id").ToString() %>' data-toggle="popover" tabindex="0" data-trigger="focus"
                            data-content="View Profile">View Profile</a>
                        <a class="btn-remove-interview" data-toggle="popover" tabindex="0" data-trigger="focus" data='<%#Eval("ProposalId") %>'
                            data-content="Remove from Interview List" id="btn_remove_interview" runat="server">Remove</a>
                    </div>
                    <div class="hire-result">
                        <p class="hire-result-top">
                            <b><%#Eval("JobTitle") %></b>
                        </p>
                        <div class="row">
                            <div class="col-4 col-lg-3">
                                <div class="profilefoto" id="div_profile_photo" runat="server">
                                    <img alt="user" runat="server" id="imgFL_ProfilePic" src="" />
                                </div>
                            </div>
                            <div class="col-8 col-lg-9">
                                <div class="profile-inner-l">
                                    <div class="ellipsis"><b><%#Eval("Name") %></b></div>
                                    <div class="ellipsis"><%#Eval("Title") %></div>
                                    <div class="ellipsis"><%#Eval("LocalTime") %></div>
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
                    </div>

                    <div class="interview-schedule">
                        <asp:Repeater ID="rInterview_Schedules" runat="server">
                            <ItemTemplate>
                                <div class="interview-sch-item-main row">
                                    <div class='<%# "interview-sch-item "+(Convert.ToBoolean(Eval("CanViewReport"))?"col-sm-10":"col-sm-12") %>' data='<%#Eval("InterviewId_Enc") %>'>
                                        <%#Eval("Interview_Schedule") %>
                                    </div>
                                    <div class="col-sm-2 interview-report" data='<%#Eval("InterviewId_Enc") %>'>
                                        <a class="btn-interview-report" runat="server" visible='<%#Eval("CanViewReport") %>'>View Report</a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="interview-sch-request" id="div_interview_request" runat="server" visible='<%#Eval("InterviewRequest") %>' data='<%#Eval("ProposalId") %>'>
                            <%#Eval("InterviewRequestStatus") %>
                            <a class="btn-int-request-accept" runat="server" visible='<%#Eval("CanTakeAction_InterviewRequest") %>'>Accept Request</a>
                            <a class="btn-int-request-reject" runat="server" visible='<%#Eval("CanTakeAction_InterviewRequest") %>'>Reject Request</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class="no-result-found" id="div_no_result" runat="server" visible="false">
            <div><i class="flaticon-not-found" aria-hidden="true"></i></div>
            <h3>No Interview scheduled yet</h3>
        </div>
    </div>
    <div id="divTrabau_Interview_details" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg interview-question" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4>Interview details</h4>
                    <button class="close" onclick="HandlePopUp('0', 'divTrabau_Interview_details');return false;">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body interviewdetails-content">
                </div>
            </div>
        </div>
    </div>
</div>
