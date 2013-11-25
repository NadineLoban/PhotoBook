using System;
using System.Collections.Generic;
using System.Linq;
using PhotoUploadService;
using PhotogramWeb.Filters;
using Services.Services;
using System.Web.Mvc;
using ServicesContracts.Contracts;

namespace PhotogramWeb.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        IPhotoService photoService = new PhotoService();

        public ActionResult Index()
        {
            return View(photoService.GetTheMostPopularForSlideShow());
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your app description page. ";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
//        public String AllTags()
//        {
//            var ctx = new UsersContext();
//            return string.Join(",", ctx.Tags.Select(tag => tag.Name));
//        }

        public ActionResult GetTagClouds()
        {

            return PartialView("_TagClouds", new TagService().GetAllTags());
        }

        private readonly IUserProfileService userProfileService = new UserProfileService();

        [HttpPost]
        public JsonResult GetUsers(string query)
        {
            var users = userProfileService.GetUserProfiles().Where(user => user.UserName.Contains(query));

            return Json(users);
        }

        public JsonResult GetData(string query)
        {
            var users = userProfileService.GetUserProfiles().Where(user => user.UserName.Contains(query));
//            var list = new TagSys();
//            var fetchTag = list.GetTagList().Where(m => m.name.ToLower().StartsWith(q.ToLower()));
            return Json(users, JsonRequestBehavior.AllowGet);
        }


    }
}
