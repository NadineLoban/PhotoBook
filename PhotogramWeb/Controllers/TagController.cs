using System.Linq;
using System.Web.Mvc;
using DomainViewModels.ViewModels;
using Services.Services;
using ServicesContracts.Contracts;

namespace PhotogramWeb.Controllers
{
    public class TagController : Controller
    {
        //
        // GET: /Tag/
        readonly ITagService tagService = new TagService();
        readonly IPhotoService photoService = new PhotoService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(TagAddViewModel tagAddViewModel)
        {
            tagService.AddTag(tagAddViewModel);
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(tagService.GetAllTags());
        }


        public JsonResult GelAllTags(string term)
        {
            var tags = tagService.FindTagByName(term);
            return Json(tags.Select(tag => tag.Name).ToList());
        }
            
        [HttpGet]
        public PartialViewResult GetTopTags()
        {
            return PartialView("_TopTags");
        }
        
        [HttpPost]
        public JsonResult GetTop5Tags()
        {
            var topTags = tagService.GetTopTags(5).ToList();
            return Json(new {Tags = topTags.Select(tag => tag.Name), Counts = topTags.Select(tag => tag.Count)});
        }
    }
}
