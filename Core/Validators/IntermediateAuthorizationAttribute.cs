using System.Text.Json;
using EskhataDigital.Domain.AuthEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EskhataDigital.Core.Validators
{
	/// <summary>
	/// Проверяет действительно ли клиент является тем, кому было отправлено временный токен
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class IntermediateAuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		/// <summary>
		/// <inheritdoc cref="IntermediateAuthorizationAttribute"/>
		/// </summary>
		/// <param name="context">Контекст связанный с текущим запросом</param>
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			try
			{
				var deviceDataFromRequestToken = context.HttpContext.Items["Device"] as Device;
				var deviceDataSentAtHandShake = GetDataFromSession("Device", typeof(Device), context.HttpContext) as Device;

				if (deviceDataFromRequestToken == null || deviceDataSentAtHandShake!.Equals(deviceDataFromRequestToken) == false)
					throw new Exception();
			}
			catch
			{
				context.Result = new UnauthorizedResult();
			}
		}

		/// <summary>
		/// Получает значение из сессии по заданному ключу и десериализует в объект указанного типа
		/// </summary>
		/// <param name="key">Ключ для доступа к значению в сессии</param>
		/// <param name="type">Тип в объект которого надо десериализовать строку</param>
		/// <param name="context">Контекст для доступа к сессии</param>
		/// <returns>Объект десериализованного в указанный тип</returns>
		private object GetDataFromSession(string key, Type type, HttpContext context)
		{
			return JsonSerializer.Deserialize(context.Session.GetString(key)!, type)!;
		}
	}
}
