<%@ Page Title="Highest Rated Freelancers by Category - Trabau" Language="C#" MasterPageFile="~/index.master" AutoEventWireup="true" CodeFile="hire.aspx.cs" Inherits="hire" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="inner-page-banner hire-banner">
        <div class="container">
            <div class="row d-flex align-items-center">
                <div class="col-lg-7">
                    <div class="bannerContent">
                        <h2>Hire the best freelancers, 
                            <br>
                            agencies, and contractors</h2>
                        <p>For every demand, we have a service to offer with the best freelancers, agencies, and contractors for your writing, marketing, management, renovation and other types of projects.</p>
                        <a href="#" class="cta-btn-md">Get Started</a>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="bannerGraphic">
                        <img src="assets/uploads/banner-graphic-7.png" alt="bannerGraphic" />
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="category-sec p-80">
        <div class="container">
            <div class="main-sub-heading">
                <h2>Browse top freelancers, agencies, 
                    <br />
                    and contractors by category</h2>
                <p>Offer services as a freelancer, agency, or contractor in any industry. Get hired by reputable clients to deliver services on a profitable contract.</p>
            </div>

            <div class="category-slider-block">

                <div class="slider variable-width category-content">
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
                    <%-- <div class="linear-background">
                        <div class="inter-right-top-1"></div>
                        <div class="inter-right-top-2"></div>
                        <div class="inter-right-top-3"></div>
                        <div class="inter-right-top-4"></div>
                        <div class="inter-right-top-5"></div>
                        <div class="inter-right-top-6"></div>
                    </div>
                    <div class="linear-background">
                        <div class="inter-right-top-1"></div>
                        <div class="inter-right-top-2"></div>
                        <div class="inter-right-top-3"></div>
                        <div class="inter-right-top-4"></div>
                        <div class="inter-right-top-5"></div>
                        <div class="inter-right-top-6"></div>
                    </div>
                    <div class="linear-background">
                        <div class="inter-right-top-1"></div>
                        <div class="inter-right-top-2"></div>
                        <div class="inter-right-top-3"></div>
                        <div class="inter-right-top-4"></div>
                        <div class="inter-right-top-5"></div>
                        <div class="inter-right-top-6"></div>
                    </div>
                    <div class="linear-background">
                        <div class="inter-right-top-1"></div>
                        <div class="inter-right-top-2"></div>
                        <div class="inter-right-top-3"></div>
                        <div class="inter-right-top-4"></div>
                        <div class="inter-right-top-5"></div>
                        <div class="inter-right-top-6"></div>
                    </div>
                </div>--%>
                </div>
            </div>
        </div>
    </section>

    <section class="freelancers-sec p-80 pt-0">
        <div class="container">
            <div class="main-sub-heading">
                <h2>Top 10 Trending Freelancers</h2>
                <p>Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi</p>
            </div>

            <div class="category-slider-block">

                <div class="slider variable-width-2">

                    <asp:Repeater ID="rFreelancers" runat="server">
                        <ItemTemplate>
                            <div class="fs-item">
                                <div class="fs-thumbnail shadow-sm trending-freelancers">
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
                                        <p class="freelancer-overview"><%#Eval("Overview") %></p>
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

            </div>
        </div>
    </section>
    <script src='<%= Page.ResolveUrl("~/assets/js/trabau-utility.js?version=1.4") %>'></script>
    <script>
        var pathconfig = '<%= Page.ResolveUrl("hire.aspx") %>';
        $(document).ready(function () {

        });
        function ViewMore(id) {
            $(id).parent('div').find('ul li:nth-child(10)').nextAll().slideDown(200);
            $(id).hide();
            $(id).parent('div').find('input[id*="btnLess"]').show();
            return false;
        }

        function ViewLess(id) {
            $(id).parent('div').find('ul li:nth-child(10)').nextAll().slideUp(200);
            $(id).hide();
            $(id).parent('div').find('input[id*="btnMore"]').show();
            return false;
        }



        <%-- function LoadCategories() {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: '<%= Page.ResolveUrl("~/hire.aspx/DisplayCategories") %>',
                data: "{}",
                success: function (msg) {
                    var cat_details = msg.d;
                    cat_details = cat_details.substring(cat_details.indexOf('<div class="div_categories">'), cat_details.indexOf('</form>'));

                    $('.category-content').html(cat_details);

                    CategoriesSlick();

                }
               ,
                error: function (xhr, ajaxOptions, thrownError) {
                    // window.location = "Login.aspx";
                }
            });
        }--%>
    </script>
    <script src='<%= Page.ResolveUrl("~/assets/js/search_freelancers.js?version=1.3") %>'></script>

</asp:Content>

