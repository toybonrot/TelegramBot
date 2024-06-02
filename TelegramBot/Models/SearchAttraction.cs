using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    public class SearchAttraction
    {
        public DataAtt data { get; set; }
        public class DataAtt
        {
            public Product[] products { get; set; }
        }
        public class Product
        {
            public string id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string shortDescription { get; set; }
            public Representativeprice representativePrice { get; set; }
            public Primaryphoto primaryPhoto { get; set; }
            public Reviewsstats reviewsStats { get; set; }
            public Ufidetails ufiDetails { get; set; }
        }
        public class Representativeprice
        {
            public string __typename { get; set; }
            public float chargeAmount { get; set; }
            public string currency { get; set; }
            public float publicAmount { get; set; }
        }

        public class Primaryphoto
        {
            public string __typename { get; set; }
            public string small { get; set; }
        }

        public class Reviewsstats
        {
            public string __typename { get; set; }
            public int allReviewsCount { get; set; }
            public string percentage { get; set; }
            public Combinednumericstats combinednumericstats { get; set; }
        }

        public class Combinednumericstats
        {
            public string __typename { get; set; }
            public float average { get; set; }
            public int total { get; set; }
        }

        public class Ufidetails
        {
            public string __typename { get; set; }
            public string bCityName { get; set; }
            public int ufi { get; set; }
            public Url url { get; set; }
        }

        public class Url
        {
            public string __typename { get; set; }
            public string country { get; set; }
        }
    }
}
