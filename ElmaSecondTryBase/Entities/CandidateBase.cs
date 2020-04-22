using ElmaSecondTryBase.Enums;
//using ElmaSecondTryBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    public class CandidateBase : Announcement
    {
        /*
        /// <summary>
        /// Ай-ди объявления
        /// </summary>
        public virtual Guid Id { get; set; }
        /// <summary>
        /// Создатель (Компания, Employee)
        /// </summary>
        public virtual UserBase Creator { get; set; }
        /// <summary>
        /// Дата создания вакансии
        /// </summary>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата последних изменений
        /// </summary>
        public virtual DateTime LastEdited { get; set; }
        /// <summary>
        /// Пользователь, вносивший последние изменения
        /// </summary>
        public virtual UserBase LastEditor { get; set; }
        */
        /// <summary>
        /// Имя
        /// </summary>
        public virtual string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public virtual string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public virtual string Patronymic { get; set; }
    }
}
