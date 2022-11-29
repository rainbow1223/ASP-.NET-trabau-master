<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucMyInterviewDetails.ascx.cs" Inherits="Jobs_interview_UserControls_wucMyInterviews" %>
<div class="interview-details-data">
    <div class="row">
        <div class="col-sm-4">
            <label><b>Date</b></label>
            <asp:TextBox ID="txtInterviewDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-4">
            <label><b>From Time</b></label>
            <asp:TextBox ID="txtInterviewFromTime" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-4">
            <label><b>To Time</b></label>
            <asp:TextBox ID="txtInterviewToTime" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

    </div>
    <div class="row">
        <div class="col-sm-12">
            <input type="button" value="Request Update" class="cta-btn-sm btn-request-update" />
        </div>
    </div>
    <asp:HiddenField ID="hfInterview_data" runat="server" ClientIDMode="Static" />
</div>