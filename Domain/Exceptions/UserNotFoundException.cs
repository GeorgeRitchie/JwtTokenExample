namespace EskhataDigital.Domain.Exceptions
{
	/// <summary>
	/// Исключение выбрасываемое если пользователь не был найден
	/// </summary>
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException()
		{

		}

		public UserNotFoundException(string msg) : base(msg)
		{

		}
	}
}
