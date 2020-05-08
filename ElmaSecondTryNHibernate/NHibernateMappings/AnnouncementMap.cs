using ElmaSecondTryBase.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    /// <summary>
    /// Класс маппинга интерфейса IAnnouncement 
    /// </summary>
    public class AnnouncementMap : ClassMapping<IAnnouncement>
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
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
