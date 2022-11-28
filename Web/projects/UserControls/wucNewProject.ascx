<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucNewProject.ascx.cs" Inherits="projects_UserControls_wucNewProject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="projectSteps-bg p-80">
    <div class="container">
        <div class="steps-point-circle">
            <ul>
                <li id="li_step0" runat="server" class="process"></li>
                <li id="li_step1" runat="server"></li>
                <li id="li_step2" runat="server"></li>
            </ul>
        </div>
        <div class="steps-point-heading">
            <h3>
                <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal></h3>
            <span>Step
                                <asp:Literal ID="ltrlPageNo" runat="server"></asp:Literal>
                of 3</span>
        </div>
    </div>
</div>
<div class="new-project-steps p-80">
    <div class="warning-content-message">
        <asp:ValidationSummary ID="vsPostJobValidation_Summary" runat="server" ValidationGroup="NewProject_Next" ShowMessageBox="false" ShowSummary="false" />
    </div>
    <div class="container warning-container">
        <div class="warning-content-config">
            <h6>Inline Warning
                                <asp:CheckBox ID="chkWarningInlineConfig" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkWarningInlineConfig_CheckedChanged" /></h6>
        </div>
        <div id="div_step0" runat="server" visible="false">
            <div class="my-card shadow-sm">
                <div class="myCard-heading">
                    <h4>Project Type</h4>
                </div>
                <div class="myCard-body" id="div_min_step1" runat="server">
                    <div class="row">
                        <div class="col-sm-12">
                            <h6>Project Type
                                            <asp:RequiredFieldValidator ID="rfvProjectType" runat="server" ControlToValidate="rbtnlProjectType"
                                                ErrorMessage="Select Project Type" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:RadioButtonList ID="rbtnlProjectType" runat="server" CssClass="radio-btn">
                                <asp:ListItem Text="Post-hiring Project" Value="Post"></asp:ListItem>
                                <asp:ListItem Text="Pre-hiring Project" Value="Pre"></asp:ListItem>
                                <asp:ListItem Text="Other Project" Value="O"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                    </div>
                </div>
                <div class="myCard-body" id="div_min_step2" runat="server" visible="false">
                    <div class="row">
                        <div class="col-sm-12">
                            <h6>Other Project Type
                                            <asp:RequiredFieldValidator ID="rfvOtherProject_Step2" runat="server" ControlToValidate="rbtnlOtherProject_Step1"
                                                ErrorMessage="Select Other Project Type" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:RadioButtonList ID="rbtnlOtherProject_Step1" runat="server" CssClass="radio-btn">
                            </asp:RadioButtonList>
                        </div>

                    </div>
                </div>
                <div class="myCard-body" id="div_min_step3" runat="server" visible="false">
                    <div class="row">
                        <div class="col-sm-12">
                            <h6><span id="spn_otherproject_step2" runat="server"></span>
                                <asp:RequiredFieldValidator ID="rfvOtherProject_Step3" runat="server" ControlToValidate="rbtnlOtherProject_Step2"
                                    ErrorMessage="Select Other Project Type" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:RadioButtonList ID="rbtnlOtherProject_Step2" runat="server" CssClass="radio-btn">
                            </asp:RadioButtonList>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div id="div_step1" runat="server" visible="false">
            <div class="my-card shadow-sm">
                <div class="myCard-heading">
                    <h4>About the project</h4>
                </div>
                <div class="myCard-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <h6>Project Name <span class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="This is the name of the project, for example, Logo Design for my ecommerce website, logo design for my company, ABC Game development, my logo design project, 1985 Honda Civic Radiator Replacement, Kitchen Faucets Replacement,  Water Heater Replacement, Buying a House in Washington DC, Buying a car from ABC dealer, etc. (this is simply a phrase, a group of words, etc.) You can also add: the house renovation project, radiator replacement project, my logo design project, my video project, picture editing project, The Data Reader Project.">!</span>
                                <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName"
                                    ErrorMessage="Project name cannot be empty. A project must have a name" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:TextBox ID="txtProjectName" runat="server" placeholder="Enter Project Name" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <h6>Company Name
                                            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="ddlCompany"
                                                ErrorMessage="Select company name" InitialValue="0" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:DropDownList ID="ddlCompany" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-12 mt-3">
                            <h6>Application Name <span class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="Application is what give you an idea of what to do. Example of application names depend on project. Project name is Logo Design for ABCD website then Application Name can be ABCD Logo. Project name: 1985 Honda Civic Radiator Replacement, then Application Name can be: Radiator or Honda Civic Radiator or 1985 Civic Radiator. This is simply a noun.">!</span>
                                <asp:RequiredFieldValidator ID="rfvApplicationName" runat="server" ControlToValidate="txtApplicationName"
                                    ErrorMessage="Enter the application name" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:TextBox ID="txtApplicationName" runat="server" placeholder="Enter Application Name" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12" id="div_comm_function" runat="server">
                            <h6>Communication Function <span class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="This is simply the function of the project or the main function of the project: for example, Buy House, Design Logo, Replace Water Heater, Replace Radiator, Design Website, etc.  This is simply a verb.">!</span>
                                <asp:RequiredFieldValidator ID="rfvCommunicationFunction" runat="server" ControlToValidate="ddlCommunicationFunction"
                                    ErrorMessage="Select the communication function" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:DropDownList ID="ddlCommunicationFunction" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-12 mt-3" id="div_other_comm_func" style="display: none;">
                            <h6><span id="spn_comm_function">Other Communication Function</span> <span id="other_comm_func_help" class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="Other Communication Function">!</span>
                                <asp:RequiredFieldValidator ID="rfvOtherCommFunction" runat="server" ControlToValidate="txtOtherCommFunction"
                                    ErrorMessage="Enter other communication function" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:TextBox ID="txtOtherCommFunction" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 mt-3">
                            <h6>Manager Name
                                            <asp:RequiredFieldValidator ID="rfvManagerName" runat="server" ControlToValidate="txtManagerName"
                                                ErrorMessage="Enter the manager name" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:TextBox ID="txtManagerName" runat="server" placeholder="Enter Manager Name" class="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <h6>Description of the project <span class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="Provide a description for the project: example, types of projects with a description (this can be a couple of sentences, a paragraph, or multiple paragraphs). For example, for Logo Design for ABCD Website: A good description is: This project enables the creation of a visual entity for our organization. Once people visit our website and see that image, they can quickly identify us. Whenever that image is identified, people can quickly identify our organization. This logo must include the name of our organization as well as an image to match with that name. <a id='vm_desc'>View More</a>">!</span>
                                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                    ErrorMessage="Enter the description of the peoject" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <div class="dx-viewport">
                                <div class="html-editor" id="div_project_desc" runat="server">
                                </div>
                            </div>
                            <asp:HiddenField ID="hfProjectDescription" runat="server" />
                            <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description of the Project" Style="z-index: -1; position: absolute; top: 120px; width: 0;"></asp:TextBox>

                            <div id="divTrabau_moreproject_description" class="modal fade" role="dialog">

                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">

                                        <!-- Modal Header -->
                                        <div class="modal-header myCard-heading">
                                            <h4 class="modal-title">Project Description</h4>
                                            <input id="btnClose" type="button" value="&times;" class="close white" onclick="HandlePopUp('0','divTrabau_moreproject_description');" />
                                        </div>

                                        <!-- Modal body -->
                                        <div class="modal-body">
                                            <p>Provide a description for the project: example, types of project with a description (this can be a couple of sentences, a paragraph, or multiple paragraphs). </p>
                                            <p><u>Logo Design for ABCD Website:</u> This project enables the creation of a visual entity for our organization. Once people visit our website and see that image, they can quickly identify us. Whenever that image is identified, people can quickly identify our organization. This logo must include the name of our organization as well as an image to match with that name.</p>
                                            <p><u>1985 Honda Civic Radiator Replacement:</u> The radiator replacement project enables the replacement of the radiator in my 1985 Honda Civic.</p>
                                            <p><u>Buying a House in Washington DC:</u> This project is to buy the house that is located at a specific address in Washington DC. Note: You can provide the exact address of the house.</p>
                                            <p><u>ABC Game Development:</u> This project enables the development of a game that can be used by any age in mobile platform.</p>
                                            <p><u>Buying a Car from ABC Dealer:</u> This project is to buy a Ford Escape at ABC Dealer that is located at the corner of 16th street and Park Avenue next to the XYZ shopping mall.</p>
                                            <p><u>Video editing:</u> This project enables the editing of the video that I recorded in the trial yesterday. Adding text throughout the video provides viewers with more information about the trial.</p>
                                            <p><u>House renovation:</u> This project is undertaken to enable the replacement of the roof in my house and the cabinet doors in the kitchen. A granite countertop will replace the older one as well as the sink will be replaced.</p>
                                            <p><u>Project to read data from instrument:</u> The data reader project enables the reading of data from an external instrument attached to a computer. Once the data is read, it can be displayed to a grid where further analysis can be done with the data. After displaying the data to a grid, it is also possible to display the data graphically.</p>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 mt-4">
                            <h6>Additional project files (optional):</h6>
                            <asp:Button ID="btnAddProjectFiles" runat="server" ClientIDMode="AutoID" OnClick="btnAddProjectFiles_Click" Style="display: none" />
                            <div class="form-control file-upload text-center">
                                <cc1:AsyncFileUpload ID="afuProjectFiles" runat="server" OnClientUploadStarted="StartUpload"
                                    OnClientUploadComplete="UploadComplete" OnUploadedComplete="afuProjectFiles_UploadedComplete" Style="position: relative; z-index: 999; width: 100%;" />
                                <p class="progress-bar-percentage">0%</p>
                                <div class="progress-bar-parent">
                                    <div class="progress-bar progress-bar-override" style="width: 0%;"></div>
                                </div>
                            </div>
                            <br />
                            <%-- <ol class="project-items">
                                                <asp:Repeater ID="rProjectFiles" runat="server">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfilekey" runat="server" Text='<%#Eval("file_key") %>' Visible="false"></asp:Label>
                                                        <li>
                                                            <asp:LinkButton ID="lbtnDownloadFile" runat="server" Text='<%#Eval("file_name") %>' OnClick="lbtnDownloadFile_Click"></asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnRemoveFile" runat="server" CssClass="remove_item" OnClick="lbtnRemoveFile_Click">X</asp:LinkButton>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ol>--%>
                            <asp:GridView ID="rProjectFiles" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfilekey" runat="server" Text='<%#Eval("file_key") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblfilename" runat="server" Text='<%#Eval("file_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfilesize" runat="server" Text='<%#Eval("file_size") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Download">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDownloadFile" runat="server" Text="Download" OnClick="lbtnDownloadFile_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnRemoveFile" runat="server" OnClick="lbtnRemoveFile_Click">Remove</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="div_step2" runat="server" visible="false">
            <div class="my-card shadow-sm">
                <div class="myCard-heading">
                    <h4>Project Schedule & Budget</h4>
                </div>
                <div class="myCard-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <h6>Start Date <span class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="The date to start the project">!</span>
                                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                    ErrorMessage="Enter the start date" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:TextBox ID="txtStartDate" runat="server" placeholder="Enter Start Date" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <h6>End Date <span class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="The date to end the project">!</span>
                                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                    ErrorMessage="Enter the end date" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:TextBox ID="txtEndDate" runat="server" placeholder="Enter End Date" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <h6>Start Time
                                            <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime"
                                                ErrorMessage="Enter the start time" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:HiddenField ID="hfStartTime" runat="server" />
                            <asp:TextBox ID="txtStartTime" runat="server" placeholder="Enter Start Time" class="form-control" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <h6>End Time
                                            <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ControlToValidate="txtEndTime"
                                                ErrorMessage="Enter the end time" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:HiddenField ID="hfEndTime" runat="server" />
                            <asp:TextBox ID="txtEndTime" runat="server" placeholder="Enter End Time" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-12">
                            <h6>Budget Type
                                            <asp:RequiredFieldValidator ID="efvBudgetType" runat="server" ControlToValidate="ddlBudgetType"
                                                ErrorMessage="Select Budget Type" ValidationGroup="NewProject_Next" CssClass="alert-validation" InitialValue="0"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:DropDownList ID="ddlBudgetType" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBudgetType_SelectedIndexChanged">
                                <asp:ListItem Text="Select Budget Type" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Hourly" Value="H"></asp:ListItem>
                                <asp:ListItem Text="Fixed Price" Value="F"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-12" id="div_hourly" runat="server" visible="false">
                            <h6>Total Hours <span class="get-help tow-tooltip" data-toggle="popover" tabindex="0" data-trigger="focus" data-original-title="" title="" data-content="The total number of hours for the project; for example, 500 hours">!</span>
                                <asp:RequiredFieldValidator ID="rfvHours" runat="server" ControlToValidate="txtTotalHours"
                                    ErrorMessage="Enter total hours" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>
                            </h6>
                            <asp:TextBox ID="txtTotalHours" runat="server" placeholder="Enter Total Hours" class="form-control"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="fteTotalHours" runat="server" TargetControlID="txtTotalHours" FilterMode="ValidChars"
                                ValidChars="0123456789" />
                        </div>
                        <div class="col-sm-12">
                            <h6>Additional Information (Optional)
                                           <%-- <asp:RequiredFieldValidator ID="rfvAdditionalInfo" runat="server" ControlToValidate="txtAdditionalInfo"
                                                ErrorMessage="Enter the Additional Information" ValidationGroup="NewProject_Next" CssClass="alert-validation"></asp:RequiredFieldValidator>--%>
                            </h6>
                            <asp:TextBox ID="txtAdditionalInfo" runat="server" placeholder="Enter Additional Information" class="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="submit-form-btn">
            <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" class="cta-btn-md" />
            <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" class="cta-btn-md" ValidationGroup="NewProject_Next" />
        </div>
    </div>
</div>
