﻿using Newtonsoft.Json;

namespace UITestSampleApp.Common
{
	public static class ConverterHelpers
	{
		public static string ConvertSerializableObjectToString<T>(T objectToConvert) =>
            JsonConvert.SerializeObject(objectToConvert);

        public static T ConvertStringToObject<T>(string stringToConvert) where T : class =>
            JsonConvert.DeserializeObject<T>(stringToConvert);
	}
}
