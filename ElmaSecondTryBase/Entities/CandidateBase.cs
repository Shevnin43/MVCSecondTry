using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;

namespace ElmaSecondTryBase.Entities
{
    public class CandidateBase : IAnnouncement
    {
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
        /// <summary>
        /// О себе
        /// </summary>
        public virtual string About { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public virtual DateTime BirthDay { get; set; }
        /// <summary>
        /// Образование
        /// </summary>
        public virtual string Education { get; set; }
        /// <summary>
        /// Опыт работы
        /// </summary>
        public virtual string Experience { get; set; }
        /// <summary>
        /// Фото
        /// </summary>
        public virtual byte[] Photo { get; set; }
        /// <summary>
        /// ПРофессия
        /// </summary>
        public virtual string Profession { get; set; }
        /// <summary>
        /// Тип объявления
        /// </summary>
        public virtual AnnouncementType Type { get; set; }
        /// <summary>
        /// Объявление заблокировано
        /// </summary>
        public virtual bool IsBlocked { get; set; }
    }
}
