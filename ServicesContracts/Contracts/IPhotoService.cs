using System;
using System.Collections.Generic;
using System.Web;
using DomainViewModels.ViewModels;

namespace ServicesContracts.Contracts
{
    public interface IPhotoService
    {
        Int32 CreatePhoto(PhotoAddInfoViewModel photoCard);
        AfterUploadPhotoViewModel UploadPhoto(HttpPostedFileBase file);
        IEnumerable<String> GetPhotoUrlsForUser(String login);
        PhotoDetailedInfoViewModel GetPhotoDetailsById(Int32 id, String userLogin);
        PhotoCommonInfoViewModel GetPhotoCommonById(Int32 id, String userLogin);
        PhotoUpdateInfoViewModel GetPhotoForEdit(Int32 id);
        void DeletePhotoById(Int32 id);
        void UpdatePhoto(PhotoUpdateInfoViewModel model);
        IEnumerable<PhotoCommonInfoViewModel> GetPhotosByUserLogin(String login);
        IEnumerable<PhotoCommonInfoViewModel> GetPhotosByUserId(Int32 id);
        String GetUrlPhoto(String publicId, string effectName, int value);
        String GetUrlPhoto(String publicId, string effectName);
        PhotoCommonInfoViewModel LikePhoto(Int32 photoId, String userLogin);
        PhotoCommonInfoViewModel DislikePhoto(Int32 photoId, String userLogin);
        List<PhotoSlideShowViewModel> GetTheMostPopularForSlideShow();
        List<PhotoCommonInfoViewModel> GetTheMostPopular();
    }
}
