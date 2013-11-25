using System;
using System.Collections.Generic;

namespace DomainModels.Models
{
    public class Photo : BaseEntity
    {
        public String Description { get; set; }
        public virtual ImageData OriginalPhoto { get; set; }
        public virtual String ModifiedPhoto { get; set; }

        public virtual Int32 AmountOfLikes { get; set; }
        public virtual Int32 AmountOfDislikes { get; set; }
        public virtual Int32 CommonRaiting { get; set; }

        public virtual UserProfile Owner { get; set; }
        public virtual ICollection<UserProfile> PhotoLikes { get; set; }
        public virtual ICollection<UserProfile> PhotoDislikes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        
    }
}
