using BoardgameMeetup.Api.IntegrationTests.Helpers;
using BoardgameMeetup.Api.Models.Login;
using BoardgameMeetup.Api.Models.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;

namespace BoardgameMeetup.Api.IntegrationTests.Common
{

    public class ApiServer : IDisposable
    {
        private IConfigurationRoot _config; 
        public const string Username = "admin";
        public const string Password = "admin";
        public HttpClient Client { get; private set; }
        public TestServer Server { get; private set; }

        public ApiServer()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = GetAuthenticatedClient(Username, Password);
        }

        public HttpClient GetAuthenticatedClient(string username, string password)
        {
            var client = Server.CreateClient();
            var response = client.PostAsync("/api/Login/Authenticate",
                    new JsonContent(new LoginModel 
                    { 
                        Username = username,
                        Password = password
                    })
                ).Result;

            response.EnsureSuccessStatusCode();

            var data = JsonConvert.DeserializeObject<UserWithTokenModel>(
                response.Content.ReadAsStringAsync().Result);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer" + data.Token);
            return client;
        }

        public void Dispose()
        {
            if(Client != null)
            {
                Client.Dispose();
                Client = null;
            }

            if(Server != null)
            {
                Server.Dispose();
                Server = null;
            }
        }
    }
}
