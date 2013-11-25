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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(Int32 photoId)
        {
            return View(photoService.GetPhotoDetailsById(photoId, User.Identity.Name));
        }


        [HttpPost]
//        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize]
        public ActionResult Create(PhotoAddInfoViewModel photoAddInfoViewModel)
        {
            Int32 photoId;
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
                photoId = photoService.CreatePhoto(photoAddInfoViewModel);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Details", new {photoId = photoId});
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
            return RedirectToAction("Details", new { photoId = photoUpdateInfoViewModel.Id });
        }

        public ActionResult ShowPhotosByTag(String tagName)
        {
            var photos = tagService.GetPhotosByTag(tagName, User.Identity.Name);
            return View("List", photos.ToList());
        }

        public ActionResult GetAllUserPhotos(String userName)
        {
            var photos = photoService.GetPhotosByUserLogin(userName);
            return View("List", photos.ToList());
        }

        public ActionResult List(List<PhotoCommonInfoViewModel> photoCommonInfoViewModel)
        {
            return View(photoCommonInfoViewModel);
        }

        public ActionResult PhotoSlide(List<PhotoCommonInfoViewModel> photoCommonViewModels)
        {
            var photoSlideShowViewModels = new List<PhotoSlideShowViewModel>();
            foreach (var photoCommonInfoViewModel in photoCommonViewModels)
            {
                photoSlideShowViewModels.Add(new PhotoSlideShowViewModel(photoCommonInfoViewModel));
            }
            return View(photoSlideShowViewModels);
        }

        [Authorize]
        public String SetEffect(string publicId, string effectName, int valueOfEffect)
        {
            if (effectName.Equals("nofilter"))
            {
                return "";
            }
            return photoService.GetUrlPhoto(publicId, effectName, valueOfEffect);
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
        public PartialViewResult LikePhotoDetailed(string photoId)
        {
            var photoCommon = photoService.LikePhoto(Int32.Parse(photoId), User.Identity.Name);
            var photoDetailed = new PhotoDetailedInfoViewModel
                {
                    AmountOfDislikes = photoCommon.AmountOfDislikes,
                    AmountOfLikes = photoCommon.AmountOfLikes,
                    CommonRaiting = photoCommon.CommonRaiting,
                    OwnerLogin = photoCommon.OwnerOfPhotoName,
                    CurrentUserLogin = photoCommon.CurrentUserName,
                    PhotoId = photoCommon.PhotoId,
                    PhotoUrl = photoCommon.PhotoUrl,
                    IsLiked = photoCommon.IsLiked,
                    IsDisliked = photoCommon.IsDisliked
                    
                };
            return PartialView("_LikeBar", photoDetailed );
        }

        [Authorize]
        public PartialViewResult DislikePhotoDetailed(string photoId)
        {
            var photoCommon = photoService.DislikePhoto(Int32.Parse(photoId), User.Identity.Name);
            var photoDetailed = new PhotoDetailedInfoViewModel
            {
                AmountOfDislikes = photoCommon.AmountOfDislikes,
                AmountOfLikes = photoCommon.AmountOfLikes,
                CommonRaiting = photoCommon.CommonRaiting,
                OwnerLogin = photoCommon.OwnerOfPhotoName,
                CurrentUserLogin = photoCommon.CurrentUserName,
                PhotoId = photoCommon.PhotoId,
                PhotoUrl = photoCommon.PhotoUrl,
                IsLiked = photoCommon.IsLiked,
                IsDisliked = photoCommon.IsDisliked
            };
            return PartialView("_LikeBar", photoDetailed);
        }

        [Authorize]
        public ActionResult GetCurrentUserPhotos()
        {
            return View("List", photoService.GetPhotosByUserLogin(User.Identity.Name).ToList());
        }

        public ActionResult AddEffect(string photoId, string publicId, string effectName, Int32 valueOfEffect)
        {

            return View("Details", photoId);
        }

        [Authorize]
        public PartialViewResult ShowUsersPhotoSlideShow()
        {
            return PartialView("_PhotoSlideShow",
                               photoService.GetPhotosByUserLogin(User.Identity.Name)
                                           .OrderByDescending(photo => photo.CommonRaiting).Take(5)
                                           .Select(photo => photo.PhotoUrl));
        }


        public ActionResult GetTheMostPopularPhotos()
        {
            return RedirectToAction("PhotoSlide", photoService.GetTheMostPopularForSlideShow());
        }

        public ActionResult ShowTheMostPopularPhotos()
        {
            return View("List", photoService.GetTheMostPopular());
        }

        public ActionResult DeletePhoto(Int32 photoId)
        {
            if (!photoService.GetPhotoCommonById(photoId, User.Identity.Name).OwnerOfPhotoName.Equals(User.Identity.Name))
            {
                return RedirectToAction("Details", new {photoId = photoId});
            }
            photoService.DeletePhotoById(photoId);
            return RedirectToAction("GetCurrentUserPhotos");
        }
    }
        
}
