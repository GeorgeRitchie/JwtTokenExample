namespace EskhataDigital.Domain.ModelView
{
	/// <summary>
	/// Представляет данные нового пользователя для регистрации
	/// </summary>
	public class RegisterUserViewModel         // TODO add mapper to create new User object 
	{
		/// <summary>
		/// Логин пользователя
		/// </summary>
		public string Login { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Подтверждение пароля
		/// </summary>
		public string ConfirmPassword { get; set; }

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
		/// Адрес электронной почты пользователя
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Номер телефона пользователя
		/// </summary>
		public string Phone { get; set; }
	}
}
