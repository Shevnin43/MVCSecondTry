using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElmaSecondTryBase.Entities
{
    public abstract class AnnouncementBase
    {
        public abstract AnnouncementType AnnType { get; }
    }
}