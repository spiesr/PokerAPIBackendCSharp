using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    /* 
     * Here is a class for breaking two way ties when two players have the same
     * hand type that happens to be the highest hand type in that game. Here I
     * make the assumption that Royal Flush ties are unbreakable: everyone with a
     * Royal Flush will tie just because there's no way to break ties via kickers,
     * etc. Otherwise, the tiebreaking process for high card hands, flushes,
     * straights, and straight flushes is the same, allowing it to be rolled into
     * one function. Every other hand type has its own tiebreaking function. The
     * winner will be returned by their int identifier. If no winner can truly
     * be determined via the tiebreaking process, the function will return 0.
     **/
    public class TieBreakerTwoWay
    {
        // Used to break two way ties among high cards, flushes, straights, and straight flushes
        public int highCardFlushStraight(int player1, int player2, int[] cards1, int[] cards2)
        {
            // Sort card values from highest to lowest.
            Array.Sort(cards1);
            Array.Sort(cards2);
            
            // Proceed down the list of ranked cards until either a tiebreaker is found, or a tie is maintained
            if (cards1[4] == cards2 [4])
            {
                if (cards1[3] == cards2[3])
                {
                    if (cards1[2] == cards2[2])
                    {
                        if (cards1[1] == cards2[1])
                        {
                            if (cards1[0] == cards2[0])
                            {
                                return 0;
                            }
                            else if (cards1[0] > cards2[0])
                            {
                                return player1;
                            }
                            else
                            {
                                return player2;
                            }
                        }
                        else if (cards1[1] > cards2[1])
                        {
                            return player1;
                        }
                        else
                        {
                            return player2;
                        }
                    }
                    else if (cards1[2] > cards2[2])
                    {
                        return player1;
                    }
                    else
                    {
                        return player2;
                    }
                }
                else if (cards1[3] > cards2[3])
                {
                    return player1;
                }
                else
                {
                    return player2;
                }
            }
            else if (cards1[4] > cards2[4])
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }

        // Used to break ties with four of a kind
        public int fourOfAKind(int player1, int player2, int[] cards1, int[] cards2)
        {
            // There can only be one winner, so we figure out first what the distinct card ranks are
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] repetitions1 = { 0, 0 };
            int[] repetitions2 = { 0, 0 };
            int highFour1;
            int highFour2;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (distinct1[i] == cards1[j])
                    {
                        repetitions1[i]++;
                    }
                    if (distinct2[i] == cards2[j])
                    {
                        repetitions2[i]++;
                    }
                }
            }

            // C# as a language seems to prefer this to some sort of for loop structure
            if (repetitions1[0] == 1)
            {
                highFour1 = distinct1[1];
            }
            else
            {
                highFour1 = distinct1[0];
            }
            if (repetitions2[0] == 1)
            {
                highFour2 = distinct2[1];
            }
            else
            {
                highFour2 = distinct2[0];
            }

            // Whoever has the bigger four of a kind is the winner
            if (highFour1 > highFour2)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }

        // Used to break ties for full house
        public int fullHouse(int player1, int player2, int[] cards1, int[] cards2)
        {
            // We find the repetitions of each card
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] repetitions1 = { 0, 0 };
            int[] repetitions2 = { 0, 0 };
            int highThree1;
            int highThree2;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (distinct1[i] == cards1[j])
                    {
                        repetitions1[i]++;
                    }
                    if (distinct2[i] == cards2[j])
                    {
                        repetitions2[i]++;
                    }
                }
            }

            // We then figure out what the rank of the "house," or the three cards are
            if (repetitions1[0] == 2)
            {
                highThree1 = distinct1[1];
            }
            else
            {
                highThree1 = distinct1[0];
            }
            if (repetitions2[0] == 2)
            {
                highThree2 = distinct2[1];
            }
            else
            {
                highThree2 = distinct2[0];
            }

            // The bigger "house" wins
            if (highThree1 > highThree2)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }

        // Used to break ties for three of a kind
        public int threeOfAKind(int player1, int player2, int[] cards1, int[] cards2)
        {
            // We find the repetitions, as with most hands
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] repetitions1 = { 0, 0, 0 };
            int[] repetitions2 = { 0, 0, 0 };
            int highThree1;
            int highThree2;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (distinct1[i] == cards1[j])
                    {
                        repetitions1[i]++;
                    }
                    if (distinct2[i] == cards2[j])
                    {
                        repetitions2[i]++;
                    }
                }
            }

            // Since the C# compiler wants assurance that a value will be assigned, we do this
            if (repetitions1[0] == 3)
            {
                highThree1 = distinct1[0];
            }
            else if (repetitions1[1] == 3)
            {
                highThree1 = distinct1[1];
            }
            else
            {
                highThree1 = distinct1[2];
            }
            if (repetitions2[0] == 3)
            {
                highThree2 = distinct2[0];
            }
            else if (repetitions2[1] == 3)
            {
                highThree2 = distinct2[1];
            }
            else
            {
                highThree2 = distinct2[2];
            }

            // There can only be one three of a kind that wins
            if (highThree1 > highThree2)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }

        // Breaks ties for two pair hands
        public int twoPair(int player1, int player2, int[] cards1, int[] cards2)
        {
            // We first set everything up for the high pair, low pair, and kicker since we can have a true two-way tie here
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] repetitions1 = { 0, 0, 0 };
            int[] repetitions2 = { 0, 0, 0 };
            int kicker1;
            int kicker2;
            int highPair1;
            int highPair2;
            int lowPair1;
            int lowPair2;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (distinct1[i] == cards1[j])
                    {
                        repetitions1[i]++;
                    }
                    if (distinct2[i] == cards2[j])
                    {
                        repetitions2[i]++;
                    }
                }
            }

            // We use the position of the kicker to assign the other values
            if (repetitions1[0] == 1)
            {
                kicker1 = distinct1[0];
                if (distinct1[1] > distinct1[2])
                {
                    highPair1 = distinct1[1];
                    lowPair1 = distinct1[2];
                }
                else
                {
                    highPair1 = distinct1[2];
                    lowPair1 = distinct1[1];
                }
            }
            else if (repetitions1[1] == 1)
            {
                kicker1 = distinct1[1];
                if (distinct1[0] > distinct1[2])
                {
                    highPair1 = distinct1[0];
                    lowPair1 = distinct1[2];
                }
                else
                {
                    highPair1 = distinct1[2];
                    lowPair1 = distinct1[0];
                }
            }
            else
            {
                kicker1 = distinct1[2];
                if (distinct1[0] > distinct1[1])
                {
                    highPair1 = distinct1[0];
                    lowPair1 = distinct1[1];
                }
                else
                {
                    highPair1 = distinct1[1];
                    lowPair1 = distinct1[0];
                }
            }
            if (repetitions2[0] == 1)
            {
                kicker2 = distinct2[0];
                if (distinct2[1] > distinct2[2])
                {
                    highPair2 = distinct2[1];
                    lowPair2 = distinct2[2];
                }
                else
                {
                    highPair2 = distinct2[2];
                    lowPair2 = distinct2[1];
                }
            }
            else if (repetitions2[1] == 1)
            {
                kicker2 = distinct2[1];
                if (distinct2[0] > distinct2[2])
                {
                    highPair2 = distinct2[0];
                    lowPair2 = distinct2[2];
                }
                else
                {
                    highPair2 = distinct2[2];
                    lowPair2 = distinct2[0];
                }
            }
            else
            {
                kicker2 = distinct2[2];
                if (distinct2[0] > distinct2[1])
                {
                    highPair2 = distinct2[0];
                    lowPair2 = distinct2[1];
                }
                else
                {
                    highPair2 = distinct2[1];
                    lowPair2 = distinct2[0];
                }
            }

            // Now we check all possible outcomes, including a tie
            if (highPair1 == highPair2)
            {
                if (lowPair1 == lowPair2)
                {
                    if (kicker1 == kicker2)
                    {
                        return 0;
                    }
                    else if (kicker1 > kicker2)
                    {
                        return player1;
                    }
                    else
                    {
                        return player2;
                    }
                }
                else if (lowPair1 > lowPair2)
                {
                    return player1;
                }
                else
                {
                    return player2;
                }
            }
            else if (highPair1 > highPair2)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }

        // Breaks ties for single pair hands
        public int singlePair(int player1, int player2, int[] cards1, int[] cards2)
        {
            // We count our repetitions to find the pair value and the kickers
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] repetitions1 = { 0, 0, 0, 0 };
            int[] repetitions2 = { 0, 0, 0, 0 };
            int pair1;
            int pair2;
            int[] kickers1 = new int[3];
            int[] kickers2 = new int[3];
            for (int i = 0; i < 4; i++)
            {
                for (int l = 0; l < 5; l++)
                {
                    if (distinct1[i] == cards1[l])
                    {
                        repetitions1[i]++;
                    }
                    if (distinct2[i] == cards2[l])
                    {
                        repetitions2[i]++;
                    }
                }
            }

            // It would make sense at first to do the following in a for loop, but this approach makes the compiler happy
            if (repetitions1[0] == 2)
            {
                pair1 = distinct1[0];
            }
            else if (repetitions1[1] == 2)
            {
                pair1 = distinct1[1];
            }
            else if (repetitions1[2] == 2)
            {
                pair1 = distinct1[2];
            }
            else
            {
                pair1 = distinct1[3];
            }
            if (repetitions2[0] == 2)
            {
                pair2 = distinct2[0];
            }
            else if (repetitions2[1] == 2)
            {
                pair2 = distinct2[1];
            }
            else if (repetitions2[2] == 2)
            {
                pair2 = distinct2[2];
            }
            else
            {
                pair2 = distinct2[3];
            }

            // We can use a for loop here to assign the kickers to an array
            int j = 0;
            int k = 0;
            int n = 0;
            for (int i = 0; i < 3; i++)
            {
                if (distinct1[i] == pair1)
                {
                    n++;
                }
                else
                {
                    kickers1[j] = distinct1[i];
                    j++;
                }
                if (distinct2[i] == pair2)
                {
                    n++;
                }
                else
                {
                    kickers2[k] = distinct2[i];
                    k++;
                }
            }

            // We sort the kickers in case we need them for further tie breaking
            Array.Sort(kickers1);
            Array.Sort(kickers2);

            // Then we determine the winner, or if there is a two-way tie
            if (pair1 == pair2)
            {
                if (kickers1[2] == kickers2[2])
                {
                    if (kickers1[1] == kickers2[1])
                    {
                        if (kickers1[0] == kickers2[0])
                        {
                            return 0;
                        }
                        else if (kickers1[0] > kickers2[0])
                        {
                            return player1;
                        }
                        else
                        {
                            return player2;
                        }
                    }
                    else if (kickers1[1] > kickers2[1])
                    {
                        return player1;
                    }
                    else
                    {
                        return player2;
                    }
                }
                else if (kickers1[2] > kickers2[2])
                {
                    return player1;
                }
                else
                {
                    return player2;
                }
            }
            else if (pair1 > pair2)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }
    }
}