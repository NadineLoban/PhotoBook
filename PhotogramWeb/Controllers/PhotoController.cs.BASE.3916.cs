using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainViewModels.ViewModels;
using Services.Services;
using ServicesContracts.Contracts;

namespace PhotogramWeb.Controllers
{
    public class PhotoController : Controller
    {
        //
        // GET: /Photo/
        private readonly IPhotoService photoService = new PhotoService();
        private readonly ITagService tagService = new TagService();

        public ActionResult Index()
        {
            return View(photoService.GetPhotoUrlsForUser(User.Identity.Name));
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Create()
        {
            return View();
        }

//        [HttpPost]
//        [Authorize]
//        public JsonResult UploadPhoto(HttpPostedFileBase file)
//        {
////            var count = Request.Files.Count;
////            var file = HttpContext.Request.Files[0];
////            var file = Request.Files["file"];
//
//            var imageData = photoService.UploadPhoto(file);
//            return Json(new {publicId = imageData.PublicId, imageDataId = imageData.ImageDataId});
//        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        public ActionResult Create(PhotoAddInfoViewModel photoAddInfoViewModel)
        {
            var file = HttpContext.Request.Files[0];
            if (file != null)
            {
                photoAddInfoViewModel.OriginalPhoto = file;
                photoAddInfoViewModel.Login = User.Identity.Name;
                var tags =
                    new List<String>(photoAddInfoViewModel.TagsString.ToLower()
                                                          .Split(new string[] {","},
                                                                 StringSplitOptions.RemoveEmptyEntries));
                photoAddInfoViewModel.TagAddViewModels = new Collection<TagAddViewModel>();
                foreach (var tag in tags)
                {
                    photoAddInfoViewModel.TagAddViewModels.Add(new TagAddViewModel(tag));
                }
                photoService.CreatePhoto(photoAddInfoViewModel);
            }
            else
            {
                return View();
            }
            return View("Index", photoService.GetPhotoUrlsForUser(User.Identity.Name));
        }

        [Authorize]
        public ActionResult Edit(Int32 id)
        {
            if (ModelState.IsValid)
            {
                var photoUpdateInfoViewModel = photoService.GetPhotoForEdit(id);

                return View(photoUpdateInfoViewModel);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult GetCurrentUserPhoto()
        {
            return View("List", photoService.GetPhotosByUserLogin(User.Identity.Name).ToList());
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(PhotoUpdateInfoViewModel photoUpdateInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                photoUpdateInfoViewModel.Login = User.Identity.Name;
                List<String> tags = new List<String>(photoUpdateInfoViewModel.TagsString.ToLower().Split(new string[] { ","}, StringSplitOptions.RemoveEmptyEntries));
                photoUpdateInfoViewModel.TagsViewModels = new Collection<TagSimpleViewModel>();
                foreach (var tag in tags)
                {
                    photoUpdateInfoViewModel.TagsViewModels.Add(new TagSimpleViewModel(tag));
                }
                photoService.UpdatePhoto(photoUpdateInfoViewModel);
            }
            return View("Index", photoService.GetPhotoUrlsForUser(User.Identity.Name));
        }

        public ActionResult ShowPhotosByTag(String tagName)
        {
            var photos = tagService.GetPhotosByTag(tagName, User.Identity.Name);
            return View("List", photos.ToList());
        }

        public ActionResult List(List<PhotoCommonInfoViewModel> photoCommonInfoViewModel)
        {
            return View(photoCommonInfoViewModel);
        }

        [Authorize]
        public String SetEffect(string publicId, string effectName)
        {
            return photoService.GetUrlPhoto(publicId, effectName);
        }

        [Authorize]
        public PartialViewResult LikePhoto(string photoId)
        {
            return PartialView("_PhotoCommonView", photoService.LikePhoto(Int32.Parse(photoId), User.Identity.Name));
        }

        [Authorize]
        public PartialViewResult DislikePhoto(string photoId)
        {
            return PartialView("_PhotoCommonView", photoService.DislikePhoto(Int32.Parse(photoId), User.Identity.Name));
        }

        [Authorize]
        public ActionResult GetCurrentUserPhotos()
        {
            return View("List", photoService.GetPhotosByUserLogin(User.Identity.Name).ToList());
        }
    }
}
