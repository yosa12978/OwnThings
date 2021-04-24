using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OwnThings.Core.Data;
using OwnThings.Core.Models;
using OwnThings.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OwnThings.Core.Repositories.Mocks
{
    public class UserRepository : IUserRepository
    {
        private readonly OwnThingsContext _db;
        private IOptions<AppSettings> _settings;
        private ILogger<UserRepository> _logger;
        public UserRepository(OwnThingsContext db, IOptions<AppSettings> settings, ILogger<UserRepository> logger)
        {
            _db = db;
            _settings = settings;
            _logger = logger;
        }

        public User Authenticate(string username, string password)
        {
            string password_hash = GenerateHash(password);
            User user = _db.users.FirstOrDefault(m => m.username == username && m.password == password_hash);

            if (user == null)
                return null;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_settings.Value.JWTSecret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.username), new Claim(ClaimTypes.Role, user.role) }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);
            _logger.LogInformation($"Returning token for user {username}");
            return user;
        }

        public User CreateUser(string username, string password)
        {
            if (_db.users.Any(m => m.username == username))
                return null;

            User user = new User
            {
                username = username,
                password = GenerateHash(password)
            };
            _db.users.Add(user);
            _db.SaveChanges();
            _logger.LogInformation($"Created user with username {username}");
            return user;
        }

        public string GenerateHash(string payload)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(payload));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    builder.Append(hashBytes[i].ToString("x2"));
                return builder.ToString();
            }
        }

        public User GetUserById(long id)
        {
            _logger.LogInformation($"Returning user with id {id}");
            return _db.users
                .Include(m => m.devices)
                .FirstOrDefault(m => m.id == id);
        }

        public User GetUserByName(string username)
        {
            _logger.LogInformation($"Returning user with username {username}");
            return _db.users
                .Include(m => m.devices)
                .FirstOrDefault(m => m.username == username);
        }

        public User GetUserByToken(string token)
        {
            _logger.LogInformation($"Returning user with token {token}");
            return _db.users
                .Include(m => m.devices)
                .FirstOrDefault(m => m.token == token);
        }
    }
}
