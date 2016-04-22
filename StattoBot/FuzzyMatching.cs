using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FuzzyString;

namespace StattoBot
{
    /// <summary>
    /// Class to perform Fuzzy Matching routines using the FuzzyString package 
    /// </summary>
    public class FuzzyMatching
    {
        /// <summary>
        /// Perform a Fuzzy Search of the submitted team against a list of team names to find the best match. 
        /// </summary>
        /// <param name="submittedTeam"></param>
        /// <returns></returns>
        public static string DoSearch(string submittedTeam)
        {
            double ratOberDist = 999/*, jaccardDist = 999*/;
            //string overlapCoeff = "", lcSubseqDist = "", lcSubstrDist = "", levDist = "", normLevDist = "";
            //string hammDist = "", jaroWinkDist = "", sorensenDist = "", tanimotoDist = "";

            // Read list of team names from CSV generated from the BigQuery Betfair dataset
            List<string> apiTeams = File.ReadLines(HttpContext.Current.Server.MapPath("soccer-teams-top500.txt")).ToList();
            
            // Check if the submitted team exactly matches any of the team names in the list and return if match found 
            if (apiTeams.Contains(submittedTeam))
                return submittedTeam;

            // If exact match not found, then create a KeyValuePair List
            var approxMatches = new List<KeyValuePair<string, double>>();
            int approxMatchesFound = 0; // Set counter to 0

            // Loop through each team in the apiTeams List
            foreach (var apiTeam in apiTeams)
            {
                // Check if the IsApproxEqual method returns true
                if (FuzzyMatching.IsApproxEqual(submittedTeam, apiTeam))
                {
                    // Get the Ratcliff Obershelp Similarity
                    ratOberDist = FuzzyMatching.GetRatOberDistance(submittedTeam, apiTeam);
                    // After trial and error the above was found to be the most suitable distance measure to use
                    // 
                    //overlapCoeff = FuzzyMatching.GetOverlapCoefficient(submittedTeam, apiTeam).ToString("0.00");
                    //lcSubseqDist = FuzzyMatching.GetLCSubsequence(submittedTeam, apiTeam).ToString();
                    //lcSubstrDist = FuzzyMatching.GetLCSubsring(submittedTeam, apiTeam).ToString();
                    //jaccardDist = FuzzyMatching.GetJaccardDistance(submittedTeam, apiTeam);
                    //levDist = FuzzyMatching.GetLevDistance(submittedTeam, apiTeam).ToString();
                    //normLevDist = FuzzyMatching.GetNormLevDistance(submittedTeam, apiTeam).ToString("0.00");
                    //hammDist = FuzzyMatching.GetHammingDistance(submittedTeam, apiTeam).ToString();
                    //jaroWinkDist = FuzzyMatching.GetJaroWinklerDistance(submittedTeam, apiTeam).ToString("0.00");
                    //sorensenDist = FuzzyMatching.GetSorensonDistance(submittedTeam, apiTeam).ToString("0.00");
                    //tanimotoDist = FuzzyMatching.GetTanimotoDistance(submittedTeam, apiTeam).ToString("0.00");

                    approxMatchesFound++; // Increment counter
                    // Store the matched team and it's Ratcliff Obershelp Similarity measure to the submitted team 
                    approxMatches.Add(new KeyValuePair<string, double>(apiTeam, ratOberDist));
                }
            }

            // If only one approximate match is found then return it.
            if (approxMatchesFound == 1)
            {
                return approxMatches[0].Key;
            }
            // If more than one approximate match is found then return the one with the highest Ratcliff Obershelp Similarity
            else if (approxMatchesFound > 1)
            {
                var max = default(KeyValuePair<string, double>);
                foreach (var match in approxMatches)
                {
                    if (match.Value > max.Value)
                        max = match;
                }
                return max.Key;
            }
            else
            {
                return "NO MATCH";
            }
        }

        /// <summary>
        /// Estimate if two strings are approximately equal based on weighted algorithm distance calculations
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsApproxEqual(string source, string target)
        {
            // Initialise List of options
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            // Choose which algorithms should weigh in for the comparison
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            options.Add(FuzzyStringComparisonOptions.UseRatcliffObershelpSimilarity);
            options.Add(FuzzyStringComparisonOptions.UseJaccardDistance);
            //options.Add(FuzzyStringComparisonOptions.UseLevenshteinDistance);
            //options.Add(FuzzyStringComparisonOptions.UseNormalizedLevenshteinDistance);
            //options.Add(FuzzyStringComparisonOptions.UseHammingDistance);
            //options.Add(FuzzyStringComparisonOptions.UseJaroDistance);
            //options.Add(FuzzyStringComparisonOptions.UseSorensenDiceDistance);
            //options.Add(FuzzyStringComparisonOptions.UseTanimotoCoefficient);

            // Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
            var tolerance = FuzzyStringComparisonTolerance.Strong;

            // Get a boolean determination of approximate equality
            bool result = source.ApproximatelyEquals(target, options, tolerance);
            return result;
        }

        public static int GetLevDistance(string source, string target)
        {
            return source.LevenshteinDistance(target);

        }
        public static double GetNormLevDistance(string source, string target)
        {
            return source.NormalizedLevenshteinDistance(target);

        }
        public static int GetHammingDistance(string source, string target)
        {
            return source.HammingDistance(target);

        }
        public static double GetJaroWinklerDistance(string source, string target)
        {
            return source.JaroWinklerDistance(target);

        }
        public static double GetJaccardDistance(string source, string target)
        {
            return source.JaccardDistance(target);

        }
        public static string GetLCSubsequence(string source, string target)
        {
            return source.LongestCommonSubsequence(target);

        }
        public static string GetLCSubsring(string source, string target)
        {
            return source.LongestCommonSubstring(target);

        }
        public static double GetOverlapCoefficient(string source, string target)
        {
            return source.OverlapCoefficient(target);

        }
        public static double GetRatOberDistance(string source, string target)
        {
            return source.RatcliffObershelpSimilarity(target);

        }
        public static double GetSorensonDistance(string source, string target)
        {
            return source.SorensenDiceDistance(target);

        }
        public static double GetTanimotoDistance(string source, string target)
        {
            return source.TanimotoCoefficient(target);

        }
    }
}