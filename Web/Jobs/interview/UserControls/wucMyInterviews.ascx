<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucMyInterviews.ascx.cs" Inherits="Jobs_interview_UserControls_wucMyInterviews" %>
<div class="interview-data">
    <div class="row">
        <div class="col-sm-12">
            <label>Filter</label>
            <asp:HiddenField ID="hfInterviewFilter" runat="server" Visible="false" />
            <asp:DropDownList ID="ddlInterviewFilter" runat="server" CssClass="form-control">
                <asp:ListItem Text="All Interviews" Value="All"></asp:ListItem>
                <asp:ListItem Text="Scheduled by Client" Value="C"></asp:ListItem>
                <asp:ListItem Text="Requested by me" Value="R"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div>
        <asp:Repeater ID="rMyInterviews" runat="server">
            <ItemTemplate>
                <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
                <div class="myjobs-row" id='<%#Eval("ScheduleId") %>'>
                    <div class="myjobs-result row">
                        <div class="col-md-5">
                            <p class="myjobs-result-top">
                                <b><%#Eval("JobTitle") %></b>
                            </p>
                            <p class="myjobs-result-bottom">
                                Client Name: <b><%#Eval("Client_Name") %></b>
                            </p>
                        </div>
                        <div class="col-md-7">
                            <p><b>$<%#Eval("Budget") %> <%#Eval("BudgetType") %></b> budget</p>
                            <p>Project length: <%#Eval("ProjectLength") %></p>
                        </div>

                    </div>
                    <div class="margin-top-20">
                        <div class="interview-sch">
                            <%#Eval("Interview_Schedule") %>
                            <div class="interview-sch-actions" data='<%#Eval("ScheduleId") %>' runat="server" visible='<%#Eval("CanTakeAction") %>'>
                                <a class="btn-int-sch-accept">Accept</a>
                                <a class="btn-int-sch-reject">Reject</a>
                                <a class="btn-int-sch-change">Request Changes</a>
                            </div>
                            <div class="interview-request-status" runat="server" visible='<%#Eval("CanViewInterviewRequestStatus") %>'><%#Eval("InterviewRequestStatus") %></div>
                            <div class='<%#"interview-sch-taken "+Eval("InterviewAction_Class").ToString() %>' runat="server" visible='<%#(!Convert.ToBoolean(Eval("CanTakeAction").ToString()) && !Convert.ToBoolean(Eval("CanViewInterviewRequestStatus").ToString())?true:false) %>'>
                                <%#Eval("InterviewActionType") %>
                            </div>
                            <div class='<%#Eval("InterviewStatus_Class").ToString() %>' runat="server" visible='<%# !Convert.ToBoolean(Eval("CanViewInterviewRequestStatus").ToString()) %>'>
                                <a class="btn-start-interview" href='<%#"../interview/start.aspx?schedule="+Eval("ScheduleId").ToString() %>'
                                    runat="server" visible='<%#Eval("CanStartInterview") %>'>Start Interview</a>
                                <%#Eval("InterviewStatus") %>
                            </div>
                            <a class="btn-interview-report" runat="server" visible='<%#Eval("CanViewReport") %>' data='<%# Eval("ScheduleId") %>'>View Report</a>
                        </div>
                    </div>
                    <div id="divTrabau_Interview_Action" class="modal fade" role="dialog" runat="server" visible='<%#Eval("CanTakeAction") %>' clientidmode="Static">

                        <div class="modal-dialog modal-lg" role="document">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4>You are accepting Interview for <b><%#Eval("JobTitle") %></b></h4>
                                    <button class="close" onclick="HandlePopUp('0', 'divTrabau_Interview_Action');return false;">&times;</button>
                                </div>

                                <!-- Modal body -->
                                <div class="modal-body interviewaction-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <label class="interview-sch"><%#Eval("Interview_Schedule") %></label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>Contact Number for communication with Client (Optional)</label>
                                            <asp:TextBox ID="txtContactNumber" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Enter Contact Number"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <a class="cta-btn-sm pointer btnaccept-schedule" data='<%#Eval("ScheduleId") %>'>Accept</a>
                                            <a class="cta-btn-sm pointer btn-red" onclick="HandlePopUp('0', 'divTrabau_Interview_Action');return false;">Cancel</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="no-result-found" id="div_no_result" runat="server" visible="false">
        <div><i class="flaticon-not-found" aria-hidden="true"></i></div>
        <h3>No Interview scheduled yet</h3>
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
