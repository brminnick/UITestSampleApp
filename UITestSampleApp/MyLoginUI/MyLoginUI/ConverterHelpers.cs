using Newtonsoft.Json;

namespace MyLoginUI
{
	public static class ConverterHelpers
	{
		public static string ConvertSerializableObjectToString<T>(T objectToConvert) =>
            JsonConvert.SerializeObject(objectToConvert);

        public static T ConvertStringToObject<T>(string stringToConvert) where T : class =>
            JsonConvert.DeserializeObject<T>(stringToConvert);
	}
}
