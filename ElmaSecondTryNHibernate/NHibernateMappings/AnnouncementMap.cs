using ElmaSecondTryBase.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    public class AnnouncementMap : ClassMapping<IAnnouncement>
    {
        public AnnouncementMap()
        {
            Id(x => x.Id);
            Property(b => b.CreationDate, 
                c => {
                    c.Update(false);
                    c.NotNullable(true);
                });
            Property(b => b.LastEdited,
                c => {
                    c.NotNullable(true);
                });
            ManyToOne(x => x.Creator,
                c => {
                    c.Cascade(Cascade.None);
                    c.Column("AnnouncementId");
                    c.Lazy(LazyRelation.NoLazy);
                    c.Update(false);
                    c.NotNullable(true);
                });
            ManyToOne(x => x.LastEditor,
                c => {
                    c.Column("LastEditorId");
                    c.Lazy(LazyRelation.NoLazy);
                    c.NotNullable(true);
                });
            Property(b => b.IsBlocked,
                c => {
                    c.NotNullable(true);
                });
            Property(b => b.Type, 
                c => {
                    c.Update(false);
                    c.NotNullable(true);
                });
            Table(nameof(IAnnouncement));
            DynamicUpdate(true);
            DynamicInsert(true);
        }
    }
}
