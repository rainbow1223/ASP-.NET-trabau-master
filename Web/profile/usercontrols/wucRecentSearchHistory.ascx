<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucRecentSearchHistory.ascx.cs" Inherits="profile_usercontrols_wucRecentSearchHistory" %>
<div class="search-history-data">
    <div class="search-header">
        Recent searches
    </div>
    <asp:Repeater ID="rSearchHistory" runat="server">
        <ItemTemplate>
            <div class="search-item">
                <a href='<%# "@href?query="+Eval("SearchText").ToString() %>'><%#Eval("SearchText") %></a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
