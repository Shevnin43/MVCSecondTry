using ElmaSecondTryBase.Enums;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.AccountModel
{
    /// <summary>
    /// Модель регистрации пользователя
    /// </summary>
    public class Registration : MyAccount
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