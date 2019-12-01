using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class Antwoord
    {
        public int AntwoordId { get; set; }
        public string Naam { get; set; }
        public int PollId { get; set; }

        public Poll Poll { get; set; }

        public List<Stem> Stemmen { get; set; }
    }
}
