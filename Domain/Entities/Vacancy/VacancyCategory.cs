using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Класс описывающий категорию вакансии
	/// </summary>
	public class VacancyCategory : IBaseEntity
	{
		/// <inheritdoc cref="IBaseEntity.Id"/>
		public Guid Id { get; set; }

		/// <inheritdoc cref="IBaseEntity.IsDeleted"/>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Имя категория
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Радительская категория
		/// </summary>
		public VacancyCategory ParendVacancyCategory { get; set; }

		/// <summary>
		/// Все вакансии входящие в данную категорию
		/// </summary>
		public List<Vacancy> Vacancies { get; set; }
	}
}