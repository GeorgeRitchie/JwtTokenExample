using Microsoft.AspNetCore.Mvc.Filters;

namespace EskhataDigital.Core.Validators
{
	/// <summary>
	/// Разрешает доступ анонимному пользователю
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class AllowAnonymousAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{

		}
	}
}
