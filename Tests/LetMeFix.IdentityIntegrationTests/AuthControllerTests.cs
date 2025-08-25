using LetMeFix.Domain.Entities;
using LetMeFix.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.IdentityIntegrationTests
{
    public class AuthControllerTests : IClassFixture<AuthWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public AuthControllerTests(AuthWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Register()
        {
            var request = new
            {
                Email = "newuser@gmail.com",
                UserName = "newuser",
                Name = "New",
                Lastname = "User",
                Password = "1234Xyz!"
            };

            var response = await _httpClient.PostAsJsonAsync("/api/identity/register", request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}