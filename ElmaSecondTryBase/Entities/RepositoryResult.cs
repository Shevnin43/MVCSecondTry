using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    /// <summary>
    /// Статусы результата действия репозитория
    /// </summary>
    public enum ActionStatus
    {
        Success = 0,
        Fatal = 1,
        Error = 2,
        Warning = 3
    }

    /// <summary>
    /// Класс, возвращаемый в качестве результата всех действий репозитория
    /// </summary>
    public class RepositoryResult
    {
        /// <summary>
        /// Статус действия
        /// </summary>
        public ActionStatus Status { get; set; }
        /// <summary>
        /// Возвращаемые сущности
        /// </summary>
        public IEnumerable<object> Entity { get; set; }
        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        public string Message { get; set; }
    }
}
