<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="company-details.aspx.cs" Inherits="Signup_company_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label[for*="rbtnlNoOfEmployees"], label[for*="rbtnlDepartment"] {
            margin-left: 10px;
        }

        .category, .minutes {
            display: inline-block;
            margin: 10px;
            vertical-align: top;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            cursor: pointer;
        }

            .category:hover, .minutes:hover {
                box-shadow: 0 16px 32px 0 rgba(0,0,0,0.2);
            }

            .category .category-img img {
                width: 40%;
            }

            .category .category-img {
                background-color: #435382;
                width: 220px;
                height: 120px;
                padding: 10px;
            }

        .category-bottom {
            height: 35px;
            width: 200px;
            word-break: break-all;
            white-space: nowrap;
            overflow: hidden !important;
            text-overflow: ellipsis;
            padding: 5px 10px;
        }

        .Category-Selected {
            background: url("../assets/Images/tick.png") no-repeat center !important;
            color: #000 !important;
            border: 1px solid #eae3e3 !important;
        }

        .minutes {
            width: 300px;
        }

        .minutes-top {
            font-size: 16px;
            width: 100%;
            height: 50px;
            padding: 10px;
            font-weight: bold;
        }

        .minutes-bottom {
            height: 80px;
            width: 100%;
            padding: 5px 10px;
        }

        .minutes-main {
            margin-top: 30px;
        }

        .minutes-selected {
            background: url("../assets/Images/tick.png") no-repeat center !important;
            color: #000 !important;
            border: 1px solid #867e7e !important;
        }
    </style>
    <script>
        function LoadEvents() {
            $(document).ready(function () {
                $('.category').click(function () {
                    var CategoryId = $(this).find('span[id*="lblCategoryId"]').text();
                    $('input[id*="hfCategoryId"]').val(CategoryId);
                    $('.category-img').removeClass('Category-Selected');
                    $(this).find('.category-img').toggleClass('Category-Selected');
                });

                $('.minutes').click(function () {
                    var minutesid = $(this).attr('id');
                    $('input[id*="hf_minutes"]').val(minutesid);
                    $('.minutes').removeClass('minutes-selected');
                    $(this).toggleClass('minutes-selected');
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="CompanyDetails-Updation">
            <asp:UpdatePanel ID="upSignUp" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <script>
                        Sys.Application.add_load(LoadEvents);
                    </script>
                    <div class="row SignUp" id="div_signup_step1" runat="server" visible="false">
                        <div class="col-sm-12 text-center">
                            <h2>Get better matched by adding company details</h2>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label>How many employees does your company have?</label>
                                <asp:RadioButtonList ID="rbtnlNoOfEmployees" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnlNoOfEmployees_SelectedIndexChanged">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="rbtnlNoOfEmployees" SetFocusOnError="true" ErrorMessage="Select Employees Count" ValidationGroup="Company_Update" Display="Dynamic" />
                            </div>
                            <div class="form-group" id="div_department" runat="server" visible="false">
                                <label>Which department do you work in?</label>
                                <asp:RadioButtonList ID="rbtnlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnlDepartment_SelectedIndexChanged">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="rbtnlDepartment" SetFocusOnError="true" ErrorMessage="Select Department" ValidationGroup="Company_Update" Display="Dynamic" />
                            </div>
                            <div class="form-group" id="div_phonenumber" runat="server" visible="false">
                                <label>What's your business phone number?</label>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Phone Number"></asp:TextBox>
                                    </div>
                                </div>


                                <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="ddlCountryCode" SetFocusOnError="true" ErrorMessage="Select Country Code" ValidationGroup="Company_Update" Display="Dynamic" />
                            </div>
                            <div class="form-group text-center">
                                <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="cta-btn-sm"  OnClick="btnNext_Click" ValidationGroup="Company_Update" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div_signup_step2" runat="server" visible="false">
                        <div class="col-sm-12 text-center">
                            <h2>Which category are you interested in?</h2>
                        </div>
                        <div class="col-sm-12">
                            <asp:Repeater ID="rCategories" runat="server">
                                <ItemTemplate>
                                    <div class="category">
                                        <div class="category-img text-center">
                                            <img src='<%# Eval("IconPath") %>' />
                                            <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("Value") %>' Style="display: none;"></asp:Label>
                                        </div>
                                        <div class="text-center category-bottom"><%# Eval("Text") %></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:HiddenField ID="hfCategoryId" runat="server" />
                            <div class="form-group text-center">
                                <asp:Button ID="btnNext_Step2" runat="server" Text="Next" OnClick="btnNext_Step2_Click" CssClass="cta-btn-sm" />
                            </div>
                        </div>

                    </div>

                    <div class="row" id="div_signup_step3" runat="server" visible="false">
                        <div class="col-sm-12 text-center">
                            <h2>Connect with qualified talent in minutes</h2>
                        </div>
                        <div class="col-sm-12 text-center minutes-main">
                            <div class="minutes" id="detailed">
                                <div class="minutes-top text-center">
                                    Detailed Job Post
                                </div>
                                <div class="minutes-middle text-center">
                                    (~ 6 minutes)
                                </div>
                                <div class="text-center minutes-bottom">Popular for long term, ongoing contract work and complex projects</div>
                            </div>
                            <div class="minutes" id="quick">
                                <div class="minutes-top text-center">
                                    Quick Job Post
                                </div>
                                <div class="minutes-middle text-center">
                                    (~ 3 minutes)
                                </div>
                                <div class="text-center minutes-bottom">Popular for simple, short term projects and contract work</div>
                            </div>
                            <asp:HiddenField ID="hf_minutes" runat="server" />
                            <div class="form-group text-center">
                                <asp:Button ID="btnNext_Step3" runat="server" Text="Next" OnClick="btnNext_Step3_Click" CssClass="cta-btn-sm" />
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

