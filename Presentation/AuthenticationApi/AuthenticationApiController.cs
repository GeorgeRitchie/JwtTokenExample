using EskhataDigital.Core.Validators;
using EskhataDigital.Domain.AuthEntities;
using EskhataDigital.Domain.Entities;
using EskhataDigital.Domain.ModelView;
using EskhataDigital.Services.AuthenticationSevice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EskhataDigital.Presentation.AuthenticateApi
{
	/// <summary>
	/// Контроллер для создания и обновления токенов
	/// </summary>
	[Route("[controller]/[action]")]
	public class AuthenticationApiController : ApiBaseController
	{
		private readonly AuthenticationService authenticationService;
		private readonly ApplicationContext applicationContext;

		/// <summary>
		/// Создает объект <see cref="AuthenticationApiController"/>
		/// </summary>
		/// <param name="authenticationService"><inheritdoc cref="AuthenticationService"/></param>
		/// <param name="applicationContext"><inheritdoc cref="ApplicationContext"/></param>
		public AuthenticationApiController(AuthenticationService authenticationService, ApplicationContext applicationContext)
		{
			this.authenticationService = authenticationService;
			this.applicationContext = applicationContext;
		}

		/// <summary>
		/// Проводит рукопожатие межу сервером и неизветным клиентом для регистрации или авторизации в дальнейшем
		/// </summary>
		/// <param name="device">Данные клиента</param>
		/// <returns>Результат запроса</returns>
		[AllowAnonymous]
		[HttpPost()]
		public ActionResult HandShake([FromBody] Device device)
		{
			var tempToken = authenticationService.GetTempToken(device);

			SaveDataInSession("Device", device, HttpContext);

			return Ok(new { token = tempToken });
		}

		/// <summary>
		/// Сохраняет данные в сессию по указанному ключу, если данные по ключу уже имеются, то они перезаписываются
		/// </summary>
		/// <param name="key">Ключ по которому сохраняется данные</param>
		/// <param name="value">Данные для сохранения</param>
		/// <param name="context">Контекст запроса</param>
		private void SaveDataInSession(string key, object value, HttpContext context)
		{
			if (context.Session.Keys.Contains(key))
				context.Session.Remove(key);

			context.Session.SetString(key, JsonSerializer.Serialize(value));
		}

		/// <summary>
		/// Проводит регистрацию нового пользователя
		/// </summary>
		/// <param name="registerUserVm">Данные нового пользователя</param>
		/// <returns>Токены доступа и обновления</returns>
		[IntermediateAuthorization]
		[HttpPost()]
		public async Task<ActionResult> Register([FromBody] RegisterUserViewModel registerUserVm)
		{
			var user = CreateUser(registerUserVm);

			applicationContext.Users.Add(user);
			await applicationContext.SaveChangesAsync();

			var tokens = await authenticationService.AuthenticateAsync(user.Id, new CancellationTokenSource().Token);

			return Ok(tokens);
		}

		/// <summary>
		/// Создает нового пользователя
		/// </summary>
		/// <param name="vm">Данные нового пользователя</param>
		/// <returns>Новый пользователь</returns>
		private User CreateUser(RegisterUserViewModel vm)
		{
			var user = new User()
			{
				Name = vm.Name,
				Surname = vm.Surname,
				Patronymic = vm.Patronymic,

				InvitationReference = Guid.NewGuid(),
				Phone = vm.Phone,
				Email = vm.Email,
			};

			user.Role = new Role()
			{
				User = user,
				Name = "User"
			};

			user.UserAuthenticationData = new UserAuthenticationData()
			{
				Login = vm.Login,
				Password = HashPassword(vm.Password),
				User = user,
				RefreshToken = "",
			};

			return user;
		}

		/// <summary>
		/// Хеширует пароль пользователя
		/// </summary>
		/// <param name="password">Пароль пользоателя</param>
		/// <returns>Хешированный пароль</returns>
		private string HashPassword(string password)
		{
			// TODO add password hasher
			return password;
		}

		/// <summary>
		/// Проводит вход пользователя в случае правильных данных, иначе возвращает статус код 400 (BadRequest)
		/// </summary>
		/// <param name="loginUserVm">Данные для входа</param>
		/// <returns>Токены доступа и обновления</returns>
		[IntermediateAuthorization]
		[HttpPost]
		public async Task<ActionResult> Login([FromBody] LoginUserViewModel loginUserVm)
		{
			var password = HashPassword(loginUserVm.Password);
			var user = await applicationContext.Users.AsNoTracking().Include(u => u.UserAuthenticationData).FirstOrDefaultAsync(u => u.UserAuthenticationData.Login == loginUserVm.Login && u.UserAuthenticationData.Password == password);

			if (user != null)
			{
				var tokens = await authenticationService.AuthenticateAsync(user.Id, new CancellationTokenSource().Token);
				return Ok(tokens);
			}

			return BadRequest(new { Errors = "Incorrect login or password" });
		}

		/// <summary>
		/// Проводит выход пользователя
		/// </summary>
		/// <returns>Результат запроса</returns>
		[Authorize]
		[HttpGet]
		public async Task<ActionResult> Logout()
		{
			var user = HttpContext.Items["User"] as User;

			user!.UserAuthenticationData.RefreshToken = "";
			applicationContext.Users.Update(user);
			await applicationContext.SaveChangesAsync();

			return NoContent();
		}

		/// <summary>
		/// Обновляет устаревший токен доступа через токен обновления при условии валидности токена обновления, иначе возвращает статус код 400 (BadRequest) 
		/// </summary>
		/// <param name="refreshToken">Токен обновления</param>
		/// <returns>Новый токен доступа и обновления</returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult> Refresh([FromBody] string refreshToken)
		{
			try
			{
				authenticationService.ValidateRefreshToken(refreshToken);
				var userId = await applicationContext.Users.Include(u => u.UserAuthenticationData).Where(u => u.UserAuthenticationData.RefreshToken == refreshToken).Select(u => u.Id).FirstOrDefaultAsync();

				if (userId == Guid.Empty)
					throw new Exception();

				return Ok(await authenticationService.AuthenticateAsync(userId, new CancellationTokenSource().Token));
			}
			catch
			{
				return BadRequest(new { Errors = "Invalid refresh token" });
			}
		}
	}
}
