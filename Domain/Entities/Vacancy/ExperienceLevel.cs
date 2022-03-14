using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Класс описывающий уровень опыта работы кандидата 
	/// </summary>
	public class ExperienceLevel : IBaseEntity
	{
		/// <inheritdoc cref="IBaseEntity.Id" />
		public Guid Id { get; set; }

		/// <inheritdoc cref="IBaseEntity.IsDeleted" />
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Имя уровня опыта работы
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Список вакансий допускающие данный уровень опыта работы
		/// </summary>
		public List<Vacancy> Vacancies { get; set; }
	}
}