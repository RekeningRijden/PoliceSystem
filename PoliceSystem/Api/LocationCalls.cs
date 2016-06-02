using Newtonsoft.Json;
using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PoliceSystem.Api
{
    public class LocationCalls
    {
        private static string baseUrl = "http://movement.s63a.marijn.ws/";

        /// <summary>
        /// Creates an async Get call
        /// </summary>
        /// <returns>A task with a fully filled <Car> object</returns>
        public async Task<List<TrackingPeriod>> GetAllTrackingPeriodsFromUri(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("RESPONSE: " + jsonResponse);
                    List<TrackingPeriod> trackingPeriods = JsonConvert.DeserializeObject<List<TrackingPeriod>>(jsonResponse);
                    
                    return trackingPeriods;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Something went wrong with the API call. Status code: " + response.StatusCode);
                }
                throw new WebException("Something went wrong with the API call. Status code: " + response.StatusCode);
            }
        }

        public async Task<List<TrackingPeriod>> GetTrackingPeriodsForCar(int cartrackerId)
        {
            string uri = "api/trackers/" + cartrackerId;
            return await GetAllTrackingPeriodsFromUri(uri);
        }

        public async Task<List<TrackingPeriod>> GetTrackingPeriodsForCarWithinPeriod(int cartrackerId, string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            startDate = start.ToString("yyyy-MM-dd");
            endDate = end.ToString("yyyy-MM-dd");
            string uri = "api/trackers/" + cartrackerId + "/movements/_byperiod?endDate=" + endDate + "&startDate=" + startDate;
            return await GetAllTrackingPeriodsFromUri(uri);
        }
    }
}