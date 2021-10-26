using Bits_on_chips_application.Models;
using Bits_on_chips_application.Models.ViewModels;
using Bits_on_chips_application.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Jwt
{
    /// <summary>
    /// The Jwt middleware used for validating the token and attaching the authenticated user to the
    /// HttpContext in order to make the user's details available to any code within the current request scope.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies["Authorization"]?.Split(" ").Last();
            if (token != null)
            {
                var jwtToken = ValidateToken(token);
                if (jwtToken != null)
                {
                    context.Request.Headers["Authorization"] = "Bearer " + token;
                    context.Items["Username"] = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
                    context.Items["FullName"] = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.GivenName).Value + " " +
                                                jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.FamilyName).Value;
                }
            }

            await _next(context);
        }

        public static JwtSecurityToken ValidateToken(string token)
        {
            // Token handler used in order to validate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            // Get the secret key from the jwtSettings instance
            var key = Encoding.ASCII.GetBytes(ConstantsJwt.Secret);

            try
            {
                // Validate the token and store it in the validatedToken variable
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // This is used so that the token expires exactly at its expiry time, not 5 minutes later
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch
            {
                // Return null if validation fails
                return null;
            }
        }

        public static UserResponseVM GenerateToken(ApplicationUser user, IList<string> userRoles)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantsJwt.Secret));

            var token = new JwtSecurityToken(
                ConstantsJwt.Issuer,
                ConstantsJwt.Audience,
                authClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new UserResponseVM
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        public static bool IsUserLoggedId(HttpContext context)
        {
            var token = context.Request.Cookies["Authorization"]?.Split(" ").Last();
            if (token != null)
            {
                var jwtToken = ValidateToken(token);
                if (jwtToken != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsUserInRole(HttpContext context, string role)
        {
            var token = context.Request.Cookies["Authorization"]?.Split(" ").Last();
            var jwtToken = ValidateToken(token);
            if (jwtToken != null)
            {
                return jwtToken.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == role);
            }
            return false;
        }

        public static string getUserId(HttpContext context)
        {
            var token = context.Request.Cookies["Authorization"]?.Split(" ").Last();
            var jwtToken = ValidateToken(token);
            if (jwtToken != null)
            {
                return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            }
            return null;
        }
    }
}
