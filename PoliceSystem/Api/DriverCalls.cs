using Newtonsoft.Json;
using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PoliceSystem.Api
{
    public class DriverCalls
    {
        private static string baseUrl = "http://administration.s63a.marijn.ws/";

        /// <summary>
        /// Creates an async Get call, If await client.GetAsync doesn't work, it's because of a deadlock. See AccountController/Test
        /// </summary>
        /// <returns>A task with list<Driver></returns>
        public async Task<List<Driver>> GetAllDrivers()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/users?pageIndex=0&pageSize=50");
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
                    List<Driver> drivers = JsonConvert.DeserializeObject<List<Driver>>(jsonResponse);

                    return drivers;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Something went wrong with the API call. Status code: " + response.StatusCode);
                }
                throw new WebException("Something went wrong with the API call. Status code: " + response.StatusCode);
            }
        }

    }
}