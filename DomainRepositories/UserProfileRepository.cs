using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomainModels.Models;
using DomainRepositories.Contracts;

namespace DomainRepositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {

        public List<UserProfile> FindUserByName(String login)
        {
            return this.GetBy(user => user.UserName.StartsWith(login)).ToList();
        }

        public UserProfile GetByLogin(string login)
        {
            return PhotoBookContext.UserProfiles.SingleOrDefault(u => u.UserName == login);
        }

        public UserProfile GetByEmail(string email)
        {
            return PhotoBookContext.UserProfiles.SingleOrDefault(u => u.Email == email);
        }
        
        public void ConfirmRegistration(String email)
        {
            UserProfile user = GetByEmail(email);
            user.IsBlocked = false;
            this.Update(user);
        }
        
        public override void Update(UserProfile entity)
        {
            PhotoBookContext.Entry(entity).State = EntityState.Modified;
            PhotoBookContext.SaveChanges();
        }

        public override IQueryable<UserProfile> GetAll()
        {
            return PhotoBookContext.UserProfiles.AsQueryable();
        }

        public override UserProfile GetById(int id)
        {
            return PhotoBookContext.UserProfiles.Find(id);
        }

        public override UserProfile Create(UserProfile entity)
        {
            throw new NotImplementedException();
        }
        
        public override void Remove(UserProfile entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoveById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
