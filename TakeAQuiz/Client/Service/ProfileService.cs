using System.Net.Http.Json;
using TakeAQuiz.Shared.ViewModels;

namespace TakeAQuiz.Server.Service
{
    public class ProfileService
    {
        private readonly HttpClient _httpClient;

        public ProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserViewModel> GetUserData()
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>("api/profile/getuserdata");
        }
    }
}
