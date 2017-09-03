using Newtonsoft.Json;

namespace UITestSampleApp.Shared
{
    public static class ConverterHelpers
    {
        public static string ConvertSerializableObjectToBase64String<T>(T objectToConvert) =>
            JsonConvert.SerializeObject(objectToConvert);

        public static T ConvertBase64StringToObject<T>(string base64String) where T : class => 
            JsonConvert.DeserializeObject(base64String) as T;
    }
}
