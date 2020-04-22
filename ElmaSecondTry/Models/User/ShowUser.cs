using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.User
{
    public class ShowUser : BaseUser
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Display(Name="Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        [Display(Name="Дата рег.")]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        [Display(Name="Объявления пользователя")]
        public IEnumerable<Announcement> Announcements { get; set; }
    }
}