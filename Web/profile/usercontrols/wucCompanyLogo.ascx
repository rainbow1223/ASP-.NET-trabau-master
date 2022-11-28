<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCompanyLogo.ascx.cs" Inherits="profile_usercontrols_wucCompanyLogo" %>
<%--*********************cropping***********************************************--%>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/camera/js/cropper.js") %>'></script>
    <link href='<%= Page.ResolveUrl("~/assets/plugins/camera/css/cropper.css") %>' rel="stylesheet" type="text/css" />
    <%--*********************cropping***********************************************--%>
<div id="div_company_logo_popup" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <!-- Modal Header -->
            <div class="modal-header">
                <h4 class="modal-title" id="H3" runat="server">
                    <asp:Literal ID="ltrlCompanyLogo_Header" runat="server"></asp:Literal></h4>
                <asp:Button ID="btnCloseCompanyLogo_popup" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseCompanyLogo_popup_Click" />
            </div>

            <!-- Modal body -->
            <asp:UpdatePanel ID="upCompanyLogo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group text-center" id="company_logo_browse">
                                    <asp:Image ID="imgCompanyLogo_Upload" runat="server" ImageUrl="~/assets/images/default-avatar.png" Width="300px" />
                                </div>
                                <div class="form-group text-center" id="companylogo_crop">
                                    <asp:Image ID="img_company_logo_crop" runat="server" ClientIDMode="Static" Width="100%" />
                                    <asp:HiddenField ID="hf_CompanyLogo_Cropped" runat="server" ClientIDMode="Static" />
                                    <input type="button" id="btn_companylogo_destroy_crop" style="display: none" />
                                </div>
                                <div class="form-group text-center">
                                    <asp:LinkButton ID="lbtnUploadCompanyLogo" runat="server" Text="<i class='fa fa-plus-circle' aria-hidden='true'></i> Add New Photo"
                                        CssClass="profile-picture-add-btn" Style="display: block;" />
                                    <asp:FileUpload ID="fu_companylogo_upload" runat="server" ClientIDMode="Static" Style="display: none" onchange="ValidateCompanyLogoFileUploadExtension()" />
                                </div>

                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSaveCompanyLogo" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveCompanyLogo_Click" />
                        <asp:Button ID="btnClose_CompanyLogo_Popup" runat="server" Text="Close" OnClick="btnCloseCompanyLogo_popup_Click" CssClass="cta-btn-sm" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
