<%@ Page Title="Document Category Master - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="document-categorymaster.aspx.cs" Inherits="admin_document_categorymaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.23/css/jquery.dataTables.min.css" />
    <style>
        #btnSaveDocumentCategory {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="problem-heading p-80 pt-0">
        <h3>Document Category Master</h3>
    </div>
    <div class="row">
        <div class="col-sm-12" id="div_cities">
            <table id="tblDataTable" class="display">
                <thead>
                    <tr>
                        <th>Document Category Name</th>
                        <th>Active</th>
                        <th></th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div class='AddButton'>
        <a id="lnkAddDocument" class="float" onclick="OpenAddDocumentPopup();"><i class='fa fa-plus my-float'></i></a>
    </div>
    <div id="divTrabau_Popup_EditDocument" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="H1" runat="server">Add / Modify Document Catgeory</h4>
                    <input type="button" value="&times;" class="close" onclick="HandlePopUp('0','divTrabau_Popup_EditDocument')" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="txtDocumentCategoryName">Document Category Name</label>
                                <asp:HiddenField ID="hfDocumentId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox ID="txtDocumentCategoryName" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="chkDocumentStatus">Status</label>
                                <asp:CheckBox ID="chkDocumentStatus" runat="server" ClientIDMode="Static"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a id="btnSaveDocumentCategory" class="cta-btn-sm">Save Document Catgeory</a>
                    <input type="button" value="Close" class="cta-btn-sm" onclick="HandlePopUp('0','divTrabau_Popup_EditDocument')" />
                </div>
            </div>
        </div>
    </div>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("document-categorymaster.aspx") %>';
        var services_pathconfig = '<%= Page.ResolveUrl("~/services.asmx") %>';
    </script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.23/js/jquery.dataTables.min.js"></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/documentmaster.js?version=1.0") %>' type="text/javascript"></script>
</asp:Content>

