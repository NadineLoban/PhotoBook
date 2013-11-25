using System;
using System.Collections.Generic;
using DomainModels.Models;
using DomainViewModels.ViewModels;

namespace ServicesContracts.Contracts
{
    public interface IUserProfileService
    {
        Boolean IsBlocked(String login);
        List<UserInfoViewModel> GetUserProfiles();
        List<UserProfile> FindUserByName(String login);
    }
}
