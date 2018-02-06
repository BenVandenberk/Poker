using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public class Card
    {
        public CardValue CardValue { get; private set; }
        public CardType CardType { get; private set; }

        public Card(CardValue cardValue, CardType cardType)
        {
            this.CardValue = cardValue;
            this.CardType = cardType;
        }

        public override string ToString()
        {
            return CardValue + " of " + CardType;
        }

        public bool Equals(Card card)
        {
            if (this.CardType == card.CardType && this.CardValue == card.CardValue)
            {
                return true;
            }
            return false;
        }

        public char toChar()
        {
            return (char)((int)CardValue + 95);
        }

        public string ImagePath()
        {
            return "Images\\" + CardType + "\\" + (int)CardValue + ".png";
        }
    }
}
