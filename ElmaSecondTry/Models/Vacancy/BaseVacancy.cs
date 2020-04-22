using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElmaSecondTry.Models.Vacancy
{
    public abstract class BaseVacancy : Announcement
    {
        /// <summary>
        /// Наименование вакансии
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Описание вакансии
        /// </summary>
        [Required]
        public string About { get; set; }
        /// <summary>
        /// Занятость
        /// </summary>
        public TimeJob Employment { get; set; }
        /// <summary>
        /// Требования к кандидату
        /// </summary>
        [Required]
        public string Requirement { get; set; }
        /// <summary>
        /// Флаг доступности/открытости вакансии
        /// </summary>
        [Required]
        public bool IsOpen { get; set; }
        /// <summary>
        /// Дата до которой объявление действительно
        /// </summary>
        public DateTime ValidDay { get; set; }
        /// <summary>
        /// Зарплата
        /// </summary>
        public int Salary { get; set; }
    }
}