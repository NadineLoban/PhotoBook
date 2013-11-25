using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Models;

namespace DomainViewModels.ViewModels
{
    public class TagAddViewModel
    {
        public String Name { get; set; }

        public TagAddViewModel()
        {
            
        }

        public TagAddViewModel(String name)
        {
            Name = name;
        }
    }
}
