using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class BaseAttraction
    {
        public string user { get; set; }
        public string city { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string currency { get; set; }
        public string photoURL { get; set; }
        public double score { get; set; }
    }
}
