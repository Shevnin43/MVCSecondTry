using System.Collections.Generic;
using System.Web.Mvc;


namespace ElmaSecondTry.Models.UserModel
{
    public class EditUser : MyUser
    {

        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        public ICollection<MyAnnouncement> Announcements { get; set; }
        
    }
}