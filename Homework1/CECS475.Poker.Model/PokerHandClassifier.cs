using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CECS475.Poker.Model
{
    public class PokerHandClassifier
    {
        public static PokerHand ClassifyHand(IEnumerable<Card> cards)
        {
            List<Card> cardList = new List<Card>();
            foreach (Card card in cards)
            {
                cardList.Add(card);
            }
            PokerHand hand = new PokerHand(cardList[0], cardList[1], cardList[2], cardList[3], cardList[4]);
            return hand;
        }
    }
}
