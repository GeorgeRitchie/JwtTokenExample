using EskhataDigital.Domain.AuthEntities;
using System.Security.Claims;

namespace EskhataDigital.Services.AuthenticationSevice
{
	/// <summary>
	/// Сервис для создания временного токена 
	/// </summary>
	public class TempTokenService
	{
		private readonly TokenGeneratorService tokenGeneratorService;

		/// <summary>
		/// Создает экземпляр сервиса <see cref="TempTokenService"/>
		/// </summary>
		public TempTokenService()
		{
			this.tokenGeneratorService = new TokenGeneratorService();
		}

		/// <summary>
		/// Созадет временный токен для заданного устройства
		/// </summary>
		/// <param name="device">Данные устройства в виде класса <see cref="Device"/></param>
		/// <returns>Временный токен</returns>
		public string GenerateToken(Device device)
		{
			var claims = new List<Claim>
			{
				new Claim(DefaultDeviceClaimsNames.Id, device.Id.ToString()),
				new Claim(DefaultDeviceClaimsNames.Name, device.Name),
				new Claim(DefaultDeviceClaimsNames.Type, device.Type),
				new Claim(DefaultDeviceClaimsNames.AppName, device.AppName),
				new Claim(DefaultDeviceClaimsNames.AppType, device.AppType),
			};

			return tokenGeneratorService.Generate(AuthOptions.TEMPTOKENLIFETIME, claims);
		}

		/// <summary>
		/// Извлекает из клеймов данные устройства и возвращает данные в виде объекта <see cref="Device"/>
		/// </summary>
		/// <param name="claims">Данные устройства в виде клеймов</param>
		/// <returns>Экземпляр класса <see cref="Device"/> с данными устройства</returns>
		public Device GenerateDevice(IEnumerable<Claim> claims)
		{
			var device = new Device()
			{
				Id = Guid.Parse(claims.First(u => u.Type == DefaultDeviceClaimsNames.Id).Value),
				Name = claims.First(u => u.Type == DefaultDeviceClaimsNames.Name).Value,
				Type = claims.First(u => u.Type == DefaultDeviceClaimsNames.Type).Value,
				AppName = claims.First(u => u.Type == DefaultDeviceClaimsNames.AppName).Value,
				AppType = claims.First(u => u.Type == DefaultDeviceClaimsNames.AppType).Value,
			};

			return device;
		}
	}

	/// <summary>
	/// Класс содержащий названия типов значений в клеймах для устройства
	/// </summary>
	internal class DefaultDeviceClaimsNames
	{
		public const string Id = "Id";
		public const string Name = "Name";
		public const string Type = "Type";
		public const string AppName = "AppName";
		public const string AppType = "AppType";
	}
}
