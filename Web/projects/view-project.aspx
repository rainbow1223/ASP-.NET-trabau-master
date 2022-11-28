<%@ Page Title="Project Panel - Trabau" Language="C#" MasterPageFile="~/project.master" AutoEventWireup="true" CodeFile="view-project.aspx.cs" Inherits="projects_view_project" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .carousel {
            background: #EEE;
        }

        .carousel-cell {
            width: 28%;
            height: 200px;
            margin-right: 10px;
            background: #8C8;
            border-radius: 5px;
            counter-increment: carousel-cell;
        }

            .carousel-cell.is-selected {
                background: #ED2;
            }

            /* cell number */
            .carousel-cell:before {
                display: block;
                text-align: center;
                content: counter(carousel-cell);
                line-height: 200px;
                font-size: 80px;
                color: white;
            }

        .navigation-cell {
            width: 50px;
            height: 75px;
            margin-right: 10px;
            border-radius: 5px;
        }
    </style>
    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.common.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.light.css") %>' rel="stylesheet" type="text/css" />

    <link href='<%= Page.ResolveUrl("~/assets/css/toolbar.css") %>' rel="stylesheet" type="text/css" />

    <!-- Essential JS 2 Toolbar's dependent material theme -->
    <link href='<%= Page.ResolveUrl("~/assets/plugins/syncfusion/css/material.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/syncfusion/css/material-1.css") %>' rel="stylesheet" type="text/css" />
    <link href='<%= Page.ResolveUrl("~/assets/plugins/syncfusion/css/material-2.css") %>' rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx-quill.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/HTMLEditor/dx.all.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/select2/js/select-custom.js") %>'></script>

    <!-- Essential JS 2 Toolbar's dependent scripts -->
    <script src='<%= Page.ResolveUrl("~/assets/plugins/syncfusion/js/ej2-base.min.js") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/syncfusion/js/ej2-popups.min.js") %>'></script>
    <!-- Essential JS 2 Toolbar's global script -->
    <script src='<%= Page.ResolveUrl("~/assets/plugins/syncfusion/js/ej2-navigations.min.js") %>'></script>

    <%--<div class="project-loading">
        <div class="loading-linear-background">
            <div class="loading-top-1"></div>
            <div class="loading-top-2"></div>
            <div class="loading-top-3"></div>
        </div>
    </div>--%>
    <div class="navigation-content" style="min-height: 600px;" runat="server" visible="false">
        <div class="navigation-result no-background">
            <div class="window">
                <nav data-role="ribbonmenu">
                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                        <li class="nav-item" style="background: #0bbc56 !important" role="presentation">

                            <ul id="ribbon" style="border-color: #0bbc56 !important" class="e-rbncustomelement e-menu e-js e-widget e-box e-horizontal e-separator" aria-labelledby='filemenu' role="menu" tabindex="0">
                                <li class="e-list e-haschild e-active e-mfocused" role="menuitem" aria-haspopup="true" tabindex="-1" aria-label="FILE">
                                    <a id="filetoggle" style="margin-top: 2px" class="aschild e-menulink e-arrow-space">FILE<span class="e-icon e-arrowhead-down"></span></a>
                                    <ul class="dropdownfile" aria-hidden="false" style="top: 33px; left: -1px; z-index: 10001;">

                                        <li class="e-list" role="menuitem" tabindex="-1" aria-label="Export Project Model"><a class="e-menulink">Export Project Model</a></li>
                                        <li class="e-list" role="menuitem" tabindex="-1" aria-label="Page Setup"><a class="e-menulink">Page Setup</a></li>
                                        <li class="e-list" role="menuitem" tabindex="-1" aria-label="Print Preview"><a class="e-menulink">Print Preview</a></li>
                                        <li class="e-list" role="menuitem" tabindex="-1" aria-label="Print Model"><a class="e-menulink">Print Model</a></li>
                                        <li class="e-list" role="menuitem" tabindex="-1" aria-label="Project Report"><a class="e-menulink">Project Report</a></li>

                                    </ul>
                                    <span class="e-menu-arrow e-menu-left dropdownfile" style="z-index: 10002; display: none"><span class="e-arrowMenuOuter"></span><span class="e-arrowMenuInner"></span></span></li>
                            </ul>

                        </li>
                        <asp:Repeater ID="rRibbonTabs" runat="server">
                            <ItemTemplate>
                                <li class="nav-item" role="presentation">
                                    <a class="nav-link" id='<%# "tab"+Eval("MenuId").ToString() %>' data-toggle="tab" href='<%# "#content"+Eval("MenuId").ToString() %>' role="tab"
                                        aria-controls="home" aria-selected="true"><%#Eval("Name") %></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li class="project-maximize-arrow"><i class="flaticon-down-arrow"></i></li>
                    </ul>
                    <div class="tab-content" id="myTabContent">
                        <asp:Repeater ID="rRibbonTabs_Content" runat="server">
                            <ItemTemplate>
                                <div class="tab-pane fade e-ribbon e-js e-tab e-widget" id='<%# "content"+Eval("MenuId").ToString() %>' role="tabpanel" aria-labelledby='<%# "tab"+Eval("MenuId").ToString() %>'>
                                    <asp:Label ID="lblMainMenuId" runat="server" Visible="false" Text='<%#Eval("MenuId") %>'></asp:Label>
                                    <asp:Repeater ID="rRibbonTabs_Menu" runat="server">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubMenuId" runat="server" Visible="false" Text='<%#Eval("MenuId") %>'></asp:Label>

                                            <ul class="tab-content e-groupdiv e-centeralign" id="div_content" runat="server">
                                                <asp:Repeater ID="rRibbonTabs_ChildMenu" runat="server">
                                                    <ItemTemplate>
                                                        <li class="tab-content-inner e-ribGroupContent e-resgroupheader" style='<%#Eval("ContentStyle") %>;' title='<%#Eval("Tooltip") %>'>
                                                            <div class="e-ribupdivarrow"><span class="e-icon e-groupresponsive e-ribdownarrow"></span></div>
                                                            <asp:Label ID="lblInnerSubMenuId" runat="server" Visible="false" Text='<%#Eval("MenuId") %>'></asp:Label>
                                                            <a id='<%#"drop"+Eval("MenuId").ToString() %>' class='<%# Eval("CSSClass") %> e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-ribbonbtn e-rbn-button' style="width: 100%;" role="button" aria-describedby="Ribbon_configureWarningMessage" aria-disabled="false" data-toggle='<%# Int32.Parse(Eval("ChildCount").ToString())>0?"dropdown":"" %>' aria-haspopup="true" aria-expanded="false"
                                                                href='<%#Eval("DomainSwitchURL") %>' nav_id='<%#Eval("MenuId_Enc") %>' onclick='<%#Eval("ClickEventName") %>'>
                                                                <span class="e-btn-span"><span class="icon" style="display: inherit"><%#Eval("IconURL") %></span>
                                                                    <span class="e-btntxt"><%#Eval("Name") %></span>
                                                                </span>
                                                            </a>
                                                            <ul class="dropdown-menu navbar-nav trabau-nav-dropdown" aria-labelledby='<%#"drop"+Eval("MenuId").ToString() %>' id="ul_dropdown" runat="server" visible="false">
                                                                <asp:Repeater ID="rRibbonTabs_InnerChildMenu" runat="server">
                                                                    <ItemTemplate>
                                                                        <li class='<%# (Convert.ToInt32(Eval("ChildCount").ToString())>0?"dropleft ":"")+"tab-content-sub-inner dropdown-submenu nav-item"%>'>
                                                                            <asp:Label ID="lblSubInnerSubMenuId" runat="server" Visible="false" Text='<%#Eval("MenuId") %>'></asp:Label>
                                                                            <a href='<%#Eval("NavigationURL") %>' id='<%#"drop"+Eval("MenuId").ToString() %>' class='<%#Eval("Seperator") %>'
                                                                                data-toggle='<%# Int32.Parse(Eval("ChildCount").ToString())>0?"dropdown":"" %>'
                                                                                aria-haspopup="true" aria-expanded="true" nav_id='<%#Eval("MenuId_Enc") %>' onclick='<%#Eval("ClickEventName") %>'><%#Eval("IconURL") %> <%#Eval("Name") %></a>
                                                                            <ul class="dropdown-menu" aria-labelledby='<%#"drop"+Eval("MenuId").ToString() %>' id="sub_ul_dropdown" runat="server" visible="false">
                                                                                <asp:Repeater ID="rRibbonTabs_SubInnerChildMenu" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <li>
                                                                                            <a href="#" class='<%#Eval("Seperator") %>'><%#Eval("IconURL") %> <%#Eval("Name") %></a>
                                                                                        </li>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </ul>
                                                                        </li>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </ul>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <div class="e-contentbottom">
                                                    <div class="e-captionarea"><%#Eval("Name") %></div>
                                                </div>
                                                <div class="tab-content-inner" id="div_nav_ele" runat="server" visible="false">
                                                    <a id="btnSubMenuNav" runat="server" class="project-link">
                                                        <span class="icon">
                                                            <span><%#Eval("IconURL") %></span>
                                                        </span>
                                                        <span class="caption"><%#Eval("Name") %></span>
                                                    </a>
                                                </div>
                                            </ul>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>


                    </div>
                </nav>
            </div>
        </div>
    </div>


    <nav class="navbar navbar-expand-lg project-navigation">
        <%--<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#main_navigation">
            <span class="navbar-toggler-icon"></span>
        </button>--%>
        <div class="collapse navbar-collapse" id="main_navigation">
            <ul class="navbar-nav">
                <asp:Repeater ID="rMainMenu" runat="server">
                    <ItemTemplate>
                        <li navigation-id='<%#Eval("MenuIdEnc") %>' class='<%# (Int32.Parse(Eval("ChildCount").ToString()) > 0 ? "nav-item dropdown" : "nav-item") + (Eval("Name").ToString() == "File" ? " li-file-controls" : "") %>'>
                            <a class='<%# Int32.Parse(Eval("ChildCount").ToString()) > 0 ? "nav-link dropdown-toggle custom-link" : "nav-link custom-link" %>'
                                id='<%# "tab" + Eval("MenuId").ToString() %>' href='<%# "#content" + Eval("MenuId").ToString() %>' data-toggle="dropdown"><%#Eval("Name") %></a>
                            <asp:Literal ID="ltrlSubMenu" runat="server"></asp:Literal>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <li class="project-maximize-arrow"><i class="flaticon-down-arrow"></i></li>
            </ul>
        </div>
        <!-- navbar-collapse.// -->
    </nav>
    <%--    <div class="e-toolbar-items" id="template_toolbar">
        <div>--%>
     <div class="control-section">
        <div id="toolbar_content" class="e-ribGroupContent e-resgroupheader" style="height: auto;">
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_copy" class="e-innerdivchild e-controlpadding" title="Copy">
                        <button id="toolbar_copy" class="toolbarbtn e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button"  aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-copy" style="display:inherit"></span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_paste" class="e-innerdivchild e-controlpadding" title="Paste">
                        <button id="toolbar_paste" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="toolbar_paste" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-paste" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_cut" class="e-innerdivchild e-controlpadding" title="Cut">
                        <button id="toolbar_scissors" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="toolbar_cut" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-scissors" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_delete" class="e-innerdivchild e-controlpadding" title="Delete">
                        <button id="toolbar_delete" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="toolbar_delete" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-delete" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_export" class="e-innerdivchild e-controlpadding" title="Export">
                        <button id="toolbar_export" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-share" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                </div>
                <!-- Undo / -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_Undo" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_back_arrow" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-back-arrow" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_Undo" class="groupdiv e-innerdivchild e-controlpadding">
                        <button id="toolbar_redo_arrow" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-redo-arrow" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                </div>
                <!-- Formant / -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_Format" class="e-innerdivchild e-controlpadding">
                        <div id="toolbar_zoom" class="e-innerdivchild e-controlpadding">
                            <span id="toolbar_zoom_wrapper" class="e-ddl e-widget e-rbn-ddl" role="listbox" aria-expanded="false" aria-haspopup="true" aria-owns="toolbar_zoom_popup" tabindex="0" style="height: 28px; width: 80px;">
                                <span id="toolbar_zoom_container" class="toolbar_popup_menu e-in-wrap e-box">
                                    <input id="toolbar_input" class="e-input" readonly="readonly" tabindex="-1" data-role="textbox" placeholder="" value="100%">
                                    <span id="toolbar_zoom_dropdown" class="toolbarbtn toolbar-pop e-select" role="button" unselectable="on">
                                        <span class="e-icon e-arrow-sans-down" aria-label="select" unselectable="on">
                                        </span>
                                    </span>
                                </span>
                            </span>
                        </div>
                        <div id="toolbar__fontfamily" class="e-innerdivchild e-controlpadding">
                            <span id="toolbar_fontfamily_wrapper" class="e-ddl e-widget e-rbn-ddl" role="listbox" aria-expanded="false" aria-haspopup="true" aria-owns="toolbar_fontSize_popup" tabindex="0" style="height: 28px; width: 100px;">
                                <span id="toolbar_fontfamily_container" class="toolbar_popup_menu e-in-wrap e-box">
                                    <input id="toolbar_input" class="e-input" readonly="readonly" tabindex="-1" data-role="textbox" placeholder="" value="Segoe UI">
                                    <span id="toolbar_fontfamily_dropdown" class="toolbarbtn toolbar-pop e-select" role="button" unselectable="on">
                                        <span class="e-icon e-arrow-sans-down" aria-label="select" unselectable="on">
                                        </span>
                                    </span>
                                </span>
                            </span>
                        </div>
                        <div id="toolbar__fontSize" class="e-innerdivchild e-controlpadding">
                            <span id="toolbar_fontSize_wrapper" class="e-ddl e-widget e-rbn-ddl" role="listbox" aria-expanded="false" aria-haspopup="true" aria-owns="toolbar_fontSize_popup" tabindex="0" style="height: 28px; width: 60px;">
                                <span id="toolbar_fontSize_container" class="toolbar_popup_menu e-in-wrap e-box">
                                    <input id="toolbar_input" class="e-input" readonly="readonly" tabindex="-1" data-role="textbox" placeholder="" value="1pt">
                                    <span id="toolbar_fontSize_dropdown" class="toolbarbtn toolbar-pop e-select" role="button" unselectable="on">
                                        <span class="e-icon e-arrow-sans-down" aria-label="select" unselectable="on">
                                        </span>
                                    </span>
                                </span>
                            </span>
                        </div>
                    </div>
                        
                    <div id="toolbar__bold" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_bold" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="toolbar_cut" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 e-icon flaticon-bold" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar__italic" class="e-innerdivchild e-controlpadding" title="Delete">
                        <button id="toolbar_italic" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="toolbar_delete" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 e-icon flaticon-italic" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar__strikethrough" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_strikethrough" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 e-icon flaticon-strikethrough" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar__left_align" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_left_align" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 e-icon flaticon-left-align" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar__center_align" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_center_align" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 e-icon flaticon-center-align" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar__right_align" class=" groupdiv e-innerdivchild e-controlpadding">
                        <button id="toolbar_right_align" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 e-icon flaticon-right-align" style="display:flex">
                                </span>
                            </span>
                        </button>
                    </div>
                </div>
                <!-- Style  -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_style" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_bucket" class="toolbarbtn e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 e-icon flaticon-bucket-large" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_style" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_line_large" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 flaticon-pencil-drawing-a-line-large" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_style" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_text_box" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 flaticon-text-box-large" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_style" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_double_arrow_start" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 flaticon-double-arrow-large" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_style" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_double_arrow_end" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 flaticon-double-arrow-large" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                </div>
                <!-- Annotation  -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_Annotation" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_cursor" class="toolbarbtn  toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_15 flaticon-cursor" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_Annotation" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_capital" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 flaticon-capital-a" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                        
                    <div id="toolbar_Annotation" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_diagonal_line" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_13 flaticon-diagonal-line" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_Annotation" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_rectangular_shape_outline" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="flaticon-rectangular-shape-outline" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_Annotation" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_ellipse_shape_outline" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 flaticon-ellipse-outline-shape-variant" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_Annotation" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_flaticon_curved_line" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="flaticon-curved-line" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                    <div id="toolbar_Annotation" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_business_ascendant_graphic_line" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 flaticon-business-ascendant-graphic-line" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                </div>
                <!-- Enitity  -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_Enitity" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_click" class="toolbarbtn toolbarbtn_size e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="toolbaricon_16 flaticon-click" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                </div>
                <!-- Communication  -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_communication" class="e-innerdivchild e-controlpadding">
                        <div id="toolbar__communication" class="toolbarbtn e-innerdivchild e-controlpadding" title="FontSize">
                            <div id="toolbar_communication_relation" class = "communication_line">
                                <span class="toolbaricon_16 flaticon-line" style="font-size: 10px;" aria-label="select" unselectable="on">
                                </span>
                            </div>
                            <div id="toolbar_communication_arrow_down">
                                <span class="e-icon e-arrow-sans-down" aria-label="select" unselectable="on">
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- action -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_action" class="e-innerdivchild e-controlpadding">
                        <div id="toolbar__action" class="toolbarbtn e-innerdivchild e-controlpadding" title="FontSize">
                            <div id="toolbar_action_relation" class = "action_relation">
                                <span class="flaticon-relation" style="font-size: 10px;" aria-label="select" unselectable="on">
                                </span>
                            </div>
                            <div id="toolbar_action_arrow_down">
                                <span class="e-icon e-arrow-sans-down" aria-label="select" unselectable="on">
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- layout  -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_grid" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_layout" class="toolbarbtn e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            <span class="e-btn-span">
                                <span class="e-icon flaticon-grid" style="display:inherit">
                                </span>
                            </span>
                        </button>
                    </div>
                </div>
                <!-- view -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_view" class="e-innerdivchild e-controlpadding">
                        <span id="toolbar_view_wrapper" class="e-ddl e-widget e-rbn-ddl" role="listbox" aria-expanded="false" aria-haspopup="true" aria-owns="toolbar_view_popup" tabindex="0" style="height: 28px; width: 120px;">
                            <span id="toolbar_view_container" class="toolbar_popup_menu e-in-wrap e-box">
                                <input id="toolbar_input" class="e-input" readonly="readonly" tabindex="-1" data-role="textbox" placeholder="" value="Sub Function">
                                <span id="toolbar_view_dropdown" class="toolbarbtn toolbar-pop e-select" role="button" unselectable="on">
                                    <span class="e-icon e-arrow-sans-down" aria-label="select" unselectable="on">
                                    </span>
                                </span>
                            </span>
                        </span>
                    </div>
                </div>
                <!--  -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_div_layout" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_layout" class="toolbarbtn e-toolbar-text e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                Last Action
                        </button>
                    </div>
                </div>
                <!--  -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow">
                    <div id="toolbar_div_layout" class="e-innerdivchild e-controlpadding">
                        <button id="toolbar_layout" class="toolbarbtn e-toolbar-text e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-toolbarbtn e-rbn-button" type="submit" role="button" aria-describedby="" aria-disabled="false">
                            Warning Messages
                        </button>
                    </div>
                </div>
                <!-- Align -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow"></div>
                <!-- Size 1 -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow"></div>
                <!-- Size 2 -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow"></div>
                <!-- Snap -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow"></div>
                <!-- Center -->
                <div id="toolbar_Clipboard" class="groupdiv e-innerdivrow"></div>
            </div>
        </div>
    </div>
    <div class="project-maximize-arrow"><i class="flaticon-down-arrow"></i></div>
    <%--<div class="file-nav-content fade e-ribbon e-js e-tab e-widget active">
    </div>--%>

