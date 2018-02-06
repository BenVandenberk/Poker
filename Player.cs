using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    class Player
    {
        public List<Card> Hand { get; private set; }
        public string Name { get; private set; }
        public List<PokerHand> AllPossibleHands { get; private set; }
        public PokerHand BestHand { get; set; }
        public bool Won { get; set; }
        public int Points { get; set; }

        public Player()
        {
            Hand = new List<Card>();
            Name = "";
            AllPossibleHands = new List<PokerHand>();
            BestHand = new PokerHand();
            Won = false;
            Points = 0;
        }

        public Player(string name)
        {
            Hand = new List<Card>();
            this.Name = name;
            AllPossibleHands = new List<PokerHand>();
            BestHand = new PokerHand();
            Won = false;
            Points = 0;
        }

        public void Muck()
        {
            Hand.Clear();
            AllPossibleHands.Clear();
            BestHand = new PokerHand();
            Won = false;
        }

        public void GetBestHand()
        {
            if (AllPossibleHands.Count != 21)
            {
                throw new Exception("Insufficient data");
            }            

            foreach (PokerHand ph in AllPossibleHands)
            {
                if (ph.ValueCode.CompareTo(BestHand.ValueCode) > 0)
                {                    
                    BestHand = ph;
                }
            }            
        }
    }
}
