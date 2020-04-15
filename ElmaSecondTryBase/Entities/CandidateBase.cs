using ElmaSecondTryBase.Enums;
//using ElmaSecondTryBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Entities
{
    public class CandidateBase : AnnouncementBase
    {
        public override AnnouncementType AnnType { get; } = AnnouncementType.Candidate;
    }
}
