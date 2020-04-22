using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    public class VacancyBase : Announcement
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
        /// Наименование вакансии
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Описание вакансии
        /// </summary>
        public virtual string About { get; set; }
        /// <summary>
        /// Занятость
        /// </summary>
        public virtual TimeJob Employment { get; set; }
        /// <summary>
        /// Требования к кандидату
        /// </summary>
        public virtual string Requirement { get; set; }
        /// <summary>
        /// Флаг доступности/открытости вакансии
        /// </summary>
        public virtual bool IsOpen { get; set; }
        /// <summary>
        /// Дата до которой объявление действительно
        /// </summary>
        public virtual DateTime ValidDay { get; set; }
        /// <summary>
        /// Зарплата
        /// </summary>
        public virtual int Salary { get; set; }
    }
}
