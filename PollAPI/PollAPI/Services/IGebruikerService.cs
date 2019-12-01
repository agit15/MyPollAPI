using PollAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Services
{
    public interface IGebruikerService
    {
        Gebruiker Authenticate(string gebruikersnaam, string wachtwoord);
    }
}
