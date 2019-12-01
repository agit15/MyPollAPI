using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class Stem
    {
        public int StemId { get; set; }
        public int AntwoordId { get; set; }
        public Antwoord Antwoord { get; set; }
        public int GebruikerId { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}
