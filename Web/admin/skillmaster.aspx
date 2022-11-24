<%@ Page Title="Skill Master - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="skillmaster.aspx.cs" Inherits="admin_skillmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.23/css/jquery.dataTables.min.css" />
    <style>
        #btnSaveSkillDetails {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="problem-heading p-80 pt-0">
        <h3>Skill Master</h3>
    </div>
    <div class="row">
        <div class="col-sm-12" id="div_cities">
            <table id="tblDataTable" class="display">
                <thead>
                    <tr>
                        <th>Skill Name</th>
                        <th>Skill Type</th>
                        <th>Active</th>
                        <th></th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div class='AddButton'>
        <a id="lnkAddSkill" class="float" onclick="OpenAddSkillPopup();"><i class='fa fa-plus my-float'></i></a>
    </div>
    <div id="divTrabau_Popup_EditSkill" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="H1" runat="server">Add / Modify Skill</h4>
                    <input type="button" value="&times;" class="close" onclick="HandlePopUp('0','divTrabau_Popup_EditSkill')" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="txtSkillName">Skill Name</label>
                                <asp:HiddenField ID="hfSkillId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox ID="txtSkillName" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="ddlSkillType">Skill Type</label>
                                <asp:DropDownList ID="ddlSkillType" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static">
                                    <asp:ListItem Text="None" Value="None"></asp:ListItem>
                                    <asp:ListItem Text="Front End Deliverables" Value="Front End Deliverables"></asp:ListItem>
                                    <asp:ListItem Text="Front End Languages" Value="Front End Languages"></asp:ListItem>
                                    <asp:ListItem Text="Front End Skills" Value="Front End Skills"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="chkSkillStatus">Status</label>
                                <asp:CheckBox ID="chkSkillStatus" runat="server" ClientIDMode="Static"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a id="btnSaveSkillDetails" class="cta-btn-sm">Save Skill Details</a>
                    <input type="button" value="Close" class="cta-btn-sm" onclick="HandlePopUp('0','divTrabau_Popup_EditSkill')" />
                </div>
            </div>
        </div>
    </div>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("skillmaster.aspx") %>';
        var services_pathconfig = '<%= Page.ResolveUrl("~/services.asmx") %>';
    </script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.23/js/jquery.dataTables.min.js"></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/skillmaster.js?version=1.0") %>' type="text/javascript"></script>
</asp:Content>

