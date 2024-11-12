using CommonServices.EncyptionDecryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Claims
{
    public class ClaimValues : IClaimValues
    {
        private readonly IEncyptDecryptService _encyptDecryptService;
        private readonly IConfiguration _configuration;

        public ClaimValues(IEncyptDecryptService encyptDecryptService, IConfiguration configuration)
        {
            _encyptDecryptService = encyptDecryptService;
            _configuration = configuration;
        }

        public bool ValidateUser(string userId, string token)
        {
            string? secretKey = _configuration.GetSection("JwtSettings:Key").Value;
            string? issuer = _configuration.GetSection("JwtSettings:Issuer").Value;
            string? audience = _configuration.GetSection("JwtSettings:Audience").Value;
            
            string[] claimTypes = new string[]
            {
                ClaimTypes.NameIdentifier, // For example: user ID
                ClaimTypes.Role,           // For example: user role
            };

            string[] claims = GetClaimsFromToken(token, secretKey, issuer, audience, claimTypes);

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(claims[0]))
            {
                if (!string.IsNullOrEmpty(_encyptDecryptService.DecryptPayload(claims[0])) && _encyptDecryptService.DecryptPayload(claims[0]).Equals(userId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string[] GetClaimsFromToken(string token, string secretKey, string issuer, string audience, string[] claimTypes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            string[] claimValues = new string[claimTypes.Length];

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Iterate over the claimTypes array and extract values from the token's claims
                for (int i = 0; i < claimTypes.Length; i++)
                {
                    var claim = principal.FindFirst(claimTypes[i]);
                    claimValues[i] = claim != null ? claim.Value : string.Empty; // Return empty string if claim is not found
                }
            }
            catch
            {
                // Handle token validation failure, you can log or rethrow the exception based on your need
                //throw new SecurityTokenException("Invalid token", ex);
            }

            return claimValues; // Return the array of claim values
        }
    }
}
