using System;
using System.Collections.Generic;
using DomainModels.Models;
using DomainViewModels.ViewModels;

namespace ServicesContracts.Contracts
{
    public interface ITagService
    {
        List<Tag> FindTagByName(String term);
        List<PhotoCommonInfoViewModel> GetPhotosByTag(String name, String login);
        List<TagViewModel> GetAllTags();
        void AddTag(TagAddViewModel tagAddViewModel);
        IEnumerable<Tag> GetTopTags(int count);
    }
}
