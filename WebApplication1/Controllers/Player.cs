using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    /*
     * Here is a class for each individual player. The basic idea is that
     * the API path will generate an array of three Player objects, and
     * then the incoming JSON from Angular will populate the name, cards,
     * and suits values. The number will correspond to its array index plus
     * one. handType as a property will be a number from 1 to 10 indicating
     * which of the 10 poker hands it is.
     **/
    public class Player
    {
        // Make all these public so we can use them in function calls and in building arrays
        public string name;
        public int number;
        public int[] cards = new int[5];
        public char[] suits = new char[5];
        public int handType;

        // It is better to have these explicit methods than to not have them, depending on program flow
        public void setName(string apiName)
        {
            this.name = apiName;
        }

        public void setNumber(int systemNumber)
        {
            this.number = systemNumber;
        }

        public void setCards(int[] apiCards)
        {
            this.cards = apiCards;
        }

        public void setSuits(char[] apiSuits)
        {
            this.suits = apiSuits;
        }

        public void setHandType(int value)
        {
            this.handType = value;
        }
    }
}