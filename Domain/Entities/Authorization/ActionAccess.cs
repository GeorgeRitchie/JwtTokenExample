using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Представляет имя действия контроллера для управления доступа пользователя.
	/// </summary>
	public class ActionAccess : IBaseEntity
	{
		/// <inheritdoc cref="IBaseEntity.Id" />
		public Guid Id { get; set; }

		/// <inheritdoc cref="IBaseEntity.IsDeleted" />
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Настоящее имя действия контроллера, используйте nameof(), по нему будет проводиться проверка соответствия
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// Имя действия контроллера используемое для отображения в UI пользователя
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Описание действия контроллера
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Список контроллеров имеющих такое действие
		/// </summary>
		public List<ControllerAccess> ControllerAccess { get; set; }
	}
}
