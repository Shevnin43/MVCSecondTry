using ElmaSecondTryBase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElmaSecondTry.Models
{
    public abstract class Announcement
    {
        public abstract AnnouncementType AnnType { get; }
    }
}