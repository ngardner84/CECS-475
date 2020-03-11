﻿using System;
using Cecs475.BoardGames.Model;

namespace Cecs475.BoardGames.Chess.Model {
	/// <summary>
	/// Represents a single move to be applied to a chess board.
	/// </summary>
	public class ChessMove : IGameMove, IEquatable<ChessMove> {
		// You can add additional fields, properties, and methods as you find
		// them necessary, but you cannot MODIFY any of the existing implementations.

		/// <summary>
		/// The starting position of the move.
		/// </summary>
		public BoardPosition StartPosition { get; }

		/// <summary>
		/// The ending position of the move.
		/// </summary>
		public BoardPosition EndPosition { get; }

		/// <summary>
		/// The type of move being applied.
		/// </summary>
		public ChessMoveType MoveType { get; }

        // You must set this property when applying a move.
        public ChessPieceType PawnPieceType { get; }

		public int Player { get; set; }

		/// <summary>
		/// Constructs a ChessMove that moves a piece from one position to another
		/// </summary>
		/// <param name="start">the starting position of the piece to move</param>
		/// <param name="end">the position where the piece will end up</param>
		/// <param name="moveType">the type of move represented</param>
		public ChessMove(BoardPosition start, BoardPosition end, ChessMoveType moveType = ChessMoveType.Normal) {
			StartPosition = start;
			EndPosition = end;
			MoveType = moveType;
		}

        public ChessMove(BoardPosition start, BoardPosition end, ChessPieceType newPieceType, ChessMoveType moveType = ChessMoveType.PawnPromote)
        {
            StartPosition = start;
            EndPosition = end;
            MoveType = moveType;
            PawnPieceType = newPieceType;
        }

		// TODO: You must write this method.
		public virtual bool Equals(ChessMove other) {
			// Most chess moves are equal to each other if they have the same start and end position.
			// PawnPromote moves must also be promoting to the same piece type.
			if (this.StartPosition == other.StartPosition && this.EndPosition == other.EndPosition)
            {
                return true;
            }
            if (this.MoveType == ChessMoveType.PawnPromote && other.MoveType == ChessMoveType.PawnPromote && this.StartPosition == other.StartPosition && this.EndPosition == other.EndPosition)
            {
                return true;
            }
            return false;
		}



		// Equality methods.
		bool IEquatable<IGameMove>.Equals(IGameMove other) {
			ChessMove m = other as ChessMove;
			return this.Equals(m);
		}

		public override bool Equals(object other) {
			return Equals(other as ChessMove);
		}

		public override int GetHashCode() {
			unchecked {
				var hashCode = StartPosition.GetHashCode();
				hashCode = (hashCode * 397) ^ EndPosition.GetHashCode();
				hashCode = (hashCode * 397) ^ (int)MoveType;
				return hashCode;
			}
		}

		public override string ToString() {
			return $"{StartPosition} to {EndPosition}";
		}
	}
}
