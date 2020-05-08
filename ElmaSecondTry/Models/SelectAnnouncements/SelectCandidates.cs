using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ElmaSecondTry.Models.SelectAnnouncements
{
    public class SelectCandidates
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        /// <summary>
        /// О себе
        /// </summary>
        [Display(Name = "О себе")]
        public string About { get; set; }
        /// <summary>
        /// Дата рождения минимальное значение
        /// </summary>
        [Display(Name = "Возраст от")]
        [Range(14,120, ErrorMessage = "Пользователю системы может быть от 14 до 120 лет." )]
        public int AgeMin { get; set; } = 14;
        /// <summary>
        /// Дата рождения максимальное значение
        /// </summary>
        [Display(Name = "до")]
        [Range(14, 120, ErrorMessage = "Пользователю системы может быть от 14 до 120 лет.")]
        public int AgeMax { get; set; } = 120;
        /// <summary>
        /// Образование
        /// </summary>
        [Display(Name = "Образование")]
        public string Education { get; set; }
        /// <summary>
        /// Опыт работы
        /// </summary>
        [Display(Name = "Опыт работы")]
        public string Experience { get; set; }
        /// <summary>
        /// ПРофессия
        /// </summary>
        [Display(Name = "Профессия")]
        public string Profession { get; set; }
    }
}