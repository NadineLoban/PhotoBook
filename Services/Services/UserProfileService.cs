using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using DomainModels.Models;
using DomainRepositories;
using DomainViewModels.ViewModels;
using ServicesContracts.Contracts;

namespace Services.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserProfileRepository userProfileRepository = new UserProfileRepository();

        public List<UserInfoViewModel> GetUserProfiles()
        {
            var infoViewModels = new List<UserInfoViewModel>();
            foreach (var user in userProfileRepository.GetAll())
            {
                infoViewModels.Add(new UserInfoViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email
                    });
            }
            
            return infoViewModels;
        }

        public List<UserProfile> FindUserByName(String login)
        {
            return userProfileRepository.GetBy(user => user.UserName.StartsWith(login)).ToList();
        }

        public Boolean IsBlocked(String login)
        {
            return userProfileRepository.GetByLogin(login).IsBlocked;
        }
    }
}
