<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucProfilePicture.ascx.cs" Inherits="profile_usercontrols_wucProfilePicture" %>
 <%--*********************cropping***********************************************--%>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/camera/js/cropper.js") %>'></script>
    <link href='<%= Page.ResolveUrl("~/assets/plugins/camera/css/cropper.css") %>' rel="stylesheet" type="text/css" />
    <%--*********************cropping***********************************************--%>
<div id="div_profile_pic_popup" runat="server" visible="false" class="modal fade" role="dialog" clientidmode="static">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <!-- Modal Header -->
            <div class="modal-header">
                <h4 class="modal-title" id="H3" runat="server">
                    <asp:Literal ID="ltrlProfilePic_Header" runat="server"></asp:Literal></h4>
                <asp:Button ID="btnCloseProfilePic_popup" runat="server" Text="&times;" CssClass="close" OnClick="btnCloseProfilePic_popup_Click" />
            </div>

            <!-- Modal body -->
            <asp:UpdatePanel ID="upProfilePicture" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-body" id="div_profile_pic_content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group text-center" id="pic_browse">
                                    <asp:Image ID="imgProfilePic_Upload" runat="server" ImageUrl="~/assets/images/default-avatar.png" Width="300px" />
                                </div>
                                <div class="form-group text-center" id="pic_crop">
                                    <asp:Image ID="img_profile_pic_crop" runat="server" ClientIDMode="Static" Width="100%" />
                                    <asp:HiddenField ID="hfCropped_Picture" runat="server" ClientIDMode="Static" />
                                    <input type="button" id="btndestroy_crop" style="display: none" />
                                </div>
                                <div class="form-group text-center">
                                    <asp:LinkButton ID="lbtnUploadProfilePicture" runat="server" Text="<i class='fa fa-plus-circle' aria-hidden='true'></i> Add New Photo"
                                        CssClass="profile-picture-add-btn" Style="display: block;" />
                                    <asp:FileUpload ID="fu_profilepic_upload" runat="server" ClientIDMode="Static" Style="display: none" onchange="ValidateFileUploadExtension()" />
                                </div>

                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSaveProfilePicture" runat="server" Text="Save" CssClass="cta-btn-sm" OnClick="btnSaveProfilePicture_Click" />
                        <asp:Button ID="btnClose_Profile_Pic_Popup" runat="server" Text="Close" OnClick="btnCloseProfilePic_popup_Click" CssClass="cta-btn-sm" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
