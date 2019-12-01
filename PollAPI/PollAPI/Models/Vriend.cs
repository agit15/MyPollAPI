using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class Vriend
    {
        public int VriendId { get; set; }
        public int VerzenderId { get; set; }
        public int OntvangerId { get; set; }
        public bool Aanvaard { get; set; }
        public Gebruiker Verzender { get; set; }
        public Gebruiker Ontvanger { get; set; }
    }
}
