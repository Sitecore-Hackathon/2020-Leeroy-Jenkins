using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BasicCompany.Feature.Projects.Models;
using BasicCompany.Feature.Projects.Repositories;
using Sitecore.ContentSearch.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Security.Authentication;

namespace BasicCompany.Feature.Projects.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public ActionResult ProjectDetail()
        {
            //Item parentItem = Sitecore.Context.Database.GetItem("");
            //var list = parentItem.GetChildren();



            return View("ProjectDetail", new ProjectModel());
        }


        public ActionResult FeaturedProjects()
        {
            var projectList = new FeaturedProjectModel
            {
                FeaturedProjects = new List<ProjectModel>()
            };
            var parentItem = Sitecore.Context.Database.GetItem("{40AEC78F-751C-492C-BA22-79FA456D1613}");
            var projectSitecoreList = parentItem.GetChildren().ToList();

            foreach (var item in projectSitecoreList)
            {
                projectList.FeaturedProjects.Add(new ProjectModel() { ModuleTitle = item["ModuleTitle"], ModuleDescription = item["ModuleDescription"], SitecoreProduct = item["SitecoreProduct"], WebServerOs = item["WebServerOS"] });
            }

            return View("FeaturedProjects", projectList);
        }

        public ActionResult ProjectListByParticipant()
        {
            var parentItem = Sitecore.Context.Database.GetItem("{40AEC78F-751C-492C-BA22-79FA456D1613}");
            var modelItems = _projectRepository.GetProjectsByUser(parentItem);
            var model = _projectRepository.MapItemToModel(modelItems);
            return View("ProjectDetailByParticipant", model);
        } 

        [HttpPost]
        public ActionResult FeaturedProjects(FeaturedProjectModel projectDetail)
        {
            //Add new applicant name to sitecore item
            // Move to another view 
            return View();
        }

        [HttpPost]
        public ActionResult ProjectDetail(ProjectModel projectDetail)
        {
            if (ModelState.IsValid)
            {
                using (new Sitecore.SecurityModel.SecurityDisabler())
                {
                    try
                    {
                        var master = Sitecore.Data.Database.GetDatabase("master");
                        // Get the template to base the new item on
                        TemplateItem template =
                            master.GetItem(new ID("{25E91140-7DEC-4A28-8CD9-59C432C39A16}"));

                        // Get the place in the site tree where the new item must be inserted
                        var parentItem = master.GetItem(new ID("{40AEC78F-751C-492C-BA22-79FA456D1613}"));

                        if (parentItem != null)
                        {
                            var projectName = string.Format("{0}",
                                projectDetail.ModuleTitle.Replace(@"\", "").Replace(@"/", "").Replace(@":", "")
                                    .Replace(@"?", "").Replace(@"<", "").Replace(@">", "").Replace(@"|", "")
                                    .Replace(@"[", "").Replace(@"]", "").Replace(@"-", "").Replace("\"", "")
                                    .Replace(@"(", "").Replace(@")", ""));
                            var displayName = string.Format("{0}",
                                projectDetail.ModuleTitle.Replace(@"\", "").Replace(@"/", "").Replace(@":", "")
                                    .Replace(@"?", "").Replace(@"<", "").Replace(@">", "").Replace(@"|", "")
                                    .Replace(@"[", "").Replace(@"]", "").Replace(@"-", "").Replace("\"", "")
                                    .Replace(@"(", "").Replace(@")", ""));

                            var newProjectItem = parentItem.Add(projectName, template);
                            SetProjectInfo(projectDetail, newProjectItem, displayName);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                }


            }

            return View("ProjectDetail", new ProjectModel());
        }

        private static void SetProjectInfo(ProjectModel projectModel, Item projectSitecoreItem, string displayName)
        {
            try
            {
                projectSitecoreItem.Editing.BeginEdit();
                projectSitecoreItem.Fields["ModuleTitle"].Value = projectModel.ModuleTitle;
                projectSitecoreItem.Fields["ModuleDescription"].Value = projectModel.ModuleDescription;
                projectSitecoreItem.Fields["WebServerOS"].Value = projectModel.WebServerOs;
                projectSitecoreItem.Fields["SitecoreProduct"].Value = projectModel.SitecoreProduct;
                projectSitecoreItem.Fields["Owner"].Value = projectModel.Owner;
                projectSitecoreItem.Fields["Participants"].Value = projectModel.Participants;
                projectSitecoreItem.Fields["Price"].Value = projectModel.Price.ToString(CultureInfo.InvariantCulture);
                projectSitecoreItem.Editing.EndEdit();
                PublishItem(projectSitecoreItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// Publish item in web db
        /// </summary>
        /// <param name="item"></param>
        private static void PublishItem(Item item)
        {
            // The publishOptions determine the source and target database,
            // the publish mode and language, and the publish date
            var publishOptions =
                new Sitecore.Publishing.PublishOptions(item.Database,
                    Database.GetDatabase("web"),
                    Sitecore.Publishing.PublishMode.SingleItem,
                    item.Language,
                    DateTime.Now);  // Create a publisher with the publishoptions
            var publisher = new Sitecore.Publishing.Publisher(publishOptions);

            // Choose where to publish from
            publisher.Options.RootItem = item;

            // Publish children as well?
            publisher.Options.Deep = true;

            // Do the publish!
            publisher.Publish();
        }

        public ActionResult ProjectView()
        {
            var item = Sitecore.Context.Item;
            var model = new ProjectModel()
            {
                ModuleDescription = item.Fields["ModuleDescription"]?.Value,
                ModuleTitle = item.Fields["ModuleTitle"]?.Value,
                WebServerOs = item.Fields["WebServerOS"]?.Value,
                Owner = item.Fields["Owner"]?.Value,
                Price = string.IsNullOrEmpty(item.Fields["Price"]?.Value) ? 0 : Convert.ToDecimal(item.Fields["Price"]?.Value),
                SitecoreProduct = item.Fields["SitecoreProduct"]?.Value,
                Participants = item.Fields["Participants"]?.Value
            };
            return View("ProjectView", model);
        }
        [HttpPost]
        public ActionResult ProjectView(ProjectModel model)
        {
            var item = Sitecore.Context.Item;
            try
            {
                var userName = AuthenticationManager.GetActiveUser()?.Name;
                
                item.Editing.BeginEdit();
                item.Fields["Participants"].Value = $"{item.Fields["Participants"].Value} | {userName}";
                item.Editing.EndEdit();
                PublishItem(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View("ProjectView", new ProjectModel()
            {
                ModuleDescription = item.Fields["ModuleDescription"]?.Value,
                ModuleTitle = item.Fields["ModuleTitle"]?.Value,
                WebServerOs = item.Fields["WebServerOS"]?.Value,
                Owner = item.Fields["Owner"]?.Value,
                Price = string.IsNullOrEmpty(item.Fields["Price"]?.Value) ? 0 : Convert.ToDecimal(item.Fields["Price"]?.Value),
                SitecoreProduct = item.Fields["SitecoreProduct"]?.Value,
                Participants = item.Fields["Participants"]?.Value
            });
        }
    }
}