using ElmaSecondTryBase.Entities;
using NHibernate.Mapping.ByCode.Conformist;

namespace ElmaSecondTryNHibernate.NHibernateMappings
{
    /// <summary>
    /// Маппинг модели VacancyBase на таблицу VacancyBase
    /// </summary>
    public class VacancyMap : JoinedSubclassMapping<VacancyBase>
    {
        /// <summary>
        /// Конструктор маппинга VacancyBase
        /// </summary>
        public VacancyMap()
        {
            Table(nameof(VacancyBase));
            Property(b => b.Name);
            Property(b => b.About);
            Property(b => b.Employment); 
            Property(b => b.Requirement);
            Property(b => b.IsOpen);
            Property(b => b.ValidDay);
            Property(b => b.Salary);
        }
    }
}
