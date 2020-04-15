using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    public class VacancyBase : AnnouncementBase
    {
        public override AnnouncementType AnnType { get; } = AnnouncementType.Vacancy;
    }
}
