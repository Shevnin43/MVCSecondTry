using ElmaSecondTryBase.Enums;
using System.Collections.Generic;

namespace ElmaSecondTry.Helpers
{
    /// <summary>
    /// Справочник всякой слегка полезной информации
    /// </summary>
    public static class General
    {

        /// <summary>
        /// Список ролей, доступных для установки пользователю администратором
        /// </summary>
        public static readonly IEnumerable<UserRoles> RolesForUserByAdmin = new List<UserRoles>
        {
            UserRoles.Admin,
            UserRoles.Employee,
            UserRoles.HR,
            UserRoles.Jobseeker,
            UserRoles.None
        };

        /// <summary>
        /// Список ролей, доступных для установки пользователю самим пользователем
        /// </summary>
        public static readonly IEnumerable<UserRoles> RolesForUserBySelf = new List<UserRoles>
        {
            UserRoles.Employee,
            UserRoles.HR,
            UserRoles.Jobseeker,
            UserRoles.None
        };

        /// <summary>
        /// Список ролей, доступных для фильтрации пользователей
        /// </summary>
        public static readonly IEnumerable<UserRoles> RolesForFilter = new List<UserRoles>
        {
            UserRoles.Admin,
            UserRoles.Employee,
            UserRoles.HR,
            UserRoles.Jobseeker,
            UserRoles.None,
            UserRoles.All
        };

        /// <summary>
        /// Отображаемые названия для ролей пользователей
        /// </summary>
        public static readonly Dictionary<UserRoles, string> RolesShownNames = new Dictionary<UserRoles, string>
        {
            [UserRoles.Admin] = "Администратор",
            [UserRoles.All] = "Все пользователи",
            [UserRoles.Employee] = "Работодатель",
            [UserRoles.HR] = "HR-менеджер",
            [UserRoles.Jobseeker] = "Соискатель",
            [UserRoles.None] = "Неопределенный пользователь"
        };

        /// <summary>
        /// Отображаемые названия параметра "Занятость" вакансии
        /// </summary>
        public static readonly Dictionary<TimeJob, string> Employments = new Dictionary<TimeJob, string>
        {
            [TimeJob.FullTime] = "Полная занятость",
            [TimeJob.Internship] = "Стажировка",
            [TimeJob.None] = "Не указано",
            [TimeJob.Partial] = "Частичная",
            [TimeJob.Remote] = "Удаленная работа",
            [TimeJob.Watch] = "Вахта",
            [TimeJob.All] = "Все варианты"
        };

        /// <summary>
        /// Список свойств модели GeneralSelect по которым не надо делать выборку из базы данных
        /// </summary>
        public static readonly string[] NotFilteredProperties = new string[] 
        { 
            "CanSelectVacancyes", 
            "CanSelectCandidates", 
            "SelectVacancyes", 
            "SelectCandidates", 
            "IncludeVacancyes", 
            "IncludeCandidates",
            "AgeMin",
            "AgeMax"
        };

        /// <summary>
        /// Список свойств модели GeneralSelect которые имеют тип bool? нужны только для обхода валидации моделей на стороне сервера, 
        /// которая, почему то считает модель не валидной если bool? == null
        /// </summary>
        public static readonly string[] NulableBoolPropertyes = new string[] { "SelectVacancyes.IsOpen", "IsBlocked" };
    }
}