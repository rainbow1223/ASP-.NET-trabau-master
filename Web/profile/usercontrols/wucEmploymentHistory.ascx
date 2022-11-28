<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucEmploymentHistory.ascx.cs" Inherits="profile_usercontrols_wucEmploymentHistory" %>
<script>
    function LoadEmploymentEvents() {
        $(document).ready(function () {
            $('.employment-item').mouseover(function () {
                $(this).find('div[class*="edit-button"]').show();
            });
            $('.employment-item').mouseout(function () {
                $(this).find('div[class*="edit-button"]').hide();
            });
        });
    }
</script>
<div class="airCardHeader d-flex align-items-center">
    <h2>Employment History</h2>
    <div class="ml-auto editCard-btn">
        <asp:LinkButton ID="lbtnAddEmploymentHistory" runat="server" Text="<i class='fa fa-plus' aria-hidden='true'></i>" OnClick="lbtnAddEmploymentHistory_Click"
            CssClass="edit-pencil-button"></asp:LinkButton>
    </div>
</div>
<div class="airCardBody padding-20 employment-card-body">
    <div class="card-body-content">
        <asp:Repeater ID="rEmployment" runat="server">
            <ItemTemplate>
                <div class="row employment-item" style="padding: 10px;">
                    <asp:Label ID="lblEmploymentId" runat="server" Visible="false" Text='<%#Eval("EmploymentId") %>'></asp:Label>
                    <div class="col-sm-10" style="display: inline-block;">
                        <%#Eval("FullDetails") %>
                        <br />
                        <%#Eval("YearDetails") %>
                    </div>
                    <div class="col-sm-2 edit-button">
                        <asp:LinkButton ID="lbtnEditEmployment" runat="server" ClientIDMode="AutoID" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditEmployment_Click"
                            CssClass="edit-pencil-button" Visible='<%#Eval("EditMode") %>'></asp:LinkButton>
                    </div>
                </div>
                <hr />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

<div id="divTrabau_AddEmployment" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">

            <!-- Modal Header -->
            <div class="modal-header">
                <h4 class="modal-title">
                    <asp:Literal ID="ltrlEmployment_Header" runat="server"></asp:Literal></h4>
                <asp:Button ID="btnClose_employment_top" runat="server" Text="&times;" OnClick="btnCloseEmployment_Click" CssClass="close" />
            </div>

            <!-- Modal body -->
            <asp:UpdatePanel ID="UpEmployment" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-body" id="div_employment_content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtCompanyName">Company</label>
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtCompanyName" SetFocusOnError="true" ErrorMessage="Enter Company Name" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="txtCityName">City Name</label>
                                    <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtCityName" SetFocusOnError="true" ErrorMessage="Enter City Name" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlCountry">Country</label>
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" autocomplete="Off"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlCountry" SetFocusOnError="true" ErrorMessage="Select Country" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtEmploymentTitle">Title</label>
                                    <asp:TextBox ID="txtEmploymentTitle" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtEmploymentTitle" SetFocusOnError="true" ErrorMessage="Enter Title" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="ddlRole">Role</label>
                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlRole" SetFocusOnError="true" ErrorMessage="Select Role" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlPeriodFrom_Month">Period From</label>
                                    <asp:DropDownList ID="ddlPeriodFrom_Month" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodFrom_Month" SetFocusOnError="true" ErrorMessage="Select Period From Month" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlPeriodFrom_Year">&nbsp;</label>
                                    <asp:DropDownList ID="ddlPeriodFrom_Year" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodFrom_Year" SetFocusOnError="true" ErrorMessage="Select Period From Year" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6" id="div_Period_To_Month" runat="server">
                                <div class="form-group">
                                    <label for="ddlPeriodTo_Month">Period To</label>
                                    <asp:DropDownList ID="ddlPeriodTo_Month" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodTo_Month" SetFocusOnError="true" ErrorMessage="Select Period To Month" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6" id="div_Period_To_Year" runat="server">
                                <div class="form-group">
                                    <label for="ddlPeriodTo_Year">&nbsp;</label>
                                    <asp:DropDownList ID="ddlPeriodTo_Year" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlPeriodTo_Year" SetFocusOnError="true" ErrorMessage="Select Period To Year" InitialValue="0" ValidationGroup="SaveEmployment" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkWorkingStatus" runat="server" Text="I currently working here" AutoPostBack="true" OnCheckedChanged="chkWorkingStatus_CheckedChanged" />
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtEmploymentDescription">Description (Optional)</label>
                                    <asp:TextBox ID="txtEmploymentDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="70px"
                                        Style="resize: none;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSaveEmployment" runat="server" Text="Save" OnClick="btnSaveEmployment_Click" CssClass="cta-btn-sm" CommandName="save" ValidationGroup="SaveEmployment" />
                        <asp:Button ID="btnCloseEmployment" runat="server" Text="Close" OnClick="btnCloseEmployment_Click" CssClass="cta-btn-sm" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
