using ElmaSecondTry.Models.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.Vacancy
{
    public class ShowVacancy : BaseVacancy
    {
        /// <summary>
        /// Аф-Ди ваканcии
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Работодатель
        /// </summary>
        public ShowUser Employee { get; set; }

        /// <summary>
        /// Дата создания вакансии
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Заработная плата
        /// </summary>
        [Display(Name = "Заработная плата")]
        [RegularExpression(@"[\d]{3,6}", ErrorMessage = "Некорректная сумма")]
        public int Salary { get; set; }

        /// <summary>
        /// Дата последних изменений
        /// </summary>
        public DateTime LastEdited { get; set; }

        /// <summary>
        /// Пользователь, вносивший последние изменения
        /// </summary>
        public ShowUser LastEditer { get; set; } 
    }
}