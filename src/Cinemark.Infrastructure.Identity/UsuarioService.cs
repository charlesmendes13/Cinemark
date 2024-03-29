﻿using Cinemark.Domain.AggregatesModels.UsuarioAggregate;
using Cinemark.Infrastructure.Identity.Option;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cinemark.Infrastructure.Identity
{
    public class UsuarioService : IUsuarioService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expires;

        public UsuarioService(IOptions<JwtConfiguration> jwtOptions)
        {
            _key = jwtOptions.Value.Key;
            _issuer = jwtOptions.Value.Issuer;
            _audience = jwtOptions.Value.Audience;
            _expires = jwtOptions.Value.Expires;
        }

        public async Task<Token> CreateTokenAsync(string email, string senha)
        {
            try
            {
                var claims = new[]
                {
                     new Claim(ClaimTypes.Email, email),
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_key != null ? _key : "")
                    );

                var creds = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256
                    );

                var jwt = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    expires: DateTime.Now.AddMinutes(Convert.ToInt32(_expires)),
                    claims: claims,
                    signingCredentials: creds
                    );

                var accessKey = new JwtSecurityTokenHandler().WriteToken(jwt);
                var validTo = jwt.ValidTo.ToString();

                return await Task.FromResult(new Token(JwtBearerDefaults.AuthenticationScheme + " " + accessKey, validTo));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
