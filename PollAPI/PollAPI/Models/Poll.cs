using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class Poll
    {
        public int PollId { get; set; }
        public string Naam { get; set; }

        public List<Antwoord> Antwoorden { get; set; }
        public List<PollGebruiker> PollGebruikers { get; set; }
    }
}
