using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    public class VacancyBase : IAnnouncement
    {
        private bool _isOpen;
        private DateTime _validDay;
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
        /// Тип объявления
        /// </summary>
        public virtual AnnouncementType Type { get; set; }
        /// <summary>
        /// Объявление заблокировано
        /// </summary>
        public virtual bool IsBlocked { get; set; }
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
        public virtual bool IsOpen
        {
            get
            {
                return DateTime.Now < ValidDay;
            }
            set 
            {}
        } 
        /// <summary>
        /// Дата до которой объявление действительно
        /// </summary>
        public virtual DateTime ValidDay 
        {
            get { return _validDay; }
            set 
            {
                _validDay = value;
                _isOpen = DateTime.Now < value; 
            } 
        }
        /// <summary>
        /// Зарплата
        /// </summary>
        public virtual int Salary { get; set; }
    }
}
