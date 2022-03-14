namespace EskhataDigital.Domain.ModelView
{
	/// <summary>
	/// Данные необходимые для входа пользователя в систему
	/// </summary>
	public class LoginUserViewModel
	{
		/// <summary>
		/// Логин пользователя
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Пароль пользователя
		/// </summary>
		public string Password { get; set; }
	}
}
