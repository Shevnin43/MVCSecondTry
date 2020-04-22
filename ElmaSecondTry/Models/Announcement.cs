using ElmaSecondTry.Models.User;
using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElmaSecondTry.Models
{
    public abstract class Announcement
    {
        /// <summary>
        /// Ай-ди объявления
        /// </summary>
        public Guid Id { get; set; }
    }
}