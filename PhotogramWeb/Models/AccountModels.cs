using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;
using Resources;

namespace PhotogramWeb.Models
{
    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(ResourceType = typeof (UIStrings), Name = "UserName")]
        public string UserName { get; set; }
        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (UIStrings), Name = "CurrentPassword")]
        public string OldPassword { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (UIStrings_en_EN), ErrorMessageResourceName = "PasswordValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (UIStrings), Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (UIStrings), Name = "ConfirmNewPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (UIStrings_en_EN), ErrorMessageResourceName = "NewPasswordAndConfirmationPasswordDontMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(ResourceType = typeof (UIStrings), Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (UIStrings), Name = "Password")]
        public string Password { get; set; }
        
        [Display(ResourceType = typeof (UIStrings), Name = "RememberMe")]
        public bool RememberMe { get; set; }

        
    }

    public class RegisterModel
    {
        [Required]
        [Display(ResourceType = typeof (UIStrings), Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(ResourceType = typeof (UIStrings), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (UIStrings_en_EN), ErrorMessageResourceName = "PasswordValidation", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (UIStrings), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (UIStrings), Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof (UIStrings), ErrorMessageResourceName = "PasswordAndConfirmationDontMatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "IsBlocked")]
        public bool IsBlocked { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
