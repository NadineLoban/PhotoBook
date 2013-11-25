using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Models;

namespace DomainViewModels.ViewModels
{
    public class PhotoSlideShowViewModel
    {
        public String PhotoUrl { get; set; }
        public Int32 PhotoId { get; set; }
        public String OwnerLogin { get; set; }
        public Int32 CommonRaiting { get; set; }

        public PhotoSlideShowViewModel(Photo photo)
        {
            PhotoUrl = photo.ModifiedPhoto ?? photo.OriginalPhoto.Url;
            PhotoId = photo.Id;
            OwnerLogin = photo.Owner.UserName;
            CommonRaiting = photo.CommonRaiting;
        }

        public PhotoSlideShowViewModel(PhotoCommonInfoViewModel photo)
        {
            PhotoUrl = photo.PhotoUrl;
            PhotoId = photo.PhotoId;
            OwnerLogin = photo.OwnerOfPhotoName;
            CommonRaiting = photo.CommonRaiting;
        }
    }
}
