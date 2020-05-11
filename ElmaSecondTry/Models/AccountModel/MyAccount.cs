using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ElmaSecondTry.Models.AccountModel
{
    public abstract class MyAccount
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [MinLength(8, ErrorMessage = "Минимальная длина логина - не менее 8 символов.")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="Минимальная длина пароля - не менее 8 символов.")]
        public string Password { get; set; }

    }
}