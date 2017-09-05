using Newtonsoft.Json;

namespace UITestSampleApp.Shared
{
    public static class ConverterHelpers
    {
        public static string SerializeObject<T>(T objectToConvert) =>
            JsonConvert.SerializeObject(objectToConvert);

        public static T DeserializeObject<T>(string base64String) where T : class => 
            JsonConvert.DeserializeObject<T>(base64String);
    }
}
