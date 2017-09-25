using System.Net.Http;
using System.Text;
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

        [Fact]
        public async void GetByIdReturnsNote()
        {
            var response = await _client.GetAsync("/api/notes/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var note = JsonConvert.DeserializeObject<Note>(responseString);
            Assert.Equal(1, note.Id);
            Assert.Equal("Note 1", note.Body);
        }

        [Fact]
        public async void CreateAddsNote()
        {
            var requestBody = JsonConvert.SerializeObject(new Note() {Body = "Write code"});
            var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/notes", httpContent);
            response.EnsureSuccessStatusCode();

            var url = response.Headers.Location;
            var todoItemResponse = await _client.GetAsync(url);
            var responseString = await todoItemResponse.Content.ReadAsStringAsync();

            var note = JsonConvert.DeserializeObject<Note>(responseString);

            Assert.Equal("Write code", note.Body);
            Assert.Equal(2, note.Id);
        }
    }
}