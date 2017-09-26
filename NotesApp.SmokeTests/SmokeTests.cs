using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Xunit;

namespace NotesApp.SmokeTests
{
    public class SmokeTests
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public SmokeTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            _baseUrl = configuration["Endpoint"];
            _httpClient = new HttpClient();
        }

        [Fact]
        public async void GetReturnsListOfNotes()
        {
            var httpResponseMessage = await _httpClient.GetAsync(_baseUrl + "/api/notes");
            httpResponseMessage.EnsureSuccessStatusCode();
            
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var note = JArray.Parse(responseString)[0];
            
            Assert.Equal(1, note["id"]);
            Assert.Equal("Note 1", note["body"]);
        }

        [Fact]
        public async void GetByIdReturnsOneNote()
        {
            var response = await _httpClient.GetAsync(_baseUrl + "/api/notes/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var note = JObject.Parse(responseString);
            
            Assert.Equal(1, note["id"]);
            Assert.Equal("Note 1", note["body"]);
        }
    }
}