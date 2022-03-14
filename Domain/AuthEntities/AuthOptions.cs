using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EskhataDigital.Domain.AuthEntities
{
	/// <summary>
	/// Класс содержащий параметры для создания jwt токена
	/// </summary>
	public static class AuthOptions
	{
		/// <summary>
		/// Издатель токена
		/// </summary>
		public static string ISSUER = "EskhataCareer";

		/// <summary>
		/// Потребитель токена
		/// </summary>
		public static string AUDIENCE = "EskhataCareer";

		/// <summary>
		/// Ключ для шифрования
		/// </summary>
		public static string KEY;

		/// <summary>
		/// Время жизни временного токена
		/// </summary>
		public static int TEMPTOKENLIFETIME = 3;

		/// <summary>
		/// Время жизни токена доступа
		/// </summary>
		public static int ACCESSTOKENLIFETIME = 3;

		/// <summary>
		/// Время жизни токена предназначенного для обновления токена доступа
		/// </summary>
		public static int REFRESHTOKENLIFETIME = 30;

		/// <summary>
		/// Гарантирует случайное новое значение ключа шифрования при каждом старте программы
		/// </summary>
		static AuthOptions()
		{
			KEY = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Возвращает симетричный ключ шифрования
		/// </summary>
		/// <returns>Симетричный ключ шифрования</returns>
		public static SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
		}
	}
}
