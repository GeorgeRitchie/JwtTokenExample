namespace EskhataDigital.Domain.Interfaces
{
	/// <summary>
	/// Базовый интерфейс для всех сущностей системы
	/// </summary>
	public interface IBaseEntity
	{
		/// <summary>
		/// <value>Получает или устонавливает уникальный идентификатор</value>
		/// </summary>
		Guid Id { get; set; }

		/// <summary>
		/// <value>Получает или изменяеть значение, указывающее, был ли объект удален из базы данных.</value>
		/// </summary>
		bool IsDeleted { get; set; }
	}
}
