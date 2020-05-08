using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.AccountModel
{
    public abstract class MyAccount
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}