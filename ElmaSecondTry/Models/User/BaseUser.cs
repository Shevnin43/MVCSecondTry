using ElmaSecondTry.WebEnums;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.User
{
    public abstract class BaseUser
    {
        /// <summary>
        /// Наименование пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Required]
        public WebUserRoles Role { get; set; }

        /// <summary>
        /// Описание пользователя
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        [RegularExpression(@"[\d+]{0,1}[0-9]{5,11}", ErrorMessage = "Некорректный номер")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Номер должен содержать от 6 до 12 символов")]
        public string Phone { get; set; }

        /// <summary>
        /// Контактный Email пользователя
        /// </summary>
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
    }
}