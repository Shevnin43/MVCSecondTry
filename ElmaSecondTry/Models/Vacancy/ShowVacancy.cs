using ElmaSecondTry.Models.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.Vacancy
{
    public class ShowVacancy : BaseVacancy
    {
        /// <summary>
        /// Создатель (Компания, Employee)
        /// </summary>
        public BaseUser Creator { get; set; }
        /// <summary>
        /// Дата создания вакансии
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата последних изменений
        /// </summary>
        public DateTime LastEdited { get; set; }
        /// <summary>
        /// Пользователь, вносивший последние изменения
        /// </summary>
        public BaseUser LastEditor { get; set; }
    }
}