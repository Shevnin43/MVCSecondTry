using ElmaSecondTry.Models.UserModel;
using ElmaSecondTryBase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.VacancyModel
{
    public class MyVacancy : MyAnnouncement
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
        [Display(Name="Занятость")]
        public TimeJob Employment { get; set; }
        /// <summary>
        /// Требования к кандидату
        /// </summary>
        [Display(Name="Требования к кандидату")]
        public string Requirement { get; set; }
        /// <summary>
        /// Флаг доступности/открытости вакансии
        /// </summary>
        [Display(Name="Вакансия открыта")]
        public bool IsOpen { get; set; }
        /// <summary>
        /// Дата до которой объявление действительно
        /// </summary>
        [Display(Name = "Срок актуальности")]
        public DateTime ValidDay { get; set; }
        /// <summary>
        /// Зарплата
        /// </summary>
        [Display(Name = "Заработная плата")]
        public int Salary { get; set; }
    }
}