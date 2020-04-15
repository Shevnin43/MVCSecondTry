using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.User
{
    public class TempUser : BaseUser
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
        /// Дата регистрации пользователя
        /// </summary>
        [Required]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        public IEnumerable<Announcement> Announcements { get; set; } = new List<Announcement>();
    }
}