using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ElmaSecondTry.Models.User
{
    public class EditUser : BaseUser
    {

        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        public IEnumerable<Announcement> Announcements { get; set; }
        
        /// <summary>
        /// Список доступных ролей для редактирования пользователя
        /// </summary>
        public IEnumerable<SelectListItem> AvailableRoles { get; set; }
    }
}