using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using DomainModels.Models;
using DomainRepositories;
using DomainViewModels.ViewModels;

namespace Services.Services
{
    public class NotificationService
    {
        private readonly UserProfileRepository userProfileRepository = new UserProfileRepository();
        private SmtpClient smtpClient;

        public UserInfoViewModel GetUserByEmail(String email)
        {
            UserProfile userProfile = userProfileRepository.GetByEmail(email);
            var user = new UserInfoViewModel(userProfile);
            return user;
        }

        public bool IsUserBlocked(String email)
        {
            return userProfileRepository.GetByEmail(email).IsBlocked;
        }

        public void RegisterUser(String email, String userName)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine(String.Format("Dear, {0}. Please confirm your registration", userName));
            message.AppendLine(String.Format(@"http://localhost:5257/Account/ConfirmRegistration?email={0}", email));
            SendEmail(message.ToString(), "Подтверждение регистрации", email);
        }

        public void ConfirmationRegistration(String email)
        {
            userProfileRepository.ConfirmRegistration(email);
        }

        private void InitSmtpServer()
        {
            smtpClient = new SmtpClient("smtp.gmail.com", 587) //587  smtp.aol.com
            {
                Credentials = new NetworkCredential("site.photobook@gmail.com", "photobook"),
                EnableSsl = true
            };
        }

        private void SendEmail(String message, String theme, String email)
        {
            if (smtpClient == null)
            {
                InitSmtpServer();
            }
            smtpClient.Send("site.photobook@gmail.com", email, theme, message);
        }
    }
}
