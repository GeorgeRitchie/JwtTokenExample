using EskhataDigital.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EskhataDigital.Core.Validators
{
	/// <summary>
	/// Авторизует аутентифицированного пользователя
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		private readonly string roleName;
		private readonly string controllerName;
		private readonly string actionName;

		/// <summary>
		/// <inheritdoc cref="AuthorizeAttribute"/>
		/// </summary>
		/// <param name="roleName">Роль которому должен соответствовать роль пользователя чтобы пройти авторизацию</param>
		/// <param name="controllerName">Имя контроллера которого должен иметь пользователь в списке <see cref="ControllerAccess"/> для прохождения авторизации</param>
		/// <param name="actionName">Имя действия которого должен иметь польователь в списке <see cref="ActionAccess"/> указанного <see cref="ControllerAccess"/> для прохождения авторизации</param>
		public AuthorizeAttribute(string roleName, string controllerName, string actionName = null!)
		{
			this.roleName = roleName;
			this.controllerName = controllerName;
			this.actionName = actionName;
		}

		/// <summary>
		/// <inheritdoc cref="AuthorizeAttribute"/>
		/// </summary>
		/// <param name="roleName">Роль которому должен соответствовать роль пользователя чтобы пройти авторизацию</param>
		public AuthorizeAttribute(string roleName) : this(roleName, null!, null!)
		{ }

		/// <summary>
		/// <inheritdoc cref="AuthorizeAttribute"/>
		/// </summary>
		/// <param name="controllerName">Имя контроллера которого должен иметь пользователь в списке <see cref="ControllerAccess"/> для прохождения авторизации</param>
		/// <param name="actionName">Имя действия которого должен иметь польователь в списке <see cref="ActionAccess"/> указанного <see cref="ControllerAccess"/> для прохождения авторизации</param>
		public AuthorizeAttribute(string controllerName, string actionName = null!) : this(null!, controllerName, actionName)
		{ }

		/// <summary>
		/// <inheritdoc cref="AuthorizeAttribute"/>
		/// </summary>
		public AuthorizeAttribute() : this(null!, null!, null!)
		{ }

		/// <summary>
		/// Авторизует пользователя по заданным в конструкторе параметрам
		/// </summary>
		/// <param name="context">Контекст связанный с текущим запросом</param>
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			try
			{
				var user = context.HttpContext.Items["User"] as User;

				if (DidUserLogout(user!))
				{
					throw new Exception();
				}

				if (roleName != null)
				{
					if (user!.Role.Name != roleName)
						throw new Exception();
				}

				if (controllerName != null)
				{
					var foundController = user!.Role.ControllerAccess.FirstOrDefault(u => u.SystemName == controllerName);
					if (foundController != null)
					{
						if (actionName != null && foundController.Actions.Any(u => u.SystemName == actionName) == false)
						{
							context.Result = new ForbidResult();
						}
					}
					else
					{
						context.Result = new ForbidResult();
					}
				}
			}
			catch
			{
				context.Result = new UnauthorizedResult();
			}
		}

		/// <summary>
		/// Проверяет выполнил ли пользователь "выход"
		/// </summary>
		/// <param name="user">Пользователь</param>
		/// <returns><see cref="true"/> если пользователь выполнил "выход", иначе <see cref="false"/></returns>
		private bool DidUserLogout(User user)
		{
			return string.IsNullOrEmpty(user!.UserAuthenticationData.RefreshToken);
		}
	}
}
