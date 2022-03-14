using EskhataDigital.Domain.Interfaces;

namespace EskhataDigital.Domain.Entities
{
	/// <summary>
	/// Класс содержащий данные пользователя для аутентификации
	/// </summary>
	public class UserAuthenticationData : IBaseEntity
	{
		/// <inheritdoc cref="IBaseEntity.Id" />
		public Guid Id { get; set; }

		/// <inheritdoc cref="IBaseEntity.IsDeleted" />
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Id пользователя с которым связанные текущие аутентификационные данные
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// Пользователь с которым связанные текущие аутентификационные данные
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Логин пользователя
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Пароль пользователя
		/// </summary>
		public string Password { get; set; }
		
		/// <summary>
		/// Токен обновления пользователя
		/// </summary>
		public string? RefreshToken { get; set; }
	}
}
