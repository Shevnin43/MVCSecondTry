using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.AccountModel
{
    public class EditAccount : MyAccount
    {
        /// <summary>
        /// Ай-ди пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Новый логин")]
        public string NewLogin { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Минимальная длина пароля - не менее 8 символов.")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Подтверждение пароля пользователем
        /// </summary>
        [Display(Name = "Подтверждение нового пароля")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Новые пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
    }
}