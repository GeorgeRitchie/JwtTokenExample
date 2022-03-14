namespace EskhataDigital.Domain.AuthEntities
{
	/// <summary>
	/// Класс содержащий токены доступа и обновления
	/// </summary>
	public class AuthenticateResponse
	{
		/// <summary>
		/// AccessToken отправляемый клиенту
		/// </summary>
		public string AccessToken { get; set; }

		/// <summary>
		/// RefreshToken отправляемый клиенту
		/// </summary>
		public string RefreshToken { get; set; }
	}
}
