﻿using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using FluentAssertions;
using CECS475.Poker.Model;

namespace Cecs475.Poker.Test
{
    public class Program

    {
        [Fact]
        public void FourOfAKind()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Ace, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Ace, Card.CardSuit.Spades),
                new Card(Card.CardKind.Ace, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Ace, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Three, Card.CardSuit.Clubs)});

            h.PokerHandType.Should().Be(HandType.FourOfKind);
        }

        [Fact]
        public void ThreeOfAKind()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Four, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Nine, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Four, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Ace, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Four, Card.CardSuit.Spades)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.ThreeOfKind);
        }

        [Fact]
        public void FullHouse()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Four, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Nine, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Four, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Nine, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Four, Card.CardSuit.Spades)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.FullHouse);
        }

        [Fact]
        public void Pair()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Two, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Jack, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Jack, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Nine, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Four, Card.CardSuit.Spades)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.Pair);
        }

        [Fact]
        public void TwoPair()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Queen, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Jack, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Ten, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Queen, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Ten, Card.CardSuit.Spades)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.TwoPair);
        }

        [Fact]
        public void Straight()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Nine, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Queen, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Jack, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Eight, Card.CardSuit.Spades),
                new Card(Card.CardKind.Ten, Card.CardSuit.Hearts)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.Straight);
        }

        [Fact]
        public void Flush()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Queen, Card.CardSuit.Spades),
                new Card(Card.CardKind.Jack, Card.CardSuit.Spades),
                new Card(Card.CardKind.Ace, Card.CardSuit.Spades),
                new Card(Card.CardKind.Three, Card.CardSuit.Spades),
                new Card(Card.CardKind.Five, Card.CardSuit.Spades)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.Flush);
        }

        [Fact]
        public void StraightFlush()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Nine, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Queen, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Jack, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Eight, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Ten, Card.CardSuit.Diamonds)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.StraightFlush);
        }

        [Fact]
        public void HighCard()
        {
            PokerHand h = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Queen, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Jack, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Ten, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Two, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Five, Card.CardSuit.Spades)});

            var type = h.PokerHandType;
            type.Should().Be(HandType.HighCard);
        }

        [Fact]
        public void CompareHighCard()
        {
            PokerHand h1 = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Queen, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Jack, Card.CardSuit.Clubs),
                new Card(Card.CardKind.Ten, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Two, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Five, Card.CardSuit.Spades)});

            PokerHand h2 = PokerHandClassifier.ClassifyHand(new Card[] {
                new Card(Card.CardKind.Queen, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Jack, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Three, Card.CardSuit.Hearts),
                new Card(Card.CardKind.Two, Card.CardSuit.Diamonds),
                new Card(Card.CardKind.Five, Card.CardSuit.Spades)});

            //h1.ShouldBeEquivalentTo(h1);
        }


        [Fact]
        public void HandTypeOrder()
        {
            var types = Enum.GetValues(typeof(HandType));
            types.Should().ContainInOrder(HandType.HighCard,
                HandType.Pair,
                HandType.TwoPair,
                HandType.ThreeOfKind,
                HandType.Straight,
                HandType.Flush,
                HandType.FullHouse,
                HandType.FourOfKind,
                HandType.StraightFlush,
                HandType.RoyalFlush);
        }
    }
}

