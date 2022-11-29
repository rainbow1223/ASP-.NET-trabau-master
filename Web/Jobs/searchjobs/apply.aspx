<%@ Page Title="Submit a Proposal" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="apply.aspx.cs" Inherits="Jobs_searchjobs_apply" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.css") %>' rel="stylesheet" type="text/css" />--%>
    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.common.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.light.css") %>' rel="stylesheet" type="text/css" />
    <script src='<%= Page.ResolveUrl("~/assets/js/apply_1.js?version=1.0") %>'></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-1.10.2.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.min.js") %>'></script>--%>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx-quill.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.all.js") %>'></script>
    <asp:UpdatePanel ID="upApplyJob" runat="server">
        <ContentTemplate>
            <script src='<%= Page.ResolveUrl("~/assets/js/apply_2.js?version=1.3") %>'></script>
            <div class="problem-heading p-80 pt-0">
                <h3>Submit a proposal </h3>
            </div>
            <div class="profile-card">
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
                    </div>
                </div>
            </div>

            <div class="profile-card">
                <div class="airCardHeader d-flex align-items-center terms-header">
                    <h2>Terms</h2>
                    <p class="apply-job-client-budget">
                        Client’s budget:
                        <asp:Literal ID="ltrlBudgetValue" runat="server"></asp:Literal>
                    </p>
                </div>
                <div class="airCardBody padding-20">
                    <div class="card-body-content">
                        <div class="row">
                            <div class="col-sm-12 terms row" id="div_hourly_terms" runat="server" visible="true">
                                <div class="col-sm-12">
                                    <h6 class="apply-job-content-header">What is the rate you'd like to bid for this job?</h6>
                                    <p class="apply-profile-rate">
                                        Your profile rate: <b>
                                            <asp:Literal ID="ltrlHourlyRate" runat="server"></asp:Literal></b>
                                    </p>
                                </div>
                                <hr />
                                <div class="col-sm-12">
                                    <label>
                                        Hourly Rate 
                                        <asp:RequiredFieldValidator ID="rfvHourlyRate" runat="server" ControlToValidate="txtBid_HourlyRate"
                                            ErrorMessage="Enter hourly rate to bid for the job" ValidationGroup="Apply_Job" CssClass="alert-validation" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </label>
                                    <div>
                                        <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                        <asp:TextBox ID="txtBid_HourlyRate" runat="server" CssClass="form-control dollar-hourly"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fteHourlyRate" runat="server" TargetControlID="txtBid_HourlyRate" FilterMode="ValidChars" ValidChars="1234567890." />
                                        <b>/hr</b>
                                    </div>
                                </div>
                                <hr />
                                <div class="col-sm-12 service-fee">
                                    <label>10% Trabau Service Fee </label>
                                    <div>
                                        <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                        <span class="form-control trabau-fee-hourly">
                                            <asp:Label ID="lblTrabauServiceFee_HourlyRate" runat="server"></asp:Label></span>
                                        <b>/hr</b>
                                    </div>
                                </div>
                                <hr />
                                <div class="col-sm-12">
                                    <label>
                                        You'll Receive
                                        <p>(The estimated amount you'll receive after service fees)</p>
                                        <asp:RequiredFieldValidator ID="rfvReceive_HourlyRate" runat="server" ControlToValidate="txtReceive_HourlyRate"
                                            ErrorMessage="Enter the estimated amount you'll receive after service fees" ValidationGroup="Apply_Job" CssClass="alert-validation" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </label>
                                    <div>
                                        <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                        <asp:TextBox ID="txtReceive_HourlyRate" runat="server" CssClass="form-control dollar-hourly" Enabled="false"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fteReceive_HourlyRate" runat="server" TargetControlID="txtReceive_HourlyRate" FilterMode="ValidChars" ValidChars="1234567890." />
                                        <b>/hr</b>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 terms" id="div_FixedPrice_terms" runat="server" visible="false">
                                <div class="col-sm-12">
                                    <h6 class="apply-job-content-header">What is the full amount you'd like to bid for this job? </h6>
                                </div>
                                <hr />
                                <div class="col-sm-12">
                                    <label>
                                        Bid
                                        <p>(Total amount the client will see on your proposal)</p>
                                        <asp:RequiredFieldValidator ID="rfvBid_FixedPrice" runat="server" ControlToValidate="txtBid_FixedPrice"
                                            ErrorMessage="Enter fixed price to bid for the job" ValidationGroup="Apply_Job" CssClass="alert-validation" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </label>
                                    <div>
                                        <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                        <asp:TextBox ID="txtBid_FixedPrice" runat="server" CssClass="form-control dollar" ClientIDMode="Static"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fteBid_FixedPrice" runat="server" TargetControlID="txtBid_FixedPrice" FilterMode="ValidChars" ValidChars="1234567890." />
                                    </div>
                                </div>
                                <hr />
                                <div class="col-sm-12">
                                    <label>10% Trabau Service Fee </label>
                                    <div>
                                        <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                        <span class="form-control trabau-fee">
                                            <asp:Label ID="lblTrabauServiceFee_FixedPrice" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <hr />
                                <div class="col-sm-12">
                                    <label>
                                        You'll Receive
                                        <p>(The estimated amount you'll receive after service fees)</p>
                                        <asp:RequiredFieldValidator ID="rfvReceive_FixedPrice" runat="server" ControlToValidate="txtReceive_FixedPrice"
                                            ErrorMessage="Enter the estimated amount you'll receive after service fees" ValidationGroup="Apply_Job" CssClass="alert-validation" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </label>
                                    <div>
                                        <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                        <asp:TextBox ID="txtReceive_FixedPrice" runat="server" CssClass="form-control dollar" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="fteReceive_FixedPrice" runat="server" TargetControlID="txtReceive_FixedPrice" FilterMode="ValidChars" ValidChars="1234567890." />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="profile-card" id="div_project_length" runat="server" visible="false">
                <div class="airCardBody padding-20">
                    <div class="card-body-content">
                        <div class="row">
                            <div class="col-sm-12">
                                <label>
                                    How long will this project take?
                                    <asp:RequiredFieldValidator ID="rfvProjectLength" runat="server" ControlToValidate="ddlProjectLength"
                                        ErrorMessage="Select Project Length" ValidationGroup="Apply_Job" CssClass="alert-validation"></asp:RequiredFieldValidator></label>
                                <asp:DropDownList ID="ddlProjectLength" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="profile-card">
                <div class="airCardHeader d-flex align-items-center">
                    <h2>Additional details
                    </h2>
                </div>
                <div class="airCardBody padding-20">
                    <div class="card-body-content">
                        <div class="row">
                            <div class="col-sm-12">
                                <label>
                                    Cover Letter
                                    <%--<asp:RequiredFieldValidator ID="rfvCoverLetter" runat="server" ControlToValidate="txtCoverLetter"
                                        ErrorMessage="Enter Cover Letter" ValidationGroup="Apply_Job" CssClass="alert-validation" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </label>
                                <%--<asp:TextBox ID="txtCoverLetter" runat="server" CssClass="form-control textEditor" TextMode="MultiLine"></asp:TextBox>--%>
                                <div class="dx-viewport">
                                    <div class="html-editor" id="div_coverletter" runat="server">
                                    </div>
                                    <asp:HiddenField ID="hfCoverLetter" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <asp:Repeater ID="rScreenQuestions" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestionId" runat="server" Text='<%#Eval("QuestionId") %>' Visible="false"></asp:Label>
                                        <div class="apply-questions">
                                            <label>
                                                <%#Eval("QuestionName") %>
                                                <asp:RequiredFieldValidator ID="rfvQuestionName" runat="server" ControlToValidate="txtScreeningQuestion"
                                                    ErrorMessage="Enter Answer" ValidationGroup="Apply_Job" CssClass="alert-validation" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                            <asp:TextBox ID="txtScreeningQuestion" runat="server" CssClass="form-control textEditor" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="col-sm-12 apply-job-files">
                                <h6>Additional files (optional):</h6>
                                <asp:Button ID="btnAddProfileFiles" runat="server" ClientIDMode="AutoID" OnClick="btnAddProfileFiles_Click" Style="display: none" />
                                <div class="form-control file-upload text-center">
                                    <p>Click to browse and add project file</p>
                                    <p class="progress-bar-percentage">0%</p>
                                    <div class="progress-bar-parent">
                                        <div class="progress-bar progress-bar-override" style="width: 0%;"></div>
                                    </div>
                                </div>
                                <p class="note-p">You may attach upto 5 files under 100 MB each</p>
                                <ol class="project-items">
                                    <asp:Repeater ID="rProfileFiles" runat="server">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfilekey" runat="server" Text='<%#Eval("file_key") %>' Visible="false"></asp:Label>
                                            <li>
                                                <asp:LinkButton ID="lbtnDownloadFile" runat="server" Text='<%#Eval("file_name") %>' OnClick="lbtnDownloadFile_Click"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnRemoveFile" runat="server" CssClass="remove_item" OnClick="lbtnRemoveFile_Click">X</asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="airCardHeader d-flex align-items-center submit-form-btn">
                    <asp:Button ID="btnSubmitProposal" runat="server" Text="Submit a Proposal" CssClass="cta-btn-md" ValidationGroup="Apply_Job" OnClick="btnSubmitProposal_Click" Visible="false" />
                    <asp:Button ID="btnAlreadySubmitted" runat="server" Text="" CssClass="cta-btn-md" Visible="false" Enabled="false" />
                    &nbsp;
                    <asp:Button ID="btnCancelProposal" runat="server" Text="Cancel" CssClass="cta-btn-md btn-disabled" OnClick="btnCancelProposal_Click" />
                    <asp:ValidationSummary ID="vsApplyJobValidation_Summary" runat="server" ValidationGroup="Apply_Job" ShowMessageBox="true" ShowSummary="false" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:AsyncFileUpload ID="afuProjectFiles" runat="server" OnClientUploadStarted="StartUpload"
        OnClientUploadComplete="UploadComplete" OnUploadedComplete="afuProjectFiles_UploadedComplete" Style="display: none" />
</asp:Content>

