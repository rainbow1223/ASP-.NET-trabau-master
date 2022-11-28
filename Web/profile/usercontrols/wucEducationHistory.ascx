<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucEducationHistory.ascx.cs" Inherits="profile_usercontrols_wucEducationHistory" %>
<script>
    function LoadEducationEvents() {
        $(document).ready(function () {
            $('.education-item').mouseover(function () {
                $(this).find('div[class*="edit-button"]').show();
            });
            $('.education-item').mouseout(function () {
                $(this).find('div[class*="edit-button"]').hide();
            });
        });
    }
</script>
<div class="airCardHeader d-flex align-items-center">
    <h2>Education History</h2>
    <div class="ml-auto editCard-btn">
        <asp:LinkButton ID="lbtnAddEducationHistory" runat="server" Text="<i class='fa fa-plus' aria-hidden='true'></i>" OnClick="lbtnAddEducationHistory_Click"
            CssClass="edit-pencil-button"></asp:LinkButton>
    </div>
</div>
<div class="airCardBody padding-20 education-card-body">
    <div class="card-body-content">
        <asp:Repeater ID="rEducation" runat="server">

            <ItemTemplate>
                <div class="row education-item">
                    <asp:Label ID="lblEducationId" runat="server" Visible="false" Text='<%#Eval("EducationId") %>'></asp:Label>
                    <div class="col-sm-10" style="display: inline-block;">
                        <%#Eval("FullDetails") %>
                        <br />
                        <%#Eval("YearDetails") %>
                    </div>
                    <div class="col-sm-2 edit-button">
                        <asp:LinkButton ID="lbtnEditEducation" runat="server" ClientIDMode="AutoID" Text="<i class='fa fa-pencil' aria-hidden='true'></i>" OnClick="lbtnEditEducation_Click"
                            CssClass="edit-pencil-button" Visible='<%#Eval("EditMode") %>'></asp:LinkButton>
                    </div>
                </div>
                <hr />
            </ItemTemplate>

        </asp:Repeater>
    </div>
</div>

<div id="divTrabau_AddEducation" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">

    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">

            <!-- Modal Header -->
            <div class="modal-header">
                <h4 class="modal-title" id="popHeader" runat="server">
                    <asp:Literal ID="lblAddEducation_Header" runat="server"></asp:Literal></h4>
                <asp:Button ID="btnCloseEducation_top" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseEducation_Click" />
            </div>

            <!-- Modal body -->
            <asp:UpdatePanel ID="UpnlAddEducation" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-body" id="div_education_content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtSchoolName">School</label>
                                    <asp:TextBox ID="txtSchoolName" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="txtSchoolName" SetFocusOnError="true" ErrorMessage="Enter School Name" ValidationGroup="SaveEducation" Display="Dynamic" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlEducationYearFrom">Education Attended</label>
                                    <asp:DropDownList ID="ddlEducationYearFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="ddlEducationYearTo">&nbsp;</label>
                                    <asp:DropDownList ID="ddlEducationYearTo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtEducationDegree">Degree</label>
                                    <asp:TextBox ID="txtEducationDegree" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtEducationAreaOfStudy">Area of Study</label>
                                    <asp:TextBox ID="txtEducationAreaOfStudy" runat="server" CssClass="form-control" autocomplete="Off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="txtEducationDescription">Description</label>
                                    <asp:TextBox ID="txtEducationDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="70px"
                                        Style="resize: none;" autocomplete="Off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSaveEducation" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveEducation_Click" CommandName="save" ValidationGroup="SaveEducation" />
                        <asp:Button ID="btnCloseEducation" runat="server" Text="Close" OnClick="btnCloseEducation_Click" CssClass="cta-btn-sm" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
