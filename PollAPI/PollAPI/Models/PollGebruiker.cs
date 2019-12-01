using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class PollGebruiker
    {
        public int PollGebruikerId { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; }
        public int GebruikerId { get; set;}
        public Gebruiker Gebruiker { get; set; }
        public bool Aanvaard { get; set; }
    }
}
