using EskhataDigital.Domain.AuthEntities;
using System.Security.Claims;

namespace EskhataDigital.Services.AuthenticationSevice
{
	/// <summary>
	/// Сервис для создания токена доступа
	/// </summary>
	public class AccessTokenService
	{
		private readonly TokenGeneratorService tokenGeneratorService;

		/// <summary>
		/// Создает экземпляр сервиса <see cref="AccessTokenService"/>
		/// </summary>
		public AccessTokenService()
		{
			tokenGeneratorService = new TokenGeneratorService();
		}

		/// <summary>
		/// Создает токен доступа
		/// </summary>
		/// <param name="user">Данные пользователя для которого создается токен доступа</param>
		/// <returns>Токен доступа</returns>
		public string GenerateToken(UserAuthDto user)
		{
			var claims = new List<Claim>
			{
				new Claim(DefaultUserClaimsNames.Id, user.Id.ToString()),
				new Claim(DefaultUserClaimsNames.Login, user.Login),
			};

			return tokenGeneratorService.Generate(AuthOptions.ACCESSTOKENLIFETIME, claims);
		}

		/// <summary>
		/// Извлекает из клеймов данные пользователя и возвращает данные в виде объекта <see cref="UserAuthDto"/>
		/// </summary>
		/// <param name="claims">Данные пользователя в виде клеймов</param>
		/// <returns>Экземпляр класса <see cref="UserAuthDto"/> с данными пользователя</returns>
		public UserAuthDto GenerateUsetAuthDto(IEnumerable<Claim> claims)
		{
			var userAuthDto = new UserAuthDto()
			{
				Id = Guid.Parse(claims.First(u => u.Type == DefaultUserClaimsNames.Id).Value),
				Login = claims.First(u => u.Type == DefaultUserClaimsNames.Login).Value,
			};

			return userAuthDto;
		}
	}

	/// <summary>
	/// Класс содержащий названия типов значений в клеймах для пользователя
	/// </summary>
	internal class DefaultUserClaimsNames
	{
		public const string Id = "Id";
		public const string Login = "Login";
	}
}
