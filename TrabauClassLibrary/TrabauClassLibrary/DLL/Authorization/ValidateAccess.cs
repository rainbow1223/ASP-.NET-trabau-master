using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabauClassLibrary.DLL.Authorization
{
    public class ValidateAccess
    {
        public static bool Validate(string URL, bool EmailAddressVerfied, string UserType)
        {
            bool val = false;
            try
            {
                if (URL == "profile/settings" || URL == "profile/settings/" || URL == "profile/settings/index.aspx"
                || URL == "error" || URL == "error.aspx" || URL == "single-category" || URL == "single-category.aspx"
                || URL.Contains("profile/user/profile.aspx?profile=")
                || URL.Contains("profile/user/userprofile.aspx?profile=")
                || URL == "signup/profile-updation.aspx" || URL == "signup/profile-updation"
                || URL == "verification.aspx" || URL == "verification" || URL == "signup/company-details.aspx" || URL == "signup/company-details"
                || URL == "jobs/searchjobs/search" || URL == "jobs/searchjobs/search.aspx")
                {
                    val = true;
                }


                if (!val)
                {
                    if (EmailAddressVerfied)
                    {
                        if (UserType == "H")
                        {
                            if (URL == "jobs/posting/postjob" || URL == "jobs/posting/postjob.aspx" || URL.Contains("jobs/posting/postjob.aspx?asyncfileuploadid") ||
                                URL == "jobs/posting/postedjobs" || URL == "jobs/posting/postedjobs.aspx" ||
                                URL == "jobs/posting/jobposting" || URL == "jobs/posting/jobposting.aspx" ||
                                URL == "jobs/hires/index" || URL == "jobs/hires/index.aspx" ||
                                URL.Contains("profile/search") ||
                                URL == "profile/preferlist/index" || URL == "profile/preferlist/index.aspx" ||
                                URL == "jobs/interview/index" || URL == "jobs/interview/index.aspx" ||
                                URL.Contains("projects/view-project?domain=MkhQU25RSSt5bldQR3JYSkRKUDRhQT09".ToLower()) || URL.Contains("projects/view-project.aspx?domain=MkhQU25RSSt5bldQR3JYSkRKUDRhQT09".ToLower()) ||
                                URL.Contains("projects/view-project?domain=NVlQdmorT0p5bmx2NHNLa0RRM29CZz09".ToLower()) || URL.Contains("projects/view-project.aspx?domain=NVlQdmorT0p5bmx2NHNLa0RRM29CZz09".ToLower()) ||
                                URL == "projects/index" || URL == "projects/index.aspx" ||
                                URL == "jobs/proposals/all-proposals" || URL == "jobs/proposals/all-proposals.aspx" ||
                                URL == "projects/new-project" || URL == "projects/new-project.aspx" || URL.Contains("projects/new-project.aspx?asyncfileuploadid") ||
                                URL.Contains("projects/edit-project.aspx?projectid=")
                                )
                            {
                                val = true;
                            }
                        }
                        else if (UserType == "W")
                        {
                            if (URL == "signup/profile-updation" || URL == "signup/profile-updation.aspx"
                                || URL == "profile/user/profile.aspx" || URL == "profile/user/profile"
                                || URL.Contains("profile/user/portfolios") || URL.Contains("jobs/searchjobs/apply")
                                || URL.Contains("jobs/searchjobs/viewjob")
                                || URL == "jobs/searchjobs/search" || URL == "jobs/searchjobs/search.aspx" || URL == "jobs/searchjobs/search.aspx?location=savedjobs"
                                || URL == "jobs/proposals/index.aspx" || URL == "jobs/proposals/index"
                                || URL.Contains("jobs/proposals/viewproposal.aspx?proposal=")
                                || URL.Contains("jobs/searchjobs/index.aspx") || URL.Contains("profile/search")
                                || URL == "profile/preferlist/index" || URL == "profile/preferlist/index.aspx"
                                || URL == "jobs/myjobs" || URL == "jobs/myjobs.aspx"
                                || URL == "jobs/interview/myinterviews" || URL == "jobs/interview/myinterviews.aspx"
                                || URL.Contains("jobs/interview/start.aspx")
                                || URL.Contains("projects/view-project?domain=MkhQU25RSSt5bldQR3JYSkRKUDRhQT09".ToLower()) || URL.Contains("projects/view-project.aspx?domain=MkhQU25RSSt5bldQR3JYSkRKUDRhQT09".ToLower())
                                || URL.Contains("projects/view-project?domain=NVlQdmorT0p5bmx2NHNLa0RRM29CZz09".ToLower()) || URL.Contains("projects/view-project.aspx?domain=NVlQdmorT0p5bmx2NHNLa0RRM29CZz09".ToLower())
                                || URL == "projects/index" || URL == "projects/index.aspx"

                                 )
                            {
                                val = true;
                            }
                        }
                        else if (UserType == "A")
                        {
                            if (URL == "admin/citymaster" || URL == "admin/citymaster.aspx"
                                || URL == "admin/countrymaster" || URL == "admin/countrymaster.aspx"
                                || URL == "admin/statemaster" || URL == "admin/statemaster.aspx"
                                || URL == "admin/employment-rolemaster" || URL == "admin/employment-rolemaster.aspx"
                                || URL == "admin/skillmaster" || URL == "admin/skillmaster.aspx"
                                || URL == "admin/document-categorymaster" || URL == "admin/document-categorymaster.aspx"
                                || URL == "admin/services-categorymaster" || URL == "admin/services-categorymaster.aspx"
                                || URL == "admin/generic-categories" || URL == "admin/generic-categories.aspx"
                                )
                            {
                                val = true;
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {

            }
            return val;
        }
    }
}
