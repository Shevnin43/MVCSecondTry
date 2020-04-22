using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.Account
{
    public class EditAccount : AccountBase
    {
        /// <summary>
        /// Ай-ди пользователя
        /// </summary>
        public Guid Id { get; set; }

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