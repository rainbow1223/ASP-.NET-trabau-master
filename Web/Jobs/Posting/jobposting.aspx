<%@ Page Title="Job Posting" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="jobposting.aspx.cs" Inherits="Jobs_Posting_jobposting"
    ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/jquery.dropdown.min.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/css/jquery.timepicker.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/js/jquery.dropdown.js") %>'></script>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("jobposting.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/jquery-timepicker/js/jquery.timepicker.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/AutoComplete/js/AutoCompleteText.js") %>' type="text/javascript"></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/job_posting.js?version=2.9") %>'></script>
    <style>
        .row_srno {
            top: 1px;
        }

        .people_row hr {
            margin-top: 1rem;
        }
        ul[class*="ui-autocomplete"] {
            z-index: 9999;
        }
    </style>
    <div class="problem-heading p-80 pt-0">
        <h3>
            <asp:Label ID="ltrlJobTitle" runat="server"></asp:Label></h3>
    </div>

    <div class="details-tabs myTabsSection">
        <asp:UpdatePanel ID="upJobPosting" runat="server" class="row" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="col-xl-3 col-lg-4 mb-3 pr-0">
                    <ul class="nav nav-pills flex-column d-none d-lg-block" role="tablist">
                        <li class="nav-item">
                            <asp:LinkButton ID="lbtnTabViewJobPost" runat="server" OnClick="lbtnTabViewJobPost_Click" Text="<i class='flaticon-null'></i>View Job Post" CssClass="nav-link"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lbtnTabInvite" runat="server" OnClick="lbtnTabInvite_Click" Text="<i class='flaticon-null-1'></i>Invite Freelancers" CssClass="nav-link"></asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="proposals-tab" onclick="LoadProposals()" data-toggle="tab" href="#div_proposals" role="tab" aria-controls="contact" aria-selected="false"><i class="flaticon-null-2"></i>Review Proposals</a>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lbtnTabHire" runat="server" OnClick="lbtnTabHire_Click" Text="<i class='flaticon-null-3'></i>Hire" CssClass="nav-link"></asp:LinkButton>
                        </li>
                    </ul>
                    <div class="select-box-common d-block d-lg-none">
                        <asp:DropDownList ID="ddlJobPostingMenu" runat="server" CssClass="form-control">
                            <asp:ListItem Value="VJP" Text="View Job Post"></asp:ListItem>
                            <asp:ListItem Value="IF" Text="Invite Freelancers"></asp:ListItem>
                            <asp:ListItem Value="RP" Text="Review Proposals"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Hire"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <!-- /.col-md-4 -->
                <div class="col-xl-9 col-lg-8 pl-0">
                    <div class="tab-content" id="myTabContent">
                        <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                            <div class="dt-block">
                                <h3>Job Details</h3>
                                <div class="job-description">
                                    <asp:Literal ID="ltrlJobDescription" runat="server"></asp:Literal>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-12">
                                        <h3 class="no-margin-bottom">Job, Budget, and Experience</h3>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="jd-opt">
                                            <i class="flaticon-null-11"></i>
                                            <div class="jd-heading">
                                                <h6>
                                                    <asp:Literal ID="ltrlJobCategory" runat="server"></asp:Literal></h6>
                                                <p>
                                                    <asp:Literal ID="ltrlJobPostedOn" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="jd-opt">
                                            <i class="flaticon-null-4"></i>
                                            <div class="jd-heading">
                                                <h6>
                                                    <asp:Literal ID="ltrlBudgetValue" runat="server"></asp:Literal></h6>
                                                <p>
                                                    <asp:Literal ID="ltrlBudgetType" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="jd-opt">
                                            <i class="flaticon-null-5"></i>
                                            <div class="jd-heading">
                                                <h6>
                                                    <asp:Literal ID="ltrlExperienceLevel" runat="server"></asp:Literal></h6>
                                                <p>
                                                    <asp:Literal ID="ltrlExperienceInfo" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="jd-opt">
                                            <i class="fa fa-map-marker" aria-hidden="true"></i>
                                            <div class="jd-heading">
                                                <h6>Location</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlLocationInfo" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="jd-opt">
                                            <i class="fa fa-credit-card" aria-hidden="true"></i>
                                            <div class="jd-heading">
                                                <h6>Payment Type</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlPaymentType" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="jd-opt">
                                            <i class="fa fa-tasks" aria-hidden="true"></i>
                                            <div class="jd-heading">
                                                <h6>Type Of Work</h6>
                                                <p>
                                                    <asp:Literal ID="ltrlTypeOfWork" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <hr>

                            <div class="dt-block">
                                <h3>Skills and expertise</h3>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="jd-opt">
                                            <div class="jd-heading">
                                                <h6>Front-End Deliverables</h6>
                                                <ul class="tags">
                                                    <asp:Repeater ID="rFE_Deliverables" runat="server">
                                                        <ItemTemplate>
                                                            <li class="ticked"><%#Eval("Text") %></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="jd-opt">
                                            <div class="jd-heading">
                                                <h6>Front-End Languages</h6>
                                                <ul class="tags">
                                                    <asp:Repeater ID="rFE_Languages" runat="server">
                                                        <ItemTemplate>
                                                            <li class="ticked"><%#Eval("Text") %></li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="jd-opt">
                                            <div class="jd-heading">
                                                <h6>Additional Skills</h6>
                                                <ul class="tags">
                                                    <asp:Repeater ID="rAdditionalSkills" runat="server">
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

                            <hr>

                            <div class="dt-block">
                                <h3>Additional project files</h3>
                                <div class="row">
                                    <ol class="project-items">
                                        <asp:Repeater ID="rProfileFiles" runat="server">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfilekey" runat="server" Text='<%#Eval("file_key") %>' Visible="false"></asp:Label>
                                                <li>
                                                    <asp:LinkButton ID="lbtnDownloadFile" runat="server" Text='<%#Eval("file_name") %>' OnClick="lbtnDownloadFile_Click"></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="empty-data" id="div_profile_files_empty" runat="server" visible="false">No files added</div>
                                    </ol>
                                </div>
                            </div>

                            <hr />
                            <div class="dt-block">
                                <h3>Number of people needed for this job:
                                    <asp:Label ID="lblNoOfPeople" runat="server"></asp:Label></h3>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col">Position Number</th>
                                            <th scope="col">Title</th>
                                            <th scope="col">Budget</th>
                                            <th scope="col">Payment Frequency</th>
                                            <th scope="col">Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rNoOfPeople" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex + 1 %>.</td>
                                                    <td>
                                                        <asp:Label ID="lblPeopleTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblPeopleBudgetValue" runat="server" Text='<%#Eval("Budget") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblPeopleBudget" runat="server" Text='<%#"$"+Eval("Budget").ToString() %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblPaymentFrequency" runat="server" Text='<%#Eval("PaymentFrequency") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblPositionStatus" runat="server" Text='<%#Eval("PositionStatus") %>'></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr id="tr_Empty_People" runat="server" visible="false">
                                            <td class="text-center text-danger td-empty" colspan="5">Number of people details not defined for this job 
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td></td>
                                            <td><b>Total</b></td>
                                            <td colspan="2">
                                                <asp:Label ID="lblTotalPeopleBudget" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                            </div>
                            <hr />
                            <div class="dt-block">
                                <h3>Activity on this job</h3>
                                <ul>
                                    <li>Proposals: <b>
                                        <asp:Literal ID="ltrlProposalsCount" runat="server"></asp:Literal></b></li>
                                    <li>Interviewing: <b>
                                        <asp:Literal ID="ltrlInterviewingCount" runat="server"></asp:Literal></b></li>
                                    <li>Invites sent: <b>
                                        <asp:Literal ID="ltrlInvitesCount" runat="server"></asp:Literal></b></li>
                                    <li>Unanswererd invites: <b>
                                        <asp:Literal ID="ltrlUnanswererdInvitesCount" runat="server"></asp:Literal></b></li>
                                </ul>
                            </div>


                            <div class="dt-block dt-block-ft">
                                <ul>
                                    <asp:Repeater ID="rPostedJobMenu" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblMenuCode" runat="server" Text='<%#Eval("MenuCode") %>' Visible="false"></asp:Label>
                                                <a id="lbtnMenuLink" onclick="RemovalConfirmation(this)" runat="server" visible='<%#Eval("ConfirmRequired") %>'><%#Eval("Name") %></a>
                                                <asp:LinkButton ID="lbtnConfirmation_MenuLink" runat="server" Text='<%#Eval("Name") %>' Style='<%#Eval("Display") %>' OnClick="lbtnMenuLink_Click"
                                                    ClientIDMode="AutoID"></asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>

                        </div>



                        <div class="tab-pane fade" id="invite" role="tabpanel" aria-labelledby="invite-tab-main">
                            <div class="dt-block">

                                <div class="borderLine-tabs">
                                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lbtnChildSearchTab" runat="server" OnClick="lbtnChildSearchTab_Click" CssClass="nav-link" Text-="Search"></asp:LinkButton>
                                        </li>
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lbtnChildInviteTab" runat="server" OnClick="lbtnChildInviteTab_Click" CssClass="nav-link" Text-="Invited Freelancers"></asp:LinkButton>
                                        </li>
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lbtnChildHireTab" runat="server" OnClick="lbtnChildHireTab_Click" CssClass="nav-link" Text-="My Hires"></asp:LinkButton>
                                        </li>
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lbtnChildSavedTab" runat="server" OnClick="lbtnChildSavedTab_Click" CssClass="nav-link" Text-="Saved"></asp:LinkButton>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane fade show active" id="search-f-tab" role="tabpanel" aria-labelledby="search-tab">
                                            <asp:UpdatePanel ID="upSearch_Freelancers" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="search-wrapper searchFreelancers-form" id="div_search_FL" runat="server">
                                                        <div class="header-search" style="z-index: 99;">
                                                            <div class="search-dd">
                                                                <button type="button" class="search-dd-icon"><i class="flaticon-search"></i></button>
                                                                <%-- <button type="button" class="angle-dd-icon" data-jq-dropdown="#jq-dropdown-2"><i class="flaticon-down-chevron"></i></button>--%>
                                                            </div>
                                                            <asp:TextBox ID="txtSearch_FL_JobPosting" runat="server" CssClass="form-control search-freelancers" ClientIDMode="Static"
                                                                placeholder="Freelancers, Contractors, & Agencies"></asp:TextBox>
                                                            <asp:LinkButton ID="lbtnSearch" runat="server" Text="<i class='flaticon-search'></i> Search" OnClick="lbtnSearch_Click" CssClass="cta-btn-md" />
                                                            <%-- <button type="button" class="search_close"><i class="flaticon-close" style="display: none"></i></button>--%>

                                                            <%--                                                <div id="jq-dropdown-2" class="jq-dropdown dd-dropdown-search">
                                                    <ul class="jq-dropdown-menu">
                                                        <li><a onclick="ChangeDropDown(this,'txtSearch_FL_JobPosting')">Freelancers &amp; Agencies</a></li>
                                                        <li><a onclick="ChangeDropDown(this,'txtSearch_FL_JobPosting')">Jobs</a></li>
                                                    </ul>
                                                </div>--%>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <asp:Repeater ID="rFreelancers" runat="server">
                                                            <ItemTemplate>
                                                                <div class="col-xl-6 col-lg-6 col-sm-6 freelancer-row">
                                                                    <div class="fs-thumbnail shadow-sm mb-4">
                                                                        <div class="fs--img-thumb"></div>
                                                                        <div class="fs-profile">
                                                                            <div class="profilefoto" id="div_profile_photo" runat="server">
                                                                                <img alt="user" runat="server" id="imgFL_ProfilePic" src="" />
                                                                            </div>
                                                                            <div class="fs-view-profile"><a target="_blank" id="a_profile" runat="server" class="cta-btn-sm" data-toggle="popover" data-trigger="focus" data-content="View Profile">View Profile</a></div>
                                                                            <div class="profileRating">
                                                                                <h5><%#Eval("JobSuccessRate") %></h5>
                                                                                <p><%#Eval("CountryName") %></p>
                                                                            </div>
                                                                        </div>
                                                                        <div class="fs-content-thumb">
                                                                            <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' Visible="false"></asp:Label>
                                                                            <h4><%#Eval("Name") %></h4>
                                                                            <p class="expertskills"><%#Eval("Title") %></p>
                                                                            <ul class="tags">
                                                                                <asp:Repeater ID="rFreelancers_Skills" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <li><%#Eval("SkillName") %></li>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </ul>

                                                                            <div runat="server" id="div_actions">
                                                                                <div class="div-save-action">
                                                                                    <asp:Label ID="lblSaved" runat="server" Text='<%#Eval("Saved") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="lbtnSave" runat="server" class="edit-pencil-button" Text='<%#Eval("SavedText") %>' OnClick="lbtnSave_Click" ClientIDMode="AutoID" data-toggle="popover" TabIndex="0" data-trigger="focus" data-content="Save Freelancer"></asp:LinkButton>
                                                                                </div>

                                                                                <div class="div-profile-action">
                                                                                    <%--<a id="lbtnHire" runat="server" class='<%#"cta-btn-sm btn-profile-action btn-hire"+Eval("Hired_Class").ToString() %>'><%#Eval("HiredText") %></a>--%>

                                                                                    <asp:LinkButton ID="lbtnHire" runat="server" class="cta-btn-sm btn-profile-action" Text="Hire" OnClick="lbtnHire_Click" Visible='<%# !Convert.ToBoolean(Eval("Hired").ToString()) %>' data-toggle="popover" data-trigger="focus" data-content="Hire Freelancer"></asp:LinkButton>
                                                                                    <asp:Label ID="lblHiredText" runat="server" Text='<%#Eval("HiredText") %>' Visible='<%# Convert.ToBoolean(Eval("Hired").ToString()) %>' CssClass="cta-btn-sm btn-profile-action disabled" data-toggle="popover" data-trigger="focus" data-content="Hired"></asp:Label>

                                                                                    <asp:LinkButton ID="lbtnInvite" runat="server" class="cta-btn-sm btn-profile-action" Text="Invite" OnClick="lbtnInvite_Click" Visible='<%# !Convert.ToBoolean(Eval("Invited").ToString()) %>' data-toggle="popover" data-trigger="focus" data-content="Invite Freelancer"></asp:LinkButton>
                                                                                    <asp:Label ID="lblInvitedText" runat="server" Text='<%#Eval("InvitedText") %>' Visible='<%# Convert.ToBoolean(Eval("Invited").ToString()) %>' CssClass="cta-btn-sm btn-profile-action disabled" data-toggle="popover" data-trigger="focus" data-content="Invited"></asp:Label>

                                                                                    <a id="lbtnAddToPrefer" runat="server" class='<%#"cta-btn-sm btn-profile-action btn-prefer-list"+Eval("Prefer_Class").ToString() %>' data-toggle="popover" data-trigger="focus" data-content='<%#Eval("AddedText_Tooltip") %>'><%#Eval("AddedText") %></a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>


                        <div class="tab-pane fade" id="div_proposals" role="tabpanel" aria-labelledby="proposals-tab">
                        </div>

                        <div class="tab-pane fade" id="hiretb" role="tabpanel" aria-labelledby="hiree-tab">
                        </div>


                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <div id="divTrabau_SendJobToFriend" class="modal fade" role="dialog">

            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Send Job To Friend</h4>
                        <input id="btnClose" type="button" value="&times;" class="close" onclick="HandlePopUp('0','divTrabau_SendJobToFriend');" />
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <asp:HiddenField ID="hfFriendUserId" runat="server" ClientIDMode="Static" />
                            <div class="col-sm-12">
                                <label>Enter Friend Name</label>
                                <asp:TextBox ID="txtFriendName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-sm-12">
                                <label>Enter Friend Email Address</label>
                                <asp:TextBox ID="txtFriendEmailAddress" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="col-sm-12">
                                <asp:Button ID="btnSendJobToFriend" runat="server" CssClass="cta-btn-sm" Text="Send Job to Friend" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

