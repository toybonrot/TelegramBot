using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class BaseHotel
    {
        public string Name { get; set; }
        public int Stars { get; set; }
        public double Score { get; set; }
        public int Reviews { get; set; }
        public int Hotel_id { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string PhotoURL { get; set; }
    }
}
