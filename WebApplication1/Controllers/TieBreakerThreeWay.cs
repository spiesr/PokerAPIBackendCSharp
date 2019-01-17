using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    /*
     * The approach to the three way tie problem is slightly different than
     * the approach I dook for the two way tie problem. While the two way tie
     * problem's goal is to find a clear winner, here I instead choose to
     * focus on finding a clear loser. If a clear loser is found, then we can
     * use the two way tie functions to find the winner(s). If no clear loser
     * is found for the cases where there can possibly be a three way tie, then
     * everyone wins.
     **/
    public class TieBreakerThreeWay
    {
        // Used to break three way ties among high cards, flushes, straights, and straight flushes
        public int highCardFlushStraight(int player1, int player2, int player3, int[] cards1, int[] cards2, int[] cards3)
        {
            // We sort the cards out by rank
            Array.Sort(cards1);
            Array.Sort(cards2);
            Array.Sort(cards3);

            // Then we proceed down the ranks. A three way tie is possible, but rare in the scheme of things
            if (cards1[4] == cards2[4] && cards1[4] == cards3[4] && cards2[4] == cards3[4])
            {
                if (cards1[3] == cards2[3] && cards1[3] == cards3[3] && cards2[3] == cards3[3])
                {
                    if (cards1[2] == cards2[2] && cards1[2] == cards3[2] && cards2[2] == cards3[2])
                    {
                        if (cards1[1] == cards2[1] && cards1[1] == cards3[1] && cards2[1] == cards3[1])
                        {
                            if (cards1[0] == cards2[0] && cards1[0] == cards3[0] && cards2[0] == cards3[0])
                            {
                                return 0;
                            }
                            else if (cards1[0] < cards2[0] && cards1[0] < cards3[0])
                            {
                                return player1;
                            }
                            else if (cards2[0] < cards1[0] && cards2[0] < cards3[0])
                            {
                                return player2;
                            }
                            else
                            {
                                return player3;
                            }
                        }
                        else if (cards1[1] < cards2[1] && cards1[1] < cards3[1])
                        {
                            return player1;
                        }
                        else if (cards2[1] < cards1[1] && cards2[1] < cards3[1])
                        {
                            return player2;
                        }
                        else
                        {
                            return player3;
                        }
                    }
                    else if (cards1[2] < cards2[2] && cards1[2] < cards3[2])
                    {
                        return player1;
                    }
                    else if (cards2[2] < cards1[2] && cards2[2] < cards3[2])
                    {
                        return player2;
                    }
                    else
                    {
                        return player3;
                    }
                }
                else if (cards1[3] < cards2[3] && cards1[3] < cards3[3])
                {
                    return player1;
                }
                else if (cards2[3] < cards1[3] && cards2[3] < cards3[3])
                {
                    return player2;
                }
                else
                {
                    return player3;
                }
            }
            else if (cards1[4] < cards2[4] && cards1[4] < cards3[4])
            {
                return player1;
            }
            else if (cards2[4] < cards1[4] && cards2[4] < cards3[4])
            {
                return player2;
            }
            else
            {
                return player3;
            }
        }

        // Finds the loser for a three way tie of four of a kind hands
        public int fourOfAKind(int player1, int player2, int player3, int[] cards1, int[] cards2, int[] cards3)
        {
            // We count repetitions
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] distinct3 = cards3.Distinct().ToArray();
            int[] repetitions1 = { 0, 0 };
            int[] repetitions2 = { 0, 0 };
            int[] repetitions3 = { 0, 0 };
            int highFour1;
            int highFour2;
            int highFour3;
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
                    if (distinct3[i] == cards3[j])
                    {
                        repetitions3[i]++;
                    }
                }
            }

            // We use repetitions to find the rank of the four of a kind
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
            if (repetitions3[0] == 1)
            {
                highFour3 = distinct3[1];
            }
            else
            {
                highFour3 = distinct3[0];
            }

            // Then there can only be one winner, so we find the first loser
            if (highFour1 < highFour2 && highFour1 < highFour3)
            {
                return player1;
            }
            else if (highFour2 < highFour1 && highFour2 < highFour3)
            {
                return player2;
            }
            else
            {
                return player3;
            }
        }

        // Finds the loser of a three way full house tie
        public int fullHouse(int player1, int player2, int player3, int[] cards1, int[] cards2, int[] cards3)
        {
            // We start by counting repetitions
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] distinct3 = cards3.Distinct().ToArray();
            int[] repetitions1 = { 0, 0 };
            int[] repetitions2 = { 0, 0 };
            int[] repetitions3 = { 0, 0 };
            int highThree1;
            int highThree2;
            int highThree3;
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
                    if (distinct3[i] == cards3[j])
                    {
                        repetitions3[i]++;
                    }
                }
            }

            // Then they are used to find the "house"
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
            if (repetitions3[0] == 2)
            {
                highThree3 = distinct3[1];
            }
            else
            {
                highThree3 = distinct3[0];
            }

            // Whoever has the lowest "house" automatically is eliminated
            if (highThree1 < highThree2 && highThree1 < highThree3)
            {
                return player1;
            }
            else if (highThree2 < highThree1 && highThree2 < highThree3)
            {
                return player2;
            }
            else
            {
                return player3;
            }
        }

        // Finds the loser of a three way tie of three of a kind
        public int threeOfAKind(int player1, int player2, int player3, int[] cards1, int[] cards2, int[] cards3)
        {
            // Count repetitions
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] distinct3 = cards3.Distinct().ToArray();
            int[] repetitions1 = { 0, 0, 0 };
            int[] repetitions2 = { 0, 0, 0 };
            int[] repetitions3 = { 0, 0, 0 };
            int highThree1;
            int highThree2;
            int highThree3;
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
                    if (distinct3[i] == cards3[j])
                    {
                        repetitions3[i]++;
                    }
                }
            }

            // USe them to find the rank of the three of a kind
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
            if (repetitions3[0] == 3)
            {
                highThree3 = distinct3[0];
            }
            else if (repetitions3[1] == 3)
            {
                highThree3 = distinct3[1];
            }
            else
            {
                highThree3 = distinct3[2];
            }

            // Whoever has the lowest three of a kind is eliminated
            if (highThree1 < highThree2 && highThree1 < highThree3)
            {
                return player1;
            }
            else if (highThree2 < highThree1 && highThree2 < highThree3)
            {
                return player2;
            }
            else
            {
                return player3;
            }
        }

        // Finds the loser of a three way tie of two pair hands
        public int twoPair(int player1, int player2, int player3, int[] cards1, int[] cards2, int[] cards3)
        {
            // We count repetitions to find the high pairs
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] distinct3 = cards3.Distinct().ToArray();
            int[] repetitions1 = { 0, 0, 0 };
            int[] repetitions2 = { 0, 0, 0 };
            int[] repetitions3 = { 0, 0, 0 };

            // Only two people maximum can win, so we only need to screen by the highest pair
            int highPair1;
            int highPair2;
            int highPair3;
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
                    if (distinct3[i] == cards3[j])
                    {
                        repetitions3[i]++;
                    }
                }
            }

            // We find the highest pair using the repetition counts
            if (repetitions1[0] == 1)
            {
                if (distinct1[1] > distinct1[2])
                {
                    highPair1 = distinct1[1];
                }
                else
                {
                    highPair1 = distinct1[2];
                }
            }
            else if (repetitions1[1] == 1)
            {
                if (distinct1[0] > distinct1[2])
                {
                    highPair1 = distinct1[0];
                }
                else
                {
                    highPair1 = distinct1[2];
                }
            }
            else
            {
                if (distinct1[0] > distinct1[1])
                {
                    highPair1 = distinct1[0];
                }
                else
                {
                    highPair1 = distinct1[1];
                }
            }
            if (repetitions2[0] == 1)
            {
                if (distinct2[1] > distinct2[2])
                {
                    highPair2 = distinct2[1];
                }
                else
                {
                    highPair2 = distinct2[2];
                }
            }
            else if (repetitions2[1] == 1)
            {
                if (distinct2[0] > distinct2[2])
                {
                    highPair2 = distinct2[0];
                }
                else
                {
                    highPair2 = distinct2[2];
                }
            }
            else
            {
                if (distinct2[0] > distinct2[1])
                {
                    highPair2 = distinct2[0];
                }
                else
                {
                    highPair2 = distinct2[1];
                }
            }
            if (repetitions3[0] == 1)
            {
                if (distinct3[1] > distinct3[2])
                {
                    highPair3 = distinct3[1];
                }
                else
                {
                    highPair3 = distinct1[2];
                }
            }
            else if (repetitions3[1] == 1)
            {
                if (distinct3[0] > distinct3[2])
                {
                    highPair3 = distinct3[0];
                }
                else
                {
                    highPair3 = distinct3[2];
                }
            }
            else
            {
                if (distinct3[0] > distinct3[1])
                {
                    highPair3 = distinct3[0];
                }
                else
                {
                    highPair3 = distinct3[1];
                }
            }

            // We find a pair that is at least lower than one of the others. A tie for second place doesn't enable a player to win
            if (highPair1 < highPair2 || highPair1 < highPair3)
            {
                return player1;
            }
            else if (highPair2 < highPair1 || highPair2 < highPair3)
            {
                return player2;
            }
            else
            {
                return player3;
            }
        }

        // Finds the loser of a three way tie of single pair hands
        public int singlePair(int player1, int player2, int player3, int[] cards1, int[] cards2, int[] cards3)
        {
            // We count repetitions and find the values of the pairs
            int[] distinct1 = cards1.Distinct().ToArray();
            int[] distinct2 = cards2.Distinct().ToArray();
            int[] distinct3 = cards3.Distinct().ToArray();
            int[] repetitions1 = { 0, 0, 0, 0 };
            int[] repetitions2 = { 0, 0, 0, 0 };
            int[] repetitions3 = { 0, 0, 0, 0 };
            int pair1;
            int pair2;
            int pair3;
            for (int i = 0; i < 4; i++)
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
                    if (distinct3[i] == cards3[j])
                    {
                        repetitions3[i]++;
                    }
                }
            }

            // The repetitions are used to find the pair
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
            if (repetitions3[0] == 2)
            {
                pair3 = distinct3[0];
            }
            else if (repetitions3[1] == 2)
            {
                pair3 = distinct3[1];
            }
            else if (repetitions3[2] == 2)
            {
                pair3 = distinct3[2];
            }
            else
            {
                pair3 = distinct3[3];
            }

            // Since a tie for second place won't let a player win, we use the same logic as was used in this.twoPair()
            if (pair1 < pair2 || pair1 < pair3)
            {
                return player1;
            }
            else if (pair2 < pair1 || pair2 < pair3)
            {
                return player2;
            }
            else
            {
                return player3;
            }
        }
    }
}