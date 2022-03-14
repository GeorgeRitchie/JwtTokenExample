using EskhataDigital.Domain.AuthEntities;

namespace EskhataDigital.Services.AuthenticationSevice
{
	/// <summary>
	/// Сервис для создания токена обновления
	/// </summary>
	public class RefreshTokenService
	{
		private readonly TokenGeneratorService tokenGeneratorService;

		/// <summary>
		/// Создает экземпляр сервиса <see cref="RefreshTokenService"/>
		/// </summary>
		public RefreshTokenService()
		{
			tokenGeneratorService = new TokenGeneratorService();
		}

		/// <summary>
		/// Создает токен обновления
		/// </summary>
		/// <returns>Токен обновления</returns>
		public string GenerateToken()
		{
			return tokenGeneratorService.Generate(AuthOptions.REFRESHTOKENLIFETIME);
		}
	}
}
