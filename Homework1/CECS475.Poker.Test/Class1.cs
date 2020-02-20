using System;
using System.Collections.Generic;
using System.Text;
using CECS475.Poker.Model;

namespace CECS475.Poker.Test
{
    class Class1
    {
        private void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            PokerHand newHand = new PokerHand
                (
                new Card(Card.CardKind.Ace, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Ace, Card.CardSuit.Spades),
                new Card(Card.CardKind.Ace, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Ace, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Three, Card.CardSuit.Clubs)
                );
            Console.WriteLine(newHand.CardHand.Count);
        }
    }
}
