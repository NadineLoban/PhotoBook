using System;
using System.Collections.Generic;
using DomainModels.Models;

namespace DomainRepositories.Contracts
{
    interface IUserProfileRepository
    {
        List<UserProfile> FindUserByName(String login);
        UserProfile GetByLogin(String login);
        UserProfile GetByEmail(String email);
        void ConfirmRegistration(String email);

    }
}
