<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucProjects.ascx.cs" Inherits="projects_UserControls_wucProjects" %>
<div class="projects-control">
    <asp:Repeater ID="rProjects" runat="server">
        <ItemTemplate>
            <asp:Label ID="lblProjectID" runat="server" Visible="false" Text='<%#Eval("ProjectID") %>'></asp:Label>
            <div class="profile-card">
                <%--                <div class="project-actions">
                    <a href='<%#Eval("ProjectURL") %>' class="btn-view-search-profile" data-toggle="popover" tabindex="0" data-trigger="focus" data-content="View Details" data-original-title="" title="">View Details</a>
                </div>--%>
                <div class="airCardBody padding-20">
                    <div class="row">
                        <div class="col-sm-5">
                            <div class="profile-card project-card">
                                <div class="airCardBody padding-20 project-menu-main">
                                    <span class="project-options">Project Options</span>
                                    <a class="btn-project-details" href='<%#Eval("ProjectURL") %>' runat="server" visible='<%#Eval("ViewProject_Visibility") %>'>View Project</a>
                                    <a class="edit-pencil-button aproject_menu" id="aproject_menu" runat="server"><i class="fa fa-ellipsis-h" aria-hidden="true"></i></a>
                                    <ul class="project-menu" id="ul_project_menu" runat="server">
                                        <asp:Repeater ID="rProjectMenu" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <a href='<%#Eval("ProjectURL") %>'><%#Eval("MenuName") %></a>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-7">
                            <div>
                                <span>Project Name: <b><%#Eval("ProjectName") %></b></span>
                            </div>
                            <div>
                                <span>Project Function: <b><%#Eval("ProjectFunction") %></b></span>
                            </div>
                             <div>
                                <span>Number of People: <b><%#Eval("NoOfPeople") %></b></span>
                            </div>
                            <div>
                                <span>Project Status: <b><%#Eval("ProjectStatus") %></b></span>
                            </div>
                            <div>
                                <span>Project Budget: <b><%#Eval("ProjectBudget") %></b></span>
                            </div>
                            <div>
                                <span>Start Date: <b><%#Eval("StartDate") %></b></span>
                            </div>
                            <div>
                                <span>Expected Completion Date: <b><%#Eval("EndDate") %></b></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
     <div class="no-jobs-found text-center" id="div_noprojects" runat="server" visible="false">No Projects added yet</div>
    <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
</div>
