using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UITestSampleApp.Shared
{
	public static class ConverterHelpers
	{
		public static string ConvertSerializableObjectToBase64String<T>(T objectToConvert)
		{
			var binaryFormatter = new BinaryFormatter();
			var memoryStream = new MemoryStream();

			binaryFormatter.Serialize(memoryStream, objectToConvert);

			var objectDataAsByteArray = memoryStream.ToArray();
			var objectDataAsBase64String = Convert.ToBase64String(objectDataAsByteArray);

			return objectDataAsBase64String;
		}

		public static T ConvertBase64StringToObject<T>(string base64String) where T : class
		{
			var objectDataAsByteArray = Convert.FromBase64String(base64String);

			var mStream = new MemoryStream();
			var binFormatter = new BinaryFormatter();

			mStream.Write(objectDataAsByteArray, 0, objectDataAsByteArray.Length);
			mStream.Position = 0;

			var objectDataAsType = binFormatter.Deserialize(mStream) as T;

			return objectDataAsType;
		}
	}
}
