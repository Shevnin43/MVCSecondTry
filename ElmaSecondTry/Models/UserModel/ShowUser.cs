using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.UserModel
{
    public class ShowUser : MyUser
    {
        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        [Display(Name="Дата рег.")]
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        [Display(Name="Объявления пользователя")]
        public ICollection<MyAnnouncement> Announcements { get; set; }
    }
}