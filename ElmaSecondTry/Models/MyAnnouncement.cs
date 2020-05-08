using ElmaSecondTryBase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models
{
    /// <summary>
    /// Базовый класс для моделей объявлений ElmaSecondTry
    /// </summary>
    public class MyAnnouncement : IMyAnnouncement
    {
        /// <summary>
        /// Ай-ди объявления
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Тип объявления
        /// </summary>
        public AnnouncementType Type { get; set; }
        /// <summary>
        /// Объявление заблокировано
        /// </summary>
        [Display(Name = "Объявление заблокировано")]
        public bool IsBlocked { get; set; }
        /// <summary>
        /// Логин владельца
        /// </summary>
        [Display(Name ="Создатель")]
        public string OwnerLogin { get; set; }
        /// <summary>
        /// Логин редактора
        /// </summary>
        [Display(Name = "Редактор")]
        public string EditorLogin { get; set; }
        /// <summary>
        /// Дата создания вакансии
        /// </summary>
        [Display(Name = "Дата подачи объявления")]
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата последних изменений
        /// </summary>
        [Display(Name = "Отредактировано")]
        public DateTime LastEdited { get; set; }
    }
}
