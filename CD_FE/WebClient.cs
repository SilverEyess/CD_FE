using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CD_FE
{
    /// <summary>
    /// This static class will handle all Http requests to the api controller or backend.
    /// </summary>
    public static class WebClient
    {
        public static HttpClient ApiClient = new HttpClient();

        static WebClient()
        {
            ApiClient.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApiUrl"]);
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static class ApiRequest<T>
        {
            // DEPRECATED - To be deleted later
            public static HttpResponseMessage GetAll(string apiControllerName)
            {
                return WebClient.ApiClient.GetAsync(apiControllerName).Result;
            }

            /// <summary>
            /// Will return an IEnumerable of type T wherein T is the object model
            /// </summary>
            /// <param name="apiControllerName">The name of the api Controller</param>
            /// <returns>IEnumerable of type T</returns>
            public static IEnumerable<T> GetEnumerable(string apiControllerName)
            {
                return WebClient.ApiClient.GetAsync(apiControllerName).Result.Content.ReadAsAsync<IEnumerable<T>>().Result;
            }

            /// <summary>
            /// Will return an IList of type T wherein T is the object model
            /// </summary>
            /// <param name="apiControllerName">The name of the api Controller</param>
            /// <returns>IList of type T</returns>
            public static IList<T> GetList(string apiControllerName)
            {
                return WebClient.ApiClient.GetAsync(apiControllerName).Result.Content.ReadAsAsync<IList<T>>().Result;
            }

            /// <summary>
            /// Will return a single record of the object model 
            /// </summary>
            /// <param name="apiControllerNameWithId">The name of the api Controller</param>
            /// <returns>Object model</returns>
            public static T GetSingleRecord(string apiControllerNameWithId)
            {
                HttpResponseMessage response = WebClient.ApiClient.GetAsync(apiControllerNameWithId).Result;
                return response.Content.ReadAsAsync<T>().Result;

            }

            /// <summary>
            /// Will create a new record in the table using the object model
            /// </summary>
            /// <param name="apiControllerName">The name of the api Controller</param>
            /// <param name="model">The  model of type T</param>
            /// <returns>Object model</returns>
            public static T Post(string apiControllerName, T model)
            {
                HttpResponseMessage response = WebClient.ApiClient.PostAsJsonAsync(apiControllerName, model).Result;
                return response.Content.ReadAsAsync<T>().Result;
            }

            /// <summary>
            /// Will update an existing record based on the model submitted
            /// </summary>
            /// <param name="apiControllerNameWithId">The name of the api Controller</param>
            /// <param name="model">The model of type T</param>
            /// <returns>Either true of false if the update is successful or not</returns>
            public static bool Put(string apiControllerNameWithId, T model)
            {
                HttpResponseMessage response = WebClient.ApiClient.PutAsJsonAsync(apiControllerNameWithId, model).Result;
                return response.IsSuccessStatusCode;
            }

            /// <summary>
            /// Will delete the record based on the id passed in the parameter
            /// </summary>
            /// <param name="apiControllerNameWithId">The name of the api Controller with id</param>
            /// <returns>Object model</returns>
            public static T Delete(string apiControllerNameWithId)
            {
                HttpResponseMessage response = WebClient.ApiClient.DeleteAsync($"{apiControllerNameWithId}").Result;
                return response.Content.ReadAsAsync<T>().Result;
            }
        }
    }
}