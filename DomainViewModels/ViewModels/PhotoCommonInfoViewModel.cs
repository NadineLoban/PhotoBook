using System;
using System.Collections;
using DomainModels.Models;

namespace DomainViewModels.ViewModels
{
    public class PhotoCommonInfoViewModel
    {
        public Int32 PhotoId { get; set; }
        public String OwnerOfPhotoName { get; set; }
        public String CurrentUserName { get; set; }
        public String PhotoUrl { get; set; }
        public Int32 AmountOfLikes { get; set; }
        public Int32 AmountOfDislikes { get; set; }
        public Int32 CommonRaiting { get; set; }
        public Boolean IsLiked { get; set; }
        public Boolean IsDisliked { get; set; }

        public PhotoCommonInfoViewModel()
        {
            
        }

        public PhotoCommonInfoViewModel(Photo photo, String currentUserName, Boolean isLiked, Boolean isDisliked)
        {
            PhotoId = photo.Id;
            OwnerOfPhotoName = photo.Owner.UserName;
            CurrentUserName = currentUserName;
            PhotoUrl = photo.ModifiedPhoto ?? photo.OriginalPhoto.Url;
            AmountOfLikes = photo.AmountOfLikes;
            AmountOfDislikes = photo.AmountOfDislikes;
            CommonRaiting = photo.CommonRaiting;
            IsLiked = isLiked;
            IsDisliked = isDisliked;
        }

        public PhotoCommonInfoViewModel(Photo photo)
        {
            PhotoId = photo.Id;
            OwnerOfPhotoName = photo.Owner.UserName;
            PhotoUrl = photo.ModifiedPhoto ?? photo.OriginalPhoto.Url;
            AmountOfLikes = photo.AmountOfLikes;
            AmountOfDislikes = photo.AmountOfDislikes;
            CommonRaiting = photo.CommonRaiting;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
