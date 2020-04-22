using ElmaSecondTryBase.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    public class UserMap : ClassMapping<UserBase>
    {
        /// <summary>
        /// Инициализировать экземпляр <see cref="UserMap"/>
        /// </summary>
        public UserMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Guid);
                x.Generator(Generators.NativeGuid);
                x.Column("Id");
            });

            Property(b => b.Login, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.Email, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.Password, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.Role, x =>
            {
                x.Type(NHibernateUtil.Int32);
            });

            Property(b => b.About, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.Phone, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.Name, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.RegisterDate, x =>
            {
                x.Type(NHibernateUtil.DateTime);
            });

            Set(property => property.Announcements,
                collectionMapping =>
                {
                    collectionMapping.Key(keyMapping =>
                    {
                        keyMapping.Column("CreatorId");
                    });
                    collectionMapping.Cascade(Cascade.All);
                },
                mapping =>
                {
                    mapping.OneToMany();
                }
            );

            Table(nameof(UserBase));
        }
    }
}
