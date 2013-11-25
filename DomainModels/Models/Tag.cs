using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Models
{
    public class Tag : BaseEntity
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public Int32 Count { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

    }
}
