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
        /// Тип объявления - Вакансия
        /// </summary>
        public override AnnouncementType AnnType { get; } = AnnouncementType.Vacancy;

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
        /// Соц.пакет
        /// </summary>
        public bool SocialPackage { get; set; }
    }
}