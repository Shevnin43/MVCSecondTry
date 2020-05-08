using ElmaSecondTryBase.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    /// <summary>
    /// Маппинг модели CandidateBase на таблицу CandidateBase
    /// </summary>
    public class CandidateMap : JoinedSubclassMapping<CandidateBase>
    {
        /// <summary>
        /// Конструктор маппинга CandidateBase
        /// </summary>
        public CandidateMap()
        {
            Table(nameof(CandidateBase));
            Property(b => b.LastName);
            Property(b => b.FirstName);
            Property(b => b.Patronymic);
            Property(b => b.About);
            Property(b => b.BirthDay);
            Property(b => b.Education);
            Property(b => b.Photo,
                c => {
                    c.Length(2147483647);
                });
            Property(b => b.Experience);
            Property(b => b.Profession);
        }
    }
}
