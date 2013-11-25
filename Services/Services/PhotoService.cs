using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using CloudinaryDotNet;
using DomainModels.Models;
using DomainViewModels.ViewModels;
using DomainRepositories;
using ServicesContracts.Contracts;


namespace Services.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly PhotoRepository photoRepository = new PhotoRepository();
        private readonly UserProfileRepository userProfileRepository = new UserProfileRepository();
        private readonly TagRepository tagRepository = new TagRepository();
        private readonly ConversionService conversionService = new ConversionService();
        private readonly TagService tagService = new TagService();
        private static readonly Account CloudAccount;
        private readonly IPhotoUploadService photoUploadService;

        static PhotoService()
        {
            CloudAccount = new Account
            {
                ApiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"],
                ApiSecret = System.Configuration.ConfigurationManager.AppSettings["ApiSecret"],
                Cloud = System.Configuration.ConfigurationManager.AppSettings["Cloud"]
            };
        }

        public PhotoService()
        {
            photoUploadService =
                new PhotoUploadService.PhotoUploadService(CloudAccount);
        }

        public String GetUrlPhoto(String publicId, string effectName, Int32 value)
        {
            return photoUploadService.SetEffects(publicId, effectName, value);
        }

        public String GetUrlPhoto(String publicId, string effectName)
        {
            return photoUploadService.SetEffects(publicId, effectName);
        }

        public List<PhotoSlideShowViewModel> GetTheMostPopularForSlideShow()
        {
            var photos = photoRepository.GetAll().OrderByDescending(photo => photo.CommonRaiting).Take(20);
            var photoSlideShowViewModels = new List<PhotoSlideShowViewModel>();
            foreach (var photo in photos)
            {
                photoSlideShowViewModels.Add(new PhotoSlideShowViewModel(photo));
            }
            return photoSlideShowViewModels;
        }

        public List<PhotoCommonInfoViewModel> GetTheMostPopular()
        {
            var photos = photoRepository.GetAll().OrderByDescending(photo => photo.CommonRaiting).Take(20);
            var photoCommonInfoViewModels = new List<PhotoCommonInfoViewModel>();
            foreach (var photo in photos)
            {
                photoCommonInfoViewModels.Add(new PhotoCommonInfoViewModel(photo));
            }
            return photoCommonInfoViewModels;
        }

        public AfterUploadPhotoViewModel UploadPhoto(HttpPostedFileBase file)
        {
            var imageDataOriginal = photoUploadService.UploadPhoto(file.FileName, file.InputStream);
            return new AfterUploadPhotoViewModel
                {
                    ImageDataId = imageDataOriginal.Id,
                    PublicId = imageDataOriginal.PublicId,
                    PhotoUrl = imageDataOriginal.Url
                };

        }

        public IEnumerable<PhotoCommonInfoViewModel> GetPhotosByUserLogin(string login)
        {
            return photoRepository.GetBy(photo => photo.Owner.UserName.ToLower() == login.ToLower()).Select(photo => new PhotoCommonInfoViewModel(photo));
        }


        public IEnumerable<PhotoCommonInfoViewModel> GetPhotosByUserId(Int32 id)
        {
            var photoCommonInfoViewModels = new List<PhotoCommonInfoViewModel>();
            UserProfile userProfile = userProfileRepository.GetById(id);
            var photos = userProfile.Photos;
            String login = userProfile.UserName;
            foreach (var photo in photos)
            {
                photoCommonInfoViewModels.Add(new PhotoCommonInfoViewModel(photo, login, IsLiked(photo.Id, login), IsDisliked(photo.Id, login)));
            }
            return photoCommonInfoViewModels.AsEnumerable();
        }

        public Int32 CreatePhoto(PhotoAddInfoViewModel photoCard)
        {
            var imageData = photoUploadService.UploadPhoto(photoCard.OriginalPhoto.FileName, photoCard.OriginalPhoto.InputStream);
            var photo = new Photo
            {
                Description = photoCard.Description,
                OriginalPhoto = imageData

            };
            var user = userProfileRepository.GetByLogin(photoCard.Login);
            if (user != null)
            {
                photo.Owner = user;
            }
            else
            {
                throw new Exception("Шеф усё пропало");
            }
            
            photo.Tags = new Collection<Tag>();
            foreach (var tagsViewModel in photoCard.TagAddViewModels)
            {
                photo.Tags.Add(tagService.CheckTag(tagsViewModel.Name));
            }
            return photoRepository.Create(photo).Id;
            
        }


        // TODO: 
        public IEnumerable<String> GetPhotoUrlsForUser(string login)
        {
            return photoRepository.GetBy(photo => photo.Owner.UserName.ToLower() == login.ToLower())
                                  .Select(photo => photo.OriginalPhoto.Url);
        }


        public PhotoCommonInfoViewModel GetPhotoCommonById(int id, string userLogin)
        {
            return new PhotoCommonInfoViewModel(photoRepository.GetById(id), userLogin, IsLiked(id, userLogin), IsDisliked(id, userLogin));
        }

        public PhotoDetailedInfoViewModel GetPhotoDetailsById(Int32 id, String login)
        {
            return new PhotoDetailedInfoViewModel(photoRepository.GetById(id), login, IsLiked(id, login), IsDisliked(id, login));
        }

        public PhotoUpdateInfoViewModel GetPhotoForEdit(Int32 id)
        {
            Photo photo = photoRepository.GetById(id);
            return conversionService.ConvertPhotoToPhotoUpdateInfoViewModel(photo);
        }

        public void DeletePhotoById(Int32 id)
        {
            var photo = photoRepository.GetBy(p => p.Id == id).Single();
            var publicId = photo.OriginalPhoto.PublicId;
            var tags = photo.Tags.ToList();
            foreach (var tag in tags)
            {
                if (tag.Count == 1)
                {
                    tagRepository.Remove(tag);
                }
                else
                {
                    tag.Count--;
                    tagRepository.Update(tag);
                }
            }
            photoRepository.RemoveById(id);
            photoUploadService.DeletePhoto(publicId);
        }

        public void UpdatePhoto(PhotoUpdateInfoViewModel updateInfoViewModel)
        {
            Photo photo = photoRepository.GetById(updateInfoViewModel.Id);
            photo.Description = updateInfoViewModel.Description;
            photo.ModifiedPhoto = updateInfoViewModel.ModifiedPhoto;
            CompareTags(photo, updateInfoViewModel.TagsViewModels.ToList());
        }

        public PhotoCommonInfoViewModel LikePhoto(Int32 photoId, String userLogin)
        {
            Photo photo = photoRepository.GetById(photoId);
            UserProfile user = userProfileRepository.GetByLogin(userLogin);
            if (IsDisliked(photoId, userLogin))
            {
                photoRepository.DeleteDislike(photo, user);
                photo.AmountOfDislikes--;
            }
            else if (!IsLiked(photoId, userLogin))
            {
                photoRepository.AddLike(photo, user);
                photo.AmountOfLikes++;
            }
            photo.CommonRaiting = photo.AmountOfLikes - photo.AmountOfDislikes;
            photoRepository.Update(photo);
            return GetPhotoCommonById(photoId, userLogin);
        }

        public PhotoCommonInfoViewModel DislikePhoto(Int32 photoId, String userLogin)
        {
            Photo photo = photoRepository.GetById(photoId);
            UserProfile user = userProfileRepository.GetByLogin(userLogin);
            if (IsLiked(photoId, userLogin))
            {
                photoRepository.DeleteLike(photo, user);
                photo.AmountOfLikes--;
            }
            else if (!IsDisliked(photoId, userLogin))
            {
                photoRepository.AddDislike(photo, user);
                photo.AmountOfDislikes++;
            }
            photo.CommonRaiting = photo.AmountOfLikes - photo.AmountOfDislikes;
            photoRepository.Update(photo);
            return GetPhotoCommonById(photoId, userLogin);
        }

        private void CompareTags(Photo photo, IEnumerable<TagSimpleViewModel> newTagSimpleViewModels)
        {
            var newTags = new List<Tag>();
            foreach (var newTagSimpleViewModel in newTagSimpleViewModels)
            {
                var currentTag = tagRepository.GetByName(newTagSimpleViewModel.Name);
                if (currentTag == null)
                {
                    tagRepository.Create(new Tag()
                        {
                            Name = newTagSimpleViewModel.Name,
                            Count = 1
                        });
                    photo.Tags.Add(tagRepository.GetByName(newTagSimpleViewModel.Name));
                    newTags.Add(tagRepository.GetByName(newTagSimpleViewModel.Name));
                } 
                else if (!photo.Tags.Contains(currentTag))
                {
                    currentTag.Count++;
                    photo.Tags.Add(currentTag);
                    newTags.Add(currentTag);
                }
                else
                {
                    newTags.Add(currentTag);
                }
            }
            var excepts = photo.Tags.Except(newTags.AsEnumerable()).ToList();
            foreach (var except in excepts)
            {
                if (tagRepository.GetByName(except.Name).Count == 1)
                {
                    tagRepository.Remove(except);
                }
                else
                {
                    tagRepository.GetByName(except.Name).Count--;
                    tagRepository.Update(except);
                }
            }
            photo.Tags.Clear();
            foreach (var newTag in newTags)
            {
                photo.Tags.Add(newTag);
            }
            photoRepository.Update(photo);
        }

        internal Boolean IsLiked(Int32 photoId, String userLogin)
        {
            if (userLogin.Length != 0)
            {
                var userProfile = userProfileRepository.GetByLogin(userLogin);
                return photoRepository.IsLiked(photoId, userProfile.Id);
            }
            return false;
        }

        internal Boolean IsDisliked(Int32 photoId, String userLogin)
        {
            if (userLogin.Length != 0)
            {
                var userProfile = userProfileRepository.GetByLogin(userLogin);
                return photoRepository.IsDisliked(photoId, userProfile.Id);
            }
            return false;
        }
    }
}