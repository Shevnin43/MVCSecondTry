using ElmaSecondTryBase.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    /// <summary>
    /// Маппинг модели UserBase на таблицу UserBase
    /// </summary>
    public class UserMap : ClassMapping<UserBase>
    {
        /// <summary>
        /// Конструктор маппинга UserBase
        /// </summary>
        public UserMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.NativeGuid));
            Property(b => b.Login,
                c => {
                    c.NotNullable(true);
                });
            Property(b => b.Email);
            Property(b => b.Password,
                c => {
                    c.NotNullable(true);
                });
            Property(b => b.Role,
                c => {
                    c.NotNullable(true);
                });
            Property(b => b.About);
            Property(b => b.Phone);
            Property(b => b.Name);
            Property(b => b.RegisterDate,
                c => {
                    c.NotNullable(true);
                });
            Property(b => b.NotKill,
                c => {
                    c.NotNullable(true);
                });
            Bag(b => b.Announcements,
                c => 
                { 
                    c.Key(k => k.Column("AnnouncementId")); 
                    c.Inverse(true);
                    c.Cascade(Cascade.All);
                    c.Lazy(CollectionLazy.NoLazy); 
                },
                r => r.OneToMany());
            Table(nameof(UserBase));
        }
    }
}