<%--<div id = "tab_layout">
        <div id="layoutTab" style="width: auto;z-index: 10000;display: none;">
            <ul>
                <li><a href="#steelman">Layout Setting</a></li>
            </ul>
            <div id="steelman">
                <div class="row tab-text">
                    Nudge and Pan
                </div>
                <div class="row">
                    <table>
                        <tr>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-left-arrow" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-left-arrow" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-up-arrow" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-down-arrow" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                        </tr>
                    </table>
                </div>
                <div class="row tab-text">
                    Align
                </div>
                <div class="row">
                    <table>
                        <tr>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-align-left" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-align-center" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-align-left" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-down-arrow" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                        </tr>
                        <tr style="margin-top: 10px;">
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-align" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-top-alignment" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"></td>
                            <td class = "tab_td"></td>
                        </tr>
                    </table>
                </div>
                <div class="row tab-text">
                    Spacing
                </div>
                <div class="row">
                    <table>
                        <tr>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-width" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-width" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-width" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-height" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                        </tr>
                        <tr style="margin-top: 10px;">
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-height" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="e-icon flaticon-height" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"></td>
                            <td class = "tab_td"></td>
                        </tr>
                    </table>
                </div>
                            
                <div class="row tab-text">
                    Size
                </div>
                <div class="row">
                    <table>
                        <tr>
                            <td class = "tab_td">
                                <button id="layout_sameWidth" class="toolbarbtn e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-ribbonbtn e-rbn-button" type="submit" role="button" aria-describedby="Ribbon_sameWidth" aria-disabled="false">
                                    <span class="e-btn-span">
                                        <span class="e-icon flaticon-width"></span>
                                        <span class="e-btntxt">Same Width</span>
                                    </span>
                                </button>
                            </td>
                            <td class = "tab_td">
                                <button id="layout_sameWidth" class="toolbarbtn e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-ribbonbtn e-rbn-button" type="submit" role="button" aria-describedby="Ribbon_sameWidth" aria-disabled="false">
                                    <span class="e-btn-span">
                                        <span class="e-icon flaticon-height"></span>
                                        <span class="e-btntxt">Same Height</span>
                                    </span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td class = "tab_td">
                                <button id="layout_sameWidth" class="toolbarbtn e-button e-js e-ntouch e-btn-normal e-btn e-select e-widget e-ribbonbtn e-rbn-button" type="submit" role="button" aria-describedby="Ribbon_sameWidth" aria-disabled="false">
                                    <span class="e-btn-span">
                                        <span class="e-icon flaticon-size-square"></span>
                                        <span class="e-btntxt">Same Height</span>
                                    </span>
                                </button>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <div class="row tab-text">
                    Snap
                </div>
                <div class="row">
                    <table>
                        <tr>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-grid" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-scale" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-axis" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                            <td class = "tab_td"><button id="toolbar_click" class="toolbarbtn toolbarbtn_size" type="submit" role="button" aria-describedby="" aria-disabled="false">
                                <span class="e-btn-span">
                                    <span class="toolbaricon_16 e-icon flaticon-redo-arrow" style="display:inherit">
                                    </span>
                                </span>
                            </button></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>--%>
    <div id="divTrabau_project_info" class="modal fade" role="dialog">

        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header myCard-heading">
                    <h4 class="modal-title">Project Information</h4>
                    <input id="btnClose" type="button" value="&times;" class="close white" onclick="HandlePopUp('0','divTrabau_project_info');" />
                </div>

                <!-- Modal body -->
                <div class="modal-body" id="div_project_content">
                </div>
            </div>
        </div>
    </div>


    <div id="divTrabau_project_tabs" class="modal fade" role="dialog">

        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header myCard-heading">
                    <div>
                        <h4 class="modal-title nav-modal-title"></h4>
                        <input type="button" value="&times;" class="close white" onclick="CloseProjectTabs();" />
                    </div>
                </div>

                <!-- Modal body -->
                <div class="modal-body" id="div_project_tabs_content">
                </div>
            </div>
        </div>
    </div>


    <div id="divTrabau_project_tabs_child" class="modal fade" role="dialog">

        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header myCard-heading">
                    <div>
                        <h4 class="modal-title navchild-modal-title"></h4>
                        <input type="button" value="&times;" class="close white" onclick="CloseProjectTabsChild();" />
                    </div>
                </div>

                <!-- Modal body -->
                <div class="modal-body" id="divTrabau_project_tabs_child_content">
                </div>
            </div>
        </div>
    </div>



    <div id="divTrabau_morehelp_text" class="modal fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header myCard-heading">
                    <h4 class="modal-title"></h4>
                    <input type="button" value="&times;" class="close white" onclick="HandlePopUp('0','divTrabau_morehelp_text');HandlePopUp('1', 'divTrabau_project_tabs');" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                </div>
            </div>
        </div>
    </div>


     <div id="divTrabau_NavigationContent" class="modal left fade" role="dialog">

        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header myCard-heading">
                    <input type="button" value="&times;" class="close white" onclick="CloseMobileNavigation();HandlePopUp('0','divTrabau_NavigationContent');" />
                </div>

                <!-- Modal body -->
                <div class="modal-body">
                </div>
            </div>
        </div>
    </div>
