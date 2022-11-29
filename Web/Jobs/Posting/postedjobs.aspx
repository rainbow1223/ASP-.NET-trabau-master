<%@ Page Title="My Jobs - Trabau" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="postedjobs.aspx.cs" Inherits="Jobs_Posting_postedjobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        $(document).ready(function () {
            $('.ajob_menu').click(function () {
                $('.job-menu').not($(this).parent('td').find('.job-menu')).hide();

                if ($(this).parent('td').find('.job-menu').css('display') == 'none') {
                    $(this).parent('td').find('.job-menu').show();

                    var elem = document.createElement('div');
                    elem.className = "modal-backdrop fade in job-menu-shadow";
                    elem.style.cssText = "z-index:999;";
                    document.body.appendChild(elem);

                    $('.job-menu-shadow').click(function () {
                        $('.job-menu').hide();
                        $('.job-menu-shadow').remove();
                    });
                }
                else {
                    $('.job-menu-shadow').remove();
                    $(this).parent('td').find('.job-menu').hide();
                }
            });

            $(document).on('keyup', function (evt) {
                if (evt.keyCode == 27) {
                    $('.job-menu').hide();
                    $('.job-menu-shadow').remove();
                }
            });

            $('#txtJobTitle').keyup(function () {
                var value = this.value.toLowerCase().trim();

                $("table tr").each(function (index) {
                    if (index >= 0) {
                        $row = $(this);

                        var id = $row.find("td:first").text().toLowerCase().trim();

                        if (id.indexOf(value) == -1) {
                            $(this).hide();
                        }
                        else {
                            $(this).show();
                        }
                    }

                });
            });

        });
    </script>
    <div class="my-card shadow-sm">
        <div class="myCard-heading">
            <h4>My Jobs</h4>
        </div>
        <div class="myCard-body">
            <h6 class="search-job-header">Search Job Posting</h6>
            <input id="txtJobTitle" type="text" placeholder="Type to search posted jobs" class="form-control" />


            <div class="listofjobs-posted-table p-3">
                <table>
                    <asp:Repeater ID="rPostedJobs" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
                                    <h6><%#Eval("JobTitle") %></h6>
                                    <p><%#Eval("PostedOn") %></p>
                                    <p><small><%#Eval("JobVisibility") %> - <%#Eval("JobBudgetType") %></small></p>
                                </td>
                                <td>
                                    <h6><%#Eval("Proposals") %></h6>
                                    <p>Proposals</p>
                                </td>
                                <td>
                                    <h6><%#Eval("Messaged") %></h6>
                                    <p>Messaged</p>
                                </td>
                                <td>
                                    <h6><%#Eval("Hired") %></h6>
                                    <p>Hired</p>
                                </td>
                                <td class="job-menu-main">
                                    <a class="edit-pencil-button ajob_menu"><i class="fa fa-ellipsis-h" aria-hidden="true"></i></a>
                                    <ul class="job-menu">
                                        <asp:Repeater ID="rPostedJobMenu" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Label ID="lblJobId" runat="server" Text='<%#Eval("JobId") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblMenuCode" runat="server" Text='<%#Eval("MenuCode") %>' Visible="false"></asp:Label>
                                                    <asp:LinkButton ID="lbtnMenuLink" runat="server" Text='<%#Eval("Name") %>' OnClick="lbtnMenuLink_Click"></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

