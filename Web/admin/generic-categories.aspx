<%@ Page Title="Generic Catgeories - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="generic-categories.aspx.cs" Inherits="admin_generic_categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.23/css/jquery.dataTables.min.css" />
    <style>
        #btnSaveCategoryDetails {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="problem-heading p-80 pt-0">
        <h3>Generic Catgeories</h3>
    </div>
    <div class="row">
        <div class="col-sm-12" id="div_cities">
            <table id="tblDataTable" class="display">
                <thead>
                    <tr>
                        <th>Category Name</th>
                        <th>Category Type</th>
                        <th>Active</th>
                        <th></th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div class='AddButton'>
        <a id="lnkAddCatgeory" class="float" onclick="OpenAddCatgeoryPopup();"><i class='fa fa-plus my-float'></i></a>
    </div>
    <div id="divTrabau_Popup_EditCatgeory" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="H1" runat="server">Add / Modify Portfolio Project Catgeory</h4>
                    <input type="button" value="&times;" class="close" onclick="HandlePopUp('0','divTrabau_Popup_EditCatgeory')" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="txtCategoryName">Generic Catgeory Name</label>
                                <asp:HiddenField ID="hfCategoryId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="ddlCategoryType">Category Type</label>
                                <asp:DropDownList ID="ddlCategoryType" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="chkCategoryStatus">Status</label>
                                <asp:CheckBox ID="chkCategoryStatus" runat="server" ClientIDMode="Static"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a id="btnSaveCategoryDetails" class="cta-btn-sm">Save Category Details</a>
                    <input type="button" value="Close" class="cta-btn-sm" onclick="HandlePopUp('0','divTrabau_Popup_EditCatgeory')" />
                </div>
            </div>
        </div>
    </div>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("generic-categories.aspx") %>';
        var services_pathconfig = '<%= Page.ResolveUrl("~/services.asmx") %>';
    </script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.23/js/jquery.dataTables.min.js"></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/generic-categorymaster.js?version=1.0") %>' type="text/javascript"></script>
</asp:Content>

