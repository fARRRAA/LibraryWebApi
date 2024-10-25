using LibraryWebApi.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibraryWebApi.Services
{
    public  static class ApiFetch
    {
         private static readonly HttpClient _httpClient=new HttpClient();

        public static async Task<List<Books>> GetBooksFromApi(string url)
        {
            var response = await _httpClient.GetAsync(url);

            List<Books> books;
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<List<Books>>(jsonResponse);
            }
            else
            {
                throw new Exception($"Произошла ошибка при вызове API. Статус: {response.StatusCode}");
            }
            return books;
        }
    }
}
