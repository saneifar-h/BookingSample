using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BookingSample.WebApi.Models.AuthSrv
{
    public interface IAuthService
    {
        string CreateTokenFor(string username, string password);
        ClaimsPrincipal GetPrincipal(string token);
        bool IsValidUser(string username, string password);
    }

    public class AuthService : IAuthService
    {
        private const string Secret =
            "ZGIzT0lzaitCWEU5TlpEeTB0OFczVGNOZWtyRisyZC8xc0ZuV0c0SG5WOFRaWTMwaVRPZHRWV0pHOGFiV3ZCMUdsT2dKdVFaZGNGMkx1cW0vaGNjTXc9PQ==";

        private readonly string testPassword = "TestPass";
        private readonly int testUserId = 100;


        private readonly string testUsername = "TestUser";

        public string CreateTokenFor(string username, string password)
        {
            if (username != testUsername || password != testPassword)
                throw new Exception("Invalid Username");
            var token = GenerateToken(username, testUserId, DateTime.Now.AddMinutes(30));
            return token;
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public bool IsValidUser(string username, string password)
        {
            return username == testUsername && password == testPassword;
        }

        private static string GenerateToken(string username, int userId, DateTime expireTime)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = expireTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature),
                NotBefore = DateTime.MinValue,
            };


            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}