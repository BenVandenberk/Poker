using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public enum CardValue { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

    public enum CardType { Clubs = 1, Hearts, Spades, Diamonds };

    public enum PokerCombination { High_Card = 1, Pair, Two_Pair, Three_of_a_Kind, Straight, Flush, Full_House, Four_of_a_Kind, Straight_Flush, Royal_Flush };

}
