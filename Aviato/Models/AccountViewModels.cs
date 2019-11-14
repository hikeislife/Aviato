﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aviato.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Molimo unesite svoj email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Molimo unesite svoj email")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Molimo unesite svoj password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Upamti me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        
        public virtual Zaposleni Zaposleni { get; set; }

        [Required(ErrorMessage = "Unesite validan email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password je obavezan")]
        [StringLength(100, ErrorMessage = "{0} mora imati bar {2} karaktera", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi password")]
        [Compare("Password", ErrorMessage = "Passwordi se moraju podudarati")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage =("Rola je obavezna"))]
        public string RoleName { get; set; }

        public virtual Pilot Pilot { get; set; }

        public virtual Stjuard Stjuard { get; set; }

        public virtual Mehanicar Mehanicar { get; set; }

        public string JeziciZaUnos { get; set; }

        public string tipoviZaUnos { get; set; }

        public string datumiZaUnos { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} mora imati bar {2} karaktera", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi password")]
        [Compare("Password", ErrorMessage = "Passwordi se moraju podudarati")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Unesite validan email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
