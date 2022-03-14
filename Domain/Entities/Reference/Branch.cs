using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
    /// <summary>
	/// Объект по которому создании вакансии
	/// </summary>
    public class Branch : IBaseEntity
    {
        ///	<inheritdoc cref="IBaseEntity.Id"/>
        public Guid Id { get; set; }

        ///	<inheritdoc cref="IBaseEntity.IsDeleted"/>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// <value>Адрес по которому находиться тот или иной филиал</value>
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// <value>Идентификатор города</value>
        /// </summary>
        public Guid CityId { get; set; }

        /// <summary>
        /// <value>Город по которому находиться тот или иной филиал</value>
        /// </summary>
        public City City { get; set; }

        /// <summary>
        /// <value>Вакансии в этот филиал</value>
        /// </summary>
        public List<Vacancy> Vacancies { get; set; } = new();
    }
}