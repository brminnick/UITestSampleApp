using System;
using System.IO;
using BinaryFormatter;

namespace UITestSampleApp
{
	public static class ConverterHelpers
	{
		public static string ConvertSerializableObjectToBase64String<T>(T objectToConvert)
		{
			var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryConverter();

			var serializedObject = binaryFormatter.Serialize(objectToConvert);

			var objectDataAsBase64String = Convert.ToBase64String(serializedObject);

			return objectDataAsBase64String;
		}

		public static T ConvertBase64StringToObject<T>(string base64String) where T : class
		{
			var binaryFormatter = new BinaryConverter();

            var objectDataAsByteArray = Convert.FromBase64String(base64String);

			var objectDataAsType = binaryFormatter.Deserialize<T>(objectDataAsByteArray);

			return objectDataAsType;
		}
	}
}
