using System;
using System.Collections.Generic;
using System.Text;

using Cecs475.BoardGames.Model;
using Cecs475.BoardGames.Chess.Test;
using Cecs475.BoardGames.Chess.Model;
using Cecs475.BoardGames.Chess.View;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace Lab2ChessTests {
	public class NathanGardner : ChessTest {

		/* This is where you will write your tests.
		 * Each test must be marked with the [Test] attribute.
		 * 
		 * Double check that you follow these rules:
		 * 
		 * 0. RENAME THIS FILE to YourName.cs, but USE YOUR ACTUAL NAME.
		 * 1. Every test method should have a meaningful name.
		 * 2. Every Should() must include a string description of the expectation.
		 * 3. Your buster test should be LAST in this file, and should be given a meaningful name
		 *		FOLLOWED BY AN UNDERSCORE, followed by the LAST 6 digits of your student ID.
		 *		Example:
		 *		
		 *		If my ID is 012345678 and involves undoing a castling move, my test might be named
		 *		UndoCastleQueenSide_345678
		 *	
		 */


            //Use lichess.org for an editable chess board
		[Fact]
        public void KingStalemate()
        {
            ChessBoard b = CreateBoardFromPositions(
                Pos("a7"), ChessPieceType.King, 2,
                Pos("b1"), ChessPieceType.King, 1,
                Pos("b2"), ChessPieceType.Rook, 1,
                Pos("c6"), ChessPieceType.Rook, 1);
            Apply(b, Move("c6, c7"));
            Apply(b, Move("a7, a8"));
            Apply(b, Move("b2, b7"));
            var pos = b.GetPossibleMoves();
            pos.Should().BeEmpty("Stalemate should not have possible moves for pieces");
            var KingMoves = GetMovesAtPosition(pos, Pos("a8"));
            KingMoves.Should().HaveCount(0, "King should not be able to move anywhere");
            b.PositionIsAttacked(Pos("a8"), 1).Should().BeFalse("The square that the king is on should be not attacked");
            b.IsFinished.Should().BeTrue("Game should be finished in the stalemate state");
            b.IsCheckmate.Should().BeFalse("King should not be in checkmate since the square the king is on is not attacked");
        }

        [Fact]
        public void UndoKnightKingSide()
        {
            ChessBoard b = CreateBoardFromMoves(
              "c2, c4",
              "h7, h5",
              "b1, c3",
              "e7, e5");
            Apply(b, Move("g1, f3"));
            b.PositionIsEmpty(Pos("g1")).Should().BeTrue();
            b.UndoLastMove();
            b.GetPieceAtPosition(Pos("g1")).PieceType.Should().Be(ChessPieceType.Knight, "Undo should put knight back into starting position");
            

        }

        [Fact]
        public void UndoPawnQueenSide()
        {
            ChessBoard b = CreateBoardFromMoves(
                "d2, d4",
                "c7, c5");
            Apply(b, Move("d4, c5"));
            b.PositionIsEmpty(Pos("d4")).Should().BeTrue();
            b.UndoLastMove();
            b.GetPieceAtPosition(Pos("d4")).PieceType.Should().Be(ChessPieceType.Pawn, "Undo should put pawn back into position before taking other pawn");
            b.GetPieceAtPosition(Pos("c5")).PieceType.Should().Be(ChessPieceType.Pawn, "Undo should put taken pawn back into previous position");
        }

        [Fact]
        public void QueenPossibleMoves()
        {
            ChessBoard b = CreateBoardFromMoves(
                "d2, d4",
                "b8, c6",
                "d1, d3",
                "c6, b8",
                "d4, d5",
                "b8, c6",
                "d5, c6",
                "b7, c6"
                );
            var possibleMoves = b.GetPossibleMoves();
            var QueenMoves = GetMovesAtPosition(possibleMoves, Pos("d3"));
            QueenMoves.Should().HaveCount(20, "Queen can move in 20 different squares this turn");
        }

        [Fact]
        public void BishopKingCheck()
        {
            ChessBoard b = CreateBoardFromMoves(
                "e2, e4",
                "d7, d6",
                "f1, b5"
                );
            //put black king in check
            var possibleMoves = b.GetPossibleMoves();
            var KingMoves = GetMovesAtPosition(possibleMoves, Pos("e8"));
            KingMoves.Should().BeEmpty("King cannot move while in check in this position");
            b.IsCheck.Should().BeTrue("The black king should be in check");
            b.IsCheckmate.Should().BeFalse("The black king should not be in checkmate");
        }

        [Fact]
        public void InitialState()
        {
            ChessBoard b = new ChessBoard();
            var possibleMoves = b.GetPossibleMoves();
            foreach (var pos in GetPositionsInRank(2))
            {
                b.GetPieceAtPosition(pos).PieceType.Should().Be(ChessPieceType.Pawn, "Chess piece should be a pawn");
            }
            b.GetPieceAtPosition(Pos("h1")).PieceType.Should().Be(ChessPieceType.Rook, "Chess piece should be a rook");
            b.GetPieceAtPosition(Pos("g1")).PieceType.Should().Be(ChessPieceType.Knight, "Chess piece should be a knight");
            b.GetPieceAtPosition(Pos("f1")).PieceType.Should().Be(ChessPieceType.Bishop, "Chess piece should be a bishop");
            b.GetPieceAtPosition(Pos("e1")).PieceType.Should().Be(ChessPieceType.King, "Chess piece should be a king");
            b.GetPieceAtPosition(Pos("d1")).PieceType.Should().Be(ChessPieceType.Queen, "Chess piece should be a queen");
            b.GetPieceAtPosition(Pos("c1")).PieceType.Should().Be(ChessPieceType.Bishop, "Chess piece should be a bishop");
            b.GetPieceAtPosition(Pos("b1")).PieceType.Should().Be(ChessPieceType.Knight, "Chess piece should be a knight");
            b.GetPieceAtPosition(Pos("a1")).PieceType.Should().Be(ChessPieceType.Rook, "Chess piece should be a rook");

            //Do other side of board
            foreach (var pos in GetPositionsInRank(7))
            {
                b.GetPieceAtPosition(pos).PieceType.Should().Be(ChessPieceType.Pawn, "Chess piece should be a pawn");
            }
            b.GetPieceAtPosition(Pos("h8")).PieceType.Should().Be(ChessPieceType.Rook, "Chess piece should be a rook");
            b.GetPieceAtPosition(Pos("g8")).PieceType.Should().Be(ChessPieceType.Knight, "Chess piece should be a knight");
            b.GetPieceAtPosition(Pos("f8")).PieceType.Should().Be(ChessPieceType.Bishop, "Chess piece should be a bishop");
            b.GetPieceAtPosition(Pos("e8")).PieceType.Should().Be(ChessPieceType.King, "Chess piece should be a king");
            b.GetPieceAtPosition(Pos("d8")).PieceType.Should().Be(ChessPieceType.Queen, "Chess piece should be a queen");
            b.GetPieceAtPosition(Pos("c8")).PieceType.Should().Be(ChessPieceType.Bishop, "Chess piece should be a bishop");
            b.GetPieceAtPosition(Pos("b8")).PieceType.Should().Be(ChessPieceType.Knight, "Chess piece should be a knight");
            b.GetPieceAtPosition(Pos("a8")).PieceType.Should().Be(ChessPieceType.Rook, "Chess piece should be a rook");
            
            for (int i = 3; i < 7; i++)
            {
                foreach (var pos in GetPositionsInRank(i))
                {
                    b.GetPieceAtPosition(pos).PieceType.Should().Be(ChessPieceType.Empty, "Square should be empty");
                }
            }
        }

        [Fact]
        public void FoolsMate_680753()
        {
            ChessBoard b = CreateBoardFromMoves(
                "f2, f3",
                "e7, e5",
                "g2, g4");
            Apply(b, Move("d8, h4"));
            b.DrawCounter.Should().Be(1, "Pawns should not increment the draw counter");
            var possibleMoves = b.GetPossibleMoves();
            var KingMoves = GetMovesAtPosition(possibleMoves, Pos("e1"));
            b.IsCheckmate.Should().BeTrue();
            KingMoves.Should().HaveCount(0, "King should not have any move while in checkmate");
            b.IsFinished.Should().BeTrue();
            //undo checkmate
            b.UndoLastMove();
            b.DrawCounter.Should().Be(0, "Undoing the queen's move should decrement the draw counter");
            possibleMoves = b.GetPossibleMoves();
            KingMoves = GetMovesAtPosition(possibleMoves, Pos("e1"));
            b.IsCheckmate.Should().BeFalse();
            b.IsFinished.Should().BeFalse();
            //KingMoves.Should().HaveCount(1, "King should be able to move in one spot after undoing checkmate")
            //    .And.Contain(Move("e1, f2"));

        }
	}
}
