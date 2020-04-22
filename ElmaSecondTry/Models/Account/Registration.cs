using ElmaSecondTryBase.Enums;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.Account
{
    /// <summary>
    /// Модель регистрации пользователя
    /// </summary>
    public class Registration : AccountBase
    {
        /// <summary>
        /// Подтверждение пароля пользователем
        /// </summary>
        [Display(Name = "Подтверждение пароля")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }
}