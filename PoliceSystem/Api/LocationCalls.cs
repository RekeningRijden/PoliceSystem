using Newtonsoft.Json;
using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        /// <summary>
        /// Find the coordinates of a Address by requesting a google maps transform.
        /// </summary>
        /// <param name="address">to get the coordinates from</param>
        /// <returns>a Position object with the found coordinates</returns>
        public async Task<Position> GetPosition(Address address)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string requestLink = "/maps/api/geocode/json?address=";
                requestLink += address.StreetNr + "+";
                requestLink += address.Street + ",+";
                requestLink += address.City + ",+";
                requestLink += address.Country;
                requestLink += "&key=" + "AIzaSyAOneQLa8rzsdenerJ3-UFY0bvjnorzKFg";

                decimal lat = 0;
                decimal lng = 0;

                HttpResponseMessage response = await client.GetAsync(requestLink);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(jsonResponse)))
                    {
                        reader.SupportMultipleContent = true;

                        Boolean readingLat = false;
                        Boolean readingLng = false;

                        while (reader.Read())
                        {
                            if (reader.Value != null)
                            {
                                if (readingLat)
                                {
                                    lat = Decimal.Parse(reader.Value.ToString());
                                    readingLat = false;
                                }
                                if (readingLng)
                                {
                                    lng = Decimal.Parse(reader.Value.ToString());
                                    break;
                                }

                                if (reader.Value.ToString() == "lat")
                                    readingLat = true;
                                if (reader.Value.ToString() == "lng")
                                    readingLng = true;
                            }
                        }
                    }

                    return new Position(lat, lng);
                }
            }

            return null;
        }

        /// <summary>
        /// Find the Address of a set of coordinates by requesting a google maps transform.
        /// </summary>
        /// <param name="position">to get the Address from</param>
        /// <returns>an Address object</returns>
        public async Task<Address> GetAddress(Position position)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string requestLink = "/maps/api/geocode/json?latlng=";
                requestLink += position.Latitude.ToString(CultureInfo.InvariantCulture) + ",";
                requestLink += position.Longitude.ToString(CultureInfo.InvariantCulture);
                requestLink += "&key=" + "AIzaSyAOneQLa8rzsdenerJ3-UFY0bvjnorzKFg";

                Address address = new Address();

                HttpResponseMessage response = await client.GetAsync(requestLink);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    using (JsonTextReader reader = new JsonTextReader(new StringReader(jsonResponse)))
                    {
                        reader.SupportMultipleContent = true;

                        Boolean canRead = false;
                        int i = 0;

                        while (reader.Read())
                        {
                            if (reader.Value != null)
                            {
                                if (canRead)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            address.StreetNr = reader.Value.ToString();
                                            break;
                                        case 1:
                                            address.Street = reader.Value.ToString();
                                            break;
                                        case 2:
                                            address.City = reader.Value.ToString();
                                            break;
                                        case 3:
                                            address.Country = reader.Value.ToString();
                                            break;
                                    }

                                    i++;
                                    if (i == 4)
                                        break;
                                }

                                canRead = reader.Value.ToString() == "long_name";
                            }
                        }
                    }

                    return address;
                }
            }

            return null;
        }
    }
}