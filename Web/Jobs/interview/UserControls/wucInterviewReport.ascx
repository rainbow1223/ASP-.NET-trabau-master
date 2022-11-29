<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucInterviewReport.ascx.cs" Inherits="Jobs_interview_UserControls_wucInterviewReport" %>
<div class="interview-details-data">
    <style>
        [id*="lblInterviewDate"], [id*="lblInterviewFromTime"], [id*="lblInterviewToTime"], [id*="lblInterviewType"] {
            line-height: 40px;
            height: 41px;
        }
    </style>
    <div class="row">
        <div class="col-sm-3">
            <label><b>Interview Type</b></label>
            <asp:Label ID="lblInterviewType" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-sm-3">
            <label><b>Date</b></label>
            <asp:Label ID="lblInterviewDate" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-sm-3">
            <label><b>From Time</b></label>
            <asp:Label ID="lblInterviewFromTime" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-sm-3">
            <label><b>To Time</b></label>
            <asp:Label ID="lblInterviewToTime" runat="server" CssClass="form-control"></asp:Label>
        </div>
        <div class="col-sm-12" id="div_interview_contact_info" runat="server" visible="false" clientidmode="static">
            <label><i class="flaticon-phone-call"></i><b>Contact Number for Interview</b></label>
            <asp:Label ID="lblContactNumber" runat="server" CssClass="form-control" Style="line-height: 50px;"></asp:Label>
        </div>
    </div>
    <div class="row add-question">
        <div class="col-sm-9">
            <div class="col-sm-12">
                <h4 id="h4_ques_header">Questions</h4>
                <ol id="ol_questions" runat="server" clientidmode="static">
                </ol>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12" id="div_Interview_Response" runat="server" visible="false">
        </div>
    </div>
    <asp:HiddenField ID="hfInterview_data" runat="server" ClientIDMode="Static" />
</div>
