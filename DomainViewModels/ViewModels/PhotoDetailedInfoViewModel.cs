using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DomainModels.Models;

namespace DomainViewModels.ViewModels
{
    public class PhotoDetailedInfoViewModel
    {
        public Int32 PhotoId { get; set; }
        public String CurrentUserLogin { get; set; }
        public String OwnerLogin { get; set; }
        public String PhotoUrl { get; set; }
        public String Description { get; set; }
        public Int32 AmountOfLikes { get; set; }
        public Int32 AmountOfDislikes { get; set; }
        public Int32 CommonRaiting { get; set; }
        public Boolean IsLiked { get; set; }
        public Boolean IsDisliked { get; set; }
        public ICollection<TagSimpleViewModel> TagSimpleViewModels { get; set; }

        public PhotoDetailedInfoViewModel()
        {
            
        }

        public PhotoDetailedInfoViewModel(Photo photo, String currentUserLogin, Boolean isLiked, Boolean isDisliked)
        {
            PhotoId = photo.Id;
            PhotoUrl = photo.ModifiedPhoto ?? photo.OriginalPhoto.Url;
            Description = photo.Description;
            AmountOfLikes = photo.AmountOfLikes;
            AmountOfDislikes = photo.AmountOfDislikes;
            CommonRaiting = photo.CommonRaiting;
            IsLiked = isLiked;
            IsDisliked = isDisliked;
            OwnerLogin = photo.Owner.UserName;
            CurrentUserLogin = currentUserLogin;
            var tags = photo.Tags;
            TagSimpleViewModels = new Collection<TagSimpleViewModel>();
            foreach (var tag in tags)
            {
                TagSimpleViewModels.Add(new TagSimpleViewModel(tag));
            }


        }
    }
}
