using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Данные об откликнувшем пользователе
	/// </summary>
	public class UserResume : IBaseEntity
	{
		///	<inheritdoc cref="IBaseEntity.Id"/>
		public Guid Id { get; set; }

		///	<inheritdoc cref="IBaseEntity.IsDeleted"/>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// <value>Дата подачи заявки</value>
		/// </summary>
		public DateTime AppliedDate { get; set; } = DateTime.Now;

		/// <summary>
		/// <value>Путь по которому сохранён резюме файл кандидата</value>
		/// </summary>
		public string ResumeFilePath { get; set; }

		/// <summary>
		/// <value>Вакансия на который откликнулся кандидат</value>
		/// </summary>
		public Vacancy Vacancy { get; set; }

		/// <summary>
		/// <value>Пользователь который подал заявку на вакансию</value>
		/// </summary>
		public User User { get; set; }
	}
}
