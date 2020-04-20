using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    public class VacancyBase : AnnouncementBase
    {
        public override Guid Id { get; set; }
        public string Second { get; set; }
    }
}
