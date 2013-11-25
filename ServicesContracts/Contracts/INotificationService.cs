using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainViewModels.ViewModels;

namespace ServicesContracts.Contracts
{
    interface INotificationService
    {
        UserInfoViewModel GetUserByEmail(String email);
        bool IsUserBlocked(String email);
        void RegisterUser(String email, String userName);
    }
}
