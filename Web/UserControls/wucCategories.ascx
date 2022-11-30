<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucCategories.ascx.cs" Inherits="UserControls_wucCategories" %>
<div class="div_categories">
    <asp:Repeater ID="rCategories" runat="server">
        <ItemTemplate>
            <div class="cs-item">
                <div class="cat-thumbnail">
                    <div class="cat--img-thumb shadow-sm">
                        <img src='<%#Eval("CategoryIconPath") %>' alt="image">
                    </div>
                    <div class="cat-content-thumb">
                        <asp:Label ID="lblCategoryId" runat="server" Text='<%#Eval("ServiceCategoryId") %>' Visible="false"></asp:Label>
                        <h4><%#Eval("ServiceCategoryName") %>
                            <br>
                        </h4>
                        <ul>
                            <asp:Repeater ID="rSubCategories" runat="server">
                                <ItemTemplate>
                                    <li class="sub-categories"><a href='<%#"hire-categorywise.aspx?category="+Eval("EncCateoryId") %>'><%#Eval("ServiceCategoryName") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <input type="button" runat="server" visible="false" id="btnMore" onclick="ViewMore(this);" value="More +" />
                        <input type="button" runat="server" visible="false" id="btnLess" class="hidden" onclick="ViewLess(this);" value="Less -" />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
