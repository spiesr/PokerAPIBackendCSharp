using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    /*
     * This class is meant to be a wrapper of the three poker hand evaluation classes
     * so that in the API call we can simply refer to one of the three functions in here
     * to be able to determine who the winner(s) of the game are. Here we have a method
     * to rank people's hands. If there's a tie among the people with the highest 
     * ranking hands, the relevant tiebreaking functions are used. For a two-way tie, we
     * seek out the winner, or find out if a true tie has occurred. For a three-way tie
     * there are less instances where a true three-way tie can happen, so we instead
     * seek out a loser to remove and then pass the remaining players to the two-way tie
     * breaker. I also placed a function to render string representations of card ranks
     * into numbers since this seemed like the best place to put it.
     **/
    public class WinnerFinder
    {
        // All of the poker hand evaluation classes have instances in this WinnerFinder class
        HandFinder handFinder = new HandFinder();
        TieBreakerTwoWay tieBreakerTwoWay = new TieBreakerTwoWay();
        TieBreakerThreeWay tieBreakerThreeWay = new TieBreakerThreeWay();

        // Finds the winner(s) of a two way tie
        public int breakTwoWayTie(Player player1, Player player2, int handType)
        {
            int result;
            switch(handType)
            {
                // Royal flushes always tie
                case 10:
                    return 0;

                // Straight flush ties are resolved by high cards
                case 9:
                    result = tieBreakerTwoWay.highCardFlushStraight(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // Four of a kind can only have one winner
                case 8:
                    result = tieBreakerTwoWay.fourOfAKind(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // Full house ties can only have one winner
                case 7:
                    result = tieBreakerTwoWay.fullHouse(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // Flush ties are resolved by high cards
                case 6:
                    result = tieBreakerTwoWay.highCardFlushStraight(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // Straight ties are resolved by high cards
                case 5:
                    result = tieBreakerTwoWay.highCardFlushStraight(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // There can only be one winner of a three of a kind tie
                case 4:
                    result = tieBreakerTwoWay.threeOfAKind(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // Resolve two pair ties (There can be a true tie between two players, but not three)
                case 3:
                    result = tieBreakerTwoWay.twoPair(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // Resolve single pair ties (There can be a true tie between two players, but not three)
                case 2:
                    result = tieBreakerTwoWay.singlePair(player1.number, player2.number, player1.cards, player2.cards);
                    return result;

                // Default case is high cards
                default:
                    result = tieBreakerTwoWay.highCardFlushStraight(player1.number, player2.number, player1.cards, player2.cards);
                    return result;
            }
        }

        // Finds the loser (or the absence of a loser in the rare moments that a three way tie can happen) of a three way tiebreaker
        public int breakThreeWayTie(Player[] players, int handType)
        {
            int result;
            switch(handType)
            {
                // If a three way tie happened with Royal Flushes, everybody deserves to win (and they actually all do)
                case 10:
                    return 0;

                // Straight flushes are resolved with high cards. There could be a three way tie here
                case 9:
                    result = tieBreakerThreeWay.highCardFlushStraight(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // Four of a kind can only have one winner, so we find the first loser
                case 8:
                    result = tieBreakerThreeWay.fourOfAKind(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // Full house can only have one winner, so we find the first loser
                case 7:
                    result = tieBreakerThreeWay.fullHouse(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // Flushes are resolved with high cards. There could be a three way tie here
                case 6:
                    result = tieBreakerThreeWay.highCardFlushStraight(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // Straights are resolved with high cards. There could be a three way tie here
                case 5:
                    result = tieBreakerThreeWay.highCardFlushStraight(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // Three of a kind can only have one winner, so we find the first loser
                case 4:
                    result = tieBreakerThreeWay.threeOfAKind(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // A three way tie with two pair hands is impossible. Therefore, we find the first loser
                case 3:
                    result = tieBreakerThreeWay.twoPair(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // A three way tie with single pair hands is impossible. Therefore, we find a loser
                case 2:
                    result = tieBreakerThreeWay.singlePair(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;

                // Default case is once again high cards. A three way tie is possible
                default:
                    result = tieBreakerThreeWay.highCardFlushStraight(players[0].number, players[1].number, players[2].number, players[0].cards, players[1].cards, players[2].cards);
                    return result;
            }
        }

        // Ranks the hand of the player object and returns a value that can be assigned to player.handType
        public int findHandRanking(Player player)
        {
            // We find flushes first because that's a condition for the two highest hands
            if (handFinder.isFlush(player.suits))
            {
                if (handFinder.isRoyalFlush(player.cards))
                {
                    return 10;
                }

                // This lets us find a straight flush
                else if (handFinder.isStraight(player.cards))
                {
                    return 9;
                }
                else
                {
                    return 6;
                }
            }

            // Then we proceed down the remaining list by hand ranking
            else if (handFinder.isFourOfAKind(player.cards))
            {
                return 8;
            }
            else if (handFinder.isFullHouse(player.cards))
            {
                return 7;
            }
            else if (handFinder.isStraight(player.cards))
            {
                return 5;
            }
            else if (handFinder.isThreeOfAKind(player.cards))
            {
                return 4;
            }
            else if (handFinder.isTwoPair(player.cards))
            {
                return 3;
            }
            else if (handFinder.isSinglePair(player.cards))
            {
                return 2;
            }

            // Should all other tests fail, it's automatically a high card hand
            else
            {
                return 1;
            }
        }

        // Convert the card rank characters to integer values
        public int findCardRank(char rank)
        {
            switch(rank)
            {
                // Face cards and 10 all need special cases
                case 'A':
                    return 14;
                case 'K':
                    return 13;
                case 'Q':
                    return 12;
                case 'J':
                    return 11;
                case 'T':
                    return 10;
                
                // The default is to use a string of one character to parse as an int
                default:
                    return Convert.ToInt32(new string(rank, 1));
            }
        }
    }
}