<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucProposalAdditionalFiles.ascx.cs" Inherits="Jobs_Posting_UserControls_wucProposalAdditionalFiles" %>
<div class="proposal-additional-files">
    <div id="div_additional_files" runat="server" visible="false">
        <label><b>Additional files</b></label>
        <div class="row">
            <ol class="project-items">
                <asp:HiddenField ID="hfAdditionalFiles_ProposalId" runat="server" Visible="false" />
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