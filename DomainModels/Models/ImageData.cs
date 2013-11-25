using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Models
{
    public class ImageData : BaseEntity
    {
        public virtual DateTime CreatedAt { get; set; }
        public virtual String PublicId { get; set; }
        public virtual int Version { get; set; }
        public virtual String Signature { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual String Format { get; set; }
        public virtual String ResourceType { get; set; }
        public virtual int Bytes { get; set; }
        public virtual String Type { get; set; }
        public virtual String Url { get; set; }
        public virtual String SecureUrl { get; set; }
        public virtual String Path { get; set; }


    }
}
