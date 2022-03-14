using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Класс описывающий вакансию
	/// </summary>
	public class Vacancy : IBaseEntity
	{
		/// <inheritdoc cref="IBaseEntity.Id" />
		public Guid Id { get; set; }

		/// <inheritdoc cref="IBaseEntity.IsDeleted" />
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Имя вакансии
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Описание вакансии
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Список обязанностей выполняемых сотрудников на данной позиции
		/// </summary>
		public string Duties { get; set; }

		/// <summary>
		/// Список требований к кандидату на вакансию
		/// </summary>
		public string Requirements { get; set; }

		/// <summary>
		/// Предложение банка к кандидату при его работе на данной позиции
		/// </summary>
		public string Offers { get; set; }

		/// <summary>
		/// Дата добавления вакансии
		/// </summary>
		public DateTime DateAdd { get; set; } = DateTime.Now;

		/// <summary>
		/// Дедлайн приема заявок на данную вакансию
		/// </summary>
		public DateTime Deadline { get; set; }

		/// <summary>
		/// Автор данной вакансии
		/// </summary>
		public User? Redactor { get; set; }

		/// <summary>
		/// Филиал где открыта эта вакансия
		/// </summary>
		public Branch? Branch { get; set; }

		/// <summary>
		/// Уровень опыта работы необходимой на данную позицию
		/// </summary>
		public ExperienceLevel? Level { get; set; }

		/// <summary>
		/// Категория данной ваканси
		/// </summary>
		public List<VacancyCategory> VacancyCategory { get; set; }

		/// <summary>
		/// Список резюме пользователей подавших заявку на данную вакансию
		/// </summary>
		public List<UserResume> CandidatesResume { get; set; }
	}
}
