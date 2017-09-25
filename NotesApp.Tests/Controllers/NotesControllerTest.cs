using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NotesApp.Models;
using Xunit;

namespace NotesApp.Tests.Controllers
{
    public class NotesControllerTest
    {
        
        private TestServer _server;
        private HttpClient _client;

        public NotesControllerTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        
        [Fact]
        public async void GetAllReturnsList()
        {
            var response = await _client.GetAsync("/api/notes");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            
            var items = JsonConvert.DeserializeObject<Note[]>(responseString);
            Assert.Equal(1, items.Length);
            Assert.Equal(1, items[0].Id);
            Assert.Equal("Note 1", items[0].Body);
        }
    }
}