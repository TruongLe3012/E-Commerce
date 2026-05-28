using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop.Application.DTOs.Auth;
using Shop.Domain.Entities;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Services;

namespace Shop.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly IConfiguration
            _configuration;

        public AuthServiceTests()
        {
            var settings =
                new Dictionary<string, string>
                {
                    {
                        "Jwt:Key",
                        "THIS_IS_SUPER_SECRET_KEY_12345_VERY_LONG_KEY"
                    },
                    {
                        "Jwt:Issuer",
                        "ShopAPI"
                    },
                    {
                        "Jwt:Audience",
                        "ShopAPIUsers"
                    }
                };

            _configuration =
                new ConfigurationBuilder()
                    .AddInMemoryCollection(settings!)
                    .Build();
        }

        [Fact]
        public async Task Login_Should_Return_Token()
        {
            var options =
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(
                        databaseName:
                        Guid.NewGuid().ToString())
                    .Options;

            await using var context =
                new AppDbContext(options);

            var password =
                BCrypt.Net.BCrypt.HashPassword(
                    "123456");

            context.Users.Add(new User
            {
                FullName = "Test User",
                Email = "test@gmail.com",
                PasswordHash = password,
                Role = "Customer"
            });

            await context.SaveChangesAsync();

            var authService =
                new AuthService(
                    context,
                    _configuration);

            var result =
                await authService.LoginAsync(
                    new LoginDto
                    {
                        Email = "test@gmail.com",
                        Password = "123456"
                    });

            result.Should().NotBeNull();

            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task Login_Should_Fail_When_Password_Wrong()
        {
            var options =
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(
                        Guid.NewGuid().ToString())
                    .Options;

            await using var context =
                new AppDbContext(options);

            var password =
                BCrypt.Net.BCrypt.HashPassword(
                    "123456");

            context.Users.Add(new User
            {
                FullName = "Test",
                Email = "test@gmail.com",
                PasswordHash = password,
                Role = "Customer"
            });

            await context.SaveChangesAsync();

            var authService =
                new AuthService(
                    context,
                    _configuration);

            Func<Task> action = async () =>
            {
                await authService.LoginAsync(
                    new LoginDto
                    {
                        Email = "test@gmail.com",
                        Password = "wrongpassword"
                    });
            };

            await action.Should()
                .ThrowAsync<Exception>();
        }
        [Fact]
        public async Task Login_Should_Fail_When_User_Not_Found()
        {
            var options =
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(
                        Guid.NewGuid().ToString())
                    .Options;

            await using var context =
                new AppDbContext(options);

            var authService =
                new AuthService(
                    context,
                    _configuration);

            Func<Task> action = async () =>
            {
                await authService.LoginAsync(
                    new LoginDto
                    {
                        Email = "notfound@gmail.com",
                        Password = "123456"
                    });
            };

            await action.Should()
                .ThrowAsync<Exception>();
        }
    }
}