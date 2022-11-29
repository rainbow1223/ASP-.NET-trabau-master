<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucJobAdditionalFiles.ascx.cs" Inherits="Jobs_searchjobs_UserControls_wucJobAdditionalFiles" %>

<div class="job-additional-files">
    <div id="div_additional_files" runat="server" visible="false">
        <hr />
        <h3>Additional files</h3>
        <div class="row">
            <ol class="project-items">
                <asp:HiddenField ID="hfAdditionalFiles_JobId" runat="server" Visible="false" />
                <asp:Repeater ID="rAdditionalFiles" runat="server">
                    <ItemTemplate>
                        <li download-url='<%#Eval("file_key") %>' onclick="DownloadAdditionalFile(this)">
                            <%#Eval("file_name") %>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <li class="empty-data" id="div_profile_files_empty" runat="server" visible="false">No files added</li>
            </ol>
        </div>
    </div>
</div>
