using System;
using System.Data.Entity;
using System.Linq;
using DomainModels.Models;
using DomainRepositories.Contracts;

namespace DomainRepositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public override IQueryable<Tag> GetAll()
        {
            return PhotoBookContext.Tags.ToList().OrderByDescending(c => c.Count).AsQueryable();
        }

        public override Tag GetById(int id)
        {
            return PhotoBookContext.Tags.Find(id);
        }

        public override Tag Create(Tag entity)
        {
            Tag tag = PhotoBookContext.Tags.Add(entity);
            PhotoBookContext.SaveChanges();
            return tag;
        }

        public override void Update(Tag entity)
        {
            PhotoBookContext.Entry(entity).State = EntityState.Modified;
            PhotoBookContext.SaveChanges();
        }

        public override void Remove(Tag entity)
        {
            PhotoBookContext.Tags.Remove(entity);
            PhotoBookContext.SaveChanges();
        }

        public override void RemoveById(int id)
        {
            var tag = PhotoBookContext.Tags.Find(id);
            PhotoBookContext.Tags.Remove(tag);
            PhotoBookContext.SaveChanges();
        }

        public Tag GetByName(string name)
        {
            return PhotoBookContext.Tags.SingleOrDefault(tag => tag.Name == name);   
        }

    }
}
