using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Authentication.Application.Interfaces;
using User.Authentication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace User.Authentication.Infrastructure
{
    public class IssueTokenInfra : IIssueTokenInfra
    {
        private readonly IConfiguration _configuration;

        public IssueTokenInfra(IConfiguration config)
        {
            _configuration = config; 
        }

        public async Task<string> IssueToken(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // Sets up the signing credentials using the above security key and specifying the HMAC SHA256 algorithm.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Defines a set of claims to be included in the token.
            var claims = new List<Claim>
            {
                // Custom claim using the user's ID.
                new Claim("Myapp_User_Id", user.Id.ToString()),
                // Standard claim for user identifier, using username.
                new Claim("username", user.Username),
                // Standard claim for user's email.
                new Claim(ClaimTypes.Email, user.Email),
                // Standard JWT claim for subject, using user ID.
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            // Adds a role claim for each role associated with the user.
            //user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            // Creates a new JWT token with specified parameters including issuer, audience, claims, expiration time, and signing credentials.
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration set to 1 hour from the current time.
                signingCredentials: credentials);
            // Serializes the JWT token to a string and returns it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
