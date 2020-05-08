using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElmaSecondTry.Models.UserModel
{
    /// <summary>
    /// Базовый абстрактный класс моедли пользователя
    /// </summary>
    public abstract class MyUser
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Display(Name = "Логин")]
        public string Login { get; set; }
        /// <summary>
        /// Ай-ди пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name="Наименование")]
        public string Name { get; set; }
        /// <summary>
        /// Неубиваемый пользователь
        /// </summary>
        public bool NotKill { get; }
        /// <summary>
        /// Роль 
        /// </summary>
        [Display(Name="Роль")]
        [Required]
        public UserRoles Role { get; set; }
        /// <summary>
        /// Описание 
        /// </summary>
        [Display(Name= "Описание")]
        public string About { get; set; }
        /// <summary>
        /// Телефон 
        /// </summary>
        [Display(Name = "Телефон")]
        [RegularExpression(@"[\d+]{0,1}[0-9]{5,11}", ErrorMessage = "Некорректный номер")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Номер должен содержать от 6 до 12 символов")]
        public virtual string Phone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Display(Name = "Эл. почта")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public virtual string Email { get; set; }
    }
}