using System;
using System.Net;
using System.Threading.Tasks;

namespace StattoBot
{
    /// <summary>
    /// Generate URL and send request to REST API and wait for response
    /// </summary>
    public class BigQueryApi
    {
        /// <summary>
        /// Send request to REST API which then queries BigQuery and sends back a response
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        public static async Task<string> GetBQResponseAsync(string apiRequest)
        {
            // Check if API request is null
            if (string.IsNullOrWhiteSpace(apiRequest))
                return null;

            // Contruct API URL
            Uri baseApiUri = new Uri("http://bfbotapi.azurewebsites.net/api/");
            Uri fullApiUri = new Uri(baseApiUri, apiRequest);

            string apiResponse = "Computer says no!";
            // Attempt to get an API response
            using (WebClient client = new WebClient())
            {
                try
                {
                    apiResponse = await client.DownloadStringTaskAsync(fullApiUri).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    apiResponse = "Error";//"\nException Message: " + ex.Message;
                }
            }

            return apiResponse;
        }
    }
}