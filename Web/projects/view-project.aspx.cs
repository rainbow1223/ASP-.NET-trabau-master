using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TrabauClassLibrary;
using TrabauClassLibrary.DLL.Navigation;
using TrabauClassLibrary.DLL.Projects;
using TrabauClassLibrary.DLL.Projects.Constants;
using TrabauClassLibrary.DLL.Projects.Models;
using Newtonsoft.Json;
using TrabauClassLibrary.DLL.Models;
using TrabauClassLibrary.DLL.Constants;
using TrabauClassLibrary.Interfaces;
using TrabauClassLibrary.DLL.API;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.UI;

public partial class projects_view_project : System.Web.UI.Page
{
    public static int _ProjectID { get; set; }
    private static readonly IHttpsProxy httpsProxy = new HttpsProxy();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["Project_AllNavFieldsDefinitions"] = null;
            Session["TabProject_Files"] = null;
            Session["ProjectTabContent"] = null;
            Session["ProjectNavigation_Menu_Cached_Full"] = null;
            if (!IsPostBack)
            {
                string domain = "";
                if (Request.QueryString.Count == 2)
                {
                    if (Request.QueryString["domain"] != null && Request.QueryString["id"] != null)
                    {
                        if (Request.QueryString["domain"] != string.Empty && Request.QueryString["id"] != string.Empty)
                        {
                            domain = Request.QueryString["domain"];
                            domain = MiscFunctions.DecodeAndDecrypt(domain, Trabau_Keys.Domain_Key);

                            string ProjectID_Enc = Request.QueryString["id"];
                            string ProjectID = MiscFunctions.DecodeAndDecrypt(ProjectID_Enc, Trabau_Keys.Project_Key);

                            _ProjectID = Int32.Parse(ProjectID);

                            string ProjectID_Enc_Edit = ProjectID;
                            ProjectID_Enc_Edit = MiscFunctions.EncryptAndEncode(ProjectID_Enc_Edit + "~true", Trabau_Keys.Project_Key);

                            string ProjectID_Enc_View = ProjectID;
                            ProjectID_Enc_View = MiscFunctions.EncryptAndEncode(ProjectID_Enc_View + "~false", Trabau_Keys.Project_Key);

                            //LoadNavigation(domain, ProjectID, ProjectID_Enc, ProjectID_Enc_Edit, ProjectID_Enc_View);
                            LoadMenus(domain, ProjectID, ProjectID_Enc, ProjectID_Enc_Edit, ProjectID_Enc_View);

                            Session["Project_ReturnURL"] = Request.RawUrl.Replace("/projects/", "");
                        }
                        else
                        {
                            RedirectToDefaultPage();
                        }
                    }
                    else
                    {
                        RedirectToDefaultPage();
                    }
                }
                else
                {
                    RedirectToDefaultPage();
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    public void LoadMenus(string domain, string ProjectID, string ProjectID_Enc, string ProjectID_Enc_Edit, string ProjectID_Enc_View)
    {
        try
        {
            trabau_navigation obj = new trabau_navigation();
            string UserID = Session["Trabau_UserId"].ToString();
            var _menu = obj.GetUserProjectNavigation(Int64.Parse(UserID), domain, Int32.Parse(ProjectID), ProjectID_Enc, ProjectID_Enc_Edit, ProjectID_Enc_View, ParentId: 0);

            try
            {
                string ProjectName = _menu[0].ProjectName;
                (this.Page.Master.FindControl("project_title") as HtmlGenericControl).InnerText = ProjectName;
            }
            catch (Exception)
            {
            }
            Session["Project_Menu"] = _menu;

            _menu.ForEach(x =>
            {
                x.MenuIdEnc = (string.IsNullOrEmpty(x.MenuIdEnc) ? MiscFunctions.EncryptAndEncode(x.MenuId.ToString(), Trabau_Keys.Project_Key) : x.MenuIdEnc);
            });

            var menuRequiredToUpdate = _menu.Where(x => string.IsNullOrEmpty(x.MenuIdEnc));
            if (menuRequiredToUpdate.Count() > 0)
            {
                using (Project prj = new Project())
                {
                    string menu_enc = new XElement("menudata",
                                (from d in menuRequiredToUpdate
                                 select new XElement("data",
                                     new XElement("menuid", d.MenuId),
                                     new XElement("menuid_enc", d.MenuId_Enc)
                                                    )
                                 )
                             ).ToString();
                    prj.UpdateMenuEnc(Int64.Parse(UserID), menu_enc);
                }
            }

            var parent_menu = _menu.Where(b => b.ParentId == 0).Select(a => new
            {
                MenuId = a.MenuId,
                MenuIdEnc = a.MenuIdEnc,
                Name = a.Name,
                OrderNo = a.OrderNo,
                ChildCount = _menu.Where(m => m.ParentId == a.MenuId).Count()
            }).ToList().Distinct().OrderBy(x => x.OrderNo);

            rMainMenu.DataSource = parent_menu;
            rMainMenu.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public void RedirectToDefaultPage()
    {
        try
        {
            string UserType = Session["Trabau_UserType"].ToString();
            string RedirectURL = "https://" + Request.Url.Authority + "/jobs/posting/postedjobs.aspx";
            if (UserType == "W")
            {
                RedirectURL = "https://" + Request.Url.Authority + "/jobs/searchjobs/index.aspx";
            }
            Response.Redirect(RedirectURL);
        }
        catch (Exception)
        {
        }
    }

    public void LoadNavigation(string domain, string ProjectID, string ProjectID_Enc, string ProjectID_Enc_Edit, string ProjectID_Enc_View)
    {
        try
        {
            trabau_navigation obj = new trabau_navigation();
            string UserID = Session["Trabau_UserId"].ToString();
            var _menu = obj.GetUserProjectNavigation(Int64.Parse(UserID), domain, Int32.Parse(ProjectID), ProjectID_Enc, ProjectID_Enc_Edit, ProjectID_Enc_View, ParentId: 0);

            try
            {
                string ProjectName = _menu[0].ProjectName;
                (this.Page.Master.FindControl("project_title") as HtmlGenericControl).InnerText = ProjectName;
                //h4_heading.InnerText = "Project details (" + ProjectName + ")";
            }
            catch (Exception)
            {
            }
            Session["Project_Menu"] = _menu;

            var parent_menu = _menu.Where(b => b.ParentId == 0).Select(a => new { MenuId = a.MenuId, Name = a.Name, OrderNo = a.OrderNo }).ToList().Distinct().OrderBy(x => x.OrderNo);

            rRibbonTabs.DataSource = parent_menu;
            rRibbonTabs.DataBind();

            //ddlMobileTabs.DataSource = parent_menu;
            //ddlMobileTabs.DataTextField = "Name";
            //ddlMobileTabs.DataValueField = "Name";
            //ddlMobileTabs.DataBind();


            rRibbonTabs_Content.DataSource = parent_menu;
            rRibbonTabs_Content.DataBind();

            foreach (RepeaterItem item in rRibbonTabs_Content.Items)
            {
                try
                {


                    string MainMenuId = (item.FindControl("lblMainMenuId") as Label).Text;
                    Repeater rRibbonTabs_Menu = (item.FindControl("rRibbonTabs_Menu") as Repeater);

                    var _submenu = _menu.Where(a => a.ParentId.ToString() == MainMenuId).Select(b => new
                    {
                        b.MenuId,
                        b.Name,
                        b.NavigationURL,
                        b.IconURL,
                        b.OrderNo,
                        b.CanSwitchDomain
                    }).ToList().OrderBy(x => x.OrderNo);


                    rRibbonTabs_Menu.DataSource = _submenu;
                    rRibbonTabs_Menu.DataBind();

                    string redirect_domain = "Communication";
                    if (domain == redirect_domain)
                    {
                        redirect_domain = "Theory";
                    }
                    redirect_domain = EncyptSalt.EncryptText(redirect_domain, Trabau_Keys.Domain_Key);
                    redirect_domain = MiscFunctions.Base64EncodingMethod(redirect_domain);

                    foreach (RepeaterItem childitem in rRibbonTabs_Menu.Items)
                    {
                        try
                        {
                            string SubMenuId = (childitem.FindControl("lblSubMenuId") as Label).Text;

                            Repeater rRibbonTabs_ChildMenu = (childitem.FindControl("rRibbonTabs_ChildMenu") as Repeater);


                            string DomainSwitchURL = "https://" + Request.Url.Authority + "/projects/view-project.aspx?domain=@domain&id=" + ProjectID_Enc;

                            DomainSwitchURL = DomainSwitchURL.Replace("@domain", redirect_domain);

                            var _child_submenu = _menu.Where(a => a.ParentId.ToString() == SubMenuId).Select(b => new
                            {
                                b.MenuId,
                                b.Name,
                                b.NavigationURL,
                                b.IconURL,
                                b.OrderNo,
                                b.ChildCount,
                                b.CanSwitchDomain,
                                DomainSwitchURL = (b.CanSwitchDomain ? (b.Name.Contains("Switch to " + (domain == "Communication" ? "Theory" : "Communication") + " Domain") ? DomainSwitchURL : "#") : b.NavigationURL),
                                CSSClass = (b.ChildCount > 0 ? "dropdown-toggle " : (b.CanSwitchDomain ? (b.Name.Contains("Switch to " + (domain == "Communication" ? "Theory" : "Communication") + " Domain") ? "" : "disabled ") : "")) + "project-link",
                                ContentStyle = b.CanSwitchDomain ? "width:200px;" : "",
                                b.Tooltip,
                                b.ClickEventName,
                                MenuId_Enc = (string.IsNullOrEmpty(b.MenuIdEnc) ? (MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(b.MenuId.ToString(), Trabau_Keys.Project_Key))) : b.MenuIdEnc),
                                MenuId_EncUpdateRequired = string.IsNullOrEmpty(b.MenuIdEnc)
                            }).ToList().OrderBy(x => x.OrderNo);


                            var menuRequiredToUpdate = _child_submenu.Where(x => x.MenuId_EncUpdateRequired == true);
                            if (menuRequiredToUpdate.Count() > 0)
                            {
                                using (Project prj = new Project())
                                {
                                    string menu_enc = new XElement("menudata",
                                                (from d in menuRequiredToUpdate
                                                 select new XElement("data",
                                                     new XElement("menuid", d.MenuId),
                                                     new XElement("menuid_enc", d.MenuId_Enc)
                                                                    )
                                                 )
                                             ).ToString();
                                    prj.UpdateMenuEnc(Int64.Parse(UserID), menu_enc);
                                }
                            }

                            HtmlGenericControl div_content = (childitem.FindControl("div_content") as HtmlGenericControl);

                            if (_child_submenu.Count() > 0)
                            {
                                rRibbonTabs_ChildMenu.DataSource = _child_submenu;
                                rRibbonTabs_ChildMenu.DataBind();

                                int override_width = (_child_submenu.Where(x => x.CanSwitchDomain == true).Count() == 2 ? 85 : 0);

                                int width = _child_submenu.Count();
                                width = width * (120 + override_width);

                                div_content.Attributes.Add("style", "width:" + (width.ToString() + "px"));

                                foreach (RepeaterItem childitem_inner in rRibbonTabs_ChildMenu.Items)
                                {
                                    string Inner_SubMenuId = (childitem_inner.FindControl("lblInnerSubMenuId") as Label).Text;

                                    var inner_child_submenu = _menu.Where(a => a.ParentId.ToString() == Inner_SubMenuId).Select(b => new
                                    {
                                        b.MenuId,
                                        b.Name,
                                        b.NavigationURL,
                                        b.IconURL,
                                        b.OrderNo,
                                        b.ChildCount,
                                        b.CanSwitchDomain,
                                        Seperator = (b.Seperator ? "border-bottom-dashed" : "") + (b.ChildCount > 0 ? " child-arrow dropdown-item dropdown-toggle project-link" : " dropdown-item pl5"),
                                        CSSClass = (b.ChildCount > 0 ? "dropdown-toggle " : "") + "project-link",
                                        b.ClickEventName,
                                        MenuId_Enc = (string.IsNullOrEmpty(b.MenuIdEnc) ? (MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(b.MenuId.ToString(), Trabau_Keys.Project_Key))) : b.MenuIdEnc),
                                    }).ToList().OrderBy(x => x.OrderNo);

                                    if (inner_child_submenu.Count() > 0)
                                    {
                                        //(childitem_inner.FindControl("alinkbutton") as HtmlAnchor).Attributes.Add("class", "project-link dropdown-toggle");

                                        Repeater rRibbonTabs_InnerChildMenu = (childitem_inner.FindControl("rRibbonTabs_InnerChildMenu") as Repeater);
                                        rRibbonTabs_InnerChildMenu.DataSource = inner_child_submenu;
                                        rRibbonTabs_InnerChildMenu.DataBind();

                                        (childitem_inner.FindControl("ul_dropdown") as HtmlGenericControl).Visible = true;

                                        foreach (RepeaterItem subchilditem_inner in rRibbonTabs_InnerChildMenu.Items)
                                        {
                                            try
                                            {
                                                string SubInner_SubMenuId = (subchilditem_inner.FindControl("lblSubInnerSubMenuId") as Label).Text;
                                                var sub_inner_child_submenu = _menu.Where(a => a.ParentId.ToString() == SubInner_SubMenuId).Select(b => new
                                                {
                                                    b.Name,
                                                    b.IconURL,
                                                    b.OrderNo,
                                                    Seperator = (b.Seperator ? "dropdown-item border-bottom-dashed" : "dropdown-item")
                                                }).ToList().OrderBy(x => x.OrderNo);

                                                if (sub_inner_child_submenu.Count() > 0)
                                                {
                                                    (subchilditem_inner.FindControl("sub_ul_dropdown") as HtmlGenericControl).Visible = true;

                                                    Repeater rRibbonTabs_SubInnerChildMenu = (subchilditem_inner.FindControl("rRibbonTabs_SubInnerChildMenu") as Repeater);
                                                    rRibbonTabs_SubInnerChildMenu.DataSource = sub_inner_child_submenu;
                                                    rRibbonTabs_SubInnerChildMenu.DataBind();
                                                    //HtmlAnchor a_submenu = (childitem_inner.FindControl("a_submenu") as HtmlAnchor);
                                                    //a_submenu.Attributes.Add("class", "dropdown-toggle project-link");
                                                    //a_submenu.Attributes.Add("data-toggle", "dropdown");
                                                    //a_submenu.Attributes.Add("aria-haspopup", "true");
                                                    //a_submenu.Attributes.Add("aria-expanded", "false");
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                            }



                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                            else
                            {
                                //div_content.Attributes.Add("class", "tab-content tab-single-content");
                                //HtmlAnchor btnSubMenuNav = (childitem.FindControl("btnSubMenuNav") as HtmlAnchor);
                                //HtmlGenericControl spanSubMenuNav_title = (childitem.FindControl("spanSubMenuNav_title") as HtmlGenericControl);
                                //spanSubMenuNav_title.Visible = false;
                                //(childitem.FindControl("div_nav_ele") as HtmlGenericControl).Visible = true;

                                //if (btnSubMenuNav.InnerText.Contains("Switch to " + domain + " Domain"))
                                //{
                                //    btnSubMenuNav.Attributes.Add("class", "project-link disabled");
                                //}
                                //else if (btnSubMenuNav.InnerText.Contains("Switch to " + (domain == "Communication" ? "Theory" : "Communication") + " Domain"))
                                //{
                                //    btnSubMenuNav.Attributes.Add("class", "project-link");

                                //    string redirect_domain = "Communication";
                                //    if (domain == redirect_domain)
                                //    {
                                //        redirect_domain = "Theory";
                                //    }
                                //    redirect_domain = EncyptSalt.EncryptText(redirect_domain, Trabau_Keys.Domain_Key);
                                //    redirect_domain = MiscFunctions.Base64EncodingMethod(redirect_domain);

                                //    btnSubMenuNav.Attributes.Add("onclick", "window.location.href='" + "http://" + Request.Url.Authority + "/projects/view-project.aspx?domain=" + redirect_domain + "&id=" + ProjectID_Enc + "'");
                                //}
                                //else
                                //{
                                //    btnSubMenuNav.Attributes.Add("class", "project-link");
                                //}
                            }


                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }
                catch (Exception)
                {
                }
            }
        }
        catch (Exception)
        {
        }
    }


    private StringBuilder CreateChild(StringBuilder sb, string parentId, string parentTitle, List<dynamic> parentRows)
    {
        if (parentRows.Count() > 0)
        {
            sb.Append("<ul childLevel='" + GetChildNumber(Int32.Parse(parentId)) + "' id='" + parentId + "' class='submenu dropdown-menu'>");
            foreach (var item in parentRows)
            {
                var _menu = Session["Project_Menu"] as List<dynamic>;

                string childId = item.MenuId.ToString();
                string childTitle = item.Name;

                var childRow = _menu.Where(a => a.ParentId.ToString() == childId).OrderBy(x => x.OrderNo).ToList();
                string Seperator = (item.Seperator ? "border-bottom-normal" : "");
                if (childRow.Count() > 0)
                {
                    sb.Append("<li childLevel='" + GetChildNumber(item.MenuId) + "' class='dropdown-item" + (string.IsNullOrWhiteSpace(Seperator) ? "" : " " + Seperator) + "'><a id='a" + item.MenuId.ToString()
                        + "' class='nav-link dropdown-toggle' href='" + item.NavigationURL + "'>" + item.Name + "</a>");
                }
                else
                {
                    string MenuId_Enc = (MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(item.MenuId.ToString(), Trabau_Keys.Project_Key)));
                    sb.Append("<li childLevel='" + GetChildNumber(item.MenuId) + "' class='dropdown-item" + (string.IsNullOrWhiteSpace(Seperator) ? "" : " " + Seperator) +
                        "'><a id='a" + item.MenuId.ToString() + "' nav_id='" + MenuId_Enc + "'" + (string.IsNullOrWhiteSpace(item.ClickEventName) ? "" : " onclick='" + item.ClickEventName + "'") + " class='nav-link dropdown-item"
                        + (string.IsNullOrWhiteSpace(item.ClickEventName) && item.NavigationURL.Contains("javascript:void") ? " navDisabled" : "") + "' href='" + item.NavigationURL + "'>" + item.Name + "</a>");
                }
                CreateChild(sb, childId, childTitle, childRow);
                sb.Append("</li>");
            }
            sb.Append("</ul>");

        }
        return sb;
    }

    public static string GetChildNumber(int NavID)
    {
        int childNumber = 0;
        try
        {
            List<dynamic> menus = HttpContext.Current.Session["Project_Menu"] as List<dynamic>;
            var parentMenuLevel1 = menus.Where(x => x.MenuId == NavID).FirstOrDefault();
            if (parentMenuLevel1 != null)
            {
                if (parentMenuLevel1.ParentId > 0)
                {
                    childNumber = 1;
                }
            }

            dynamic parentMenuLevel2 = null;
            if (childNumber == 1)
            {
                parentMenuLevel2 = menus.Where(x => x.MenuId == parentMenuLevel1.ParentId).FirstOrDefault();
                if (parentMenuLevel2 != null)
                {
                    if (parentMenuLevel2.ParentId > 0)
                    {
                        childNumber = 2;
                    }
                }
            }

            if (childNumber == 2)
            {
                var parentMenuLevel3 = menus.Where(x => x.MenuId == parentMenuLevel2.ParentId).FirstOrDefault();
                if (parentMenuLevel3 != null)
                {
                    if (parentMenuLevel3.ParentId > 0)
                    {
                        childNumber = 3;
                    }
                }
            }
        }
        catch (Exception)
        {
        }
        return childNumber.ToString();
    }

    protected void rSubMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    var _menu = Session["Project_Menu"] as List<dynamic>;
                    if (_menu != null)
                    {
                        dynamic drv = e.Item.DataItem as dynamic;
                        string ID = drv.MenuId.ToString();
                        string Title = drv.Name;

                        var _submenu = _menu.Where(a => a.ParentId.ToString() == ID).OrderBy(x => x.OrderNo);
                        if (_submenu.Count() > 0)
                        {

                            StringBuilder sb = new StringBuilder();
                            sb.Append("<ul childLevel='" + GetChildNumber(drv.MenuId) + "' id='" + ID + "' class='dropdown-menu'>");
                            foreach (var item in _submenu)
                            {
                                string parentId = item.MenuId.ToString();
                                string parentTitle = item.Name;

                                var _parentrow = _menu.Where(a => a.ParentId.ToString() == parentId).OrderBy(x => x.OrderNo).ToList();
                                string Seperator = (item.Seperator ? "border-bottom-normal" : "");
                                if (_parentrow.Count() > 0)
                                {
                                    sb.Append("<li childLevel='" + GetChildNumber(item.MenuId) + "' class='dropdown-item" + (string.IsNullOrWhiteSpace(Seperator) ? "" : " " + Seperator) + "'><a id='a" + item.MenuId.ToString()
                                        + "' class='nav-link dropdown-toggle' href='" + item.NavigationURL + "'>" + item.Name + "</a>");
                                }
                                else
                                {
                                    string MenuId_Enc = (MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(item.MenuId.ToString(), Trabau_Keys.Project_Key)));
                                    sb.Append("<li childLevel='" + GetChildNumber(item.MenuId) + "' class='dropdown-item" + (string.IsNullOrWhiteSpace(Seperator) ? "" : " " + Seperator) + "'><a id='a" + item.MenuId.ToString() + "' nav_id='" + MenuId_Enc + "'" + (string.IsNullOrWhiteSpace(item.ClickEventName) ? "" : " onclick='" + item.ClickEventName + "'") + " class='nav-link"
                                    + (string.IsNullOrWhiteSpace(item.ClickEventName) && item.NavigationURL.Contains("javascript:void") ? " navDisabled" : "") + "' href='"
                                    + item.NavigationURL + "'>" + item.Name + "</a>");
                                }
                                sb = CreateChild(sb, parentId, parentTitle, _parentrow);

                                sb.Append("</li>");
                            }
                            sb.Append("</ul>");
                            (e.Item.FindControl("ltrlSubMenu") as Literal).Text = sb.ToString();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }


    [WebMethod(EnableSession = true)]
    public static string GetProjectActionDetails(string action)
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                    Project obj = new Project();
                    var data = obj.GetActionDetails(Int64.Parse(UserID), _ProjectID);

                    string nav_parent = "<ul class='nav nav-tabs' id='myTab1' role='tablist'>";
                    string li = "<li class='nav-item' id='@id' target='@target' onclick='ProjectTabClick(this)'><a class='nav-link'>@TabName</a></li>";
                    string tab_content = "<div class='tab-content' id='@id' style='@style'><div class='tab-pane fade show active'>@TabContent</div></div>";

                    var tabs = data.Select(x => new { x.TabOrder, x.TabName }).Distinct().ToList();

                    for (var i = 0; i < tabs.Count; i++)
                    {
                        var tabname = tabs[i].TabName;
                        string tabid = "project_tab" + i.ToString();

                        var li_temp = li.Replace("@TabName", tabname);
                        li_temp = li_temp.Replace("@id", tabid);
                        li_temp = li_temp.Replace("@target", "project_content_" + i.ToString());

                        nav_parent = nav_parent + li_temp;
                    }

                    nav_parent = nav_parent + "</ul>";




                    string html = "<div class='tab-content'>";
                    string dropdown = "<div class='col-sm-12'><h6 class='pb-1'>@ActionName</h6><select id='@id' type='text' class='form-control' @disabled>@drophtml</select></div>";
                    string textbox = "<div class='col-sm-12'><h6 class='pb-1'>@ActionName</h6><input id='@id' type='text' class='form-control' value='@ActionValue' @disabled /></div>";
                    string editor = "<div class='col-sm-12'><h6 class='pb-1'>@ActionName</h6><div class='dx-viewport'><div class='html-editor mb-3' id='div_project_desc'></div></div><input id='hfProjectDescription' type='hidden' value='@ActionValue' /><input id='txtDescription' style='display:none;' type='text' class='form-control project-desc' @disabled /></div>";
                    string save_html = "<div class='col-sm-12'><input type='button' value='Update Project' class='cta-btn-md' /> <input type='button' value='Close' class='cta-btn-md' onclick='CloseInfo_Popup();' /></div>";

                    for (var i = 0; i < tabs.Count; i++)
                    {
                        var _data = data.Where(x => x.TabOrder == tabs[i].TabOrder).ToList();
                        string _tabcontent = GetTabContent(_data, UserID, obj, action, dropdown, textbox, editor);
                        string tabcontent_id = "project_content_" + i.ToString();

                        var tab_content_temp = tab_content.Replace("@TabContent", _tabcontent);
                        tab_content_temp = tab_content_temp.Replace("@id", tabcontent_id);
                        tab_content_temp = tab_content_temp.Replace("@style", i == 0 ? "" : "display: none;");

                        nav_parent = nav_parent + tab_content_temp;
                    }
                    html = html + nav_parent;

                    if (action == "1")
                    {
                        html = html + save_html;
                    }

                    html = html + "</div>";


                    detail.Add("response", "ok");
                    detail.Add("project_html", html);
                    detail.Add("function_name", "LoadEditor");
                }
                else
                {
                    detail.Add("response", "");
                    detail.Add("project_html", "");
                }
            }
            else
            {
                detail.Add("response", "");
                detail.Add("project_html", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }

    public static string GetTabContent(List<dynamic> data, string UserID, Project obj, string action, string dropdown, string textbox, string editor)
    {
        string tab_content = "";
        for (int i = 0; i < data.Count; i++)
        {
            int _TabOrder = data[i].TabOrder;
            string _ActionName = data[i].ActionName;
            string _ActionValue = data[i].ActionValue;
            string _ControlType = data[i].ControlType;
            string _ControlDataSource = data[i].ControlDataSource;


            string _row_template = "";
            switch (_ControlType)
            {
                case "textbox":
                    _row_template = textbox;
                    break;
                case "dropdown":
                    _row_template = dropdown;
                    break;
                case "editor":
                    _row_template = editor;
                    break;
                default:
                    _row_template = textbox;
                    break;
            }

            string id = "txt" + _ActionName.Replace(" ", "_").ToLower();
            if (_ActionValue != "")
            {
                _row_template = _row_template.Replace("@ActionName", _ActionName);
                _row_template = _row_template.Replace("@ActionValue", _ActionValue);
                _row_template = _row_template.Replace("@id", id);
                _row_template = _row_template.Replace("@disabled", (action == "0" || _ActionName == "Manager Name" ? "disabled" : ""));

                if (_ControlType == "dropdown" && _ControlDataSource != "")
                {
                    try
                    {
                        var drop_data = obj.GetControlDataSource(Int64.Parse(UserID), _ProjectID, _ControlDataSource);
                        if (drop_data != null)
                        {
                            string drop_html = "";
                            for (int j = 0; j < drop_data.Count; j++)
                            {
                                string Text = drop_data[j].Text;
                                string Value = drop_data[j].Value;

                                if (MiscFunctions.IsBase64String(Text))
                                {
                                    Text = MiscFunctions.Base64DecodingMethod(Text);
                                }

                                if (MiscFunctions.IsBase64String(Value))
                                {
                                    Value = MiscFunctions.Base64DecodingMethod(Value);
                                }

                                if (Value != "0")
                                {
                                    drop_html = drop_html + "<option value='" + Value + "' " + (Value == _ActionValue ? "selected='true'" : "") + ">" + Text + "</option>";
                                }
                            }
                            _row_template = _row_template.Replace("@drophtml", drop_html);
                        }
                    }
                    catch (Exception)
                    {
                    }

                }

                tab_content = tab_content + _row_template;
            }
        }

        return tab_content;
    }


    [WebMethod(EnableSession = true)]
    public static string UpdateProject(string project_name, string end_date, string company_name, string application_name, string description, string end_time, string budget_type, string total_hours)
    {
        string response = "";
        string _response = "error";
        string _message = "Error while updating project details, please recheck and try again";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H")
                {
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                    Project obj = new Project();
                    var data = obj.SaveNewProject(_ProjectID, Int64.Parse(UserID), project_name, DateTime.Now, Convert.ToDateTime(end_date), "", "", company_name, application_name, "", description, DateTime.Now,
                        Convert.ToDateTime(end_time), budget_type, Int32.Parse(total_hours), "", "", "", "", "", "", "");


                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            _response = data[0].Response;
                            _message = data[0].Message;
                        }
                    }

                    detail.Add("response", "ok");
                }
                else
                {
                    detail.Add("response", "");
                }
            }
            else
            {
                detail.Add("response", "");
            }

            detail.Add("res_response", _response);
            detail.Add("res_message", _message);

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }




    [WebMethod(EnableSession = true)]
    public static string GetNavigationTabs(string NavId, string DataId, string currentNavID)
    {
        string response = "";
        try
        {
            if (string.IsNullOrEmpty(ValidateNavigation(NavId)))
            {
                HttpContext.Current.Session["TabProject_Files"] = null;
                HttpContext.Current.Session["TabProject_DataIndex"] = null;
                HttpContext.Current.Session["TabProject_CurrentNavID"] = null;

                if (!string.IsNullOrWhiteSpace(DataId) && !DataId.Equals("undefined"))
                {
                    HttpContext.Current.Session["TabProject_DataIndex"] = DataId;
                }

                if (!string.IsNullOrWhiteSpace(currentNavID) && !currentNavID.Equals("undefined"))
                {
                    HttpContext.Current.Session["TabProject_CurrentNavID"] = currentNavID;
                }

                List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
                Dictionary<string, object> detail = new Dictionary<string, object>();
                if (HttpContext.Current.Session["Trabau_UserId"] != null)
                {
                    string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                    if (UserType == "H" || UserType == "W")
                    {
                        string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                        string _navid = NavId;
                        _navid = MiscFunctions.Base64DecodingMethod(_navid);
                        _navid = EncyptSalt.DecryptText(_navid, Trabau_Keys.Project_Key);

                        ProjectGenericForms obj = new ProjectGenericForms();
                        var _data = obj.GetNavigationTabs(Int64.Parse(UserID), Int32.Parse(_navid));

                        if (_data != null)
                        {
                            if (_data.Count > 0)
                            {
                                var tabs = _data.Select(x => new
                                {
                                    TabId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(x.TabId.ToString(), Trabau_Keys.Project_Key)),
                                    x.TabName,
                                    x.TabShortName,
                                    x.NavigationName,
                                    x.OrderNo,
                                    x.Visibility,
                                    OrgNavigationId = MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(x.OrgNavigationId.ToString(), Trabau_Keys.Project_Key)),
                                    x.TabClass,
                                    x.UniqueProperty
                                }).OrderBy(o => o.OrderNo).ToList();

                                detail.Add("response", "ok");
                                detail.Add("response_data", JsonConvert.SerializeObject(tabs));
                            }
                            else
                            {
                                detail.Add("response", "");
                            }
                        }
                        else
                        {
                            detail.Add("response", "");
                        }
                    }
                    else
                    {
                        detail.Add("response", "");
                    }
                }
                else
                {
                    detail.Add("response", "");
                }

                details.Add(detail);

                JavaScriptSerializer serial = new JavaScriptSerializer();
                response = serial.Serialize(details);
            }
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }


    static string help = "<span class='get-help tow-tooltip' data-toggle='popover' tabindex='0' data-trigger='focus' data-original-title='' title='' data-content='@HelpText'>!</span>";
    static string dropdown = "<div class='col-sm-12 tab-field' type='@type' id='@id'><h6 class='pb-1'>@ActionName @HelpText</h6><select type='text' class='form-control' error-message='@ErrorMessage' non-inline-error-message='@NI_ErrorMessage' @disabled @Required>@drophtml</select></div>";
    static string textbox = "<div class='col-sm-12 tab-field' type='@type' id='@id'><h6 class='pb-1'>@ActionName @HelpText</h6><input type='text' placeholder='@placeholder' class='form-control' date-picker-required='@DatePickerRequired' value='@ActionValue' error-message='@ErrorMessage' non-inline-error-message='@NI_ErrorMessage' @disabled @Required/></div>";
    static string fileupload = "<div class='col-sm-12 tab-field' type='@type' id='@id'><h6 class='pb-1'>@ActionName @HelpText</h6><div class='file-upload-main'><div class='file-upload-control'><input type='file' class='form-control' value='@ActionValue' onchange='TabFileChanged(this)' @disabled @Required/></div><p class='progress-bar-percentage'>0%</p><div class='progress-bar-parent'><div class='progress-bar progress-bar-override' style='width: 0%;'></div></div></div><div class='tab-files-output'>@TabFiles</div></div>";
    static string textarea = "<div class='col-sm-12 tab-field' type='@type' id='@id'><h6 class='pb-1'>@ActionName @HelpText</h6><textarea placeholder='@placeholder' class='form-control' error-message='@ErrorMessage' non-inline-error-message='@NI_ErrorMessage' @disabled @Required>@ActionValue</textarea></div>";
    static string editor = "<div class='col-sm-12 tab-field' type='@type' id='@id'><h6 class='pb-1 field-name'>@ActionName @HelpText</h6><div class='dx-viewport'><div class='html-editor mb-3' id='@div_id'></div></div><input id='@hfid' type='hidden' value='@ActionValue' class='hidden-field-data' error-message='@ErrorMessage' non-inline-error-message='@NI_ErrorMessage' @Required /><input id='@id' style='display:none;' type='text' class='form-control project-desc input-field-data' @disabled /></div>";
    static string grid = "grid";
    static string label = "<div class='col-sm-12 tab-field' type='@type' id='@id'><h6 class='pb-1'>@ActionName</h6><label class='form-control'>@ActionValue</label></div>";
    static string checkbox = "<div class='col-sm-12 tab-field' type='@type' id='@id'><h6 class='pb-1'>@ActionName @HelpText</h6><input type='text' placeholder='@placeholder' class='form-control' date-picker-required='@DatePickerRequired' value='@ActionValue' error-message='@ErrorMessage' non-inline-error-message='@NI_ErrorMessage' @disabled @Required/></div>";


    static string tbl_textbox = "<input type='text' placeholder='@placeholder' class='form-control' value='@ActionValue' date-picker-required='@DatePickerRequired' @disabled @Required/>";
    static string tbl_label = "<label>@ActionValue</label>";
    static string tbl_dropdown = "<select type='text' class='form-control' @disabled @Required>@drophtml</select>";

    [WebMethod(EnableSession = true)]
    public static string GetTabContent(string TabId, string navID, string uniquePropertyKey)
    {
        string response = "";
        try
        {
            HttpContext.Current.Session["Project_Default_Data"] = null;

            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                    string _tabid = TabId;
                    _tabid = MiscFunctions.Base64DecodingMethod(_tabid);
                    _tabid = EncyptSalt.DecryptText(_tabid, Trabau_Keys.Project_Key);

                    string _navID = navID;
                    _navID = MiscFunctions.Base64DecodingMethod(_navID);
                    _navID = EncyptSalt.DecryptText(_navID, Trabau_Keys.Project_Key);

                    string DataIndex = "0";
                    try
                    {
                        if (HttpContext.Current.Session["TabProject_DataIndex"] != null)
                        {
                            DataIndex = HttpContext.Current.Session["TabProject_DataIndex"].ToString();
                            DataIndex = MiscFunctions.Base64DecodingMethod(DataIndex);
                            DataIndex = EncyptSalt.DecryptText(DataIndex, Trabau_Keys.Project_Key);
                        }
                    }
                    catch
                    {
                        DataIndex = "0";
                    }
                    ProjectGenericForms obj = new ProjectGenericForms();
                    NavigationTabFields_Model data = obj.GetNavigationTabFields(Int64.Parse(UserID), Int32.Parse(_tabid), _ProjectID, Int32.Parse(_navID), Int32.Parse(DataIndex), uniquePropertyKey);


                    if (data != null)
                    {
                        var fields = data.fields;
                        var fieldItems = data.fieldItems;

                        fields = fields.OrderBy(x => x.OrderNo).ToList();
                        bool saveRequired = false;
                        bool IsReadOnly = false;
                        bool numberOnlyRequired = false;
                        string _EventName = string.Empty;

                        _EventName = fields[0].AdditionalEventName;
                        try
                        {
                            saveRequired = fields[0].CanSaveData;
                        }
                        catch (Exception)
                        {
                        }

                        string CurrentNavID = "";

                        if (HttpContext.Current.Session["TabProject_CurrentNavID"] != null)
                        {
                            CurrentNavID = HttpContext.Current.Session["TabProject_CurrentNavID"].ToString();
                        }

                        string eventName = "SaveTabData";
                        if (!string.IsNullOrWhiteSpace(_EventName))
                        {
                            eventName = _EventName;
                        }

                        string html = "<div class='row p-3'>";
                        string saveButton = "<a id='btnSaveTabData' class='cta-btn-md' onclick='" + eventName + "(@PreserveDataRequired);'>Next</a>&nbsp;";
                        string save_html = "<div class='col-sm-12 tab-footer text-right'>" + (saveRequired ? saveButton : "") + "<a class='cta-btn-md btn-red' id='aCloseProjectTabs' nav-id='" + CurrentNavID + "' onclick=CloseProjectTabs()>Cancel</a></div>";

                        bool Editor_Req = false;
                        bool DatePickerRequired = false;
                        bool _InlineErrorsSetup = false;
                        bool _ChangeEventRequired = false;
                        bool preserveDataRequired = false;
                        for (int i = 0; i < fields.Count; i++)
                        {
                            if (!preserveDataRequired && fields[i].PreserveDataRequired)
                            {
                                preserveDataRequired = true;
                            }
                            string _ControlType = fields[i].FieldType;
                            string _FieldName = fields[i].FieldName;
                            int _TabDetailsId = fields[i].TabDetailsId;
                            bool _AccordionRequired = fields[i].AccordionRequired;
                            bool _IsReadOnly = fields[i].isReadOnly;

                            if (_ControlType == "number" && !numberOnlyRequired)
                            {
                                numberOnlyRequired = true;
                            }

                            if (!_ChangeEventRequired)
                            {
                                _ChangeEventRequired = fields[i].ChangeEventRequired;
                            }

                            if (!_InlineErrorsSetup)
                            {
                                _InlineErrorsSetup = fields[i].InlineErrorsSetup;
                            }

                            if (!DatePickerRequired)
                            {
                                DatePickerRequired = fields[i].DatePickerRequired;
                            }

                            if (!IsReadOnly)
                            {
                                IsReadOnly = _IsReadOnly;
                            }

                            bool CanAdd = fields[i].CanAdd;
                            bool CanDelete = fields[i].CanDelete;

                            string _row_template = "";
                            if (_ControlType != "grid")
                            {
                                string visibilitySource = fields[i].VisibilitySource;
                                bool elementVisibility = true;
                                if (!string.IsNullOrEmpty(visibilitySource))
                                {
                                    Project pr = new Project();
                                    var visibility = pr.GetControlDataSource(Int64.Parse(UserID), _ProjectID, visibilitySource);
                                    elementVisibility = visibility != null ? (visibility[0].Visibility == "1" ? true : false) : elementVisibility;
                                }

                                _row_template = elementVisibility ? CreateFieldElement(fields[i], ref Editor_Req) : string.Empty;
                            }
                            else
                            {
                                _row_template = GetGridHTML(navID, UserID, _navID, obj, fields, fieldItems, ref DatePickerRequired, i, _FieldName, _TabDetailsId, _AccordionRequired, _IsReadOnly, CanAdd, CanDelete);
                            }

                            html = html + _row_template;
                        }

                        save_html = save_html.Replace("@PreserveDataRequired", preserveDataRequired.ToString().ToLower());
                        html = html + save_html;

                        string additional_functions = "ActivateHelpText";
                        if (Editor_Req)
                        {
                            additional_functions += ",LoadField_Editor";
                        }

                        if (DatePickerRequired)
                        {
                            additional_functions += ",ActivateDatePicker";
                        }

                        if (_InlineErrorsSetup)
                        {
                            additional_functions += ",AddInlineErrorsCheckbox";
                        }

                        if (_ChangeEventRequired)
                        {
                            additional_functions += ",RegisterFieldChangeEvent";
                        }

                        if (IsReadOnly)
                        {
                            additional_functions += ",DisableEditor";
                        }

                        if (numberOnlyRequired)
                        {
                            additional_functions += ",ActivateNumberOnly";
                        }

                        detail.Add("response", "ok");
                        detail.Add("response_data", html);
                        detail.Add("function_name", additional_functions);
                    }
                }
                else
                {
                    detail.Add("response", "");
                }
            }
            else
            {
                detail.Add("response", "");
            }


            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }

    private static string GetGridHTML(string navID, string UserID, string _navID, ProjectGenericForms obj, List<dynamic> fields, List<dynamic> fieldItems,
        ref bool DatePickerRequired, int i, string _FieldName, int _TabDetailsId, bool _AccordionRequired, bool _IsReadOnly, bool CanAdd, bool CanDelete)
    {
        string _row_template;
        bool additionalGridButton = fields[i].AdditionalGridButton;
        bool expandArrowRequired = fields[i].ExpandArrowRequired;

        string additionalGridButtonText = fields[i].AdditionalGridButtonText;
        string additionalGridButtonAction = fields[i].AdditionalGridButtonAction;
        string additionalGridButtonAction_NavID = fields[i].AdditionalGridButtonAction_NavID;

        int additionalGridTabDetailsId = fields[i].AdditionalGridTabDetailsId;
        string additionalGridPrimaryColumnId = fields[i].AdditionalGridPrimaryColumnId;
        string additionalGridTitle = fields[i].AdditionalGridTitle;

        bool AddOneRecordCurrentDay = fields[i].AddOneRecordCurrentDay;
        bool RemoveOnlyCurrentDayRecord = fields[i].RemoveOnlyCurrentDayRecord;
        string RemoveOnlyCurrentDayErrorMessage = fields[i].RemoveOnlyCurrentDayErrorMessage;
        string AddOneRecordCurrentDayErrorMessage = fields[i].AddOneRecordCurrentDayErrorMessage;
        string TableSplClass = fields[i].TableSplClass;
        string RemoveOnlyMessageforInitialRow = fields[i].RemoveOnlyMessageforInitialRow;

        bool isContextMenu = MiscFunctions.asBoolean(MiscFunctions.GetValue(fieldItems, NavigationForm_Constants.ContextMenu));
        ContextMenu contextMenu = MiscFunctions.FromXml<ContextMenu>(MiscFunctions.GetValue(fieldItems, NavigationForm_Constants.ContextMenuItems));

        var grid_struct = obj.GetNavigation_TabGridColumns(Int64.Parse(UserID), _TabDetailsId, _ProjectID, Int32.Parse(_navID));
        _row_template = BindDynamicGrid(grid_struct, CanAdd, CanDelete, _FieldName, _IsReadOnly, additionalGridButton, additionalGridButtonText, additionalGridButtonAction,
            additionalGridButtonAction_NavID, navID, additionalGridTabDetailsId, additionalGridPrimaryColumnId, additionalGridTitle, AddOneRecordCurrentDay, RemoveOnlyCurrentDayRecord,
            RemoveOnlyCurrentDayErrorMessage, AddOneRecordCurrentDayErrorMessage, RemoveOnlyMessageforInitialRow, TableSplClass, expandArrowRequired, isContextMenu, contextMenu,
            currentNavigationID: _navID, AccordionRequired: _AccordionRequired);

        if (!DatePickerRequired)
        {
            try
            {
                DatePickerRequired = grid_struct.Where(x => x.DatePickerRequired).FirstOrDefault().DatePickerRequired;
            }
            catch (Exception ex)
            {
            }
        }

        return _row_template;
    }

    [WebMethod(EnableSession = true)]
    public static string GetRowDetails(string dataSource, string dataParameter, string currentNavigationid)
    {
        string response = "";
        try
        {
            string _row_template = "";
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
                    dataSource = MiscFunctions.Base64DecodingMethod(dataSource);
                    string html = "<div class='row p-3'>";
                    ProjectGenericForms obj = new ProjectGenericForms();
                    bool CanAdd = false;
                    bool CanDelete = false;
                    string FieldName = string.Empty;
                    bool _IsReadOnly = false;

                    bool additionalGridButton = false;
                    string additionalGridButtonText = string.Empty;
                    string additionalGridButtonAction = string.Empty;
                    string additionalGridButtonAction_NavID = string.Empty;
                    string navigationID = "0";
                    try
                    {
                        navigationID = MiscFunctions.DecodeAndDecrypt(currentNavigationid, Trabau_Keys.Project_Key);
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        var additionalFields = obj.GetDynamicGridAdditionalFields(Int32.Parse(dataSource));
                        if (additionalFields != null)
                        {
                            additionalGridButton = additionalFields[0].AdditionalGridButton;
                            additionalGridButtonText = additionalFields[0].AdditionalGridButtonText;
                            additionalGridButtonAction = additionalFields[0].AdditionalGridButtonAction;
                            additionalGridButtonAction_NavID = additionalFields[0].AdditionalGridButtonAction_NavID;
                        }
                    }
                    catch (Exception)
                    {
                    }


                    var grid_struct = obj.GetNavigation_TabGridColumns(Int64.Parse(UserID), Int32.Parse(dataSource), _ProjectID, Int32.Parse(navigationID), dataParameter);
                    _row_template = BindDynamicGrid(grid_struct, CanAdd, CanDelete, FieldName, _IsReadOnly, additionalGridButton, additionalGridButtonText, additionalGridButtonAction, additionalGridButtonAction_NavID,
                        currentNavID: currentNavigationid, additionalGridTabDetailsId: 0, additionalGridPrimaryColumnId: "", additionalGridTitle: "", AddOneRecordCurrentDay: false, RemoveOnlyCurrentDayRecord: false,
                        RemoveOnlyCurrentDayErrorMessage: "", AddOneRecordCurrentDayErrorMessage: "", RemoveOnlyMessageforInitialRow: "", TableSplClass: "", expandArrowRequired: false, isContextMenu: false,
                        contextMenu: default(ContextMenu), currentNavigationID: "", AccordionRequired: false);
                    html = html + _row_template;

                    detail.Add("response", "ok");
                    detail.Add("response_data", html);
                    detail.Add("function_name", "");
                }
                else
                {
                    detail.Add("response", "");
                }
            }
            else
            {
                detail.Add("response", "");
            }


            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch { }
        return response;
    }


    [WebMethod(EnableSession = true)]
    public static string RemovePreservedTabContentData()
    {
        HttpContext.Current.Session["ProjectTabContent"] = null;

        return "success";
    }

    public static List<dynamic> GetAllNavFieldsDefinitions()
    {
        List<dynamic> lst = new List<dynamic>();
        if (HttpContext.Current.Session["Project_AllNavFieldsDefinitions"] == null)
        {
            ProjectGenericForms obj = new ProjectGenericForms();
            string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
            lst = obj.GetAllNavFieldsDefinitions(Int64.Parse(UserID));
            HttpContext.Current.Session["Project_AllNavFieldsDefinitions"] = lst;
        }
        else
        {
            lst = HttpContext.Current.Session["Project_AllNavFieldsDefinitions"] as List<dynamic>;
        }
        return lst;
    }

    public static string GetFieldDefinition(int FieldId, string DefinitionType)
    {
        var lst = GetAllNavFieldsDefinitions();
        string FieldDefinition = "";
        if (GenericFormFieldDefinition.NORMAL_TYPE == DefinitionType)
        {
            FieldDefinition = lst.Where(x => x.FieldId == FieldId).FirstOrDefault().FieldDefinition;
        }
        else if (GenericFormFieldDefinition.TABLE_TYPE == DefinitionType)
        {
            FieldDefinition = lst.Where(x => x.FieldId == FieldId).FirstOrDefault().Table_FieldDefinition;
        }

        return FieldDefinition;
    }

    private static string CreateFieldElement(dynamic data, ref bool Editor_Req, bool TableTemplate = false)
    {

        ProjectGenericForms obj = new ProjectGenericForms();
        string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
        int _TabDetailsId = data.TabDetailsId;
        string _ActionName = data.FieldName;
        string _ActionValue = data.FieldValue;
        if (HttpContext.Current.Session["ProjectTabContent"] != null)
        {
            try
            {
                var itemsData = HttpContext.Current.Session["ProjectTabContent"] as List<propProjectData>;
                if (itemsData != null)
                {
                    var newValue = itemsData.Where(x => x.key == _TabDetailsId.ToString()).SingleOrDefault();
                    if (newValue != null)
                    {
                        _ActionValue = newValue.value;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        string _ControlType = data.FieldType;
        string _ControlDataSource = data.DataSource;
        string HelpText = "";
        try
        {
            HelpText = data.HelpText;
        }
        catch (Exception)
        {
        }
        bool Required = data.Required;
        bool isReadOnly = data.isReadOnly;
        string _Placeholder = data.Placeholder;
        string _DataSourceType = data.DataSourceType;
        bool _DatePickerRequired = data.DatePickerRequired;
        string _RequiredError = data.RequiredError;
        string _NonInlineRequiredError = data.NonInlineRequiredError;
        string DefinitionType = TableTemplate ? GenericFormFieldDefinition.TABLE_TYPE : GenericFormFieldDefinition.NORMAL_TYPE;
        bool _ChangeEventRequired = data.ChangeEventRequired;
        int _ChangeEventTargetFieldId = data.ChangeEventTargetFieldId;
        bool _IsMultiDropdown = data.IsMultiDropdown;
        string _ClientSideDefaultValue = data.ClientSideDefaultValue;
        bool _SkipPreviousValues = data.SkipPreviousValues;
        bool _ClientSideOverrideReadOnly = data.ClientSideOverrideReadOnly;
        string _ClientSideOverrideValue = data.ClientSideOverrideValue;
        bool _ClientSideAvoidDuplicate = data.ClientSideAvoidDuplicate;
        bool _ClientSideRemoveSameDate = data.ClientSideRemoveSameDate;
        bool _CollectData = data.CollectData;
        bool _AvoidAddBlank = data.AvoidAddBlank;
        string _AvoidAddBlankMessage = data.AvoidAddBlankMessage;
        bool _AlwaysDisplayNewData = data.AlwaysDisplayNewData;
        string _ChangeEventType = data.ChangeEventType;
        string _NavigationId = data.NavigationId.ToString();
        string _RowSize = data.RowSize;
        string _FieldPosition = data.FieldPosition;
        string _AdditionalClass = data.AdditionalClass;
        string _ClickEventName = data.ClickEventName;
        string _ClickEventParameter = data.ClickEventParameter;

        if (_AlwaysDisplayNewData)
        {
            _ActionValue = string.Empty;
        }

        if (_ControlType != "checkbox")
        {
            if (MiscFunctions.IsBase64String(_ActionValue))
            {
                _ActionValue = MiscFunctions.Base64DecodingMethod(_ActionValue);
            }
        }
        string _row_template = GetFieldDefinition(data.FieldId, DefinitionType);
        string tab_files = "";
        switch (_ControlType)
        {
            case "editor":
                if (!Editor_Req)
                {
                    Editor_Req = true;
                }
                break;
            case "fileupload":
                DataTable dt_files = GetTabFiles(_TabDetailsId.ToString(), _ProjectID);
                tab_files = GetTabFiles_Table(dt_files);
                break;
            default:
                break;
        }

        if ((_ControlType == "textbox" || _ControlType == "label" || _ControlType == "editor") && _ControlDataSource != "")
        {
            if (string.IsNullOrEmpty(_ActionValue))
            {
                if (_DataSourceType != "S")
                {
                    bool CombinedDataPicker = data.CombinedDataPicker;
                    string CombinedDataPickerFieldName = data.CombinedDataPickerFieldName;

                    Project _obj = new Project();
                    var default_data = new List<dynamic>();
                    if (!CombinedDataPicker)
                    {
                        HttpContext.Current.Session["Project_Default_Data"] = null;
                    }

                    if (HttpContext.Current.Session["Project_Default_Data"] != null && CombinedDataPicker)
                    {
                        default_data = HttpContext.Current.Session["Project_Default_Data"] as List<dynamic>;
                    }
                    else
                    {
                        default_data = _obj.GetControlDataSource(Int64.Parse(UserID), _ProjectID, _ControlDataSource);
                        HttpContext.Current.Session["Project_Default_Data"] = default_data;
                    }

                    if (CombinedDataPicker)
                    {
                        default_data = default_data.Where(x => x.FieldName == CombinedDataPickerFieldName)
                            .Select(x => new { Text = x.FieldValue }).ToList<dynamic>();
                    }

                    if (default_data != null)
                    {
                        if (default_data.Count > 0)
                        {
                            _ActionValue = default_data[0].Text;
                            if (MiscFunctions.IsBase64String(_ActionValue))
                            {
                                _ActionValue = MiscFunctions.Base64DecodingMethod(_ActionValue);
                            }
                        }
                    }
                }
                else
                {
                    _ActionValue = _ControlDataSource;
                }
            }
        }

        string id = _TabDetailsId.ToString();
        string div_id = "div" + _ActionName.Replace(" ", "_").ToLower();
        string hf_id = "hf" + _ActionName.Replace(" ", "_").ToLower();
        _row_template = _row_template.Replace("@ActionName", _ActionName);
        _row_template = _row_template.Replace("@ActionValue", _ActionValue);
        _row_template = _row_template.Replace("@id", id);
        _row_template = _row_template.Replace("@div_id", div_id);
        _row_template = _row_template.Replace("@hfid", hf_id);
        _row_template = _row_template.Replace("@disabled", (isReadOnly ? "disabled" : ""));
        _row_template = _row_template.Replace("@Required", (Required ? "required='required'" : ""));
        _row_template = _row_template.Replace("@placeholder", _Placeholder);
        _row_template = _row_template.Replace("@type", _ControlType);
        _row_template = _row_template.Replace("@DatePickerRequired", _DatePickerRequired.ToString().ToLower());
        _row_template = _row_template.Replace("@TabFiles", tab_files);
        _row_template = _row_template.Replace("@ErrorMessage", _RequiredError);
        _row_template = _row_template.Replace("@NI_ErrorMessage", _NonInlineRequiredError);
        _row_template = _row_template.Replace("@changeRequired", (_ChangeEventRequired ? "changeEventRequired='true' targetEventFieldId='" + _ChangeEventTargetFieldId.ToString() + "'" : ""));
        _row_template = _row_template.Replace("@multiDropdown", (_IsMultiDropdown ? "multiDropdown='true'" : ""));
        _row_template = _row_template.Replace("@additionalField", (_IsMultiDropdown ? "<input type='hidden' id='hf" + _TabDetailsId.ToString() + "' value='" + _ActionValue + "' />" : ""));
        _row_template = _row_template.Replace("@ddlId", "ddl" + _TabDetailsId.ToString());
        _row_template = _row_template.Replace("@Checked", (_ActionValue == "true" ? "checked" : ""));
        _row_template = _row_template.Replace("@ClientSideDefaultValue", _ClientSideDefaultValue);
        _row_template = _row_template.Replace("@SkipPreviousValues", _SkipPreviousValues ? "true" : "false");
        _row_template = _row_template.Replace("@ClientSideOverrideReadOnly", _ClientSideOverrideReadOnly ? "true" : "false");
        _row_template = _row_template.Replace("@ClientSideOverrideValue", _ClientSideOverrideValue);
        _row_template = _row_template.Replace("@ClientSideAvoidDuplicate", _ClientSideAvoidDuplicate ? "true" : "false");
        _row_template = _row_template.Replace("@ClientSideRemoveSameDate", _ClientSideRemoveSameDate ? "true" : "false");
        _row_template = _row_template.Replace("@CollectData", _CollectData ? "true" : "false");
        _row_template = _row_template.Replace("@AvoidAddBlankMessage", _AvoidAddBlankMessage);
        _row_template = _row_template.Replace("@AvoidAddBlank", _AvoidAddBlank ? "true" : "false");
        _row_template = _row_template.Replace("@ChangeEventType", _ChangeEventType);

        if (_NavigationId != "" && _NavigationId != "0")
        {
            _NavigationId = (MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(_NavigationId, Trabau_Keys.Project_Key)));
        }
        _row_template = _row_template.Replace("@currentNavID", _NavigationId);
        _row_template = _row_template.Replace("@rowsize", _RowSize);

        _row_template = _row_template.Replace("@FieldPosition", _FieldPosition);
        _row_template = _row_template.Replace("@AdditionalClass", _AdditionalClass);
        if (!string.IsNullOrWhiteSpace(_ClickEventParameter))
        {
            _ClickEventParameter = MiscFunctions.EncryptAndEncode(_ClickEventParameter, Trabau_Keys.Project_Key);
        }
        _row_template = _row_template.Replace("@ClickEventName", _ClickEventName + "(this)");
        _row_template = _row_template.Replace("@targetNavID", _ClickEventParameter);


        if ((_ControlType == "dropdown" || _ControlType == "radiobutton") && _ControlDataSource != "")
        {
            try
            {
                if (_DataSourceType != "S")
                {
                    Project _obj = new Project();
                    var drop_data = _obj.GetControlDataSource(Int64.Parse(UserID), _ProjectID, _ControlDataSource);
                    if (drop_data != null)
                    {
                        if (_ControlType == "dropdown")
                        {
                            string drop_html = "";
                            if (!_IsMultiDropdown)
                            {
                                drop_html = drop_html + "<option value='' selected='true'>Select</option>";
                            }
                            for (int j = 0; j < drop_data.Count; j++)
                            {
                                string Text = drop_data[j].Text;
                                string Value = drop_data[j].Value;

                                if (MiscFunctions.IsBase64String(Text))
                                {
                                    Text = MiscFunctions.Base64DecodingMethod(Text);
                                }

                                if (MiscFunctions.IsBase64String(Value))
                                {
                                    Value = MiscFunctions.Base64DecodingMethod(Value);
                                }

                                if (Value != "0")
                                {
                                    drop_html = drop_html + "<option value='" + Value + "' " + (Value == _ActionValue ? "selected='true'" : "") + ">" + Text + "</option>";
                                }
                            }
                            _row_template = _row_template.Replace("@drophtml", drop_html);
                        }
                        else if (_ControlType == "radiobutton")
                        {
                            string radio_html = "<ul>";

                            for (int j = 0; j < drop_data.Count; j++)
                            {
                                string Text = drop_data[j].Text;
                                string Value = drop_data[j].Value;
                                string Tooltip = "";
                                try
                                {
                                    Tooltip = drop_data[j].Tooltip;
                                }
                                catch (Exception)
                                {
                                }

                                if (Value != "0")
                                {
                                    string AdditionalData = string.Empty;
                                    try
                                    {
                                        AdditionalData = drop_data[j].AdditionalData;
                                    }
                                    catch { }

                                    radio_html = BuildRadioButton(radio_html, Text, "radiobutton", Value, AdditionalData, Tooltip);
                                }
                            }
                            radio_html = radio_html + "</ul>";

                            _row_template = _row_template.Replace("@RadiobuttonHTML", radio_html);
                        }
                    }
                }
                else
                {
                    if (_ControlType == "dropdown")
                    {
                        string drop_html = "";
                        if (!isReadOnly)
                        {
                            drop_html = drop_html + "<option value='' selected='true'>Select</option>";
                        }
                        for (int j = 0; j < _ControlDataSource.Split(',').Length; j++)
                        {
                            string Text = _ControlDataSource.Split(',')[j];
                            string Value = Text;
                            drop_html = drop_html + "<option value='" + Value + "' " + (Value == _ActionValue ? "selected='true'" : "") + ">" + Text + "</option>";
                        }
                        _row_template = _row_template.Replace("@drophtml", drop_html);
                    }
                    else if (_ControlType == "radiobutton")
                    {
                        string radio_html = "<ul>";
                        for (int j = 0; j < _ControlDataSource.Split(',').Length; j++)
                        {
                            string Text = _ControlDataSource.Split(',')[j];
                            string Value = Text;

                            radio_html = BuildRadioButton(radio_html, Text, "radiobutton", Value, string.Empty, string.Empty);
                        }
                        radio_html = radio_html + "</ul>";

                        _row_template = _row_template.Replace("@RadiobuttonHTML", radio_html);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        string _help = help.Replace("@HelpText", HelpText);
        _row_template = _row_template.Replace("@HelpText", (HelpText != string.Empty ? _help : ""));

        return _row_template;
    }

    public static string BuildRadioButton(string radioHTML, string Text, string radioButtonName, string Value, string AdditionalClass, string Tooltip)
    {
        string help = (!string.IsNullOrWhiteSpace(Tooltip) ?
                      "<span class='get-help tow-tooltip' style='margin-bottom: 7px;' data-toggle='popover' data-placement='top' data-content='"
                      + Tooltip + "' data-original-title='' title=''>!</span>" : string.Empty);
        return radioHTML + "<li" + (!string.IsNullOrWhiteSpace(AdditionalClass) ? " class='" + AdditionalClass + "'" : string.Empty)
                                 + "><input type='radio' id='radio" + Value + "' name='" + radioButtonName + "' value='" + Value + "'><label"
                                 + (!string.IsNullOrWhiteSpace(AdditionalClass) ? " class='" + AdditionalClass + "-label'" : string.Empty)
                                 + " for='" + Text + "'>" + Text + "</label>" + help + "</li>";
    }

    public static DataTable AddTabFiles(DataTable dtfiles_new)
    {
        DataTable dtfiles = new DataTable();
        if (HttpContext.Current.Session["TabProject_Files"] != null)
        {
            dtfiles = HttpContext.Current.Session["TabProject_Files"] as DataTable;
        }
        else
        {
            dtfiles = GetTabFilesStructure();
        }

        foreach (DataRow new_row in dtfiles_new.Rows)
        {
            string file_key_new = new_row["file_key"].ToString();
            bool found = false;

            foreach (DataRow row in dtfiles.Rows)
            {
                string file_key = row["file_key"].ToString();
                if (file_key == file_key_new)
                {
                    found = true;
                }
            }


            if (!found)
            {
                dtfiles.ImportRow(new_row);
            }
        }

        HttpContext.Current.Session["TabProject_Files"] = dtfiles;

        return dtfiles;
    }

    public static DataTable GetTabFiles(string tabdetails_id, int ProjectID)
    {
        DataTable dtfiles = new DataTable();

        ProjectGenericForms obj = new ProjectGenericForms();
        string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

        DataTable dtfiles_db = obj.GetTabFileDetails(Int64.Parse(UserID), Int32.Parse(tabdetails_id), ProjectID);

        dtfiles = AddTabFiles(dtfiles_db);

        DataView dvfiles = dtfiles.DefaultView;
        dvfiles.RowFilter = "tabdetails_id = '" + tabdetails_id + "'";
        dtfiles = dvfiles.ToTable();

        //if (HttpContext.Current.Session["TabProject_Files"] != null)
        //{
        //    dtfiles = HttpContext.Current.Session["TabProject_Files"] as DataTable;
        //    DataView dvfiles = dtfiles.DefaultView;
        //    dvfiles.RowFilter = "tabdetails_id = '" + tabdetails_id + "'";
        //    dtfiles = dvfiles.ToTable();
        //}
        //else
        //{
        //    ProjectGenericForms obj = new ProjectGenericForms();
        //    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

        //    DataTable dtfiles_db = obj.GetTabFileDetails(Int64.Parse(UserID), Int32.Parse(tabdetails_id));

        //    dtfiles = AddTabFiles(dtfiles_db);
        //}

        return dtfiles;
    }

    public static string GetTabFiles_Table(DataTable dt_files)
    {
        if (dt_files.Rows.Count > 0)
        {
            string files_html = "<table class='table table-bordered'>";
            files_html = files_html + "<tr>";
            files_html = files_html + "<th>File Name</th>";
            files_html = files_html + "<th>File Size</th>";
            files_html = files_html + "<th>File Date</th>";
            files_html = files_html + "<th>File Description</th>";
            files_html = files_html + "<th>File Type</th>";
            files_html = files_html + "<th>Download</th>";
            files_html = files_html + "<th>Remove</th>";
            files_html = files_html + "</tr>";

            for (var i = 0; i < dt_files.Rows.Count; i++)
            {
                string rand = MiscFunctions.RandomString(20).ToLower();
                string file_size = dt_files.Rows[i]["file_size"].ToString();
                string file_date = dt_files.Rows[i]["file_date"].ToString();
                string file_description = dt_files.Rows[i]["file_description"].ToString();
                string file_type = dt_files.Rows[i]["file_type"].ToString();

                string file_key = dt_files.Rows[i]["file_key"].ToString();
                file_size = (file_size == null ? "" : file_size);



                var tr = "<tr>";
                tr = tr + "<td><p class='file-name'>" + dt_files.Rows[i]["file_name"].ToString() + "</p></td>";
                tr = tr + "<td>" + file_size + "</td>";
                tr = tr + "<td>" + file_date + "</td>";
                tr = tr + "<td><p file_key='" + file_key + "' class='file-name'>" + file_description + "</p></td>";
                tr = tr + "<td>" + file_type + "</td>";
                tr = tr + "<td><a href='../download.ashx?key=" + file_key + "'>Download</a></td>";
                tr = tr + "<td id='" + file_key + "'><a href='javascript:void(0);' onclick='RemoveTabFile(this)'>Remove</a></td>";

                tr = tr + "</tr>";

                files_html = files_html + tr;
            }

            files_html = files_html + "</table>";

            return files_html;
        }
        else
        {
            return "";
        }
    }

    public static string BindDynamicGrid(List<dynamic> grid_struct, bool CanAdd, bool CanDelete, string FieldName, bool _IsReadOnly, bool additionalGridButton,
        string additionalGridButtonText, string additionalGridButtonAction, string additionalGridButtonAction_NavID, string currentNavID, int additionalGridTabDetailsId, string additionalGridPrimaryColumnId,
        string additionalGridTitle, bool AddOneRecordCurrentDay, bool RemoveOnlyCurrentDayRecord, string RemoveOnlyCurrentDayErrorMessage, string AddOneRecordCurrentDayErrorMessage, string RemoveOnlyMessageforInitialRow,
        string TableSplClass, bool expandArrowRequired, bool isContextMenu, ContextMenu contextMenu, string currentNavigationID, bool AccordionRequired = false)
    {
        string additionalButton = string.Empty;
        string expandArrowButton = string.Empty;
        string contextMenuButton = string.Empty;
        if (additionalGridButton)
        {
            if (!string.IsNullOrWhiteSpace(additionalGridButtonAction_NavID))
            {
                additionalGridButtonAction_NavID = EncyptSalt.EncryptText(additionalGridButtonAction_NavID, Trabau_Keys.Project_Key);
                additionalGridButtonAction_NavID = MiscFunctions.Base64EncodingMethod(additionalGridButtonAction_NavID);
            }

            additionalButton = "<input type='button' value='" + additionalGridButtonText + "' class='btn btn-success' onclick='"
                + (!string.IsNullOrWhiteSpace(additionalGridButtonAction) ? additionalGridButtonAction + "(this)" : "ViewRowDetails(this)")
                + "'" + (!string.IsNullOrWhiteSpace(additionalGridButtonAction) ? " nav-id='" + additionalGridButtonAction_NavID + "'" : "")
                + (!string.IsNullOrWhiteSpace(currentNavID) ? " currentnav-id='" + currentNavID + "'" : "")
                + " data-index='@dataIndex'"
                + " unique-property-key='@uniquePropertyKey'"
                + " primary-data='@additionalButtonId' data-source='@additionalGridTabDetailsId' grid-title='@additionalGridTitle' />";
        }

        if (expandArrowRequired)
        {
            currentNavigationID = MiscFunctions.EncryptAndEncode(currentNavigationID, Trabau_Keys.Project_Key);
            expandArrowButton = "<i class='fa fa-angle-down' style='padding: 10px;' aria-hidden='true' onclick='ViewRowDetails(this)' primary-data='@additionalButtonId' " +
                "data-source='@additionalGridTabDetailsId' grid-title='@additionalGridTitle' current-navigationID='" + currentNavigationID + "'></i>";
        }

        if (isContextMenu)
        {
            contextMenuButton = "<div class='context-menu-main'><a class='edit-pencil-button btn-context-menu'><i class='fa fa-ellipsis-h' aria-hidden='true'></i></a>";
            contextMenuButton += "<ul class='context-menu'>";
            for (int i = 0; i < contextMenu.ContextMenuItems.Count(); i++)
            {
                contextMenuButton += "<li><a parent-unique-property-key='@parentUniquePropertyKey' href='#' currentnav-id='" + currentNavID + "' nav-id='" + MiscFunctions.EncryptAndEncode(contextMenu.ContextMenuItems[i].TargetNavId, Trabau_Keys.Project_Key)
                    + "' onclick='OpenEditDialog(this);'>" + contextMenu.ContextMenuItems[i].Text + "</a></li>";
            }
            contextMenuButton += "</ul></div>";
        }

        string btnadd = "<input type='button' value='+' class='btn btn-success tbl-btn-rounded' onclick='AddTableRow(this)' AddOneRecordCurrentDay='" + AddOneRecordCurrentDay.ToString() + "' AddOneRecordCurrentDayErrorMessage='" + AddOneRecordCurrentDayErrorMessage.ToString() + "' />&nbsp;";
        string btndelete = "<input type='button' value='-' class='btn btn-danger tbl-btn-rounded' onclick='RemoveTableRow(this)' RemoveOnlyCurrentDayRecord='" + RemoveOnlyCurrentDayRecord.ToString() + "' RemoveOnlyCurrentDayErrorMessage='" + RemoveOnlyCurrentDayErrorMessage.ToString() + "' RemoveOnlyMessageforInitialRow='" + RemoveOnlyMessageforInitialRow.ToString() + "' />";
        bool Editor_Req = false;
        string td = "<td style='@style'>@RowTemplate</td>";
        string th = "<th id='@id'>@RowTemplate</th>";
        string html = "<div id='" + additionalGridTabDetailsId.ToString() + "' class='col-sm-12 tab-field'>" + (FieldName != "" ? "<h6 class='pb-3 pl-3'>" + FieldName + "</h6>" : "") + "<table id='dynamictable' class='table table-bordered table-trabau " + TableSplClass + "' read-only='" + (_IsReadOnly ? "true" : "false") + "'><thead class='thead-light thead-trabau'><tr>"
            + "</div>";

        var grid_struct_header = grid_struct.Select(x => new { x.FieldName, x.ColumnId }).Distinct().ToList();


        for (int k = 0; k < grid_struct_header.Count; k++)
        {
            string ColumnName = grid_struct_header[k].FieldName;
            int ColumnId = grid_struct_header[k].ColumnId;
            string _th = th;
            _th = _th.Replace("@RowTemplate", ColumnName);
            _th = _th.Replace("@id", ColumnId.ToString());
            html = html + _th;
        }
        if (CanAdd || CanDelete)
        {
            string _th = th;
            _th = _th.Replace("@RowTemplate", "Action");
            _th = _th.Replace("@id", "header_actions");
            html = html + _th;
        }

        if (additionalGridButton || isContextMenu)
        {
            string _additionalth = th;
            _additionalth = _additionalth.Replace("@RowTemplate", additionalGridButtonText);
            _additionalth = _additionalth.Replace("@id", "additional_actions");
            html = html + _additionalth;
        }

        if (expandArrowRequired)
        {
            string _additionalth = th;
            _additionalth = _additionalth.Replace("@RowTemplate", string.Empty);
            _additionalth = _additionalth.Replace("@id", "additional_actions");
            html = html + _additionalth;
        }

        html = html + "</tr></thead>";

        var grid_struct_Rows = grid_struct.Where(i => i.ColumnIndex > 0).Select(x => new { RowIndex = x.ColumnIndex }).Distinct().OrderBy(o => o.RowIndex).ToList();

        if (grid_struct_Rows.Count == 0)
        {
            int totalColumns = grid_struct_header.Count;

            if (additionalGridButton)
            {
                totalColumns = totalColumns + 1;
            }

            html = html + "<tr>";
            html = html + "<td colspan='" + totalColumns.ToString() + "' class='text-center no-result-found'><div><i class='flaticon-not-found' aria-hidden='true'></i></div><h3>No records found</h3></td>";
            html = html + "</tr>";
        }
        for (int i = 0; i < grid_struct_Rows.Count; i++)
        {
            bool CanAppend_AddButton = false;

            html = html + "<tr>";
            int rowIndex = grid_struct_Rows[i].RowIndex;

            var grid_struct_row = grid_struct.Where(x => x.ColumnIndex == rowIndex).ToList();


            for (int k = 0; k < grid_struct_row.Count; k++)
            {
                string _row_template = CreateFieldElement(grid_struct_row[k], ref Editor_Req, true);
                string _td = td;
                _td = _td.Replace("@RowTemplate", _row_template);
                _td = _td.Replace("@style", "width:" + grid_struct_row[k].FieldWidth + "; text-align:" + grid_struct_row[k].TextAlignPosition);
                html = html + _td;
            }

            if (i == grid_struct_Rows.Count - 1 && CanAdd)
            {
                CanAppend_AddButton = true;
            }

            if (CanAdd || CanDelete)
            {
                string _td = td;
                _td = _td.Replace("@RowTemplate", (CanAppend_AddButton ? btnadd : "") + (CanDelete ? btndelete : ""));
                _td = _td.Replace("@style", "width:100px;");
                html = html + _td;
            }

            if (additionalGridButton)
            {
                string additionalButtonId = string.Empty;
                try
                {
                    additionalButtonId = grid_struct_row.Where(x => x.ColumnId.ToString() == additionalGridPrimaryColumnId).SingleOrDefault().FieldValue;
                }
                catch (Exception)
                {
                }

                string uniquePropertyKey = string.Empty;
                try
                {
                    uniquePropertyKey = grid_struct.Where(x => x.ColumnIndex == rowIndex).FirstOrDefault().UniquePropertyKey;
                }
                catch (Exception)
                {
                }

                string _additionaltd = td;
                _additionaltd = _additionaltd.Replace("@RowTemplate", additionalButton);
                _additionaltd = _additionaltd.Replace("@additionalButtonId", (additionalButtonId != "" ? additionalButtonId : ""));
                _additionaltd = _additionaltd.Replace("@additionalGridTabDetailsId", (additionalGridTabDetailsId > 0 ? MiscFunctions.Base64EncodingMethod(additionalGridTabDetailsId.ToString()) : ""));
                _additionaltd = _additionaltd.Replace("@additionalGridTitle", additionalGridTitle);

                string _rowIndex = rowIndex.ToString();
                _rowIndex = EncyptSalt.EncryptText(_rowIndex, Trabau_Keys.Project_Key);
                _rowIndex = MiscFunctions.Base64EncodingMethod(_rowIndex);

                if (isContextMenu)
                {
                    _additionaltd = _additionaltd.Replace("@style", "width:150px;");
                    _additionaltd = _additionaltd.Replace("</td>", "");
                    _additionaltd += contextMenuButton + "</td>";
                    _additionaltd = _additionaltd.Replace("@parentUniquePropertyKey", uniquePropertyKey);
                }
                else
                {
                    _additionaltd = _additionaltd.Replace("@style", "width:100px;");
                }

                _additionaltd = _additionaltd.Replace("@dataIndex", _rowIndex);
                _additionaltd = _additionaltd.Replace("@uniquePropertyKey", uniquePropertyKey);

                html += _additionaltd;
            }

            if (expandArrowRequired)
            {
                string expandedButtonId = string.Empty;
                try
                {
                    expandedButtonId = grid_struct_row.Where(x => x.ColumnId.ToString() == additionalGridPrimaryColumnId).SingleOrDefault().FieldValue;
                }
                catch (Exception)
                {
                }

                if (string.IsNullOrEmpty(expandedButtonId))
                {
                    expandedButtonId = grid_struct_row.FirstOrDefault().UniquePropertyKey;
                }

                string _expandtd = td;

                _expandtd = _expandtd.Replace("@RowTemplate", expandArrowButton);
                _expandtd = _expandtd.Replace("@style", "text-align: center; vertical-align: middle; width: 60px; cursor :pointer;");
                _expandtd = _expandtd.Replace("@additionalButtonId", (expandedButtonId != "" ? expandedButtonId : ""));
                _expandtd = _expandtd.Replace("@additionalGridTabDetailsId", (additionalGridTabDetailsId > 0 ? MiscFunctions.Base64EncodingMethod(additionalGridTabDetailsId.ToString()) : ""));
                _expandtd = _expandtd.Replace("@additionalGridTitle", additionalGridTitle);

                html = html + _expandtd;
            }

            html = html + "</tr>";
        }


        html = html + "</table>";

        if (AccordionRequired)
        {
            html = html + "<div class='expand-table'>Show all project communication</div>";
        }

        return html;
    }


    public static string GetProjectTabFiles()
    {
        string XMLData = "";
        try
        {
            if (HttpContext.Current.Session["TabProject_Files"] != null)
            {
                DataTable dt_files = HttpContext.Current.Session["TabProject_Files"] as DataTable;

                XMLData = MiscFunctions.GetXMLString(dt_files, "ProjectTabFiles");
            }
        }
        catch (Exception)
        {
        }

        return XMLData;
    }

    public static void RemoveFileBytes()
    {
        try
        {
            if (HttpContext.Current.Session["TabProject_Files"] != null)
            {
                DataTable dt_files = HttpContext.Current.Session["TabProject_Files"] as DataTable;
                foreach (DataRow row in dt_files.Rows)
                {
                    row["file_bytes"] = null;
                }
            }
        }
        catch (Exception ex)
        {
        }

    }

    [WebMethod(EnableSession = true)]
    public static string SaveTabContent(string itemtype, string datatoSave_table, string datatoSave, string Files_TabDetailsId, string preserveDataRequired,
        string uniquePropertyKey, string parentUniquePropertyKey)
    {
        string response = "";
        string _response = "";
        string _message = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                    var _data = ProjectGenericForms.Parse(datatoSave);
                    if (preserveDataRequired.ToLower() == "true")
                    {
                        PreserveData(_data);
                    }
                    else
                    {
                        _data = GetTabContentData(_data);
                        datatoSave = new XElement("tabdata",
                                                (from d in _data
                                                 select new XElement("data",
                                                     new XElement("key", d.key),
                                                     new XElement("value", d.value)
                                                                    )
                                                 )
                                             ).ToString();

                        string TabFiles_XML = GetProjectTabFiles();
                        string DataIndex = "0";
                        try
                        {
                            if (HttpContext.Current.Session["TabProject_DataIndex"] != null)
                            {
                                DataIndex = HttpContext.Current.Session["TabProject_DataIndex"].ToString();
                                DataIndex = MiscFunctions.DecodeAndDecrypt(DataIndex, Trabau_Keys.Project_Key);
                            }
                        }
                        catch
                        {
                            DataIndex = "0";
                        }

                        ProjectGenericForms obj = new ProjectGenericForms();
                        var data = obj.SaveTabData(Int64.Parse(UserID), datatoSave, datatoSave_table, itemtype, TabFiles_XML, Files_TabDetailsId, _ProjectID, Int32.Parse(DataIndex),
                            uniquePropertyKey, parentUniquePropertyKey);
                        if (data != null)
                        {
                            if (data.Count > 0)
                            {
                                _response = data[0].Response;
                                _message = data[0].Message;

                                if (_response == "success")
                                {
                                    HttpContext.Current.Session["TabProject_DataIndex"] = null;
                                    HttpContext.Current.Session["ProjectTabContent"] = null;
                                    detail.Add("ClosePopupRequired", data[0].ClosePopupRequired);
                                    if (HttpContext.Current.Session["TabProject_CurrentNavID"] != null)
                                    {
                                        string CurrentNavID = HttpContext.Current.Session["TabProject_CurrentNavID"].ToString();
                                        if (!string.IsNullOrWhiteSpace(CurrentNavID))
                                        {
                                            bool OpenPreviousDialog = false;
                                            try
                                            {
                                                OpenPreviousDialog = data[0].OpenPreviousDialog;
                                            }
                                            catch (Exception)
                                            {
                                            }

                                            if (OpenPreviousDialog)
                                            {
                                                detail.Add("CurrentNavID", CurrentNavID);
                                            }
                                        }
                                    }

                                    RemoveFileBytes();
                                }
                            }
                        }
                    }

                    detail.Add("response", "ok");

                }
                else
                {
                    detail.Add("response", "");
                }
            }
            else
            {
                detail.Add("response", "");
            }

            detail.Add("res_response", _response);
            detail.Add("res_message", _message);

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }

    public static List<propProjectData> GetTabContentData(List<propProjectData> data)
    {
        if (HttpContext.Current.Session["ProjectTabContent"] == null)
        {
            return data;
        }
        else
        {
            PreserveData(data);
            data = HttpContext.Current.Session["ProjectTabContent"] as List<propProjectData>;
            return data;
        }
    }

    public static void PreserveData(List<propProjectData> data)
    {
        if (HttpContext.Current.Session["ProjectTabContent"] == null)
        {
            HttpContext.Current.Session["ProjectTabContent"] = data;
        }
        else
        {
            var previousData = HttpContext.Current.Session["ProjectTabContent"] as List<propProjectData>;
            if (previousData != null)
            {
                foreach (propProjectData itemData in data)
                {
                    bool found = false;
                    foreach (propProjectData prevItemData in previousData)
                    {
                        if (itemData.key == prevItemData.key)
                        {
                            prevItemData.value = itemData.value;
                            found = true;
                            break;
                        }
                    }

                    if (!found) // new data addition required
                    {
                        previousData.Add(itemData);
                    }
                }

                HttpContext.Current.Session["ProjectTabContent"] = previousData;
            }
            else
            {
                HttpContext.Current.Session["ProjectTabContent"] = data;
            }
        }
    }


    [WebMethod(EnableSession = true)]
    public static string SaveTabFile(string base64data, string filename, string tabdetails_id, string file_desc)
    {
        string response = "";
        try
        {
            string filetype = MiscFunctions.GetFileExtension(base64data);

            base64data = base64data.Substring(base64data.IndexOf("base64,", 0) + 7, base64data.Length - (base64data.IndexOf("base64,", 0) + 7));
            //byte[] file_bytes = Encoding.ASCII.GetBytes(base64data);
            byte[] file_bytes = Convert.FromBase64String(base64data);

            DataTable dt_files = new DataTable();
            if (HttpContext.Current.Session["TabProject_Files"] != null)
            {
                dt_files = HttpContext.Current.Session["TabProject_Files"] as DataTable;
            }
            else
            {
                dt_files = GetTabFilesStructure();
            }

            filename = filename.Replace("&", "");

            dt_files = AddTabFilesItem(dt_files, filename, file_bytes, tabdetails_id, filetype, "", file_desc);
            HttpContext.Current.Session["TabProject_Files"] = dt_files;

            DataView dvfiles = dt_files.DefaultView;
            dvfiles.RowFilter = "tabdetails_id='" + tabdetails_id + "'";

            response = JsonConvert.SerializeObject(dvfiles.ToTable());
        }
        catch (Exception ex)
        {
        }

        return response;
    }


    [WebMethod(EnableSession = true)]
    public static string RemoveTabFile(string filekey, string tabdetails_id)
    {
        string response = "";
        try
        {
            DataTable dt_files = HttpContext.Current.Session["TabProject_Files"] as DataTable;

            dt_files.Rows.Cast<DataRow>().Where(
                r => r.ItemArray[0].ToString() == filekey).ToList().ForEach(r => r.Delete());

            dt_files.AcceptChanges();

            HttpContext.Current.Session["TabProject_Files"] = dt_files;

            DataView dvfiles = dt_files.DefaultView;
            dvfiles.RowFilter = "tabdetails_id='" + tabdetails_id + "'";

            response = JsonConvert.SerializeObject(dvfiles.ToTable());
        }
        catch (Exception ex)
        {
        }

        return response;
    }

    public static DataTable GetTabFilesStructure()
    {
        DataTable dt_files = new DataTable();
        dt_files.Columns.Add("file_key", typeof(string));
        dt_files.Columns.Add("file_name", typeof(string));
        dt_files.Columns.Add("file_bytes", typeof(string));
        dt_files.Columns.Add("file_type", typeof(string));
        dt_files.Columns.Add("file_size", typeof(string));
        dt_files.Columns.Add("tabdetails_id", typeof(string));
        dt_files.Columns.Add("file_date", typeof(string));
        dt_files.Columns.Add("file_description", typeof(string));
        return dt_files;
    }


    public static DataTable AddTabFilesItem(DataTable dt_files, string file_name, byte[] file_bytes, string tabdetails_id, string file_type, string file_size,
        string file_desc, string _filekey = "")
    {
        try
        {
            DataRow dr = dt_files.NewRow();
            dr["file_key"] = (_filekey == "" ? MiscFunctions.RandomString(20).ToLower() : _filekey);
            dr["file_name"] = file_name;
            dr["file_type"] = file_type;
            if (file_bytes != null)
            {
                string base64_bytes = Convert.ToBase64String(file_bytes);
                dr["file_bytes"] = base64_bytes;
            }
            else
            {
                dr["file_bytes"] = "";
            }
            dr["file_size"] = file_bytes != null ? (file_bytes.Length / 1024).ToString() + " KB" : file_size;
            dr["tabdetails_id"] = tabdetails_id;
            dr["file_date"] = DateTime.UtcNow.ToString("MMM dd yyyy hh:mm tt");
            dr["file_description"] = file_desc;
            dt_files.Rows.Add(dr);
        }
        catch (Exception ex)
        {
        }

        return dt_files;
    }



    [WebMethod(EnableSession = true)]
    public static string GetFullHelpText(string tabdetails_id)
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                    ProjectGenericForms obj = new ProjectGenericForms();
                    var data = obj.GetTabFieldFullHelpText(Int64.Parse(UserID), Int32.Parse(tabdetails_id));

                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            detail.Add("fullhelptext", data[0].HelpFullText);
                        }
                    }

                    detail.Add("response", "ok");

                }
                else
                {
                    detail.Add("response", "");
                }
            }
            else
            {
                detail.Add("response", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }


    [WebMethod(EnableSession = true)]
    public static string GetMoreFileDescription(string fileKey)
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                    ProjectGenericForms obj = new ProjectGenericForms();
                    var data = obj.GetMoreFileDescription(Int64.Parse(UserID), fileKey);

                    if (data != null)
                    {
                        if (data.Count > 0)
                        {
                            detail.Add("FileDescription", data[0].FileDescription);
                        }
                    }

                    detail.Add("response", "ok");

                }
                else
                {
                    detail.Add("response", "");
                }
            }
            else
            {
                detail.Add("response", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }


    [WebMethod(EnableSession = true)]
    public async static Task<string> CallFieldChangeEvent(string tabDetailsId, string param1)
    {
        string response = "";

        string authority = "";
        try
        {
            string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();
            string content = JsonConvert.SerializeObject(
                                new
                                {
                                    TabDetailsId = Int32.Parse(tabDetailsId),
                                    ProjectID = _ProjectID,
                                    UserId = Int64.Parse(UserID),
                                    Param1 = param1
                                });
            var request = HttpContext.Current.Request;
            if (request.Url.Host == "localhost")
            {
                authority = "https://localhost:7263";
            }
            else
            {
                authority = "https://" + request.Url.Authority + "/api";
            }

            response = await httpsProxy.CreateAsync(authority + "/Project/GetChangeEvents", HttpMethod.Post, content).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }

    [WebMethod(EnableSession = true)]
    public static string ExecuteFieldChangeEvent(string targetField_DataSource, string eventValue, string targetFieldId, string targetType)
    {
        string response = string.Empty;
        try
        {
            trabau_navigation obj = new trabau_navigation();
            string procedureName = MiscFunctions.DecodeAndDecrypt(targetField_DataSource, Trabau_Keys.Project_Key);
            string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("EventValue", eventValue);
            parameters[1] = new SqlParameter("ProjectID", _ProjectID);
            parameters[2] = new SqlParameter("UserID", UserID);
            parameters[3] = new SqlParameter("TabDetailsId", Int32.Parse(targetFieldId));

            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            var lst = obj.ExecuteProcedure(procedureName, parameters);

            if (lst != null)
            {
                if (targetType == "dropdown")
                {
                    string options = "";
                    string optionsChecked = "";
                    string DataIndex = lst[0].DataIndex.ToString();
                    for (int i = 0; i < lst.Count(); i++)
                    {
                        options = options + "<option value='" + lst[i].Value + "'>" + lst[i].Text + "</option>";
                        if (lst[i].Checked == "true")
                        {
                            if (optionsChecked == "")
                            {
                                optionsChecked = lst[i].Value;
                            }
                            else
                            {
                                optionsChecked = optionsChecked + "," + lst[i].Value;
                            }
                        }
                    }
                    detail.Add("response", "ok");
                    detail.Add("result", options);
                    detail.Add("optionsChecked", optionsChecked);
                    if (DataIndex != "0" && !string.IsNullOrEmpty(DataIndex))
                    {
                        HttpContext.Current.Session["TabProject_DataIndex"] = MiscFunctions.EncryptAndEncode(DataIndex, Trabau_Keys.Project_Key);
                    }
                    else
                    {
                        HttpContext.Current.Session["TabProject_DataIndex"] = null;
                    }
                }
                else if (targetType == "label" || targetType == "textbox")
                {
                    string result = lst[0].Value;
                    detail.Add("response", "ok");
                    detail.Add("result", result);
                }
                else if (targetType == "grid")
                {
                    // string _row_template = GetGridHTML(navID, UserID, _navID, obj, fields, fieldItems, ref DatePickerRequired, i, _FieldName, _TabDetailsId, _AccordionRequired, _IsReadOnly, CanAdd, CanDelete);
                }


            }
            else
            {
                detail.Add("response", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception)
        {
        }
        return response;
    }

    [WebMethod(EnableSession = true)]
    public static string PreserveTabContentData(string datatoSave)
    {
        try
        {
            var _data = ProjectGenericForms.Parse(datatoSave);
            PreserveData(_data);
        }
        catch (Exception)
        {
        }
        return "success";
    }

    [WebMethod(EnableSession = true)]
    public static string GetChildNavigationItems(string navigationID, string domain)
    {
        string response = "";
        try
        {
            List<Dictionary<string, object>> details = new List<Dictionary<string, object>>();
            Dictionary<string, object> detail = new Dictionary<string, object>();
            if (HttpContext.Current.Session["Trabau_UserId"] != null)
            {
                string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
                if (UserType == "H" || UserType == "W")
                {
                    HttpContext.Current.Session["ProjectNavigation_Menu_Cached"] = null;

                    string UserID = HttpContext.Current.Session["Trabau_UserId"].ToString();

                    domain = MiscFunctions.DecodeAndDecrypt(domain, Trabau_Keys.Domain_Key);
                    string ProjectID_Enc = MiscFunctions.EncryptAndEncode(_ProjectID.ToString(), Trabau_Keys.Project_Key);

                    string ProjectID_Enc_Edit = _ProjectID.ToString();
                    ProjectID_Enc_Edit = MiscFunctions.EncryptAndEncode(ProjectID_Enc_Edit + "~true", Trabau_Keys.Project_Key);

                    string ProjectID_Enc_View = _ProjectID.ToString();
                    ProjectID_Enc_View = MiscFunctions.EncryptAndEncode(ProjectID_Enc_View + "~false", Trabau_Keys.Project_Key);

                    navigationID = MiscFunctions.DecodeAndDecrypt(navigationID, Trabau_Keys.Project_Key);

                    trabau_navigation obj = new trabau_navigation();

                    var _menu = obj.GetUserProjectNavigation(Int64.Parse(UserID), domain, _ProjectID, ProjectID_Enc, ProjectID_Enc_Edit, ProjectID_Enc_View, ParentId: Int32.Parse(navigationID));

                    if (_menu != null)
                    {
                        if (_menu.Count > 0)
                        {
                            _menu.ForEach(x =>
                            {
                                x.MenuIdEnc = (string.IsNullOrEmpty(x.MenuIdEnc) ? (MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(x.MenuId.ToString(), Trabau_Keys.Project_Key))) : x.MenuIdEnc);
                                if (x.CanSwitchDomain)
                                {
                                    string redirect_domain = "Communication";
                                    if (domain == redirect_domain)
                                    {
                                        redirect_domain = "Theory";
                                    }
                                    redirect_domain = EncyptSalt.EncryptText(redirect_domain, Trabau_Keys.Domain_Key);
                                    redirect_domain = MiscFunctions.Base64EncodingMethod(redirect_domain);

                                    string DomainSwitchURL = (HttpContext.Current.Request.Url.AbsoluteUri.Contains("http://") ? "http://" : "https://") + HttpContext.Current.Request.Url.Authority + "/projects/view-project.aspx?domain=@domain&id=" + ProjectID_Enc;

                                    DomainSwitchURL = DomainSwitchURL.Replace("@domain", redirect_domain);

                                    x.NavigationURL = (x.Name.Contains("Switch to " + (domain == "Communication" ? "Theory" : "Communication") + " Domain") ? DomainSwitchURL : "javascript:void(0)");
                                }
                            });

                            string html = CreateChildNavigation(new StringBuilder(), _menu, Int32.Parse(navigationID), false);
                            detail.Add("response", "ok");
                            detail.Add("NavigationHTML", html);
                        }
                        else
                        {
                            detail.Add("response", "");
                        }
                    }
                    else
                    {
                        detail.Add("response", "");
                    }
                }
                else
                {
                    detail.Add("response", "");
                }
            }
            else
            {
                detail.Add("response", "");
            }

            details.Add(detail);

            JavaScriptSerializer serial = new JavaScriptSerializer();
            response = serial.Serialize(details);
        }
        catch (Exception ex)
        {
            //return ex.Message;
        }
        return response;
    }

    public static void UpdateCachedNavigationMenu(List<dynamic> menu)
    {
        try
        {
            if (HttpContext.Current.Session["ProjectNavigation_Menu_Cached_Full"] == null)
            {
                HttpContext.Current.Session["ProjectNavigation_Menu_Cached_Full"] = menu;
            }
            else
            {
                var cachedMenu = HttpContext.Current.Session["ProjectNavigation_Menu_Cached_Full"] as List<dynamic>;
                for (int i = 0; i < menu.Count(); i++)
                {
                    int NavigationID = menu[i].MenuId;
                    if (!cachedMenu.Any(x => x.MenuId == NavigationID))
                    {
                        cachedMenu.Add(menu[i]);
                    }
                }

                HttpContext.Current.Session["ProjectNavigation_Menu_Cached_Full"] = cachedMenu;
            }
        }
        catch (Exception)
        {
        }
    }

    public static string CreateChildNavigation(StringBuilder sb, List<dynamic> menu, int ParentID, bool subMenuClassRequired)
    {
        string navigationHTML = string.Empty;
        try
        {
            UpdateCachedNavigationMenu(menu);
            if (HttpContext.Current.Session["ProjectNavigation_Menu_Cached"] == null)
            {
                HttpContext.Current.Session["ProjectNavigation_Menu_Cached"] = menu;
            }

            var cachedMenu = HttpContext.Current.Session["ProjectNavigation_Menu_Cached"] as List<dynamic>;

            var parentMenu = cachedMenu.Where(x => x.ParentId == ParentID).OrderBy(o => o.OrderNo).ToList();
            if (parentMenu.Count > 0)
            {
                sb.Append("<ul childLevel='" + GetChildNumber(ParentID) + "' id='" + ParentID + "' class='dropdown-menu" + (subMenuClassRequired ? " submenu" : "") + "'>");
                foreach (var item in parentMenu)
                {
                    int childId = item.MenuId;
                    string childTitle = item.Name;

                    var childRow = cachedMenu.Where(a => a.ParentId == childId).OrderBy(x => x.OrderNo).ToList();
                    string Seperator = (item.Seperator ? "border-bottom-normal" : "");
                    if (childRow.Count() > 0)
                    {
                        sb.Append("<li class='dropdown-item" + (string.IsNullOrWhiteSpace(Seperator) ? "" : " " + Seperator) + "'><a id='a" + item.MenuId.ToString()
                            + "' class='nav-link dropdown-toggle' href='" + item.NavigationURL + "'>" + item.Name + "</a>");
                    }
                    else
                    {
                        string MenuId_Enc = (MiscFunctions.Base64EncodingMethod(EncyptSalt.EncryptText(item.MenuId.ToString(), Trabau_Keys.Project_Key)));
                        sb.Append("<li class='dropdown-item" + (string.IsNullOrWhiteSpace(Seperator) ? "" : " " + Seperator) +
                            "'><a id='a" + item.MenuId.ToString() + "' nav_id='" + MenuId_Enc + "'" + (string.IsNullOrWhiteSpace(item.ClickEventName) ? "" : " onclick='" + item.ClickEventName + "'") + " class='nav-link dropdown-item"
                            + (string.IsNullOrWhiteSpace(item.PermissionMessage) && string.IsNullOrWhiteSpace(item.ClickEventName) && item.NavigationURL.Contains("javascript:void") ? " navDisabled" : "")
                            + (!string.IsNullOrWhiteSpace(item.PermissionMessage) ? " navigationDisabled" : "")
                            + "' href='" + item.NavigationURL + "'>" + item.Name + "</a>");
                    }
                    CreateChildNavigation(sb, childRow, childId, true);
                    sb.Append("</li>");
                }
                sb.Append("</ul>");

                navigationHTML = sb.ToString();
            }
        }
        catch (Exception)
        {
        }
        return navigationHTML;
    }

    [WebMethod(EnableSession = true)]
    public static string ValidateNavigation(string NavigationID)
    {
        try
        {
            string UserType = HttpContext.Current.Session["Trabau_UserType"].ToString();
            if (UserType == "W")
            {
                NavigationID = MiscFunctions.DecodeAndDecrypt(NavigationID, Trabau_Keys.Project_Key);
                var cachedMenu = HttpContext.Current.Session["ProjectNavigation_Menu_Cached_Full"] as List<dynamic>;
                var permissionMenu = cachedMenu.Where(x => x.MenuId.ToString() == NavigationID).ToList();
                if (permissionMenu != null && permissionMenu.Any())
                {
                    string PermissionMessage = permissionMenu.FirstOrDefault().PermissionMessage;
                    return PermissionMessage;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            return "Unable to identify the navigation access, please refresh and try again";
        }
    }
}