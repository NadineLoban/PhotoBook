using System;
using DomainModels.Models;

namespace DomainViewModels.ViewModels
{
    public class UserInfoViewModel
    {
        public Int32 Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public UserInfoViewModel(UserProfile userProfile)
        {
            Id = userProfile.Id;
            UserName = userProfile.UserName;
            Email = userProfile.Email;
        }

        public UserInfoViewModel()
        {
        }
    }
}
