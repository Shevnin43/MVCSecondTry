using ElmaSecondTryBase.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    public class AnnouncementMap : ClassMapping<Announcement>
    {
        public AnnouncementMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Guid);
                x.Generator(Generators.NativeGuid);
                x.Column("Id");
            });

            Property(b => b.CreationDate, x =>
            {
                x.Type(NHibernateUtil.DateTime);
            });

            Property(b => b.LastEdited, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            ManyToOne(property => property.Creator, mapping =>
            {
                mapping.Column("CandidateId");
                mapping.Cascade(Cascade.All);
            });
        }
    }
}
