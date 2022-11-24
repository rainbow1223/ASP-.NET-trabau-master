<%@ Page Title="State Master - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="statemaster.aspx.cs" Inherits="admin_statemaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.23/css/jquery.dataTables.min.css" />
    <style>
        #btnSaveStateDetails {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="problem-heading p-80 pt-0">
        <h3>State Master</h3>
    </div>
    <div class="row">
        <div class="col-sm-12" id="div_cities">
            <table id="tblDataTable" class="display">
                <thead>
                    <tr>
                        <th>State Name</th>
                        <th>Country Name</th>
                        <th>Active</th>
                        <th></th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div class='AddButton'>
        <a id="lnkAddState" class="float" onclick="OpenAddStatePopup();"><i class='fa fa-plus my-float'></i></a>
    </div>
    <div id="divTrabau_Popup_EditState" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="H1" runat="server">Add / Modify State</h4>
                    <input type="button" value="&times;" class="close" onclick="HandlePopUp('0','divTrabau_Popup_EditState')" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="txtStateName">State Name</label>
                                <asp:HiddenField ID="hfStateId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox ID="txtStateName" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="ddlCountry">Country</label>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="chkStateStatus">Status</label>
                                <asp:CheckBox ID="chkStateStatus" runat="server" ClientIDMode="Static"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a id="btnSaveStateDetails" class="cta-btn-sm">Save State Details</a>
                    <input type="button" value="Close" class="cta-btn-sm" onclick="HandlePopUp('0','divTrabau_Popup_EditState')" />
                </div>
            </div>
        </div>
    </div>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("statemaster.aspx") %>';
        var services_pathconfig = '<%= Page.ResolveUrl("~/services.asmx") %>';
    </script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.23/js/jquery.dataTables.min.js"></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/statemaster.js?version=1.0") %>' type="text/javascript"></script>
</asp:Content>

