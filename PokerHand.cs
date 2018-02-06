using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public class PokerHand
    {
        private int coreValue; // a variable to pass to the Poker.GetCombination method in which the method puts f.e. the CardValue of the triple card of a Three of a Kind. To avoid putting the doubles together twice.
        
        public Card[] Cards { get; private set; }
        public string ValueCode { get; private set; }
        public PokerCombination PokerCombination { get; private set; }

        public PokerHand()
        {
            Cards = new Card[5];
            ValueCode = "";            
        }
        
        public PokerHand(Card[] cards)
        {
            this.Cards = new Card[5];

            if (cards.Length != 5) // Poker hand must contain 5 cards
            {
                throw new Exception("Invalid pokerhand.");
            }

            for (int i = 0; i < 5; i++) // No two same cards are allowed in a single poker hand
            {
                for (int j = 0; j < 5; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (cards[i].Equals(cards[j]))
                    {
                        throw new Exception("Invalid pokerhand.");
                    }
                }
            }

            cards.CopyTo(this.Cards, 0);
            PokerCombination = PokerHelp.GetCombination(this, out coreValue);
            setValueCode();
        }

        public override string ToString()
        {
            string description = "";

            foreach (Card c in Cards)
            {
                description += c.ToString() + ", ";
            }

            description = description.Remove(description.Length - 2);
            int index = description.LastIndexOf(',');
            description = description.Remove(index, 1);
            description = description.Insert(index, " and");

            return description;
        }

        public void Sort()
        {
            Card temp;
            bool isSorted = false;

            while (!isSorted)
            {
                isSorted = true;

                for (int i = 0; i < 4; i++)
                {
                    if (Cards[i].CardValue > Cards[i + 1].CardValue)
                    {
                        temp = Cards[i + 1];
                        Cards[i + 1] = Cards[i];
                        Cards[i] = temp;
                        isSorted = false;
                    }
                }
            }
        }

        private void setValueCode()  // The hand is already sorted by the Poker.GetCombination method
        {                       
            switch ((int)PokerCombination)
            {
                case 1: // High Card
                    ValueCode += "A";
                    for (int i = Cards.Length - 1; i > 0; i--)
                    {
                        ValueCode += Cards[i].toChar();
                    }
                    break;
                case 2: // Pair
                    ValueCode += "B";
                    ValueCode += (char)(coreValue + 95);
                    for (int i = Cards.Length - 1; i > 0; i--)
                    {
                        if ((int)Cards[i].CardValue != coreValue)
                        {
                            ValueCode += Cards[i].toChar();
                        }
                    }
                    break;
                case 3: // Two Pair
                    ValueCode += "C";
                    ValueCode += (char)(coreValue + 95);

                    bool stop = false;
                    int secondPair = 0;
                    for (int i = 0; !stop && i < Cards.Length; i++)
                    {
                        if ((int)Cards[i].CardValue == coreValue)
                        {
                            continue;
                        }
                        else
                        {
                            for (int j = i + 1; j < Cards.Length; j++)
                            {
                                if ((int)Cards[j].CardValue == (int)Cards[i].CardValue)
                                {
                                    ValueCode += Cards[i].toChar();
                                    stop = true;
                                    secondPair = (int)Cards[i].CardValue;
                                }
                            }
                        }
                    }

                    foreach (Card c in Cards)
                    {
                        if ((int)c.CardValue != secondPair && (int)c.CardValue != coreValue)
                        {
                            ValueCode += c.toChar();
                        }
                    }
                    break;
                case 4: // Three of a Kind
                    ValueCode += "D";
                    ValueCode += (char)(coreValue + 95);
                    for (int i = Cards.Length - 1; i > 0; i--)
                    {
                        if ((int)Cards[i].CardValue != coreValue)
                        {
                            ValueCode += Cards[i].toChar();
                        }
                    }
                    break;
                case 5: // Straight
                    ValueCode += "E";
                    ValueCode += (char)(coreValue + 95);
                    break;
                case 6: // Flush
                    ValueCode += "F";
                    for (int i = Cards.Length - 1; i > 0; i--)
                    {
                        ValueCode += Cards[i].toChar();
                    }
                    break;
                case 7: // Full House
                    ValueCode += "G";
                    ValueCode += (char)(coreValue + 95);
                    for (int i = 0; i < Cards.Length; i++)
                    {
                        if ((int)Cards[i].CardValue != coreValue)
                        {
                            ValueCode += Cards[i].toChar();
                            break;
                        }
                    }
                    break;
                case 8: // Four of a Kind
                    ValueCode += "H";
                    ValueCode += (char)(coreValue + 95);
                    for (int i = 0; i < Cards.Length; i++)
                    {
                        if ((int)Cards[i].CardValue != coreValue)
                        {
                            ValueCode += Cards[i].toChar();
                            break;
                        }
                    }
                    break;
                case 9: // Straight Flush
                    ValueCode += "I";
                    ValueCode += (char)(coreValue + 95);
                    break;
                case 10: // Royal Flush
                    ValueCode += "J";
                    break;
            }
        }        
    }
}
