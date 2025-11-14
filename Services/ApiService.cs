using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace VMS.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7001/api/"; // Change this to your API URL

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        // Get all users
        public async Task<List<Users>> GetUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}users");
                response.EnsureSuccessStatusCode();

                var users = await response.Content.ReadFromJsonAsync<List<Users>>();
                return users ?? new List<Users>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting users: {ex.Message}");
                return new List<Users>();
            }
        }

        // Get all visitors
        public async Task<List<Visitor>> GetVisitorsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}visitors");
                response.EnsureSuccessStatusCode();

                var visitors = await response.Content.ReadFromJsonAsync<List<Visitor>>();
                return visitors ?? new List<Visitor>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting visitors: {ex.Message}");
                return new List<Visitor>();
            }
        }

        // Add a new visitor
        public async Task<int> AddVisitorAsync(Visitor visitor)
        {
            try
            {
                var json = JsonSerializer.Serialize(visitor);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{BaseUrl}visitors", content);
                response.EnsureSuccessStatusCode();

                var newId = await response.Content.ReadFromJsonAsync<int>();
                return newId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding visitor: {ex.Message}");
                return -1;
            }
        }

        // Get visitor history
        public async Task<List<Visit>> GetVisitorHistoryAsync(int visitorId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}visitors/{visitorId}/history");
                response.EnsureSuccessStatusCode();

                var visits = await response.Content.ReadFromJsonAsync<List<Visit>>();
                return visits ?? new List<Visit>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting visitor history: {ex.Message}");
                return new List<Visit>();
            }
        }
    }
}