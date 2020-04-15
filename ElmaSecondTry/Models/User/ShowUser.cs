using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.User
{
    public class ShowUser : BaseUser
    {
        /// <summary>
        /// Ай-ди пользователя
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        public IEnumerable<Announcement> Announcements { get; set; }
    }
}