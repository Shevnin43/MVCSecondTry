using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    /// <summary>
    /// Интерфейс, обязывающий все типы объявлений иметь определенные общие свойства
    /// </summary>
    public interface IAnnouncement
    {
        /// <summary>
        /// Ай-ди объявления
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// Создатель (Компания, Employee)
        /// </summary>
        UserBase Creator { get; set; }
        /// <summary>
        /// Дата создания вакансии
        /// </summary>
        DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата последних изменений
        /// </summary>
        DateTime LastEdited { get; set; }
        /// <summary>
        /// Пользователь, вносивший последние изменения
        /// </summary>
        UserBase LastEditor { get; set; }
        /// <summary>
        /// Тип объявления
        /// </summary>
        AnnouncementType Type { get; set; }
        /// <summary>
        /// Объявление заблокировано
        /// </summary>
        bool IsBlocked { get; set; }
    }
}
