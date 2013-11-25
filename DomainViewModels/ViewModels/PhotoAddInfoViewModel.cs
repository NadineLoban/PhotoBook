using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DomainViewModels.ViewModels
{
    public class PhotoAddInfoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public String Login { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Описание")]
        public String Description { get; set; }
        public HttpPostedFileBase OriginalPhoto { get; set; }
        public int OriginalPhotoId { get; set; }
        public String ModifiedPhoto { get; set; }
        public ICollection<TagAddViewModel> TagAddViewModels { get; set; }
        [Display(Name = "Теги")]
        public String TagsString { get; set; }
    }
}
