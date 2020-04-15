using System;

namespace ElmaSecondTry.Models.User
{
    /// <summary>
    /// Модель для фильтрации пользователей
    /// </summary>
    public class FilterUsers : BaseUser
    {
        /// <summary>
        /// Период регистрации
        /// </summary>
        public (DateTime Min, DateTime Max) Registered { get; set; }

    }
}