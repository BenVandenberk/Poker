using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public static class PokerHelp
    {
        public static PokerCombination GetCombination(PokerHand pokerHand, out int coreValue)
        {
            bool flush = true;
            bool straight = true;
            List<List<int>> doubles = new List<List<int>>();
            int index = 0;
            int temp = 0;

            for (int i = 1; flush && i < 5; i++) // Test for flush
            {
                if (pokerHand.Cards[i].CardType != pokerHand.Cards[0].CardType)
                {
                    flush = false;
                }
            }

            pokerHand.Sort();

            for (int i = 0; i < 4; i++) // Test for straight
            {
                if ((int)pokerHand.Cards[i].CardValue + 1 != (int)pokerHand.Cards[i + 1].CardValue)
                {
                    straight = false;
                }
            }

            if (pokerHand.Cards[4].CardValue == CardValue.Ace && flush && straight)
            {
                coreValue = (int)CardValue.Ace;
                return PokerCombination.Royal_Flush;
            }
            else if (flush && straight)
            {
                coreValue = (int)pokerHand.Cards[4].CardValue;
                return PokerCombination.Straight_Flush;
            }
            
            foreach (Card c in pokerHand.Cards) // Put doubles together
            {
                if (doubles.Count == 0)
                {
                    doubles.Add(new List<int>());
                    doubles[index].Add((int)c.CardValue);
                }
                else if ((int)c.CardValue == doubles[index][0])
                {
                    doubles[index].Add((int)c.CardValue);
                }
                else
                {
                    doubles.Add(new List<int>());
                    doubles[++index].Add((int)c.CardValue);
                }
            }

            if (doubles.Count == 2)
            {
                coreValue = doubles[0].Count > doubles[1].Count ? doubles[0][0] : doubles[1][0];
                if (doubles[0].Count == 1 || doubles[0].Count == 4)
                {
                    return PokerCombination.Four_of_a_Kind;
                }
                else
                {
                    return PokerCombination.Full_House;
                }
            }

            if (flush)
            {
                coreValue = (int)pokerHand.Cards[4].CardValue;
                return PokerCombination.Flush;
            }

            if (straight)
            {
                coreValue = (int)pokerHand.Cards[4].CardValue;
                return PokerCombination.Straight;
            }

            if (doubles.Count == 3)
            {
                if (doubles[0].Count == 3 || doubles[1].Count == 3 || doubles[2].Count == 3)
                {
                    foreach (List<int> l in doubles)
                    {
                        if (l.Count == 3)
                        {
                            temp = l[0];
                        }                           
                    }
                    coreValue = temp;
                    return PokerCombination.Three_of_a_Kind;
                }
                else
                {
                    foreach (List<int> l in doubles)
                    {
                        if (l.Count == 2 && l[0] > temp)
                        {
                            temp = l[0];
                        }
                    }
                    coreValue = temp;
                    return PokerCombination.Two_Pair;
                }
            }

            if (doubles.Count == 4)
            {
                foreach (List<int> l in doubles)
                {
                    if (l.Count == 2)
                    {
                        temp = l[0];
                    }
                }
                coreValue = temp;
                return PokerCombination.Pair;
            }

            coreValue = (int)pokerHand.Cards[4].CardValue;
            return PokerCombination.High_Card;
        }

        public static Card StringToCard(string cardString)
        {
            CardType CardType = CardType.Hearts;
            int CardValue;

            if (!int.TryParse(cardString.Substring(0,1), out CardValue))
            {
                switch (cardString.Substring(0,1))
                {
                    case "T":
                        CardValue = 10;
                        break;
                    case "J":
                        CardValue = 11;
                        break;
                    case "Q":
                        CardValue = 12;
                        break;
                    case "K":
                        CardValue = 13;
                        break;
                    case "A":
                        CardValue = 14;
                        break;
                }
            }

            switch (cardString.Substring(1, 1))
            {
                case "C":
                    CardType = CardType.Clubs;
                    break;
                case "S":
                    CardType = CardType.Spades;
                    break;
                case "H":
                    CardType = CardType.Hearts;
                    break;
                case "D":
                    CardType = CardType.Diamonds;
                    break;
            }

            return new Card((CardValue)CardValue, CardType);
        }

        public static string PokerCombinationToString(PokerCombination pc)
        {
            return pc.ToString().Replace('_', ' ');
        }
    }
}
