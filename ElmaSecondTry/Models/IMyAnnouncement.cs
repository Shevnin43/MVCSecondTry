using ElmaSecondTryBase.Enums;
using System;

namespace ElmaSecondTry.Models
{
    /// <summary>
    /// Интерфейс объявлений. В принципе завел его только для того чтобы выводить информацию об объявлениях в одном окне с применением Partial
    /// </summary>
    public interface IMyAnnouncement
    {
        /// <summary>
        /// Ай-ди объявления
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// Тип объявления
        /// </summary>
        AnnouncementType Type { get; set; }
        /// <summary>
        /// Объявление заблокировано
        /// </summary>
        bool IsBlocked { get; set; }
        /// <summary>
        /// Логин владельца
        /// </summary>
        string OwnerLogin { get; set; }
        /// <summary>
        /// Логин редактора
        /// </summary>
        string EditorLogin { get; set; }
        /// <summary>
        /// Дата создания вакансии
        /// </summary>
        DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата последних изменений
        /// </summary>
        DateTime LastEdited { get; set; }
    }
}
