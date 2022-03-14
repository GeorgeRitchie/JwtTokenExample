using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Представляет роль пользователя и уровень его доступа к разным контроллерам и их действиям
	/// </summary>
	public class Role : IBaseEntity
	{
		///	<inheritdoc cref="IBaseEntity.Id"/>
		public Guid Id { get; set; }

		///	<inheritdoc cref="IBaseEntity.IsDeleted"/>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// <value>Адрес по которому находиться тот или иной филиал</value>
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// Пользователь с данной ролью
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Список контроллеров к которым имеет доступ пользователь с данной ролью
		/// </summary>
		public List<ControllerAccess> ControllerAccess { get; set; }
	}
}
