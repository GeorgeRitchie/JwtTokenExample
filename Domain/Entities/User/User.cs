using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Класс представляющий пользователя данной программы
	/// </summary>
	public class User : IBaseEntity
	{
		/// <inheritdoc cref="IBaseEntity.Id" />
		public Guid Id { get; set; }

		/// <inheritdoc cref="IBaseEntity.IsDeleted" />
		public bool IsDeleted { get; set; }


		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Фамилия пользователя
		/// </summary>
		public string Surname { get; set; }

		/// <summary>
		/// Отчество пользователя
		/// </summary>
		public string Patronymic { get; set; }

		/// <summary>
		/// Полное имя пользователя
		/// </summary>
		string FullName { get => $"{Name} {Surname} {Patronymic}"; }

		/// <summary>
		/// Адрес электронной почты пользователя
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Номер телефона пользователя
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// Данные пользователя для аутентификации
		/// </summary>
		public UserAuthenticationData UserAuthenticationData { get; set; }

		/// <summary>
		/// Роль пользователя
		/// </summary>
		public Role Role { get; set; }

		/// <summary>
		/// Ссылка-приглашение используемое для приглашения друзей
		/// </summary>
		public Guid InvitationReference { get; set; }

		/// <summary>
		/// Список приглашенных друзей
		/// </summary>
		public List<User> InvitedUsers { get; set; }

		/// <summary>
		/// Список резюме отправленных для подачи заявки в вакансии
		/// </summary>
		public List<UserResume> ResumesForRespondedVacancy { get; set; }

		/// <summary>
		/// Список вакансий созданный пользователем
		/// </summary>
		public List<Vacancy> CreatedVacancyList { get; set; }
	}
}
