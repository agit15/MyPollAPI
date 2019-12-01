﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PollAPI.Helpers;
using PollAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PollAPI.Services
{
    public class GebruikerService : IGebruikerService
    {
        private readonly AppSettings _appSettings;
        private readonly PollContext _pollContext;

        public GebruikerService(IOptions<AppSettings> appSettings, PollContext pollContext)
        {
            _appSettings = appSettings.Value;
            _pollContext = pollContext;
        }
        public Gebruiker Authenticate(string gebruikersnaam, string wachtwoord)
        {
            var gebruiker = _pollContext.Gebruikers.SingleOrDefault(x => x.Gebruikersnaam == gebruikersnaam && x.Wachtwoord == wachtwoord);

            // return null if user not found
            if (gebruiker == null)
                return null;

            // authentication successful so generate jwttoken
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("GebruikerId", gebruiker.GebruikerId.ToString()),
                    new Claim("Email", gebruiker.Email),
                    new Claim("Gebruikersnaam", gebruiker.Gebruikersnaam)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            gebruiker.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            gebruiker.Wachtwoord = null;
            return gebruiker;
        }
    }
}
