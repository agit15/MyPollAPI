using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class DBInitializer
    {
        public static void Initialize(PollContext context)
        {
            context.Database.EnsureCreated();

            if (context.Polls.Any())
            {
                return;
            }

            Poll poll1 = new Poll { Naam = "Poll1" };
            Poll poll2 = new Poll { Naam = "Poll2" };
            Poll poll3 = new Poll { Naam = "Poll3" };
            Poll poll4 = new Poll { Naam = "Poll4" };
            Gebruiker gebruiker1 = new Gebruiker { Email = "gebruiker1@hotmail.com", Gebruikersnaam = "test1", Wachtwoord = "test" };
            Gebruiker gebruiker2 = new Gebruiker { Email = "gebruiker2@hotmail.com", Gebruikersnaam = "test2", Wachtwoord = "test" };
            PollGebruiker pollGebruiker1 = new PollGebruiker { Poll = poll1, Gebruiker = gebruiker1, Aanvaard = true };
            PollGebruiker pollGebruiker2 = new PollGebruiker { Poll = poll3, Gebruiker = gebruiker1, Aanvaard = false };
            PollGebruiker pollGebruiker3 = new PollGebruiker { Poll = poll2, Gebruiker = gebruiker2, Aanvaard = true };
            Antwoord antwoord1 = new Antwoord { Naam = "A", Poll = poll1 };
            Antwoord antwoord2 = new Antwoord { Naam = "B", Poll = poll1 };
            Antwoord antwoord3 = new Antwoord { Naam = "C", Poll = poll1 };
            Vriend vriend1 = new Vriend { Aanvaard = false, Ontvanger = gebruiker2, Verzender = gebruiker1 };


            Stem stem1 = new Stem { Antwoord = antwoord1, Gebruiker = gebruiker1 };
            Stem stem2 = new Stem { Antwoord = antwoord1, Gebruiker = gebruiker2 };

            context.Polls.AddRange(
                poll1, poll2, poll3, poll4
            );             
            
            context.Stemmen.AddRange(
                stem1, stem2
            );            
            
            context.PollGebruikers.AddRange(
                pollGebruiker1, pollGebruiker2, pollGebruiker3
            );

            context.Gebruikers.AddRange(
                gebruiker1, gebruiker2
            );

            context.Antwoorden.AddRange(
                antwoord1, antwoord2, antwoord3
            );            
            
            context.Vrienden.AddRange(
                vriend1
            );

            context.SaveChanges();
        }
    }
}

