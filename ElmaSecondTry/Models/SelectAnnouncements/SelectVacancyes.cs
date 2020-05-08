using ElmaSecondTryBase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.SelectAnnouncements
{
    public class SelectVacancyes
    {
        /// <summary>
        /// Наименование вакансии
        /// </summary>
        [Display(Name = "Должность")]
        public string Name { get; set; }
        /// <summary>
        /// Описание вакансии
        /// </summary>
        [Display(Name = "Описание")]
        public string About { get; set; }
        /// <summary>
        /// Занятость
        /// </summary>
        [Display(Name = "Занятость")]
        public TimeJob Employment { get; set; } = TimeJob.All;
        /// <summary>
        /// Требования к кандидату
        /// </summary>
        [Display(Name = "Требования")]
        public string Requirement { get; set; }
        /// <summary>
        /// Флаг доступности/открытости вакансии
        /// </summary>
        [Display(Name = "Вакансия открыта")]
        public bool? IsOpen { get; set; }
        /// <summary>
        /// Зарплата минимальное значение
        /// </summary>
        [Display(Name = "Минимальная заработная плата")]
        public int SalaryMin { get; set; }
        /// <summary>
        /// Зарплата максимальное значение
        /// </summary>
        [Display(Name ="Максимальная заработная плата")]
        public int SalaryMax { get; set; } = 1000000;

    }
}