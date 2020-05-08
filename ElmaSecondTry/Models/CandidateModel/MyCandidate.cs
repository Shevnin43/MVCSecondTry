using ElmaSecondTry.Models.UserModel;
using ElmaSecondTryBase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.CandidateModel
{
    /// <summary>
    /// Класс модели кандидата
    /// </summary>
    public class MyCandidate : MyAnnouncement
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
        /// Дата рождения
        /// </summary>
        [Display(Name = "Дата рождения")]
        [Required]
        public DateTime BirthDay { get; set; }
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
        /// Фото
        /// </summary>
        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }
        /// <summary>
        /// ПРофессия
        /// </summary>
        [Display(Name = "Искомая должность")]
        public string Profession { get; set; }

    }
}