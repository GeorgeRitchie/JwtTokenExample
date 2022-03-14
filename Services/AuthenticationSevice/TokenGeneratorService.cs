using EskhataDigital.Domain.AuthEntities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EskhataDigital.Services.AuthenticationSevice
{
	/// <summary>
	/// Сервис для генерации токена
	/// </summary>
	public class TokenGeneratorService
	{
		/// <summary>
		/// Генерирует токен с указанным периодом жизни и клеймами
		/// </summary>
		/// <param name="lifeTime">Период жизни токена</param>
		/// <param name="claims">Клеймы которые включаются в токен</param>
		/// <returns>Токен</returns>
		public string Generate(int lifeTime, IEnumerable<Claim> claims = null!)
		{
			JwtSecurityToken securityToken = new(
				issuer: AuthOptions.ISSUER,
				audience: AuthOptions.AUDIENCE,
				claims: claims,
				notBefore: DateTime.Now,
				expires: DateTime.Now.AddMinutes(lifeTime),
				signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(securityToken);
		}
	}
}
