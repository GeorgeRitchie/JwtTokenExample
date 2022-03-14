using EskhataDigital.Services.AuthenticationSevice;

namespace EskhataDigital.Core.Validators
{
	/// <summary>
	/// Промежуточный элемент конвеера для аутентификации пользователя через Jwt токен
	/// </summary>
	public class JwtAuthenticateMiddleware
	{
		private readonly RequestDelegate next;
		private readonly string loginRoutePath;
		private readonly string registerRoutePath;
		private readonly string refreshRoutePath;

		/// <summary>
		/// Создает объект <see cref="JwtAuthenticateMiddleware"/>
		/// </summary>
		/// <param name="next">Действие которое должно выполниться после успешной аутентификации</param>
		/// <param name="loginRoutePath">Адрес к методу входа</param>
		/// <param name="registerRoutePath">Адрес к методу регистрации</param>
		/// <param name="refreshRoutePath">Адрес к методу обновления токена</param>
		public JwtAuthenticateMiddleware(RequestDelegate next, string loginRoutePath, string registerRoutePath, string refreshRoutePath)
		{
			this.next = next;
			this.loginRoutePath = loginRoutePath;
			this.registerRoutePath = registerRoutePath;
			this.refreshRoutePath = refreshRoutePath;
		}

		/// <summary>
		/// Проводит валидацию токена и на его основе проводит аутентификацию
		/// </summary>
		/// <param name="context">Контекст текущего запроса</param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			var urlPath = context.Request.Path.ToString();
			var authenticateService = context.RequestServices.GetRequiredService<AuthenticationService>();
			var token = ExtractTokenFromRequest(context);

			try
			{
				if (token != string.Empty && urlPath.StartsWith(refreshRoutePath) == false)
				{
					if (urlPath.StartsWith(loginRoutePath) || urlPath.StartsWith(registerRoutePath))
					{
						var device = authenticateService.ValidateTempToken(token);

						SaveDataInHttpContextItems("Device", device, context);
					}
					else
					{
						var user = await authenticateService.ValidateAccessTokenAsync(token);

						SaveDataInHttpContextItems("User", user, context);
					}
				}
			}
			catch
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsJsonAsync(new { Message = "Invalid token" });
				return;
			}

			await next(context);
		}

		/// <summary>
		/// Извлекает токен из заголовка запроса
		/// </summary>
		/// <param name="context">Котекст запроса</param>
		/// <returns>Токен</returns>
		private string ExtractTokenFromRequest(HttpContext context)
		{
			string token = string.Empty;

			var headerAuthorizationValue = context.Request.Headers["Authorization"].FirstOrDefault() ?? "";
			if (headerAuthorizationValue.StartsWith("Bearer "))
				token = headerAuthorizationValue.Split(" ").Last();

			return token;
		}

		/// <summary>
		/// Сохраняет данные в HttpContextItms по указанному ключу, если значение с таким ключом уже имеется то значение перезаписывается
		/// </summary>
		/// <param name="key">Ключ по которому проводится сохранение</param>
		/// <param name="value">Объект который сохраняется</param>
		/// <param name="context">Контекст запроса</param>
		private void SaveDataInHttpContextItems(string key, object value, HttpContext context)
		{
			if (context.Items.ContainsKey(key))
				context.Items.Remove(key);

			context.Items.Add(key, value);
		}
	}

	/// <summary>
	/// Расширение для подключения middleware
	/// </summary>
	public static class JwtAuthenticateMiddlewareExtension
	{
		/// <summary>
		/// Подключает <see cref="JwtAuthenticateMiddleware"/> в конвеер обработки запроса, с передачей в конструктор адреса к методам регистрации, входа и обновления токена доступа
		/// </summary>
		/// <param name="builder"><see cref="IApplicationBuilder"/></param>
		/// <param name="loginRoutePath">Адрес к методу входа</param>
		/// <param name="registerRoutePath">Адрес к методу регистрации</param>
		/// <param name="refreshRoutePath">Адрес к методу обновления токена</param>
		/// <returns><see cref="IApplicationBuilder"/></returns>
		public static IApplicationBuilder UseJwtAuthenticateMiddleware(this IApplicationBuilder builder, string loginRoutePath, string registerRoutePath, string refreshRoutePath)
		{
			return builder.UseMiddleware<JwtAuthenticateMiddleware>(loginRoutePath, registerRoutePath, refreshRoutePath);
		}
	}
}
