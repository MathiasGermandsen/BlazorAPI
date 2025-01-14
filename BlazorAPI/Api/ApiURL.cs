namespace BlazorAPI.Api.APIUrls
{
    public class APIUrls
    {
        private const string APIBaseUrl = "https://apicontrollers.onrender.com/api/";

        public string GetUserAPIUrl(string operation)
        {
            if (string.IsNullOrEmpty(operation))
                throw new ArgumentException("Operation cannot be null or empty.", nameof(operation));

            return $"{APIBaseUrl}My/{operation}";
        }
    }
}