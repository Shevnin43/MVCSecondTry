using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.Account
{
    /// <summary>
    /// Класс формы авторизации
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Display(Name ="Логин")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display (Name ="Пароль")]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}