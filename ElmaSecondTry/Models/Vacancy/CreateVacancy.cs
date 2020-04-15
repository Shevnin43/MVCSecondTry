﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.Vacancy
{
    public class CreateVacancy : BaseVacancy
    {
        /// <summary>
        /// Срок актуальности ваканчии
        /// </summary>
        [Required]
        public DateTime ValidDay { get; set; }

        /// <summary>
        /// Заработная плата
        /// </summary>
        [Display(Name = "Заработная плата")]
        [RegularExpression(@"[\d]{3,6}", ErrorMessage = "Некорректная сумма")]
        public int Salary { get; set; }
    }
}