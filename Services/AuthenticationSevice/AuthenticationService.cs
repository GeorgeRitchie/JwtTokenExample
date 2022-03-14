using EskhataDigital.Domain.AuthEntities;
using EskhataDigital.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using EskhataDigital.Domain.Exceptions;

namespace EskhataDigital.Services.AuthenticationSevice
{
	/// <summary>
	/// Сервис по управлению аутентификацией
	/// </summary>
	public class AuthenticationService
	{
		private readonly AccessTokenService accessTokenService;
		private readonly RefreshTokenService refreshTokenService;
		private readonly TempTokenService tempTokenService;

		private readonly ApplicationContext context;

		private readonly TokenValidationParameters validationParameters = new()
		{
			ValidateIssuerSigningKey = true,
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
			ValidIssuer = AuthOptions.ISSUER,
			ValidAudience = AuthOptions.AUDIENCE,
			ClockSkew = TimeSpan.Zero,
		};

		/// <summary>
		/// Создает экземпляр сервиса <see cref="AuthenticationService"/>
		/// </summary>
		/// <param name="context">База данных</param>
		public AuthenticationService(ApplicationContext context)
		{
			accessTokenService = new AccessTokenService();
			refreshTokenService = new RefreshTokenService();
			tempTokenService = new TempTokenService();

			this.context = context;
		}

		/// <inheritdoc cref="TempTokenService.GenerateToken(Device)"/>
		public string GetTempToken(Device device)
		{
			var token = tempTokenService.GenerateToken(device);
			return token;
		}

		/// <summary>
		/// Валидирует временный токен. Если токен прощел валидацию, то извлекаются данные устройства из токена и возвращается в виде объекта <see cref="Device"/>, иначе выбрасывается исключение
		/// </summary>
		/// <param name="tempToken">Временный токен</param>
		/// <returns>Экземпляр класса <see cref="Device"/> с данными устройства</returns>
		public Device ValidateTempToken(string tempToken)
		{
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

			var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(tempToken, validationParameters, out SecurityToken _);

			var device = tempTokenService.GenerateDevice(claimsPrincipal.Claims);

			return device;
		}

		/// <summary>
		/// Создает токены доступа и обновления для пользователя с заданным Id. Токен обновления сохраняется в БД. Если пользователь не был найден то выбрасывается исключение <see cref="UserNotFoundException"/>
		/// </summary>
		/// <param name="userId">Id пользователя</param>
		/// <param name="cancellationToken">Токен отмены действия</param>
		/// <returns>Объект класса <see cref="AuthenticateResponse"/> с токенами доступа и обновления</returns>
		/// <exception cref="UserNotFoundException"><see cref="UserNotFoundException"/></exception>
		public async Task<AuthenticateResponse> AuthenticateAsync(Guid userId, CancellationToken cancellationToken)
		{
			var user = await context.Users.Include(u => u.UserAuthenticationData).FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

			if (user == null)
			{
				throw new UserNotFoundException(nameof(userId));
			}

			var userAuthenticationData = user.UserAuthenticationData;
			var refreshToken = refreshTokenService.GenerateToken();
			userAuthenticationData.RefreshToken = refreshToken;

			context.UserAuthenticationDatas.Update(userAuthenticationData);
			await context.SaveChangesAsync(cancellationToken);

			var accessToken = accessTokenService.GenerateToken(new UserAuthDto() { Id = user.Id, Login = userAuthenticationData.Login });

			return new AuthenticateResponse
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken
			};
		}

		/// <summary>
		/// Проводит валидацию токена доступа. Если токен прошел валидацию, то извлекается пользователь с котороым связан данный токен доступа и возвращается, иначе выбрасывается исключение
		/// </summary>
		/// <param name="accessToken">Токен доступа</param>
		/// <returns>Пользователь в виде объкета <see cref="IUser"/></returns>
		public async Task<User> ValidateAccessTokenAsync(string accessToken)
		{
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

			var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken _);

			var userAuthDto = accessTokenService.GenerateUsetAuthDto(claimsPrincipal.Claims);

			var user = await context.Users.Include(u => u.Role).Include(u => u.UserAuthenticationData).FirstOrDefaultAsync(u => u.Id == userAuthDto.Id && u.UserAuthenticationData.Login == userAuthDto.Login);

			return user!;
		}

		/// <summary>
		/// Проводит валидацию токена обновления. Если токен прошел валидацию, то возвращается <see cref="true"/>, иначе выбрасывается исключение
		/// </summary>
		/// <param name="refreshToken">Токен обновления</param>
		/// <returns><see cref="true"/> если токен прошел валидацию</returns>
		public bool ValidateRefreshToken(string refreshToken)
		{
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

			jwtSecurityTokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken _);

			return true;
		}
	}

	/// <summary>
	/// Класс расширение для ServiceProvider
	/// </summary>
	public static class ServiceProviderExtensions
	{
		/// <summary>
		/// Подключает сервис <see cref="AuthenticationService"/> в режиме Transient
		/// </summary>
		/// <param name="services"><see cref="IServiceCollection"/></param>
		public static void AddAuthenticationService(this IServiceCollection services)
		{
			services.AddTransient<AuthenticationService>();
		}
	}
}
