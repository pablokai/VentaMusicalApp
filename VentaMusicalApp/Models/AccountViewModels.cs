using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VentaMusicalApp.Models
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
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Número de Identificación")]
        public string NumeroIdentificacion { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El {0} debe ser al menos de  {2}", MinimumLength = 3)]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El {0} debe ser al menos de  {2}", MinimumLength = 3)]
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El {0} debe ser al menos de  {2}", MinimumLength = 3)]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El {0} debe ser al menos de  {2}", MinimumLength = 3)]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required]
        [Display(Name = "Género")]
        public string Genero { get; set; }

        [Required]
        [Display(Name = "Tipo de tarjeta")]
        public int IdTipoTarjeta { get; set; }

        [Required]
        [Display(Name = "Número de tarjeta")]
        public string NumeroTarjeta { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Rol")]
        public string Rol { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirme la contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
