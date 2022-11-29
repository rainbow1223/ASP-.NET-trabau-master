<%@ Page Title="Post a Job" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="postjob.aspx.cs" Inherits="Jobs_Posting_postjob" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/job_posting.css?version=1.2") %>' rel="stylesheet" type="text/css" />
    <%--<link href='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.css") %>' rel="stylesheet" type="text/css" />--%>

    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.common.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.light.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select-custom.js") %>'></script>
    <%--<script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-1.10.2.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/Editor/jquery-te-1.4.0.min.js") %>'></script>--%>

    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx-quill.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.all.js") %>'></script>

    <script src='<%= Page.ResolveUrl("~/assets/js/postjob_1.js?version=1.3") %>'></script>
    <section class="jobSteps-section">
        <asp:UpdatePanel ID="upNewJobPost" runat="server">
            <ContentTemplate>
                <script src='<%= Page.ResolveUrl("~/assets/js/postjob_2.js?version=1.4") %>'></script>
                <div class="jobSteps-bg p-80">
                    <div class="container">
                        <div class="steps-point-circle">
                            <ul>
                                <li id="li_step1" runat="server" class="process"></li>
                                <li id="li_step2" runat="server"></li>
                                <li id="li_step3" runat="server"></li>
                                <li id="li_step4" runat="server"></li>
                                <li id="li_step5" runat="server"></li>
                                <li id="li_step6" runat="server"></li>
                                <li id="li_step7" runat="server"></li>
                            </ul>
                        </div>
                        <div class="steps-point-heading">
                            <h3>
                                <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal></h3>
                            <span>Step
                        <asp:Literal ID="ltrlPageNo" runat="server"></asp:Literal>
                                of 7</span>
                        </div>
                    </div>
                </div>

                <div class="job-post-steps p-80">
                    <div class="container">
                        <div class="job-page-block">


                            <div id="div_step1" runat="server" visible="false">
                                <div class="my-card shadow-sm">
                                    <div class="myCard-heading">
                                        <h4>Get Started</h4>
                                    </div>
                                    <div class="myCard-body">
                                        <h6>Enter the name of your job post
                                            <asp:RequiredFieldValidator ID="rfvJobTitle" runat="server" ControlToValidate="txtJobTitle"
                                                ErrorMessage="Enter the name of you job post" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </h6>
                                        <asp:TextBox ID="txtJobTitle" runat="server" placeholder="Enter Job Title" class="form-control"></asp:TextBox>
                                        <h6>Note: Make sure the type of professional and project are included in the name of your job post.</h6>
                                        <h6>Here are some good examples:</h6>
                                        <ul>
                                            <li>Front-end Developer needed for Website</li>
                                            <li>Logo Graphics Designer needed for Company Branding</li>
                                            <li>Roofing Contractor needed for Exterior Roof Replacement</li>
                                            <li>Furniture Designer needed for Home Renovation</li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="my-card shadow-sm">
                                    <div class="myCard-body">
                                        <h6>Job Category 
                                            <asp:RequiredFieldValidator ID="rfvJobCategory" runat="server" ControlToValidate="rbtnlJobCategory"
                                                ErrorMessage="Select Job Category" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator></h6>
                                        <p>These category options enables you to find the best professional with knowledge and experience to work on the project. You can make the category based on level of proficiency or preferred software/application/tools.</p>

                                        <div class="card-rd-acc">
                                            <div class="panel-group" id="accordion">
                                                <asp:RadioButtonList ID="rbtnlJobCategory" runat="server" CssClass="radio-btn">
                                                    <asp:ListItem Text="Front-End Development" Value="FE"></asp:ListItem>
                                                    <asp:ListItem Text="Full Stack Development" Value="FS"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <h6>Some examples:</h6>
                                        <ul>
                                            <li>Contract staff  or Freelancer</li>
                                            <li>Adobe Studio or Photoshop</li>
                                            <li>Installation or Replacement</li>
                                            <li>Front-end development or Fullstack Development</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div id="div_step2" runat="server" visible="false">
                                <div class="my-card shadow-sm">
                                    <div class="myCard-heading">
                                        <h4>Description</h4>
                                    </div>
                                    <div class="myCard-body">
                                        <h6>A good description for your job post should include:</h6>
                                        <ul>
                                            <li>Name of job</li>
                                            <li>Type of job</li>
                                            <li>Duration of job</li>
                                            <li>Any additional information that may be useful to applicants</li>
                                        </ul>
                                        <br>
                                        <div class="form-group">
                                            <%--<div class="dx-viewport demo-container">--%>
                                            <div class="dx-viewport">
                                                <div class="html-editor" id="div_job_desc" runat="server">
                                                </div>
                                            </div>

                                            <%--                                            <asp:TextBox ID="txtJobDescription" runat="server" TextMode="MultiLine" CssClass="textEditor" placeholder="Enter Job Description"></asp:TextBox>--%>
                                            <asp:HiddenField ID="hfJobDescription" runat="server" />
                                            <%--<asp:RequiredFieldValidator ID="rfvJobDescription" runat="server" ControlToValidate="txtJobDescription"
                                                ErrorMessage="Enter Job Description" ValidationGroup="PostJob_Next" CssClass="text-danger custom-validator"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <p class="note-p"><span id="s_characters_count">0</span>/5000 character (minimum 50)</p>
                                        <h6>Example:</h6>
                                        <p>Logo graphics designer needed for company branding. A transport company set to launch in a few months is in need of a graphics designer with proficiency in logos to brand the company for digital and print media. Graphics designer with corporate experience will be highly valued.</p>
                                        <br>

                                        <h6>Additional project files (optional):</h6>
                                        <asp:Button ID="btnAddProfileFiles" runat="server" ClientIDMode="AutoID" OnClick="btnAddProfileFiles_Click" Style="display: none" />
                                        <div class="form-control file-upload text-center">
                                            <cc1:AsyncFileUpload ID="afuProjectFiles" runat="server" OnClientUploadStarted="StartUpload"
                                                OnClientUploadComplete="UploadComplete" OnUploadedComplete="afuProjectFiles_UploadedComplete" Style="position: relative; z-index: 999; width: 100%;" />
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
                            <div id="div_step3" runat="server" visible="false">
                                <div class="my-card shadow-sm">
                                    <div class="myCard-heading">
                                        <h4>Details</h4>
                                    </div>
                                    <div class="myCard-body" style="padding-right: 30px">
                                        <h6>What type of work do you have?<span class="get-help tow-tooltip" data-content="Select type of work do you have" data-toggle="popover" tabindex="0" data-trigger="focus">?</span>
                                            <asp:RequiredFieldValidator ID="rfvTypeOfWork" runat="server" ControlToValidate="rbtnlTypeOfWork"
                                                ErrorMessage="Select type of work do you have" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </h6>
                                        <div class="row">
                                            <asp:RadioButtonList ID="rbtnlTypeOfWork" runat="server" CssClass="special-radio-btn" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<h6>One Time Work</h6><p>Find the right skills to solve a problem once</p>" Value="OT" custom-tooltip="Short term work"></asp:ListItem>
                                                <asp:ListItem Text="<h6>Ongoing Work</h6><p>Find the right skills to solve a problem continuously</p>" Value="ON" custom-tooltip="Long-term work"></asp:ListItem>
                                                <asp:ListItem Text="<h6>Work as Needed</h6><p>Find the right skills to solve a problem as needed and when needed</p>" Value="WN" custom-tooltip="Post a job to find someone to help you solve a problem. After solving the problem, the person will be available to add additional functionality when needed."></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <hr>
                                        <h6>Select how do you want to hire and pay the person?
                                            <span class="get-help hp-tooltip" data-content="Select how do you want to hire and pay the person" data-toggle="popover" tabindex="0" data-trigger="focus">?</span>
                                            <asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ControlToValidate="rbtnlPaymentType"
                                                ErrorMessage="Select how do you want to hire and pay the person" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </h6>
                                        <div class="row">
                                            <asp:RadioButtonList ID="rbtnlPaymentType" runat="server" CssClass="special-radio-btn special-radio-btn-override" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<h6>Pay through Trabau</h6><p>Post a job to hire and pay through Trabau</p>" Value="With_Trabau" custom-tooltip="You deposit the money and pay the person through our website."></asp:ListItem>
                                                <asp:ListItem Text="<h6>Pay outside Trabau</h6><p>Post a job to hire and pay outside Trabau</p>" Value="Without_Trabau" custom-tooltip="You pay the person directly through your organization"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </div>

                                <div class="my-card shadow-sm">
                                    <div class="myCard-body">
                                        <h6>Screening Questions (Optional)</h6>
                                        <p>
                                            Add screening questions and/or require a cover letter<asp:RequiredFieldValidator ID="rfvScreeningQuestion" runat="server" ControlToValidate="txtScreeningQuestion"
                                                ErrorMessage="Add screening question text" ValidationGroup="Add_Screening_Question" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </p>
                                        <asp:TextBox ID="txtScreeningQuestion" runat="server" CssClass="form-control" placeholder="Add screening question for job"></asp:TextBox>
                                        <b>
                                            <asp:LinkButton ID="lbtnAddScreeningQuestion" runat="server" Text="+ Add a Question" CssClass="link-btn" OnClick="lbtnAddScreeningQuestion_Click" ValidationGroup="Add_Screening_Question"></asp:LinkButton></b>
                                        <br>
                                        <ol class="project-items project-questions">
                                            <asp:Repeater ID="rScreeningQuestions" runat="server">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblquestionkey" runat="server" Text='<%#Eval("question_key") %>' Visible="false"></asp:Label>
                                                    <li>
                                                        <a><%#Eval("question") %></a>
                                                        <asp:LinkButton ID="lbtnRemoveQuestion" runat="server" CssClass="remove_item" OnClick="lbtnRemoveQuestion_Click">X</asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ol>
                                        <hr>
                                        <h6>Cover Letter</h6>
                                        If you don't add any screening questions, we'll require a cover letter to allow freelancers and agencies to introduce themselves.
                                    </div>
                                </div>
                            </div>
                            <div id="div_step4" runat="server" visible="false">
                                <div class="my-card shadow-sm">
                                    <div class="myCard-heading">
                                        <h4>Expertise</h4>
                                    </div>
                                    <div class="myCard-body">
                                        <h6>What skills and expertise are most important to you in Front-End Development</h6>
                                        <br>
                                        <h5>Front-End Development Deliverables (Optional)</h5>
                                        <asp:DropDownList ID="ddlProjectSkills_Deliverable" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                                        <asp:HiddenField ID="hfProjectSkills_Deliverable" runat="server" ClientIDMode="Static" />

                                        <hr>

                                        <h5>Front-End Development Languages (Optional)</h5>
                                        <asp:DropDownList ID="ddlProjectSkills_Languages" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                                        <asp:HiddenField ID="hfProjectSkills_Languages" runat="server" ClientIDMode="Static" />

                                        <hr>

                                        <h5>Front-End Development Skills (Optional)</h5>
                                        <asp:DropDownList ID="ddlProjectSkills_Skills" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                                        <asp:HiddenField ID="hfProjectSkills_Skills" runat="server" ClientIDMode="Static" />

                                        <hr>

                                        <h5>Business Size Experience (Optional)</h5>
                                        <asp:RadioButtonList ID="rbtnlBusinessSize" runat="server" RepeatDirection="Horizontal" CssClass="business-radio-btn">
                                            <asp:ListItem Text="Very Small (1-9 employee)" Value="VerySmall"></asp:ListItem>
                                            <asp:ListItem Text="Small (10-100 employee)" Value="Small"></asp:ListItem>
                                            <asp:ListItem Text="Mid (100-999 employee)" Value="Mid"></asp:ListItem>
                                            <asp:ListItem Text="Large (1000+ employee)" Value="Large"></asp:ListItem>
                                            <asp:ListItem Text="Startup" Value="Startup"></asp:ListItem>
                                            <asp:ListItem Text="Fortune 500" Value="Fortune500"></asp:ListItem>
                                        </asp:RadioButtonList>

                                    </div>
                                </div>

                                <div class="my-card shadow-sm">
                                    <div class="myCard-body">
                                        <h6>What aditional skills and experties are important to you? <span class="get-help" data-toggle="popover" tabindex="0" data-trigger="focus" data-content="List any additional skill you would like the person to have">?</span>
                                            <asp:RequiredFieldValidator ID="rfvAdditionalSkills" runat="server" ControlToValidate="ddlAdditionalSkills"
                                                ErrorMessage="Select aditional skills and experties are important to you" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator></h6>
                                        <asp:DropDownList ID="ddlAdditionalSkills" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>

                                        <asp:HiddenField ID="hfAdditionalSkills" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                            <div id="div_step5" runat="server" visible="false">
                                <div class="my-card shadow-sm">
                                    <div class="myCard-heading">
                                        <h4>Visibility</h4>
                                    </div>
                                    <div class="myCard-body" style="padding-right: 30px">
                                        <h6>Who can see your job?
                                            <asp:RequiredFieldValidator ID="rfvJobVisibility" runat="server" ControlToValidate="rbtnlJobVisibility"
                                                ErrorMessage="Select who can see your job" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </h6>
                                        <div class="row">
                                            <asp:RadioButtonList ID="rbtnlJobVisibility" runat="server" CssClass="special-radio-btn" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<h6>Anyone</h6><p>Public search visibility and our website</p>" Value="Anyone"></asp:ListItem>
                                                <asp:ListItem Text="<h6>Only Trabau talent</h6><p>Visible only through our website</p>" Value="Only Trabau talent"></asp:ListItem>
                                                <asp:ListItem Text="<h6>Invite Only</h6><p>Private only through invitation</p>" Value="Invite Only"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <hr>

                                        <h6>How many people do you need for this job?
                                             <asp:RequiredFieldValidator ID="rfvNoOfFreelancers" runat="server" ControlToValidate="rbtnlNoOfFreelancers"
                                                 ErrorMessage="Select how many people do you need for this job" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </h6>
                                        <div class="row">
                                            <asp:RadioButtonList ID="rbtnlNoOfFreelancers" runat="server" CssClass="special-radio-btn special-radio-btn-override" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rbtnlNoOfFreelancers_SelectedIndexChanged">
                                                <asp:ListItem Text="<h6>One</h6><p>Freelancer</p>" Value="One"></asp:ListItem>
                                                <asp:ListItem Text="<h6>More than one</h6><p>Freelancers</p>" Value="More than one"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <br>

                                        <div id="div_More_Freelancers" runat="server" visible="false">
                                            <h6>Number of freelancers
                                                <asp:RequiredFieldValidator ID="rfvNumberOfFreelancers" runat="server" ControlToValidate="txtNumberOfFreelancers"
                                                    ErrorMessage="Enter Number of freelancers" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                            </h6>
                                            <asp:TextBox ID="txtNumberOfFreelancers" runat="server" CssClass="form-control max-w-input" placeholder="Enter Number of Freelancers"></asp:TextBox>
                                        </div>


                                        <hr>

                                        <h6>Choose Location
                                            <asp:RequiredFieldValidator ID="rfvJobLocation" runat="server" ControlToValidate="ddlJobLocation"
                                                ErrorMessage="Select Location" InitialValue="0" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </h6>
                                        <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="form-control" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlJobLocation_SelectedIndexChanged">
                                        </asp:DropDownList>


                                        <div id="divLocationDistance" runat="server" visible="false" class="margin-top-20">
                                            <h6>Choose Location Distance
                                            <asp:RequiredFieldValidator ID="rfvLocationDistance" runat="server" ControlToValidate="ddlLocationDistance"
                                                ErrorMessage="Select Location Distance" InitialValue="0" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                            </h6>
                                            <asp:DropDownList ID="ddlLocationDistance" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLocationDistance_SelectedIndexChanged">
                                                <asp:ListItem Text="Select Location Distance" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="All USA" Value="All USA"></asp:ListItem>
                                                <asp:ListItem Text="Local Only" Value="Local Only"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="row margin-top-20" id="divLocalLocation" runat="server" visible="false">
                                            <div class="col-sm-6">
                                                <h6>Local Location ZipCode
                                            <asp:RequiredFieldValidator ID="rfvLocalLocation_ZipCode" runat="server" ControlToValidate="txtLocalLocation_ZipCode"
                                                ErrorMessage="Select Local Location ZipCode" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                                </h6>
                                                <asp:TextBox ID="txtLocalLocation_ZipCode" runat="server" CssClass="form-control" placeholder="Local Location ZipCode"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Local Location Distance
                                                <asp:RequiredFieldValidator ID="rfvLocalLocation_Distance" runat="server" ControlToValidate="txtLocalLocation_Distance"
                                                    ErrorMessage="Select Local Location Distance" ValidationGroup="PostJob_Next" CssClass="alert-validation" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvLocalLocation_Distance" runat="server" ControlToValidate="txtLocalLocation_Distance"
                                                        ErrorMessage="Select Location Distance (between 1 and 200 miles)" Type="Integer" MinimumValue="1" MaximumValue="200" ValidationGroup="PostJob_Next" CssClass="alert-validation" Display="Dynamic"></asp:RangeValidator>
                                                </h6>
                                                <asp:TextBox ID="txtLocalLocation_Distance" runat="server" CssClass="form-control distance-input" placeholder="Local Location Distance" MaxLength="3"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="fteLocalLocation_Distance" runat="server" TargetControlID="txtLocalLocation_Distance" FilterMode="ValidChars"
                                                    ValidChars="0123456789" />
                                                <span>miles</span>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="my-card shadow-sm">
                                    <div class="myCard-body">
                                        <h6>Talent Preferences (Optional)</h6>
                                        <p>
                                            Add screening questions and/or require a cover letter<asp:RequiredFieldValidator ID="rfvTalentPreferences" runat="server" ControlToValidate="txtTalentPreferences"
                                                ErrorMessage="Add Talent Preferences screening question text" ValidationGroup="Add_Talent_Screening_Question" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </p>
                                        <asp:TextBox ID="txtTalentPreferences" runat="server" CssClass="form-control" placeholder="Add Talent Preferences screening question"></asp:TextBox>
                                        <b>
                                            <asp:LinkButton ID="lbtnAddTalentPreferencesQuestions" runat="server" Text="+ Add" CssClass="link-btn" OnClick="lbtnAddTalentPreferencesQuestions_Click" ValidationGroup="Add_Talent_Screening_Question"></asp:LinkButton></b>
                                        <br>
                                        <ol class="project-items project-questions">
                                            <asp:Repeater ID="rTalentPreferences" runat="server">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblquestionkey" runat="server" Text='<%#Eval("question_key") %>' Visible="false"></asp:Label>
                                                    <li>
                                                        <a><%#Eval("question") %></a>
                                                        <asp:LinkButton ID="lbtnRemoveTalentPreferences" runat="server" CssClass="remove_item" OnClick="lbtnRemoveTalentPreferences_Click">X</asp:LinkButton>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div id="div_step6" runat="server" visible="false">
                                <div class="my-card shadow-sm">
                                    <div class="myCard-heading">
                                        <h4>Budget</h4>
                                    </div>
                                    <div class="myCard-body" style="padding-right: 30px">
                                        <h6>How would you like to pay the freelancer or contractor or agency?
                                            <asp:RequiredFieldValidator ID="rfvBudgetType" runat="server" ControlToValidate="rbtnlBudgetType"
                                                ErrorMessage="Select how would you like to pay the freelancer or agency" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                        </h6>
                                        <div class="row">
                                            <asp:RadioButtonList ID="rbtnlBudgetType" runat="server" CssClass="special-radio-btn special-radio-btn-override" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rbtnlBudgetType_SelectedIndexChanged">
                                                <asp:ListItem Text="<h6>Pay by the hour</h6><p>Pay the freelancer or contractor or agency hourly to help you solve the problem</p>" Value="Hour"></asp:ListItem>
                                                <asp:ListItem Text="<h6>Pay a fixed Price</h6><p>Pay the freelancer or contractor or agency a fixed price to help you solve the problem</p>" Value="Fixed"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <br>

                                        <h6>Specify details for number of people</h6>
                                        <div class="row row-header">
                                            <div class="col-lg-4 col-md-6 col-sm-12">
                                                <b>Title</b>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-sm-12 people_subrow">
                                                <b>Budget</b>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-sm-12">
                                                <b>Payment Frequency</b>
                                            </div>
                                        </div>
                                        <asp:Repeater ID="rNoOfPeople" runat="server">
                                            <ItemTemplate>
                                                <div class="row people_row">
                                                    <p class="row_srno"><%# Container.ItemIndex + 1 %>.</p>
                                                    <div class="col-lg-4 col-md-6 col-sm-12 people_subrow">
                                                        <asp:Label ID="lblPeopleTitle" runat="server" Visible="false" Text='<%#Eval("Title") %>'></asp:Label>
                                                        <asp:TextBox ID="txtPeopleTitle" runat="server" placeholder="Enter Title" CssClass="form-control"
                                                            Text='<%#Eval("Title") %>'></asp:TextBox>
                                                        <%--  <asp:DropDownList ID="ddlPeopleTitle" runat="server">
                                                        </asp:DropDownList>--%>
                                                        <asp:RequiredFieldValidator ID="rfvPeopleTitle" runat="server" ControlToValidate="txtPeopleTitle"
                                                            ErrorMessage="Enter Title" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-sm-12 people_subrow">
                                                        <asp:TextBox ID="txtPeopleBudget" runat="server" placeholder="Enter budget" CssClass="form-control dollar" MaxLength="10"
                                                            AutoPostBack="true" OnTextChanged="txtPeopleBudget_TextChanged" Text='<%#Eval("Budget") %>'></asp:TextBox>

                                                        <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                                        <asp:RequiredFieldValidator ID="rfvPeopleBudget" runat="server" ControlToValidate="txtPeopleBudget"
                                                            ErrorMessage="Enter Budget" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                                        <cc1:FilteredTextBoxExtender ID="ftePeopleBudget" runat="server" TargetControlID="txtPeopleBudget" FilterMode="ValidChars"
                                                            ValidChars="0123456789." />
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-sm-12">
                                                        <asp:Label ID="lblPaymentFrequency" runat="server" Visible="false" Text='<%#Eval("PaymentFrequency") %>'></asp:Label>
                                                        <asp:DropDownList ID="ddlPaymentFrequency" runat="server">
                                                            <asp:ListItem Text="Select Payment Frequency" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Once" Value="Once"></asp:ListItem>
                                                            <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                                                            <asp:ListItem Text="Biweekly" Value="Biweekly"></asp:ListItem>
                                                            <asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem>
                                                            <asp:ListItem Text="Semimonthly" Value="Semimonthly"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvPaymentFrequency" runat="server" ControlToValidate="ddlPaymentFrequency"
                                                            ErrorMessage="Select Payment Frequency" ValidationGroup="PostJob_Next" InitialValue="0" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <hr />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="">
                                                    <h6>Specify your budget to solve the problem
                                                     <asp:RequiredFieldValidator ID="rfvBudgetValue" runat="server" ControlToValidate="txtBudgetValue"
                                                         ErrorMessage="Specify your budget" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                                                    </h6>
                                                    <asp:TextBox ID="txtBudgetValue" runat="server" placeholder="Enter budget" CssClass="form-control dollar" MaxLength="10"></asp:TextBox>
                                                    <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                                    <cc1:FilteredTextBoxExtender ID="fteBudgetValue" runat="server" TargetControlID="txtBudgetValue" FilterMode="ValidChars"
                                                        ValidChars="0123456789." />

                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="">
                                                    <h6>You can specify bonus in advance (Optional)</h6>
                                                    <asp:TextBox ID="txtBudgetBonusValue" runat="server" placeholder="Enter bonus (optional)" CssClass="form-control dollar" MaxLength="10"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fteBudgetBonusValue" runat="server" TargetControlID="txtBudgetBonusValue" FilterMode="ValidChars"
                                                        ValidChars="0123456789." />
                                                    <i class="fa fa-usd hourly-currency" aria-hidden="true"></i>
                                                </div>
                                            </div>
                                        </div>

                                        <hr>

                                        <h6>What level of experience are you looking for?
                                            <%-- <asp:RequiredFieldValidator ID="rfvExperienceLevel" runat="server" ControlToValidate="rbtnlExperienceLevel"
                                                 ErrorMessage="Select level of experience are you looking for" ValidationGroup="PostJob_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>--%>
                                        </h6>
                                        <div class="row">
                                            <asp:CheckBoxList ID="rbtnlExperienceLevel" runat="server" CssClass="special-btn" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="<i class='flaticon-null-9'></i><h6>Any</h6>" Value="Any"></asp:ListItem>
                                                <asp:ListItem Text="<i class='flaticon-null-4'></i><h6>Entry</h6>" Value="Entry"></asp:ListItem>
                                                <asp:ListItem Text="<i class='flaticon-null-4'></i> <i class='flaticon-null-4'></i><h6>Intermediate</h6>" Value="Intermediate"></asp:ListItem>
                                                <asp:ListItem Text="<i class='flaticon-null-4'></i> <i class='flaticon-null-4'></i> <i class='flaticon-null-4'></i><h6>Expert</h6>" Value="Expert"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="div_step7" runat="server" visible="false">
                                <div class="my-card shadow-sm">
                                    <div class="myCard-heading">
                                        <h4>Review and post</h4>
                                    </div>

                                    <div class="myCard-body">
                                        <h6>Title</h6>
                                        <p>
                                            <asp:Literal ID="ltrlJobTitle_Review" runat="server"></asp:Literal>
                                        </p>

                                        <h6>Job Category</h6>
                                        <p>
                                            <asp:Literal ID="ltrlJobCategory_Review" runat="server"></asp:Literal>
                                        </p>
                                    </div>
                                </div>
                                <div class="my-card shadow-sm">
                                    <div class="myCard-body job-description-review">
                                        <h6>Description</h6>
                                        <p>
                                            <asp:Literal ID="ltrlDescription_Review" runat="server"></asp:Literal>
                                        </p>
                                    </div>
                                </div>
                                <div class="my-card shadow-sm">
                                    <div class="job-posting-review-header">Details</div>
                                    <div class="myCard-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <h6>Type of Work</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlTypeOfWork_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Payment through</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlPaymentThrough_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-12" id="div_screening_ques" runat="server" visible="false">
                                                <h6>Screening Questions</h6>
                                                <ol class="project-questions-preview">
                                                    <asp:Repeater ID="rScreeningQuestions_Review" runat="server">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblquestionkey" runat="server" Text='<%#Eval("question_key") %>' Visible="false"></asp:Label>
                                                            <li>
                                                                <a><%#Eval("question") %></a>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ol>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Require Cover Letter</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlRequireCoverLetter_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="my-card shadow-sm">
                                    <div class="job-posting-review-header">Expertise</div>
                                    <div class="myCard-body">
                                        <div class="row">
                                            <div class="col-sm-6" id="div_FE_Deliverables" runat="server" visible="false">
                                                <h6>Front-End Development Deliverables</h6>
                                                <ul class="tags" style="padding-left: 0">
                                                    <asp:Repeater ID="rFE_Deliverables_Preview" runat="server">
                                                        <ItemTemplate>
                                                            <li class="ticked"><%#Eval("Text") %></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <div class="col-sm-6" id="div_FE_Languages" runat="server" visible="false">
                                                <h6>Front-End Development Languages</h6>
                                                <ul class="tags" style="padding-left: 0">
                                                    <asp:Repeater ID="rFE_Languages_Preview" runat="server">
                                                        <ItemTemplate>
                                                            <li class="ticked"><%#Eval("Text") %></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <div class="col-sm-6" id="div_FE_DevSkills" runat="server" visible="false">
                                                <h6>Front-End Development Skills</h6>
                                                <ul class="tags" style="padding-left: 0">
                                                    <asp:Repeater ID="rFE_DevSkills_Preview" runat="server">
                                                        <ItemTemplate>
                                                            <li class="ticked"><%#Eval("Text") %></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Businees Size Experience</h6>
                                                <ul class="tags" style="padding-left: 0">
                                                    <li class="ticked">
                                                        <asp:Literal ID="ltrlBusinessSize_Preview" runat="server"></asp:Literal></li>
                                                </ul>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Additional Skills</h6>
                                                <ul class="tags" style="padding-left: 0">
                                                    <asp:Repeater ID="rAdditionalSkills_Preview" runat="server">
                                                        <ItemTemplate>
                                                            <li class="ticked"><%#Eval("Text") %></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="my-card shadow-sm">
                                    <div class="job-posting-review-header">Visibility</div>
                                    <div class="myCard-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <h6>Job Posting Visibility</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlJobVisibility_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>How many people do you need for this job?</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlJobNoOfPeople_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Job Location</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlJobLocation_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6" id="divLocationDistance_Review" runat="server" visible="false">
                                                <h6>Location Distance Type</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlLocationDistance" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6" id="divLocationZipCode_Review" runat="server" visible="false">
                                                <h6>Location Zip Code</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlLocationZipCode_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6" id="divLocationMiles_Review" runat="server" visible="false">
                                                <h6>Location Distance</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlLocationDistance_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-12" id="div_TP_ScreeningQues" runat="server" visible="false">
                                                <h6 class="margin-top-20">Talent Preferences Screening Questions</h6>
                                                <ol class="project-questions-preview">
                                                    <asp:Repeater ID="rTP_ScreeningQuestions_Review" runat="server">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblquestionkey" runat="server" Text='<%#Eval("question_key") %>' Visible="false"></asp:Label>
                                                            <li>
                                                                <a><%#Eval("question") %></a>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ol>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="my-card shadow-sm">
                                    <div class="job-posting-review-header">Budget</div>
                                    <div class="myCard-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <h6>Budget (Hourly or Fixed Price)</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlBudgetType_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Budget</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlBudgetValue_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Bonus Budget</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlBudgetBonusValue_Review" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                            <div class="col-sm-6">
                                                <h6>Level of Experience</h6>
                                                <ul class="tags" style="padding-left: 0">
                                                    <asp:Repeater ID="rLevelOfExperience" runat="server">
                                                        <ItemTemplate>
                                                            <li class="ticked"><%#Eval("Text") %></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="submit-form-btn">
                                <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" class="cta-btn-md" />
                                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" class="cta-btn-md" ValidationGroup="PostJob_Next" />
                                <asp:ValidationSummary ID="vsPostJobValidation_Summary" runat="server" ValidationGroup="PostJob_Next" ShowMessageBox="true" ShowSummary="false" />
                            </div>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </section>
</asp:Content>

