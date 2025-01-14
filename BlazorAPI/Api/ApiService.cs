using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAPI.Classes;
using Newtonsoft.Json;
using BlazorAPI.Api.APIUrls;
using System.Collections.Generic;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly APIUrls _apiUrls;
    private const int PageSize = 100;
    private readonly ApiCache<List<ApiClass.User>> _cache;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiUrls = new APIUrls();
        _cache = new ApiCache<List<ApiClass.User>>(TimeSpan.FromMinutes(5)); // Cache duration
    }

    public async Task<List<ApiClass.User>> GetUserAsync(int pageNumber)
    {
        // Ensure pageNumber is valid
        if (pageNumber <= 0) throw new ArgumentException("Page number must be greater than zero.", nameof(pageNumber));
        
        string cacheKey = $"users_page_{pageNumber}";
        
        if (_cache.TryGetFromCache(cacheKey, out var cachedUsers))
        {
            return cachedUsers;
        }

        // Construct URL
        string baseUrl = _apiUrls.GetUserAPIUrl("read");
        string urlParams = $"pageSize={PageSize}&pageNumber={pageNumber}";
        string constructedUrl = $"{baseUrl}?{urlParams}";

        // Fetch and deserialize the response
        try
        {
            string apiResponse = await _httpClient.GetStringAsync(constructedUrl);
            List<ApiClass.User> userList = JsonConvert.DeserializeObject<List<ApiClass.User>>(apiResponse);
            
            
            if (userList != null)
            {
                _cache.AddToCache(cacheKey, userList);
            }
            
            return userList ?? new List<ApiClass.User>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error fetching data: {ex.Message}", ex);
        }
    }
}