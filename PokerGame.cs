using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    class PokerGame
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Deck Deck { get; private set; }
        public List<Card> TableCards { get; private set; }     
        public Player Winner { get; private set; }

        public PokerGame()
        {
            Player1 = new Player();
            Player2 = new Player();
            Deck = new Deck();
            TableCards = new List<Card>();
        }

        public PokerGame(string name1, string name2)
        {
            Player1 = new Player(name1);
            Player2 = new Player(name2);
            Deck = new Deck();
            TableCards = new List<Card>();
        }

        public void Reset()
        {
            Deck.Restore();
            Player1.Muck();
            Player2.Muck();
            TableCards.Clear();
            Winner = null;
        }

        public void DecideWinner()
        {
            loadAllPossibleHands(Player1);
            loadAllPossibleHands(Player2);

            Player1.GetBestHand();
            Player2.GetBestHand();

            if (Player1.BestHand.ValueCode.CompareTo(Player2.BestHand.ValueCode) > 0)
            {
                Winner = Player1;
                Player1.Won = true;
                Player1.Points++;
            }
            else if (Player1.BestHand.ValueCode.CompareTo(Player2.BestHand.ValueCode) < 0)
            {
                Winner = Player2;
                Player2.Won = true;
                Player2.Points++;
            }
            else
            {
                Player1.Won = true;
                Player2.Won = true;
                Player1.Points++;
                Player2.Points++;
            }
        }

        private void loadAllPossibleHands(Player Player)
        {
            Card[] current = new Card[5];
            int index = 0;
            
            Player.AllPossibleHands.Add(new PokerHand(TableCards.ToArray())); // 1 Hand with 5 table cards        

            for (int c = 0; c < Player.Hand.Count; c++) // 10 Hands with 4 table cards
            {
                current[index++] = Player.Hand[c];
                for (int c2 = 0; c2 < TableCards.Count; c2++)
                {
                    for (int c3 = 0; c3 < TableCards.Count; c3++)
                    {
                        if (c3 != c2)
                        {
                            current[index++] = TableCards[c3];
                        }
                    }
                    index = 1;
                    Player.AllPossibleHands.Add(new PokerHand(current));                  
                }
                index = 0;
            }

            current[0] = Player.Hand[0]; // 10 Hands with 3 table cards)
            current[1] = Player.Hand[1];

            for (int c = 0; c < TableCards.Count; c++)
            {
                current[2] = TableCards[c];
                for (int c2 = c + 1; c2 < TableCards.Count; c2++)
                {
                    current[3] = TableCards[c2];
                    for (int c3 = c2 + 1; c3 < TableCards.Count; c3++)
                    {
                        current[4] = TableCards[c3];
                        Player.AllPossibleHands.Add(new PokerHand(current));
                        current[4] = null;
                    }
                    current[3] = null;
                }
                current[2] = null;
            }
        }
    }
}
