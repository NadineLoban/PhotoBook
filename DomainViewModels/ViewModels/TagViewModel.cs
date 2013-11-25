using System;
using DomainModels.Models;

namespace DomainViewModels.ViewModels
{
    public class TagViewModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Int32 Count { get; set; }

        public TagViewModel()
        {
            
        }

        public TagViewModel(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
            Count = tag.Count;
        }
    }
}
