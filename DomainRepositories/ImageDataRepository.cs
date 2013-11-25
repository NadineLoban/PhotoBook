using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Models;

namespace DomainRepositories
{
    public class ImageDataRepository : Repository<ImageData>
    {
        public override IQueryable<ImageData> GetAll()
        {
            return PhotoBookContext.ImageDatas.ToList().AsQueryable();
        }

        public override ImageData GetById(int id)
        {
            return PhotoBookContext.ImageDatas.Find(id);
        }

        public override ImageData Create(ImageData entity)
        {
            var imageData = PhotoBookContext.ImageDatas.Add(entity);
            PhotoBookContext.SaveChanges();
            return imageData;
        }

        public override void Update(ImageData entity)
        {
            PhotoBookContext.Entry(entity).State = EntityState.Modified;
            PhotoBookContext.SaveChanges();
        }

        public override void RemoveById(int id)
        {
            ImageData imageData = PhotoBookContext.ImageDatas.Find(id);
            PhotoBookContext.ImageDatas.Remove(imageData);
            PhotoBookContext.SaveChanges();
        }

        public override void Remove(ImageData entity)
        {
            PhotoBookContext.ImageDatas.Remove(entity);
            PhotoBookContext.SaveChanges();
        }
    }
}
