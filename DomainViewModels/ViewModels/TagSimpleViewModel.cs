using System;
using DomainModels.Models;

namespace DomainViewModels.ViewModels
{
    public class TagSimpleViewModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public TagSimpleViewModel()
        {
            
        }

        public TagSimpleViewModel(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }

        public TagSimpleViewModel(String tagName)
        {
            Name = tagName;
        }
    }
}
