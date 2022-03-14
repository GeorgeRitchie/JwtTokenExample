namespace EskhataDigital.Domain.AuthEntities
{
	/// <summary>
	/// Класс представляющий устройство с которого было принят первый запрос
	/// </summary>
	public class Device
	{
		/// <summary>
		/// Секретное ключ-значение
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Имя устройства
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Тип устройства
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Имя приложения
		/// </summary>
		public string AppName { get; set; }

		/// <summary>
		/// Тип приложения
		/// </summary>
		public string AppType { get; set; }

		/// <inheritdoc cref="object.Equals(object?)"/>
		public override bool Equals(object? obj)
		{
			var otherDevice = obj as Device;

			if (otherDevice == null) return false;

			if (otherDevice.Id == Id &&
				otherDevice.Name == Name &&
				otherDevice.Type == Type &&
				otherDevice.AppName == AppName &&
				otherDevice.AppType == AppType)
				return true;

			return false;
		}
	}
}
