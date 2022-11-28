<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="portfolios.aspx.cs" Inherits="profile_user_portfolios"
    Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href='<%= Page.ResolveUrl("~/assets/css/portfolio.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css") %>' rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select-custom.js") %>'></script>
    <script>
        function PortfolioLoad_Events() {
            $(document).ready(function () {
                setTimeout(function () {
                    $(function () {
                        $('input[id*="txtCompletiondate"]').datepicker({
                            changeMonth: true,
                            changeYear: true
                        });
                    });
                }, 200);

                var selected_template = $('#hftemplate_selected').val();
                if (selected_template != '' && selected_template != undefined) {
                    $('.portfolio-step2-item[id="' + selected_template + '"]').addClass('active');
                }

                $('.portfolio-step2-item').click(function () {
                    $('.portfolio-step2-item').removeClass('active');
                    $(this).addClass('active');
                    $('#hftemplate_selected').val($(this).attr('id'));
                });

                $('#div_browse_file').click(function () {
                    $('input[id*="afuAttachments"]').click();
                });
            });
        }
    </script>
    <asp:UpdatePanel ID="upParent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-3 d-none d-md-block leftSideBar">
                    <ul>
                        <li id="li1" runat="server"><span class="project-icon-left"><i class='fa fa-pencil' aria-hidden='true'></i></span>Add project<span class="project-icon-right"><i class="fa fa-check-square" aria-hidden="true"></i></span></li>
                        <li id="li2" runat="server"><span class="project-icon-left"><i class='fa fa-id-card' aria-hidden='true'></i></span>Select template<span class="project-icon-right"><i class="fa fa-check-square" aria-hidden="true"></i></span></li>
                        <li id="li3" runat="server"><span class="project-icon-left"><i class='fa fa-th-list' aria-hidden='true'></i></span>Add details<span class="project-icon-right"><i class="fa fa-check-square" aria-hidden="true"></i></span></li>
                        <li id="li4" runat="server"><span class="project-icon-left"><i class='fa fa-check' aria-hidden='true'></i></span>Preview<span class="project-icon-right"><i class="fa fa-check-square" aria-hidden="true"></i></span></li>
                    </ul>
                </div>
                <div class="col-md-9">
                    <div class="ui-application">
                        <div id="div_contact_info" runat="server" visible="true">
                            <div class="air-card">
                                <asp:UpdatePanel ID="upPortfolioSteps" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <script>
                                            var size = 2;
                                            var id = 0;
                                            Sys.Application.add_load(PortfolioLoad_Events);
                                            function StartUpload(sender, args) {
                                                $('.progress-bar-percentage').text('0');
                                                $('.progress-bar-override').css('width', '0');

                                                $('.image-loading-preview').attr('style', 'display:block !important');
                                                size = 2;
                                                id = 0;
                                                id = setInterval("progress()", 20);
                                            }

                                            function UploadComplete(sender, args) {
                                                clearTimeout(id);
                                                var percentage = '100%';
                                                $('.progress-bar-percentage').text(percentage);
                                                $('.progress-bar-override').css('width', percentage);

                                                setTimeout(function () {
                                                    $('.image-loading-preview').attr('style', 'display:none !important');
                                                    $('#btnRefreshGalleryItems').click();
                                                }, 1500);


                                            }

                                            function progress() {
                                                size = size + 1;
                                                if (size > 299) {
                                                    clearTimeout(id);
                                                }
                                                var percentage = parseInt(size / 3).toString() + '%';
                                                $('.progress-bar-percentage').text(percentage);
                                                $('.progress-bar-override').css('width', percentage);
                                            }

                                        </script>
                                        <div class="airCardHeader d-flex align-items-center">
                                            <h2>
                                                <asp:Literal ID="ltrlPortfoilio_Header" runat="server"></asp:Literal></h2>
                                        </div>
                                        <div class="airCardBody">
                                            <div id="div_step1" runat="server">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>Project Title</label>
                                                    </div>
                                                    <div class="col-md-12 required">
                                                        <p>
                                                            <asp:TextBox ID="txtProjectTitle" runat="server" CssClass="form-control" placeholder="Enter project title"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvProjectTitle" runat="server" ControlToValidate="txtProjectTitle"
                                                                ErrorMessage="Enter Project Title" ValidationGroup="Portfolio_Step1" CssClass="text-danger"></asp:RequiredFieldValidator>
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>Completion date</label>
                                                    </div>
                                                    <div class="col-md-12 required">
                                                        <p>
                                                            <asp:TextBox ID="txtCompletiondate" runat="server" CssClass="form-control" placeholder="Enter Completion date"></asp:TextBox>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="div_step2" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="portfolio-templates">
                                                            <asp:HiddenField ID="hftemplate_selected" runat="server" ClientIDMode="Static" />
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="portfolio-step2-item" id="1">
                                                                        <img src="../../assets/uploads/gallery.png" />
                                                                        <h4>Gallery</h4>
                                                                        <p>Display images or videos, one at a time</p>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="portfolio-step2-item" id="2">
                                                                        <img src="../../assets/uploads/gallery.png" />
                                                                        <h4>Case Study</h4>
                                                                        <p>Highlight the project problem and your solution</p>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="portfolio-step2-item" id="3">
                                                                        <img src="../../assets/uploads/gallery.png" />
                                                                        <h4>Classic</h4>
                                                                        <p>Allow clients to scroll through your work</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="div_step3" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div id="div_browse_file" class="file-browser">browse to upload</div>
                                                            </div>
                                                            <div class="col-sm-12">Upload .jpg, .gif, or .png images up to 10MB each. Images will be displayed at 690px wide, at maximum.</div>
                                                        </div>
                                                        <div class="row image-loading-preview">
                                                            <div class="col-sm-12">
                                                                <div class="loading-preview-parent">
                                                                    <div class="loading-preview">
                                                                        <p class="progress-bar-percentage">0%</p>
                                                                        <div class="progress-bar-parent">
                                                                            <div class="progress-bar progress-bar-override" style="width: 0%;"></div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="row image-list-preview">
                                                                    <asp:Button ID="btnRefreshGalleryItems" runat="server" ClientIDMode="Static" OnClick="btnRefreshGalleryItems_Click" Style="display: none" />
                                                                    <asp:Button ID="btnRemoveGalleryItem" runat="server" ClientIDMode="Static" OnClick="btnRemoveGalleryItem_Click" Style="display: none" />
                                                                    <asp:HiddenField ID="hfRemoveItemKey" runat="server" ClientIDMode="Static" />
                                                                    <asp:Repeater ID="rGallery" runat="server">
                                                                        <ItemTemplate>
                                                                            <div class="col-md-6 media-content">
                                                                                <div class="media-item">
                                                                                    <asp:Label ID="lbl_file_key" runat="server" Text='<%#Eval("file_key") %>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="lbtnRemoveMedia" runat="server" Text="<i class='fa fa-trash' aria-hidden='true'></i>" OnClick="lbtnRemoveMedia_Click"
                                                                                        CssClass="edit-pencil-button media-remove-item" ClientIDMode="AutoID"></asp:LinkButton>
                                                                                    <img src='<%#Eval("file_bytes_preview") %>' />
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Devices</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlDevices" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:HiddenField ID="hfDevices" runat="server" ClientIDMode="Static" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Mobile Platforms</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlMobilePlatforms" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:HiddenField ID="hfMobilePlatforms" runat="server" ClientIDMode="Static" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Mobile Programming Languages</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlMobileProgLanguages" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:HiddenField ID="hfMobileProgLanguages" runat="server" ClientIDMode="Static" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Mobile App Development Skills</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlMobileAppDevSkills" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:HiddenField ID="hfMobileAppDevSkills" runat="server" ClientIDMode="Static" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Databases</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlDatabases" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:HiddenField ID="hfDatabases" runat="server" ClientIDMode="Static" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Business Size Experience</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:DropDownList ID="ddlBusinessSizeExperiences" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:HiddenField ID="hfBusinessSizeExperiences" runat="server" ClientIDMode="Static" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Project URL (optional)</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:TextBox ID="txtProjectURL" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Project Description</label>
                                                            </div>
                                                            <div class="col-md-12 required">
                                                                <asp:TextBox ID="txtProjectDescription" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                                    Height="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvProjectDescription" runat="server" ErrorMessage="Enter Project Description"
                                                                    ValidationGroup="Portfolio_Step3" CssClass="text-danger" ControlToValidate="txtProjectDescription"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="div_step4" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Title</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Literal ID="ltrlTitle" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="div_Devices" runat="server" visible="false">
                                                            <div class="col-md-6">
                                                                <label>Devices</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Repeater ID="rDevices" runat="server">
                                                                    <ItemTemplate>
                                                                        <span class="item-box"><%#Eval("CategoryName") %></span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="div_MP" runat="server" visible="false">
                                                            <div class="col-md-6">
                                                                <label>Mobile Platforms</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Repeater ID="rMobilePlatforms" runat="server">
                                                                    <ItemTemplate>
                                                                        <span class="item-box"><%#Eval("CategoryName") %></span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="div_MPL" runat="server" visible="false">
                                                            <div class="col-md-6">
                                                                <label>Mobile Programming Languages</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Repeater ID="rMobileProgrammingLanguages" runat="server">
                                                                    <ItemTemplate>
                                                                        <span class="item-box"><%#Eval("CategoryName") %></span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="div_MADS" runat="server" visible="false">
                                                            <div class="col-md-6">
                                                                <label>Mobile App Development Skills</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Repeater ID="rMobileAppDevelopmentSkills" runat="server">
                                                                    <ItemTemplate>
                                                                        <span class="item-box"><%#Eval("CategoryName") %></span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="div_Databases" runat="server" visible="false">
                                                            <div class="col-md-6">
                                                                <label>Databases</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Repeater ID="rDatabases" runat="server">
                                                                    <ItemTemplate>
                                                                        <span class="item-box"><%#Eval("CategoryName") %></span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="div_BSE" runat="server" visible="false">
                                                            <div class="col-md-6">
                                                                <label>Business Size Experience</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Repeater ID="rBusinessSizeExperience" runat="server">
                                                                    <ItemTemplate>
                                                                        <span class="item-box"><%#Eval("CategoryName") %></span>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Project Description</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Literal ID="ltrlProjectDescription" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <label>Template</label>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Literal ID="ltrlTemplate" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="airCardFooter">
                                                <asp:Button ID="btnCancelPortfolio" runat="server" Text="Cancel" OnClick="btnCancelPortfolio_Click" CssClass="cta-btn-sm-cancel" />
                                                <asp:LinkButton ID="lbtnSelectTemplate" runat="server" Text="Proceed to Select Template" OnClick="lbtnSelectTemplate_Click" CssClass="cta-btn-sm pull-right" ValidationGroup="Portfolio_Step1"></asp:LinkButton>
                                            </div>
                                        </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:AsyncFileUpload ID="afuAttachments" runat="server" OnClientUploadStarted="StartUpload"
        OnClientUploadComplete="UploadComplete" OnUploadedComplete="afuAttachments_UploadedComplete" Style="display: none" />
</asp:Content>