<div id="toolbar_action_popup_wrapper">
        <div id="toolbar_action_popup_list_wrapper" class="e-ddl-popup e-box e-widget e-popup e-rbn-ddl" data-role="popup" aria-hidden="true" style="overflow: hidden; min-width: 0px; max-height: auto; min-height: 20px; left: 295px; top: 160px; z-index: 10000; visibility: visible; position: absolute;display: none;">
            <div class="e-scroller e-js e-widget" style="height: auto">
                <div class="e-content" style="width: auto; height: auto;">
                    <ul class="e-ul action_relation" role="listbox" id="toolbar_action_popup" aria-hidden="true" unselectable="on">
                        <li data-value="1pt" unselectable="on" class="e-active toolbar-action-relation"><span class="flaticon-relation" ></span></li>
                        <li data-value="2pt" unselectable="on" class="toolbar-action"><span class="flaticon-compare"></span></li>
                        <li data-value="3pt" unselectable="on" class="toolbar-action"><span class="flaticon-is-not-equal-to-mathematical-symbol"></span></li>
                        <li data-value="4pt" unselectable="on" class="toolbar-action"><span class="flaticon-puzzle"></span></li>
                        <li data-value="5pt" unselectable="on" class="toolbar-action"><span class="flaticon-group"></span></li>
                    </ul>
                </div>
            </div>
        </div>
        <div id="toolbar_communication_popup_wrapper">
            <div id="toolbar_communication_popup_list_wrapper" class="e-ddl-popup e-box e-widget e-popup e-rbn-ddl" data-role="popup" aria-hidden="true" style="overflow: hidden; min-width: 0px; max-height: auto; min-height: 20px; left: 295px; top: 160px; z-index: 10000; visibility: visible; position: absolute;display: none;">
                <div class="e-scroller e-js e-widget" style="height: auto">
                    <div class="e-content" style="width: auto; height: auto;">
                        <ul class="e-ul" role="listbox" id="toolbar_communication_popup" aria-hidden="true" unselectable="on">
                            <li data-value="1pt" unselectable="on" class="e-active communication_line"><span class="toolbaricon_16 flaticon-line" ></span></li>
                            <li data-value="2pt" unselectable="on" class="communication_line"><span class="toolbaricon_16 flaticon-line"></span></li>
                            <li data-value="3pt" unselectable="on" class="communication_line"><span class="toolbaricon_16 flaticon-line"></span></li>
                            <li data-value="4pt" unselectable="on" class="communication_right_arrow"><span class="toolbaricon_16 flaticon-right-arrow"></span></li>
                            <li data-value="5pt" unselectable="on" class="communication_double_arrows"><span class="toolbaricon_16 flaticon-double-arrows"></span></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="toolbar_zoom_popup_wrapper">
            <div id="toolbar_zoom_popup_list_wrapper" class="e-ddl-popup e-box e-widget e-popup e-rbn-ddl" data-role="popup" aria-hidden="true" style="overflow: hidden; min-width: 0px; max-height: auto; min-height: 20px; left: 295px; top: 160px; z-index: 10000; visibility: visible; position: absolute;display: none;">
                <div class="e-scroller e-js e-widget" style="height: auto">
                    <div class="e-content" style="width: auto; height: auto;">
                        <ul class="e-ul" role="listbox" id="toolbar_zoom_popup" aria-hidden="true" unselectable="on">
                            <li data-value="1pt" unselectable="on" class="e-active"><span>100%</span></li>
                            <li data-value="2pt" unselectable="on" class=""><span>200%</span></li>
                            <li data-value="3pt" unselectable="on" class=""><span>300%</span></li>
                            <li data-value="4pt" unselectable="on" class=""><span>50%</span></li>
                            <li data-value="5pt" unselectable="on" class=""><span>75%</span></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="toolbar_view_popup_wrapper">
            <div id="toolbar_view_popup_list_wrapper" class="e-ddl-popup e-box e-widget e-popup e-rbn-ddl" data-role="popup" aria-hidden="true" style="overflow: hidden; min-width: 0px; max-height: auto; min-height: 20px; left: 295px; top: 160px; z-index: 10000; visibility: visible; position: absolute;display: none;">
                <div class="e-scroller e-js e-widget" style="height: auto">
                    <div class="e-content" style="width: auto; height: auto;">
                        <ul class="e-ul" role="listbox" id="toolbar_view_popup" aria-hidden="true" unselectable="on">
                            <li data-value="" unselectable="on" class="e-active"><span>Sub Function</span></li>
                            <li data-value="" unselectable="on" class=""><span>Sub Application</span></li>
                            <li data-value="" unselectable="on" class=""><span>Sub Result</span></li>
                            <li data-value="" unselectable="on" class=""><span>Show Hide & Models</span></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="toolbar_fontSize_popup_wrapper">
        <div id="toolbar_fontSize_popup_list_wrapper" class="e-ddl-popup e-box e-widget e-popup e-rbn-ddl" data-role="popup" aria-hidden="false" style="overflow: hidden; min-width: 60px; max-height: 152px; min-height: 20px; left: 378.734px; top: 91px; z-index: 10000; visibility: visible; height: auto; display: none;">
            <div class="e-scroller e-js e-widget" style="height: auto; display: block;">
                <div class="" style="width: auto; height: auto;">
                    <ul class="e-ul" role="listbox" id="toolbar_fontSize_popup" aria-hidden="false" unselectable="on">
                        <li data-value="1pt" unselectable="on" class="e-active">1pt</li>
                        <li data-value="2pt" unselectable="on" class="">2pt</li>
                        <li data-value="3pt" unselectable="on" class="">3pt</li>
                        <li data-value="4pt" unselectable="on">4pt</li>
                        <li data-value="5pt" unselectable="on">5pt</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div id="toolbar_fontFamily_popup_wrapper">
        <div id="toolbar_fontFamily_popup_list_wrapper" class="e-ddl-popup e-box e-widget e-popup e-rbn-ddl" data-role="popup" aria-hidden="true" style="display: none; overflow: hidden; min-width: 0px; max-height: 152px; min-height: 20px; left: 287px; top: 202.5px; z-index: 10000; visibility: visible;">
            <div class="e-scroller e-js e-widget" style="height: 150px;">
                <div class="e-content" style="width: auto; height: auto;">
                    <ul class="e-ul" role="listbox" id="toolbar_fontFamily_popup" aria-hidden="true" unselectable="on">
                        <li data-value="Segoe UI" unselectable="on" class="e-active">Segoe UI</li>
                        <li data-value="Times New Roman" unselectable="on">Times New Roman</li>
                        <li data-value="Arial" unselectable="on">Arial</li>
                        <li data-value="Times New Roman" unselectable="on">Times New Roman</li>
                        <li data-value="Tahoma" unselectable="on">Tahoma</li>
                        <li data-value="Helvetica" unselectable="on">Helvetica</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script src='<%= Page.ResolveUrl("~/assets/plugins/flickity/js/flickity.pkgd.js") %>'></script>
    <link href="https://npmcdn.com/flickity@2/dist/flickity.css" rel="stylesheet" type="text/css" />
    <script>
        var pathconfig = '<%= Page.ResolveUrl("view-project.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/view-project.js?version=1.9.7") %>'></script>
    <script src='<%= Page.ResolveUrl("~/assets/js/toolbar.js") %>'></script>
</asp:Content>

