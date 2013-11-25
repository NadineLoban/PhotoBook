using System;
using System.Collections.Generic;
using System.Linq;
using DomainModels.Models;
using DomainRepositories;
using DomainViewModels.ViewModels;

namespace Services.Services
{
    public class ConversionService
    {
        internal List<Tag> ConvertListTagSimpleViewModelToListTag(ICollection<TagAddViewModel> tagSimpleViewModels)
        {
            var tagRepository = new TagRepository();
            return tagSimpleViewModels.Select(tagSimpleViewModel => tagRepository.GetByName(tagSimpleViewModel.Name)).ToList();
        }
        internal List<TagSimpleViewModel> ConvertListTagToListTagSimpleViewModel(ICollection<Tag> tags)
        {
            var tagRepository = new TagRepository();
            return tags.Select(tag => new TagSimpleViewModel(tag)).ToList();
        }

        internal Photo ConvertPhotoAddInfoModelToPhoto(PhotoAddInfoViewModel addInfoViewModel)
        {
            var photo = new Photo
                {
                    Description = addInfoViewModel.Description,
//                    OriginalPhoto = addInfoViewModel.OriginalPhoto,
//                    ModifiedPhoto = addInfoViewModel.ModifiedPhoto,
                    Tags = ConvertListTagSimpleViewModelToListTag(addInfoViewModel.TagAddViewModels).ToList()
                };
            return photo;
        }

        internal PhotoUpdateInfoViewModel ConvertPhotoToPhotoUpdateInfoViewModel(Photo photo)
        {
            var photoUpdateInfoViewModel = new PhotoUpdateInfoViewModel
                {
                    Id = photo.Id,
                    Description = photo.Description,
                    TagsViewModels = ConvertListTagToListTagSimpleViewModel(photo.Tags),
                    OriginalPhoto = photo.OriginalPhoto,
                    ModifiedPhoto = photo.ModifiedPhoto
                };
            photoUpdateInfoViewModel.TagsString = "";
            foreach (var tagViewModel in photoUpdateInfoViewModel.TagsViewModels)
            {
                photoUpdateInfoViewModel.TagsString = String.Format("{0},{1}", photoUpdateInfoViewModel.TagsString, tagViewModel.Name);
            }
            return photoUpdateInfoViewModel;
        }
}
}
