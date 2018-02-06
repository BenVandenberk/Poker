using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Poker
{
    class Deck
    {               
        public List<Card> Cards { get; private set; }

        public Deck()
        {
            Cards = new List<Card>();
            fillDeck();
        }

        private void fillDeck()
        {
            Cards.Clear();
            
            for (int i = 1; i < 5; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    Cards.Add(new Card((CardValue)j, (CardType)i));
                }
            }
        }

        public Card DealCard()
        {            
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[1];
            Card giveBack;

            do
            {
                rng.GetBytes(randomNumber);
            } while (!isFairCard(randomNumber[0]));

            giveBack = Cards[randomNumber[0] % Cards.Count];
            Cards.Remove(giveBack);

            return giveBack;                             
        }

        private bool isFairCard(byte card)
        {
            int nrOfFairSets = Byte.MaxValue / Cards.Count;
            return card < nrOfFairSets * Cards.Count;
        }

        public void Restore()
        {
            fillDeck();
        }
    }
}
