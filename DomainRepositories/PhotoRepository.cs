using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Context.Context;
using DomainModels.Models;
using DomainRepositories.Contracts;

namespace DomainRepositories
{
    public class PhotoRepository: Repository<Photo>, IPhotoRepository
    {
        public override IQueryable<Photo> GetAll()
        {
            return PhotoBookContext.Photos.AsQueryable();
        }

        public override Photo GetById(int id)
        {
            return PhotoBookContext.Photos.Find(id);
        }

        public override Photo Create(Photo entity)
        {
            Photo photo = PhotoBookContext.Photos.Add(entity);
            PhotoBookContext.SaveChanges();
            return photo;
        }

        public override void Update(Photo entity)
        {
            PhotoBookContext.Entry(entity).State = EntityState.Modified;
            PhotoBookContext.SaveChanges();
        }

        public override void RemoveById(int id)
        {
            Photo photo = PhotoBookContext.Photos.Find(id);
            PhotoBookContext.Photos.Remove(photo);
            PhotoBookContext.SaveChanges();
        }

        public override void Remove(Photo entity)
        {
            PhotoBookContext.Photos.Remove(entity);
            PhotoBookContext.SaveChanges();
        }

        public void AddLike(Photo photo, UserProfile userProfile)
        {
            photo.PhotoLikes.Add(userProfile);
            Update(photo);
        }

        public void AddDislike(Photo photo, UserProfile userProfile)
        {
            photo.PhotoDislikes.Add(userProfile);
            Update(photo);
        }

        public void DeleteLike(Photo photo, UserProfile userProfile)
        {
            photo.PhotoLikes.Remove(userProfile);
            Update(photo);
        }

        public void DeleteDislike(Photo photo, UserProfile userProfile)
        {
            photo.PhotoDislikes.Remove(userProfile);
            Update(photo);
        }

        public Boolean IsLiked(Int32 photoId, Int32 userId)
        {
            var likes = PhotoBookContext.Photos.Where(a => a.PhotoLikes.Any(f => f.Id == userId)).SingleOrDefault(b => b.Id == photoId);
            return likes != null;
        }

        public Boolean IsDisliked(Int32 photoId, Int32 userId)
        {
            var dislikes = PhotoBookContext.Photos.Where(a => a.PhotoDislikes.Any(f => f.Id == userId)).SingleOrDefault(b => b.Id == photoId);
            return dislikes != null;
        }
    }
}
