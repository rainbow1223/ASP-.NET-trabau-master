<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucProjectsCount.ascx.cs" Inherits="projects_UserControls_wucProjectsCount" %>
<div class="project-count-details">

    <asp:Repeater ID="rProjects" runat="server">
        <ItemTemplate>
            <div class="project-content">
                <div class="project-result-overview">
                    <p class="project-result-top">
                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' Style="display: none"></asp:Label>
                        <i class="fa fa-angle-down opj-arrow" aria-hidden="true"></i><a><%#Eval("Status") %> Projects  (Total: <%#Eval("Total") %>)</a>
                    </p>
                    <div class="new-project" style="display: none">
                        <a class="btn-view-search-profile btn-new-project" data-toggle="popover" tabindex="0" data-trigger="focus" data-content="Create New Project">Create New Project</a>
                        <a class="edit-pencil-button btn-new-project-small" data-toggle="popover" tabindex="0" data-trigger="focus" data-content="Create New Project">
                            <i class="fa fa-plus" aria-hidden="true"></i>
                        </a>
                    </div>
                    <div class="new-project-toolip-info">
                        <a class="edit-pencil-button" data-toggle="popover" tabindex="0" data-trigger="focus" data-content='<%#Eval("Tooltip") %>'>
                            <i class="fa fa-info" aria-hidden="true"></i>
                        </a>
                    </div>
                    <div class="project-content-details">
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
