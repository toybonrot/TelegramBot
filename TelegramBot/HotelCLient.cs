﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot;
using static TelegramBot.SearchHotel;

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
        public async Task<SearchHotel> GetHotel(string city, string arrival, string departure, long user, string filters, double priceMax, int page)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Search Hotel Controller?query={city}&arrival={arrival}&departure={departure}&user={user}" +
                $"&filters={filters}&priceMax={priceMax}&pageNum={page}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SearchHotel>(body);
            return result;
        }
        public async Task InsertHotel(SearchHotel hotel, long user, int i)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_address + $"/Add Hotel Controller?id={hotel.data.hotels[i].property.id}&user={user}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

        }
        public async Task<List<BaseHotel>> GetHotelList(long user)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Read Hotel List Controller?user={user}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<BaseHotel>>(body);
            return result;
        }
        public async Task DeleteHotel(List<BaseHotel> hotel, long user, int i)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_address + $"/Delete Hotel Controller?id={hotel[i].Hotel_id}&user={user}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

        }
        public async Task<string> GetFilterId(string filter)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Get Filter Controller?filter={filter}"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            var result = body;
            return result;
        }
        public async Task<List<string>> GetFilters()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_address + $"/Read Available Filters Controller"),
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<string>>(body);
            return result;
        }
    }
    
}
