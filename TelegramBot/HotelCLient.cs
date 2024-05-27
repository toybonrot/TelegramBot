using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    internal class HotelClient
    {
        private static string _address;
        private static string _apihost;

        public HotelClient()
        {
            _address = Constants.ApiAddress;
            _apihost = Constants.ApiHost;
        }
        public async Task<SearchHotel> GetHotel(string city, string arrival, string departure)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Search Hotel Controller/{city}/{arrival}/{departure}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SearchHotel>(body);
            return result;
        }
    }
}
