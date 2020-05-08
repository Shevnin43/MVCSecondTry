using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;

namespace ElmaSecondTryBase.Entities
{
    public class UserBase
    {
        /// <summary>
        /// Наименование пользователя
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public virtual UserRoles Role { get; set; }
        /// <summary>
        /// Запрет на удаление пользователя
        /// </summary>
        public virtual bool NotKill { get; set; }
        /// <summary>
        /// Описание пользователя
        /// </summary>
        public virtual string About { get; set; }
        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// Контактный Email пользователя
        /// </summary>
        public virtual string Email { get; set; }
        /// <summary>
        /// Ай-ди пользователя
        /// </summary>
        public virtual Guid Id { get; set; } 
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public virtual string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary>
        /// Список объявлений пользователя
        /// </summary>
        public virtual ICollection<IAnnouncement> Announcements { get; set; }
        /// <summary>
        /// Дата регистрации пользователя
        /// </summary>
        public virtual DateTime RegisterDate { get; set; }
       
    }
}