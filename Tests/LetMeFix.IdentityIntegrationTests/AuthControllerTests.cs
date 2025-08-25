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
        public async Task Profile_ReturnsUserInfo()
        {
            var registerRequest = new
            {
                Email = "newuser@gmail.com",
                UserName = "newuser",
                Name = "New",
                Lastname = "User",
                Password = "1234Xyz!"
            };

            var registerResponse = await _httpClient.PostAsJsonAsync("/api/identity/register", registerRequest);
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);


            var loginRequest = new
            {
                Email = "newuser@gmail.com",
                UserName = "string",
                Password = "1234Xyz!"
            };

            var loginResponse = await _httpClient.PostAsJsonAsync("/api/identity/login", loginRequest);
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            var json = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
            Assert.False(string.IsNullOrEmpty(json.Token));


            _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", json.Token);


            var profileResponse = await _httpClient.GetAsync("/api/identity/profile");
            Assert.Equal(HttpStatusCode.OK, profileResponse.StatusCode);

            var profileJson = await profileResponse.Content.ReadFromJsonAsync<UserProfileResponse>();
            Assert.Equal("newuser@gmail.com", profileJson.Email);
            Assert.Equal("newuser", profileJson.UserName);
        }
    }
}

public class LoginResponse
{
    public string Token { get; set; }
}

public class UserProfileResponse
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}