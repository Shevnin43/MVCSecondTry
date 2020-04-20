using ElmaSecondTryBase.Enums;
//using ElmaSecondTryBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    public class CandidateBase : AnnouncementBase
    {
        public override Guid Id { get; set; }
        public string First { get; set; }
    }
}
