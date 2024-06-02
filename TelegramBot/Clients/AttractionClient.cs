using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Models;

namespace TelegramBot.Clients
{
    public class AttractionClient
    {
        private static string _address;
        private static string _apihost;
        public AttractionClient()
        {
            _address = Constants.ApiAddress;
            _apihost = Constants.ApiHost;
        }
        public async Task<SearchAttraction> GetAttractions(string city, string arrival, string departure, string currency, long user)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Attraction Controller/Search Attraction?query={city}&arrival={arrival}&" +
                $"departure={departure}&currency={currency}&user={user}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SearchAttraction>(body);
            return result;
        }
        public async Task InsertAttraction(string id, long user)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_address + $"/Attraction Controller/Add Attraction?id={id}&user={user}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
        public async Task<List<BaseAttraction>> ReadAttractionList(long user)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Attraction Controller/Get Attraction List?user={user}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<BaseAttraction>>(body);
            return result;
        }
        public async Task DeleteAttraction(string id, long user)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_address + $"/Attraction Controller/Delete Attraction?id={id}&user={user}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
        public async Task<AttractionDetails> GetAttractionDetails(string slug, string currency)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Attraction Controller/Get Attraction Details?slug={slug}&currency={currency}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AttractionDetails>(body);
            return result;
        }
    }
}
