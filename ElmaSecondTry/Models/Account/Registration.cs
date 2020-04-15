using ElmaSecondTry.WebEnums;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.Account
{
    /// <summary>
    /// Модель регистрации пользователя
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Логин регистрируемого пользователя
        /// </summary>
        [Display(Name ="Логин")]
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль регистрируемого пользователя
        /// </summary>
        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля пользователем
        /// </summary>
        [Display(Name = "Подтверждение пароля")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Роль регистрируемого пользователя
        /// </summary>
        [Display(Name = "Роль")]
        [Required]
        public WebUserRoles Role {get; set;}
    }
}