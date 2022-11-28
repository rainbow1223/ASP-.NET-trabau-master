<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucsearch_profiles_left_filters.ascx.cs" Inherits="profile_usercontrols_wucsearch_profiles_left_filters" %>
<div class="filters-result-data">
    <asp:Repeater ID="rFilters" runat="server">
        <ItemTemplate>
            <div class="filter">

                <div class="profile-filter-main">
                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                    <i class="flaticon-down-chevron" aria-hidden="true"></i></div>
                <div class="profile-filter-child" id='<%#Eval("TypeFilter") %>'>
                    <asp:Repeater ID="rInnerFilters" runat="server">
                        <ItemTemplate>
                            <a id='<%#Eval("Value") %>'><%#Eval("Text") %></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
