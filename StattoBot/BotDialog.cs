using System;
using Microsoft.Bot.Builder.Luis;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using System.Globalization;

namespace StattoBot
{
    /// <summary>
    /// Create a Bot dialog that utilises the LUIS natural language processing cloud service.
    /// </summary>
    // LUIS.ai Model ID and Subscription Key
    [LuisModel("396f7599-acbd-448c-bfe4-682f575ea81b", "4e15959d5b404787b1b3a40cca4fdd15")]
    [Serializable]
    public class BotDialog : LuisDialog<object>
    {
        public string IntentName = "";
        public const string DefaultTeam = "unknown";
        public const string DefaultDate = "1999";
        public const string Entity_Team = "Team";
        public const string Entity_Date = "Date";
        public string chatResponse = "Hi! ";

        /// <summary>
        /// LUIS Intent Find Volume of bets
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("FindVol")]
        public async Task FindVol(IDialogContext context, LuisResult result)
        {
            IntentName = "volume";
            chatResponse = "";

            // Check if team Entity has been captured by the LUIS model
            EntityRecommendation team;
            if (!result.TryFindEntity(Entity_Team, out team))
            {
                team = new EntityRecommendation(type: Entity_Team) { Entity = DefaultTeam };
                chatResponse = chatResponse + $"I didn't recognise a team. ";
            }

            // Check if date Entity has been captured by the LUIS model
            EntityRecommendation date;
            if (!result.TryFindEntity(Entity_Date, out date))
            {
                date = new EntityRecommendation(type: Entity_Date) { Entity = DefaultDate };
                chatResponse = chatResponse + $"I'm not sure which year you want? ";
            }

            // Check if either of the Entities were not recognised
            if ((team.Entity == DefaultTeam) || (date.Entity == DefaultDate))
            {
                await context.PostAsync(chatResponse + $"Please ask me again.");
                // Wait for Bot input
                context.Wait(MessageReceived);
            }
            else
            {
                // Fuzzy Matching test
                // Unfortunately this would not work when envoked from the Bot Dialog, only from the test.aspx page.
                // Find first team only if more than one is recognised by Luis as api expects single "team" parameter.
                //string api_Team = "";
                //api_Team = FuzzyMatching.DoSearch(team.Entity.ToLower());

                // Generate API URL string
                string api_request = string.Join("/", IntentName, team.Entity, date.Entity);
                string api_response = "Computer says no!";
                // Send partial URL to the API
                api_response = await GetBQResult(api_request);

                // Check if response is an error
                if (api_response == "Error" || api_response == null || api_response == "")
                {
                    // Output to Bot
                    await context.PostAsync("I did not understand your request.");
                }
                else
                {
                    // Output to Bot
                    await context.PostAsync(api_response + " bets were placed on " + team.Entity + " in " + date.Entity + ".");
                }
                // Wait for Bot input
                context.Wait(MessageReceived);
            }
        }

        /// <summary>
        /// LUIS Intent to find the total monetary amount of bets
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("FindAmount")]
        public async Task FindAmount(IDialogContext context, LuisResult result)
        {
            IntentName = "BetValue";

            // Check if team Entity has been captured by the LUIS model
            EntityRecommendation team;
            if (!result.TryFindEntity(Entity_Team, out team))
            {
                team = new EntityRecommendation(type: Entity_Team) { Entity = DefaultTeam };
                chatResponse = chatResponse + $"I didn't recognise a team. ";
            }
            
            // Check if date Entity has been captured by the LUIS model
            EntityRecommendation date;
            if (!result.TryFindEntity(Entity_Date, out date))
            {
                date = new EntityRecommendation(type: Entity_Date) { Entity = DefaultDate };
                chatResponse = chatResponse + $"I'm not sure which year you want? ";
            }

            // Check if either of the Entities were not recognised
            if ((team.Entity == DefaultTeam) || (date.Entity == DefaultDate))
            {
                await context.PostAsync(chatResponse + $"Please ask me again.");
                // Wait for Bot input
                context.Wait(MessageReceived);
            }
            else
            {
                // Generate API URL string
                string api_request = string.Join("/", IntentName, team.Entity, date.Entity);
                string api_response = "Computer says no!";
                // Send partial URL to the API
                api_response = await GetBQResult(api_request);

                // Check if response is an error
                if (api_response == "Error" || api_response == null || api_response == "")
                {
                    // Output to Bot
                    await context.PostAsync("I did not understand your request.");
                }
                else
                {
                    // Output to Bot
                    await context.PostAsync(api_response + " was gambled on " + team.Entity + " in " + date.Entity + ".");
                }
                // Wait for Bot input
                context.Wait(MessageReceived);
            }
        }

        /// <summary>
        /// Default LUIS Intent, reached if no other Intent is envoked
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Computer says no!";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        /// <summary>
        /// Send API request to BigQuery
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        private async Task<string> GetBQResult(string apiRequest)
        {
            string strResult = string.Empty;
            string apiResponse = await BigQueryApi.GetBQResponseAsync(apiRequest);

            // Remove quotes from response
            apiResponse = apiResponse.Replace("\"", "");
            /*if (!Char.IsNumber(apiResponse, 1))
            {
                return apiResponse;//"Error";
            }*/

            int i = 0;
            double d = 0;
            // Try to Parse the response as an int
            if (int.TryParse(apiResponse, out i))
            {
                strResult = i.ToString("n0"); // Format with commas between thousands
            }
            // Try to Parse the response as a double
            else if (Double.TryParse(apiResponse, out d))
            {
                // Format result as a currency value using the £ symbol
                strResult = string.Format(CultureInfo.CreateSpecificCulture("en-GB"), "{0:C}", d);
            }

            return strResult;
        }

        public BotDialog(ILuisService service = null)
            : base(service)
        {
        }
    }
}
