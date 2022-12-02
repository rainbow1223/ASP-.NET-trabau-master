<%@ Page Title="" Language="C#" MasterPageFile="~/index.master" AutoEventWireup="true" CodeFile="hire-skillwise.aspx.cs" Inherits="hire_skillwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="inner-page-banner singleskill-banner">
        <div class="container">
            <div class="row d-flex align-items-center">
                <div class="col-lg-7">
                    <div class="bannerContent">
                        <h2>Hire the best
                            <br />
                            <asp:Literal ID="ltrlSkillName" runat="server"></asp:Literal>
                            Developers</h2>
                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, mod tempor incididunt ut labore et dolore magna ad minim veniam, quis nostrud. </p>
                        <a href="#" class="cta-btn-md">Get Started</a>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="bannerGraphic">
                        <img src="assets/uploads/banner-graphic-9.png" alt="bannerGraphic" />
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="category-sec p-80">
        <asp:UpdatePanel ID="upFreelancers" runat="server" class="container">
            <ContentTemplate>
                <div class="cat-filters">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <div class="cat-filter-opt">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <label>Country</label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="frmJob-select" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>Job Success</label>
                                        <asp:DropDownList ID="ddlJobSuccess" runat="server" CssClass="frmJob-select" OnSelectedIndexChanged="ddlJobSuccess_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label>Hourly rate</label>
                                        <asp:DropDownList ID="ddlHourlyRate" runat="server" CssClass="frmJob-select" OnSelectedIndexChanged="ddlHourlyRate_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Repeater ID="rFreelancers" runat="server">
                        <ItemTemplate>
                            <div class="col-xl-4 col-lg-6 col-sm-6">
                                <div class="fs-thumbnail shadow-sm mb-4">
                                    <div class="fs--img-thumb"></div>
                                    <div class="fs-profile">
                                        <div class="profilefoto" id="div_profile_photo" runat="server">
                                            <img alt="user" runat="server" id="imgFL_ProfilePic" src="" />
                                        </div>
                                        <div class="profileRating">
                                            <h5><%#Eval("JobSuccessRate") %></h5>
                                            <p><%#Eval("CountryName") %></p>
                                        </div>
                                    </div>
                                    <div class="fs-content-thumb">
                                        <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' Visible="false"></asp:Label>
                                        <h4><%#Eval("Name") %></h4>
                                        <p class="expertskills"><%#Eval("Title") %></p>
                                        <ul class="tags">
                                            <asp:Repeater ID="rFreelancers_Skills" runat="server">
                                                <ItemTemplate>
                                                    <li><a href='<%#"hire-skillwise.aspx?skill="+Eval("EncSkillId").ToString() %>'><%#Eval("SkillName") %></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                        <div>
                                            <a href='<%#"profile/user/userprofile.aspx?profile="+Eval("profile_id").ToString() %>' class="cta-btn-sm">View Profile</a>
                                            <a class='<%#"cta-btn-sm btn-prefer-list"+Eval("Preferred_ListClass").ToString() %>' data='<%#Eval("profile_id") %>' runat="server" visible='<%# Eval("CanAdd") %>'><%#Eval("Preferred_List") %></a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>

    <section class="green-strips text-center" id="div_signup" runat="server">
        <div class="container">
            <div class="main-sub-heading mb-4">
                <h2>Sign up to view more profiles</h2>
            </div>
            <a href='<%= Page.ResolveUrl("~/signup/index.aspx") %>' class="cta-btn-md btn-color-white">Sign Up</a>
        </div>
    </section>

    <section class="howItWork-sec p-80">
        <div class="container">
            <div class="main-heading text-center">
                <h2>How it <span>Works?</span></h2>
            </div>
            <div class="howWork-content">
                <div class="row">
                    <div class="col-lg-4 col-sm-6 col-6">
                        <div class="howWork-thumb">
                            <div class="ht-icon"><i class="flaticon-paper"></i></div>
                            <span>1</span>
                            <h5>Have a problem</h5>
                            <p>You have a problem to solve. You have performed some analysis and identified the problem.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-6">
                        <div class="howWork-thumb">
                            <div class="ht-icon"><i class="flaticon-leader"></i></div>
                            <span>2</span>
                            <h5>Post a job</h5>
                            <p>After identifying the problem, you need someone to help you solve it. You post a job to get someone to help you solve it.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-6">
                        <div class="howWork-thumb">
                            <div class="ht-icon"><i class="flaticon-work"></i></div>
                            <span>3</span>
                            <h5>Hire a person</h5>
                            <p>People who are interested apply for the job. You hire someone to help you solve the problem.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-6">
                        <div class="howWork-thumb">
                            <div class="ht-icon"><i class="flaticon-pen"></i></div>
                            <span>4</span>
                            <h5>Create a project</h5>
                            <p>You create a project to help you manage the function of the person you have hired to help you solve the problem.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-6">
                        <div class="howWork-thumb">
                            <div class="ht-icon"><i class="flaticon-safe"></i></div>
                            <span>5</span>
                            <h5>Manage the project</h5>
                            <p>You manage the project by managing the person you have hired as well as the function of that person.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-6">
                        <div class="howWork-thumb">
                            <div class="ht-icon"><i class="flaticon-safe"></i></div>
                            <span>6</span>
                            <h5>Problem solved</h5>
                            <p>Your problem is solved and you feel very happy. Now, you no longer have the same problem.</p>
                        </div>
                    </div>
                </div>
            </div>
            <br>
            <div class="text-center">
                <a href="how-it-works.aspx" class="cta-btn-md btn-color-reverse">See How it Works?</a>
            </div>

        </div>
    </section>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("hire.aspx") %>';
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/trabau-utility.js?version=1.5") %>'></script>
</asp:Content>

