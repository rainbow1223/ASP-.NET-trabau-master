<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucInterviewDetails.ascx.cs" Inherits="Jobs_Posting_UserControls_wucInterviewDetails" %>
<div class="interview-details-data">
    <div class="row">
        <div class="col-sm-3">
            <label><b>Interview Type</b></label>
            <asp:DropDownList ID="ddlInterviewType" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Interview Type" Value="0"></asp:ListItem>
                <asp:ListItem Text="On Trabau" Value="OnTrabau"></asp:ListItem>
                <asp:ListItem Text="Email" Value="Email"></asp:ListItem>
                <asp:ListItem Text="Video (Skype)" Value="Skype"></asp:ListItem>
                <asp:ListItem Text="Video (Webex)" Value="Webex"></asp:ListItem>
                <asp:ListItem Text="Video (Zoom)" Value="Zoom"></asp:ListItem>
                <asp:ListItem Text="Video (Google Meet)" Value="GMeet"></asp:ListItem>
                <asp:ListItem Text="Whatsapp" Value="Whatsapp"></asp:ListItem>
                <asp:ListItem Text="Hangout" Value="Hangout"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-sm-3">
            <label><b>Date</b></label>
            <asp:TextBox ID="txtInterviewDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <label><b>From Time</b></label>
            <asp:TextBox ID="txtInterviewFromTime" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-3">
            <label><b>To Time</b></label>
            <asp:TextBox ID="txtInterviewToTime" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-sm-12" id="div_interview_contact_info" runat="server" visible="false" clientidmode="static">
            <label><i class="flaticon-phone-call"></i><b>Contact Number for Interview</b></label>
            <asp:Label ID="lblContactNumber" runat="server" CssClass="form-control" Style="line-height: 50px;"></asp:Label>
        </div>
    </div>
    <div class="row add-question">
    </div>
    <div class="row">
        <div class="col-sm-12">
            <input type="button" value="Schedule Interview" class="cta-btn-sm btn-schedule-interview" id="btnSubmitSchedule" runat="server" />
            <input type="button" value="Cancel Interview Schedule" class="cta-btn-sm btn-cancel-interview" id="btnCancelSchedule" runat="server" visible="false" />
        </div>
        <div class="col-sm-12" id="div_Interview_Response" runat="server" visible="false">
            <div class="row">
                <div class="col-sm-6">
                    <asp:DropDownList ID="ddlInterviewResponse" runat="server" CssClass="form-control height-dropdown">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-6">
                    <input type="button" value="Update Interview Response" class="cta-btn-sm pointer" id="btnUpdateResponse" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfInterview_data" runat="server" ClientIDMode="Static" />
</div>
