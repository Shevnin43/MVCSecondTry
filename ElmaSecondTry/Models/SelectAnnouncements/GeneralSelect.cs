using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.SelectAnnouncements
{
    public class GeneralSelect
    {
        private SelectVacancyes _selectVacancyes;
        private SelectCandidates _selectCandidates;
        private bool _canSelectVacancyes;
        private bool _canSelectCandidates;

        /// <summary>
        /// Минимальное значение даты и времени подачи объявления
        /// </summary>
        [Display(Name = "Дата подачи объявления от")]
        public DateTime CreationDateMin { get; set; } = DateTime.Parse("01.01.2020");
        /// <summary>
        /// Максимальное значение даты и времени подачи объявления
        /// </summary>
        [Display(Name = "Дата подачи объявления до")]
        public DateTime CreationDateMax { get; set; } = DateTime.Now;
        /// <summary>
        /// Минимальное значение даты и времени редактирования объявления
        /// </summary>
        [Display(Name = "Дата последних изменений от")]
        public DateTime LastEditedMin { get; set; } = DateTime.Parse("01.01.2020");
        /// <summary>
        /// Максимально значение даты ивремени редактирования объявления
        /// </summary>
        [Display(Name = "Дата последних изменений до")]
        public DateTime LastEditedMax { get; set; } = DateTime.Now;
        /// <summary>
        /// Фильтрация среди объявлений включая заблокированные
        /// </summary>
        [Display(Name = "Статус объявления")]
        public bool? IsBlocked { get; set; }
        /// <summary>
        /// Признак возможности выборки среди объявлений Вакансий
        /// </summary>
        public bool CanSelectVacancyes
        {
            get
            {
                return _canSelectVacancyes; 
            }
            set
            {
                _canSelectVacancyes = value;
                if (!value)
                {
                    _selectVacancyes = null;
                }
            }
        }
        /// <summary>
        /// Признак возможности выборки среди объявлений Кандидатов
        /// </summary>
        public bool CanSelectCandidates 
        {
            get
            {
                return _canSelectCandidates;
            }
            set
            {
                _canSelectCandidates = value;
                if (!value)
                {
                    _selectCandidates = null;
                }
            }
        }
        /// <summary>
        /// Параметры выборки среди Вакансий
        /// </summary>
        public SelectVacancyes SelectVacancyes
        {
            get
            {
                return CanSelectVacancyes ? _selectVacancyes : null ; 
            }
            set
            { 
                _selectVacancyes = CanSelectVacancyes ? value : null; 
            }
        }
        /// <summary>
        /// Параметры выборки среди Кандидатов
        /// </summary>
        public SelectCandidates SelectCandidates
        {
            get
            {
                return CanSelectCandidates ? _selectCandidates : null;
            }
            set
            {
                _selectCandidates = CanSelectCandidates ? value : null;
            }
        }

        /// <summary>
        /// Фильтрация среди объявлений включая заблокированные
        /// </summary>
        [Display(Name = "Включить параметры выборки Вакансий в итоговый фильтр")]
        public bool IncludeVacancyes { get; set; }

        /// <summary>
        /// Фильтрация среди объявлений включая заблокированные
        /// </summary>
        [Display(Name = "Включить параметры выборки Кандидатов в итоговый фильтр")]
        public bool IncludeCandidates { get; set; }
    }
}