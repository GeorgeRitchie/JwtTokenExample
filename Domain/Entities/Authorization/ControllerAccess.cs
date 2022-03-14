using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Представляет контроллер для управления доступа пользователя
	/// </summary>
	public class ControllerAccess : IBaseEntity
	{
		/// <inheritdoc cref="IBaseEntity.Id" />
		public Guid Id { get; set; }

		/// <inheritdoc cref="IBaseEntity.IsDeleted" />
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Настоящее имя контроллера, используйте nameof(), по нему будет проводиться проверка соответствия
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// Имя контроллера используемое для отображения в UI пользователя
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// Описание контроллера
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Роль пользователя с которым связан данный контроллер, через которого определяется уровень доступа
		/// </summary>
		public Role Role { get; set; }

		/// <summary>
		/// Список действий контроллера к которым имеет доступ пользователь с данной ролью
		/// </summary>
		public List<ActionAccess> Actions { get; set; }
	}
}
