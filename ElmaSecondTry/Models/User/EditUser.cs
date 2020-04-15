using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.User
{
    public class EditUser : BaseUser
    {
        /// <summary>
        /// Ай-ди пользователя
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля пользователя
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        public IEnumerable<Announcement> Announcements { get; set; }

    }
}