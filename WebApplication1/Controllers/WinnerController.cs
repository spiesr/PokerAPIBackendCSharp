using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApplication1.Controllers
{
    /*
     * Here is the API route for determining the winner of the set of poker hands
     * that is sent over here from the Angular frontend.
     **/
    public class WinnerController : ApiController
    {
        // POST api/winner
        public async Task<Message> Post()
        {
            // Create a response message, a winner finder, database connection, and an array of players from the getgo
            Message response = new Message();
            WinnerFinder winnerFinder = new WinnerFinder();
            // GamePersistence db = new GamePersistence();
            Player player1 = new Player();
            Player player2 = new Player();
            Player player3 = new Player();
            Player[] players = { player1, player2, player3 };

            // Get the request body as a JSON object
            string request = await Request.Content.ReadAsStringAsync();
            dynamic theJson = JsonConvert.DeserializeObject(request);

            // Build the contents of the array of players from the JSON
            for (int i = 0; i < 3; i++)
            {
                players[i].number = i + 1;
                players[i].name = theJson.hands[i].player;
                string[] playerHand = theJson.hands[i].cards.ToObject<string[]>();
                char[] cardSuits = new char[5];
                int[] cardRanks = new int[5];
                for (int j = 0; j < 5; j++)
                {
                    cardRanks[j] = winnerFinder.findCardRank(playerHand[j][0]);
                    cardSuits[j] = playerHand[j][1];
                }

                // Let's include the low card ace straight hand exception here
                Array.Sort(cardRanks);
                int[] lowStraightException = { 2, 3, 4, 5, 14 };
                int[] lowStraight = { 1, 2, 3, 4, 5 };
                if (cardRanks == lowStraightException)
                {
                    cardRanks = lowStraight;
                }
                players[i].cards = cardRanks;
                players[i].suits = cardSuits;
            }

            // We find the winner(s) here by first ranking the hands
            int[] rankings = new int[3];
            for (int i = 0; i < 3; i++)
            {
                rankings[i] = winnerFinder.findHandRanking(players[i]);
                players[i].handType = rankings[i];
            }

            // If there's only one hand ranking, then we must break a three way tie
            if (rankings.Distinct().ToArray().Length == 1)
            {
                // We can use rankings[0] here since all values of rankings are the same in this case
                int loser = winnerFinder.breakThreeWayTie(players, rankings[0]);

                // If there is a true three-way tie, we report that
                if (loser == 0)
                {
                    string messageString = "The winners are {0}, {1}, and {2}";
                    response.message = String.Format(messageString, players[0].name, players[1].name, players[2].name);
                }

                // Otherwise, we use the value of "loser" to figure out which two players to send to the two-way tiebreaker function
                else
                {
                    int toSkip = loser - 1;
                    int[] playerIndicies = new int[2];
                    int n = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (i != toSkip)
                        {
                            playerIndicies[n] = i;
                            n++;
                        }
                    }

                    // Now we break the tie with the two remaining players
                    int winner = winnerFinder.breakTwoWayTie(players[playerIndicies[0]], players[playerIndicies[1]], rankings[0]);
                    
                    // A value of zero means a two-way tie
                    if (winner == 0)
                    {
                        string messageString = "The winners are {0} and {1}!";
                        response.message = String.Format(messageString, players[playerIndicies[0]].name, players[playerIndicies[1]].name);
                    }

                    // Otherwise, we have only one winner
                    else
                    {
                        int winnerIndex = winner - 1;
                        string messageString = "The winner is {0}!";
                        response.message = String.Format(messageString, players[winnerIndex].name);
                    }
                }
            }

            // Two way tiebreaker or tie for second place determined here
            else if (rankings.Distinct().ToArray().Length == 2)
            {
                // We start by counting repetitions to see which case it is
                int[] distinctRanks = rankings.Distinct().ToArray();
                int[] repetitions = { 0, 0 };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (distinctRanks[j] == rankings[i])
                        {
                            repetitions[j]++;
                        }
                    }
                }

                // If the highest value has the most repetitions we must break the tie
                if (distinctRanks.ToList().IndexOf(distinctRanks.Max()) == repetitions.ToList().IndexOf(repetitions.Max()))
                {
                    // We find which players are tied by hand rankings
                    int[] tiedPlayers = new int[2];
                    int n = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        if (players[i].handType == distinctRanks.Max())
                        {
                            tiedPlayers[n] = i;
                            n++;
                        }
                    }
                    int winner = winnerFinder.breakTwoWayTie(players[tiedPlayers[0]], players[tiedPlayers[1]], distinctRanks.Max());

                    // If that function returns 0, there is a tie
                    if (winner == 0)
                    {
                        string messageString = "The winners are {0} and {1}!";
                        response.message = String.Format(messageString, players[tiedPlayers[0]].name, players[tiedPlayers[1]].name);
                    }

                    // Otherwise, there is one winner
                    else
                    {
                        int winnerIndex = winner - 1;
                        string messageString = "The winner is {0}!";
                        response.message = String.Format(messageString, players[winnerIndex].name);
                    }
                }

                // Otherwise, the maximum value is not repeated, and there is one winner
                else
                {
                    int maxRanking = rankings.Max();
                    int winningPlayer = rankings.ToList().IndexOf(maxRanking);
                    string messageString = "The winner is {0}!";
                    response.message = String.Format(messageString, players[winningPlayer].name);
                }
            }

            // If there are three distinct rankings, there must be one maximum, and therefore one winner
            else
            {
                int maxRanking = rankings.Max();
                int winningPlayer = rankings.ToList().IndexOf(maxRanking);
                string messageString = "The winner is {0}!";
                response.message = String.Format(messageString, players[winningPlayer].name);
            }

            // Database persistence goes here
            // db.saveGame(request, response.message);

            // Give back the response object. This will send it as JSON automatically
            return response;
        }
    }
}
