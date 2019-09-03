using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ItersTutoriov1.Helper;
using ItersTutoriov1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ItersTutoriov1.Services
{
    public interface ISigninService
    {
        User Authenticate(string username, string password);
    }

    public enum UserStatus
    {
        SubscriptionStatusUnverified = 1,
        SubscriptionStatusActive = 2,
        SubscriptionStatusInactive = 3,
        SubscriptionStatusUnapproved = 4
    }

    public enum Role
    {
        Student,
        Instructor
    }

    public class SigninService : ISigninService
    {
        private readonly ITERSTutoriov10Context _db;
        private readonly IConfiguration _configuration;

        public SigninService(ITERSTutoriov10Context db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public User Authenticate(string username, string password)
        {
            if (username == null || password == null) return null;
            var salt = (from i in _db.TbSubscriptions
                        where string.Equals(i.Email, username, StringComparison.OrdinalIgnoreCase)
                        select i.Salt).FirstOrDefault();

            if (salt == null) return null;
            var subscription = (from i in _db.TbSubscriptions
                                where i.SubscriptionStatusId == (int)UserStatus.SubscriptionStatusActive
                                      && i.Email == username
                                      && i.Password == Security.EncryptPassword(password, salt)
                                select i).FirstOrDefault();

            if (subscription == null) return null;
            var token = (from i in _db.TbSubscriptionTokens
                         where i.Email == subscription.Email && i.ExpiresOn > DateTime.Now
                         select i).FirstOrDefault();
            if (token != null)
                return new User
                {
                    UniqueId = subscription.UniqueId,
                    FirstName = subscription.FirstName,
                    LastName = subscription.LastName,
                    Username = subscription.Email,
                    Role = subscription.GroupId,
                    Token = token.AuthToken
                };

            // Generate JWT Token
            double.TryParse(_configuration["Settings:AuthTokenExpiry"], out var authTokenExpiry);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Settings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, subscription.UniqueId.ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(authTokenExpiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            // Add Token in Database
            var tbSubscriptionTokens = new TbSubscriptionTokens
            {
                AuthToken = tokenHandler.WriteToken(jwtToken),
                Email = subscription.Email,
                Id = subscription.UniqueId.ToString(),
                IssuedOn = DateTime.Now,
                ExpiresOn = DateTime.Now.AddSeconds(authTokenExpiry)
            };
            _db.TbSubscriptionTokens.Add(tbSubscriptionTokens);
            _db.SaveChanges();

            token = new TbSubscriptionTokens()
            {
                AuthToken = tbSubscriptionTokens.AuthToken
            };

            return new User
            {
                UniqueId = subscription.UniqueId,
                FirstName = subscription.FirstName,
                LastName = subscription.LastName,
                Username = subscription.Email,
                Role = subscription.GroupId,
                Token = token.AuthToken
            };

        }
    }
}
