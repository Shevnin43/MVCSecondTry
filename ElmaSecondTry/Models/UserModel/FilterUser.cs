using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.UserModel
{
    /// <summary>
    /// Модель для фильтрации пользователей
    /// </summary>
    public class FilterUser : MyUser
    {
        /// <summary>
        /// Период регистрации
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd.MM.yyyy HH:mm}")]
        public DateTime MinRegisterDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd.MM.yyyy HH:mm}")]
        public DateTime MaxRegisterDate { get; set; }

        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        [Display(Name = "Контактный телефон")]
        [RegularExpression(@"[0-9]{0,12}", ErrorMessage = "Некорректный номер")]
        [StringLength(12, ErrorMessage = "Номер может содержать до 12 символов")]
        public override string Phone { get; set; }

        /// <summary>
        /// Контактный Email пользователя
        /// </summary>
        [Display(Name = "Электронная почта")]
        [RegularExpression(@"[A-Za-z0-9.&#64;]{0,30}", ErrorMessage = "В поле присутствуют некорректные символы")]
        public override string Email { get; set; }
    }
}