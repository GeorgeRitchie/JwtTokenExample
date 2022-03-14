namespace EskhataDigital.Domain.AuthEntities
{
	/// <summary>
	/// Класс содержащий данные хранимые в PayLoad части jwt токена необходимые для идентификации пользователя
	/// </summary>
	public class UserAuthDto
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Логин пользователя
		/// </summary>
		public string Login { get; set; }
	}
}
