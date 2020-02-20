using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CECS475.Poker.Model
{
    public enum HandType
    {
        HighCard = 1,
        Pair,
        TwoPair,
        ThreeOfKind,
        Straight,
        Flush,
        FullHouse,
        FourOfKind,
        StraightFlush,
        RoyalFlush
    }


    public class PokerHand
    {
        public HandType PokerHandType
        {
            get
            {
                if (_CardHand[0].Kind == Card.CardKind.Ace && _CardHand[1].Kind == Card.CardKind.King && _CardHand[2].Kind == Card.CardKind.Queen && _CardHand[3].Kind == Card.CardKind.Jack && _CardHand[4].Kind == Card.CardKind.Ten &&
                    _CardHand[0].Suit == _CardHand[1].Suit && _CardHand[0].Suit == _CardHand[2].Suit && _CardHand[0].Suit == _CardHand[3].Suit && _CardHand[0].Suit == _CardHand[4].Suit)
                {
                    return HandType.RoyalFlush;
                }
                else if ((_CardHand[4].Kind - _CardHand[0].Kind) == 4 &&
                    _CardHand[0].Suit == _CardHand[1].Suit && _CardHand[0].Suit == _CardHand[2].Suit && _CardHand[0].Suit == _CardHand[3].Suit && _CardHand[0].Suit == _CardHand[4].Suit)
                {
                    return HandType.StraightFlush;
                }
                int fourCounter = 0;
                int fourCounter2 = 0;
                if (_CardHand[0].Kind == _CardHand[1].Kind) { fourCounter++; } if (_CardHand[1].Kind == _CardHand[2].Kind) { fourCounter2++; }
                if (_CardHand[0].Kind == _CardHand[2].Kind) { fourCounter++; } if (_CardHand[1].Kind == _CardHand[3].Kind) { fourCounter2++; }
                if (_CardHand[0].Kind == _CardHand[3].Kind) { fourCounter++; } if (_CardHand[1].Kind == _CardHand[4].Kind) { fourCounter2++; }
                if (_CardHand[0].Kind == _CardHand[4].Kind) { fourCounter++; }
                if (fourCounter == 3 || fourCounter2 == 3)
                {
                    return HandType.FourOfKind;
                }
                int houseCounter = 0;
                //check for three of a kind and then a pair
                if (_CardHand[0].Kind == _CardHand[1].Kind) { houseCounter++; }
                if (_CardHand[0].Kind == _CardHand[2].Kind) { houseCounter++; }
                if (_CardHand[2].Kind == _CardHand[3].Kind) { houseCounter++; }
                if (_CardHand[2].Kind == _CardHand[4].Kind) { houseCounter++; }
                if (_CardHand[3].Kind == _CardHand[4].Kind) { houseCounter++; }
                
                if (houseCounter == 3)
                {
                    return HandType.FullHouse;
                }
                else if (_CardHand[0].Suit == _CardHand[1].Suit && _CardHand[0].Suit == _CardHand[2].Suit && _CardHand[0].Suit == _CardHand[3].Suit &&
                    _CardHand[0].Suit == _CardHand[4].Suit)
                {
                    return HandType.Flush;
                }
                else if ((_CardHand[4].Kind - _CardHand[0].Kind) == 4)
                {
                    return HandType.Straight;
                }
                int threeCounter = 0;
                int threeCounter2 = 0;
                int threeCounter3 = 0;
                if (_CardHand[0].Kind == _CardHand[1].Kind) { threeCounter++; }
                if (_CardHand[0].Kind == _CardHand[2].Kind) { threeCounter++; }
                if (_CardHand[0].Kind == _CardHand[3].Kind) { threeCounter++; }
                if (_CardHand[0].Kind == _CardHand[4].Kind) { threeCounter++; }
                if (_CardHand[1].Kind == _CardHand[2].Kind) { threeCounter2++; }
                if (_CardHand[1].Kind == _CardHand[3].Kind) { threeCounter2++; }
                if (_CardHand[1].Kind == _CardHand[4].Kind) { threeCounter2++; }
                if (_CardHand[2].Kind == _CardHand[3].Kind) { threeCounter3++; }
                if (_CardHand[2].Kind == _CardHand[4].Kind) { threeCounter3++; }
                if (threeCounter == 2 || threeCounter2 == 2 || threeCounter3 == 2)
                {
                    return HandType.ThreeOfKind;
                }
                int pairCounter = 0; int pairCounter2 = 0;
                int pairCounter3 = 0; int pairCounter4 = 0;
                if (_CardHand[0].Kind == _CardHand[1].Kind) { pairCounter++; } if (_CardHand[1].Kind == _CardHand[2].Kind) { pairCounter2++; }
                if (_CardHand[0].Kind == _CardHand[2].Kind) { pairCounter++; } if (_CardHand[1].Kind == _CardHand[3].Kind) { pairCounter2++; }
                if (_CardHand[0].Kind == _CardHand[3].Kind) { pairCounter++; } if (_CardHand[1].Kind == _CardHand[4].Kind) { pairCounter2++; }
                if (_CardHand[0].Kind == _CardHand[4].Kind) { pairCounter++; } if (_CardHand[2].Kind == _CardHand[3].Kind) { pairCounter3++; }
                if (_CardHand[2].Kind == _CardHand[4].Kind) { pairCounter3++; } if (_CardHand[3].Kind == _CardHand[4].Kind) { pairCounter4++; }
                if ((pairCounter + pairCounter2 + pairCounter3 + pairCounter4) == 2)
                {
                    return HandType.TwoPair;
                }
                else if (pairCounter == 1 || pairCounter2 == 1 || pairCounter3 == 1 || pairCounter4 == 1)
                {
                    return HandType.Pair;
                }
                else
                {
                    return HandType.HighCard;
                }
            }
        }
        public PokerHand(Card card1, Card card2, Card card3, Card card4, Card card5)
        {
            _CardHand.Add(card1); _CardHand.Add(card2); _CardHand.Add(card3); _CardHand.Add(card4); _CardHand.Add(card5);
            //Compare the cards by the kind, not sure why already implemented CompareTo in Card doesn't work
            _CardHand.Sort((x, y) => x.Kind.CompareTo(y.Kind));
        }
        public static HandType HandType { get; }
        private List<Card> _CardHand = new List<Card>();
        public List<Card> CardHand
        {
            get
            {
                return _CardHand;
            }
        }
    }
}
