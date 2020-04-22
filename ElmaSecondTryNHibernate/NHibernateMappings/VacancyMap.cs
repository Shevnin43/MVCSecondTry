using ElmaSecondTryBase.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    public class VacancyMap : ClassMapping<VacancyBase>
    {
        /// <summary>
        /// Инициализировать экземпляр <see cref="VacancyMap"/>
        /// </summary>
        public VacancyMap()
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

            Property(b => b.Name, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.About, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.Employment, x =>
            {
                x.Type(NHibernateUtil.Int32);
            });

            Property(b => b.Requirement, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.IsOpen, x =>
            {
                x.Type(NHibernateUtil.String);
            });

            Property(b => b.ValidDay, x =>
            {
                x.Type(NHibernateUtil.DateTime);
            });

            Property(b => b.Salary, x =>
            {
                x.Type(NHibernateUtil.Int32);
            });

            ManyToOne(property => property.Creator, mapping =>
            {
                mapping.Column("VacancyId");
                mapping.Cascade(Cascade.All);
            });

            // LastEditor

            Table(nameof(VacancyBase));
        }
    }
}
