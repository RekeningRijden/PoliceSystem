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
    public class CarCalls
    {

        private static string baseUrl = "http://administration.s63a.marijn.ws/";

        /// <summary>
        /// Creates an async Get call
        /// </summary>
        /// <returns>A task with a fully filled <Car> object</returns>
        public async Task<Car> FillCarWithData(Car car)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/users/cars/" + car.LicencePlate + "/ownerships");
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
                    List<Ownership> ownerships = JsonConvert.DeserializeObject<List<Ownership>>(jsonResponse);

                    car.CurrentOwnership = ownerships[0];
                    car.PastOwnerships = ownerships;
                    car.PastOwnerships.RemoveAt(0);

                    return car;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Something went wrong with the API call. Status code: " + response.StatusCode);
                }
                throw new WebException("Something went wrong with the API call. Status code: " + response.StatusCode);
            }
        }

        public async Task<Car> GetCarWithLicencePlate(string licencePlate)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/users/cars/" + licencePlate);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
                    Car car = JsonConvert.DeserializeObject<Car>(jsonResponse);
                    return car;
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