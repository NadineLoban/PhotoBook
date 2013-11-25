using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Models
{
    public class UserProfile : BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Boolean IsBlocked { get; set; }

        public virtual ICollection<Photo> UserLikes { get; set; }
        public virtual ICollection<Photo> UserDislikes { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}