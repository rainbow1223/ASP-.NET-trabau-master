<%@ Page Title="City Master - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="citymaster.aspx.cs" Inherits="admin_citymaster" EnableEventValidation="false"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.23/css/jquery.dataTables.min.css" />
    <style>
        #btnSaveCityDetails {
            cursor: pointer;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="problem-heading p-80 pt-0">
        <h3>City Master</h3>
    </div>
    <div class="row">
        <div class="col-sm-12" id="div_cities">
            <table id="tblDataTable" class="display">
                <thead>
                    <tr>
                        <th>City Name</th>
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
        <a id="lnkAddCity" class="float" onclick="OpenAddCityPopup();"><i class='fa fa-plus my-float'></i></a>
    </div>
    <div id="divTrabau_Popup_EditCity" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="H1" runat="server">Add / Modify City</h4>
                    <input type="button" value="&times;" class="close" onclick="HandlePopUp('0','divTrabau_Popup_EditCity')" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label for="txtCityName">City Name</label>
                                <asp:HiddenField ID="hfCityId" runat="server" ClientIDMode="Static" />
                                <asp:TextBox ID="txtCityName" runat="server" CssClass="form-control" autocomplete="Off" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="ddlCountry">Country Name</label>
                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="ddlState">State Name</label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="chkCityStatus">Status</label>
                                <asp:CheckBox ID="chkCityStatus" runat="server" ClientIDMode="Static"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a id="btnSaveCityDetails" class="cta-btn-sm">Save City Details</a>
                    <input type="button" value="Close" class="cta-btn-sm" onclick="HandlePopUp('0','divTrabau_Popup_EditCity')" />
                </div>
            </div>
        </div>
    </div>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("citymaster.aspx") %>';
        var services_pathconfig = '<%= Page.ResolveUrl("~/services.asmx") %>';
    </script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.23/js/jquery.dataTables.min.js"></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/citymaster.js?version=1.0") %>' type="text/javascript"></script>
</asp:Content>

