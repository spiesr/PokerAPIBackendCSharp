using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    /* 
     * This class has methods for determining which hand types there
     * are in the set of three hands. There are eight methods for determining
     * which poker hand is in play, and the methods isFlush() and isStraight()
     * can be combined to effectively create an isStraightFlush() test. If we
     * consider the "default" hand to be simply a single high card, then
     * these methods are enough to discern the 10 poker hands from each other.
     */
    public class HandFinder
    {
        // Checks to see if we have a flush
        public bool isFlush(char[] suits)
        {
            // If there is only one suit, there's a flush of some kind
            if (suits.Distinct().ToArray().Length == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if a particular flush is a royal flush
        public bool isRoyalFlush(int[] cards)
        {
            // If the cards array looks like this after sorting, it's a royal flush
            int[] royalFlush = { 10, 11, 12, 13, 14 };
            Array.Sort(cards);
            if (cards == royalFlush)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if we have a straight. Combine this with isFlush() for straight flush
        public bool isStraight(int[] cards)
        {
            // Sorting the cards numerically makes checking easier
            Array.Sort(cards);

            /*
             * I thought about this problem of the low ace, since in poker the only time
             * the ace effectively takes on a value of "1" instead of "14" is when it is
             * part of a straight with 2, 3, 4, and 5. I figured it would be easier to 
             * use a redefinition earlier up the pipeline than to make an exception case
             * here and in the high card tie breaker functions.
             **/
            // int[] lowAce = { 2, 3, 4, 5, 14 };

            // If there are all distinct cards, it has potential to be a straight
            if (cards.Distinct().ToArray().Length == 5)
            {
                // Since we ordered the array higher up, we can check the numerical distance to find a straight.
                if (cards[0] == (cards[4] - 4))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Checks if there's one pair and only one pair
        public bool isSinglePair(int[] cards)
        {
            // Four distinct card ranks means there is one repeated rank, or one pair
            if (cards.Distinct().ToArray().Length == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks if there's two pairs
        public bool isTwoPair(int[] cards)
        {
            // Two pair hands have three distinct ranks in them
            if (cards.Distinct().ToArray().Length == 3)
            {
                // First we check the repetitions of each rank
                int[] elements = cards.Distinct().ToArray();
                int[] repeats = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (elements[i] == cards[j])
                        {
                            repeats[i]++;
                        }
                    }
                }

                // If we have 2 repetitions, it's a two pair hand. Three of a kind can't have a repetition of 2
                if (Array.Exists(repeats, element => element == 2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Checks for three of a kind
        public bool isThreeOfAKind(int[] cards)
        {
            // Three of a kind hands have three distinct card ranks in them
            if (cards.Distinct().ToArray().Length == 3)
            {
                // We check our repetitions
                int[] elements = cards.Distinct().ToArray();
                int[] repeats = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (elements[i] == cards[j])
                        {
                            repeats[i]++;
                        }
                    }
                }

                // If there are three repetitions of any card, it's three of a kind
                if (Array.Exists(repeats, element => element == 3))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Checks for full house
        public bool isFullHouse(int[] cards)
        {
            // Full house hands only have two distinct card ranks
            if (cards.Distinct().ToArray().Length == 2)
            {
                // We check our repetitions
                int[] elements = cards.Distinct().ToArray();
                int[] repeats = new int[2];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (elements[i] == cards[j])
                        {
                            repeats[i]++;
                        }
                    }
                }

                // If one card rank is repeated three times, it's a full house
                if (Array.Exists(repeats, element => element == 3))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Checks for four of a kind
        public bool isFourOfAKind(int[] cards)
        {
            // Four of a kind hands only have two distinct card ranks
            if (cards.Distinct().ToArray().Length == 2)
            {
                // Check our repetitions
                int[] elements = cards.Distinct().ToArray();
                int[] repeats = new int[2];
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (elements[i] == cards[j])
                        {
                            repeats[i]++;
                        }
                    }
                }

                // Having a single rank repeat four times means it's a four of a kind hand
                if (Array.Exists(repeats, element => element == 4))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}