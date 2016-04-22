using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StattoBot
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string submittedTeam = SourceTextBox.Text;

            MatchResult.Text = FuzzyMatching.DoSearch(submittedTeam);

            /*
            bool MatchFound = false;

            string approxMatch = "";
            string overlapCoeff = "";
            string lcSubseqDist = "";
            string lcSubstrDist = "";
            string levDist = "";
            string normLevDist = "";
            string hammDist = "";
            string jaroWinkDist = "";
            double jaccardDist = 999;
            double ratOberDist = 999;
            string sorensenDist = "";
            string tanimotoDist = "";

            List<string> apiTeams = System.IO.File.ReadLines(Server.MapPath("soccer-teams-top500.txt")).ToList();
            foreach (var apiTeam in apiTeams)
            {
                MatchResult.Text = "";
                ResultLabel.Text = "";

                if (apiTeam == submittedTeam)
                {
                    MatchResult.Text = MatchResult.Text + submittedTeam + " : " + apiTeam + " = Match";
                    MatchFound = true;
                    break;
                }
            }
            if (!MatchFound) {
                var approxMatches = new List<KeyValuePair<string, double>>();
                int approxMatchesFound = 0;

                foreach (var apiTeam in apiTeams)
                {
                    MatchResult.Text = "";
                    ResultLabel.Text = "";

                    if (FuzzyMatching.IsApproxEqual(submittedTeam, apiTeam))
                    {
                        // TODO: find best match - store all matches in list
                        overlapCoeff = FuzzyMatching.GetOverlapCoefficient(submittedTeam, apiTeam).ToString("0.00");
                        lcSubseqDist = FuzzyMatching.GetLCSubsequence(submittedTeam, apiTeam).ToString();
                        lcSubstrDist = FuzzyMatching.GetLCSubsring(submittedTeam, apiTeam).ToString();

                        levDist = FuzzyMatching.GetLevDistance(submittedTeam, apiTeam).ToString();
                        normLevDist = FuzzyMatching.GetNormLevDistance(submittedTeam, apiTeam).ToString("0.00");
                        hammDist = FuzzyMatching.GetHammingDistance(submittedTeam, apiTeam).ToString();
                        jaroWinkDist = FuzzyMatching.GetJaroWinklerDistance(submittedTeam, apiTeam).ToString("0.00");
                        jaccardDist = FuzzyMatching.GetJaccardDistance(submittedTeam, apiTeam);
                        ratOberDist = FuzzyMatching.GetRatOberDistance(submittedTeam, apiTeam);
                        sorensenDist = FuzzyMatching.GetSorensonDistance(submittedTeam, apiTeam).ToString("0.00");
                        tanimotoDist = FuzzyMatching.GetTanimotoDistance(submittedTeam, apiTeam).ToString("0.00");

                        approxMatchesFound++;
                        approxMatches.Add(new KeyValuePair<string, double>(apiTeam, ratOberDist));
                        //MatchResult.Text = approxMatches.ElementAt(approxMatchesFound-1).ToString();

                        //approxMatch = "Is approximate match.";
                        //MatchResult.Text = MatchResult.Text + submittedTeam + " : " + apiTeam + " = " + approxMatch + "<br />";

                        //break;
                    }
                    else if (approxMatchesFound == 0)
                    {
                        approxMatch = "Not approximate match.";
                        MatchResult.Text = submittedTeam + " : " + apiTeam + " = " + approxMatch + "<br />";
                    }
                }

                var max = default(KeyValuePair<string, double>);
                foreach (var match in approxMatches)
                {
                    if (match.Value > max.Value)
                        max = match;
                }
                MatchResult.Text = MatchResult.Text + "<br />" + max + " : " + approxMatchesFound;

                ResultLabel.Text =
                    "<br />Overlap coeffecient [1]: " + overlapCoeff + "<br />" +
                    "<br />Longest common subsequence: " + lcSubseqDist + "<br />" +
                    "<br />Longest common substring: " + lcSubstrDist + "<br />" +
                    "<br /><br />Levenshtein distance [0]: " + levDist + "<br />" +
                    "<br />Normalised Levenshtein distance [0]: " + normLevDist + "<br />" +
                    "<br />Hamming distance [0]: " + hammDist + "<br />" +
                    "<br />Jaro-Winkler distance [1]: " + jaroWinkDist + "<br />" +
                    "<br />Jaccard distance [0]: " + jaccardDist + "<br />" +
                    "<br />Ratcliff-Obershelp Similarity [1]: " + ratOberDist + "<br />" +
                    "<br />Sorenson-Dice distance:  [0]" + sorensenDist + "<br />" +
                    "<br />Tanimoto coeffecient:  [1]" + tanimotoDist + "<br />";
                       
            }*/
        }
    }
}