using System;
using System.Collections.Generic;
using System.Text;
using Cecs475.BoardGames.Model;
using System.Linq;

namespace Cecs475.BoardGames.Chess.Model {
	/// <summary>
	/// Represents the board state of a game of chess. Tracks which squares of the 8x8 board are occupied
	/// by which player's pieces.
	/// </summary>
	public class ChessBoard : IGameBoard {
		#region Member fields.
		// The history of moves applied to the board.
		private List<ChessMove> mMoveHistory = new List<ChessMove>();

		public const int BoardSize = 8;

        // TODO: create a field for the board position array. You can hand-initialize
        // the starting entries of the array, or set them in the constructor.

        //starts from the left of every row
        //initial board state
        private byte[] BoardPositions = 
                {0b_1010_1011, 0b_1100_1101, 0b_1110_1100, 0b_1011_1010,
                0b_1001_1001, 0b_1001_1001, 0b_1001_1001, 0b_1001_1001,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_0001_0001, 0b_0001_0001, 0b_0001_0001, 0b_0001_0001,
                0b_0010_0011, 0b_0100_0101, 0b_0110_0100, 0b_0011_0010};




        // TODO: Add a means of tracking miscellaneous board state, like captured pieces and the 50-move rule.




        // TODO: add a field for tracking the current player and the board advantage.		
        int PlayerNumber = 1;

        int BoardAdvantage = 0;



		#endregion

		#region Properties.
		// TODO: implement these properties.
		// You can choose to use auto properties, computed properties, or normal properties 
		// using a private field to back the property.

		// You can add set bodies if you think that is appropriate, as long as you justify
		// the access level (public, private).

		public bool IsFinished { get { if (IsCheckmate == true) { return true; } return false; } }

		public int CurrentPlayer { get { if (mMoveHistory.Count() % 2 == 0) { return 1; } return 2; } }

		public GameAdvantage CurrentAdvantage { get { throw new NotImplementedException("You must implement this property."); } }

		public IReadOnlyList<ChessMove> MoveHistory => mMoveHistory;

		// TODO: implement IsCheck, IsCheckmate, IsStalemate
		public bool IsCheck {
			get { throw new NotImplementedException("You must implement this property."); }
		}

		public bool IsCheckmate {
			get { throw new NotImplementedException("You must implement this property."); }
		}

		public bool IsStalemate {
			get { throw new NotImplementedException("You must implement this property."); }
		}

		public bool IsDraw {
			get { throw new NotImplementedException("You must implement this property."); }
		}
		
		/// <summary>
		/// Tracks the current draw counter, which goes up by 1 for each non-capturing, non-pawn move, and resets to 0
		/// for other moves. If the counter reaches 100 (50 full turns), the game is a draw.
		/// </summary>
		public int DrawCounter {
			get { throw new NotImplementedException("You must implement this property."); }
		}
		#endregion


		#region Public methods.
		public IEnumerable<ChessMove> GetPossibleMoves() {
			List<ChessMove> possibleMoves = new List<ChessMove>();
			possibleMoves = (List<ChessMove>)GetPawnMoves();
			return possibleMoves;
		}

		public IEnumerable<ChessMove> GetPawnMoves()
		{
			List<ChessMove> pawnMoves = new List<ChessMove>();
			//Add en passant later
			//Player 1 goes upward
			//Player 2 goes downward
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					BoardPosition tempPosition = new BoardPosition(i, j);
					if (GetPieceAtPosition(tempPosition).PieceType == ChessPieceType.Pawn)
					{
						if (GetPlayerAtPosition(tempPosition) == 1 && PositionInBounds(tempPosition.Translate(tempPosition.Col, tempPosition.Row + 1)))
						{
						//player 1
						//check if pawn can move forward 2 spaces
							if (tempPosition.Row == 2 && PositionIsEmpty(new BoardPosition(tempPosition.Col, 4)) && PositionIsEmpty(new BoardPosition(tempPosition.Col, 3)))
							{
								pawnMoves.Append<ChessMove>(new ChessMove(tempPosition, tempPosition.Translate(0, 2)));
							}
							//check if position in front of the pawn is in bounds and empty
							if (GetPieceAtPosition(tempPosition.Translate(tempPosition.Col, tempPosition.Row + 1)).PieceType == ChessPieceType.Empty && tempPosition.Row == 7)
							{
								pawnMoves.Append<ChessMove>(new ChessMove(tempPosition, tempPosition.Translate(0, 1)));
							}
							//check if pawn is ready for pawn promotion
							if (GetPieceAtPosition(tempPosition.Translate(tempPosition.Col, tempPosition.Row + 1)).PieceType == ChessPieceType.Empty && tempPosition.Row == 7)
							{
								pawnMoves.Append<ChessMove>(new ChessMove(tempPosition, tempPosition.Translate(0, 1), ChessMoveType.PawnPromote));
							}
						}
						else
						{
							//player 2
							if (PositionInBounds(tempPosition.Translate(tempPosition.Col, tempPosition.Row - 1)))
							{
								if (tempPosition.Row == 7 && PositionIsEmpty(new BoardPosition(tempPosition.Col, 5)) && PositionIsEmpty(new BoardPosition(tempPosition.Col, 6)))
								{
									pawnMoves.Append<ChessMove>(new ChessMove(tempPosition, tempPosition.Translate(0, -2)));
								}
								//check if position in front of the pawn is in bounds and empty
								if (GetPieceAtPosition(tempPosition.Translate(tempPosition.Col, tempPosition.Row - 1)).PieceType == ChessPieceType.Empty && tempPosition.Row == 2)
								{
									pawnMoves.Append<ChessMove>(new ChessMove(tempPosition, tempPosition.Translate(0, -1)));
								}
								//check if pawn is ready for pawn promotion
								if (GetPieceAtPosition(tempPosition.Translate(tempPosition.Col, tempPosition.Row - 1)).PieceType == ChessPieceType.Empty && tempPosition.Row == 2)
								{
									pawnMoves.Append<ChessMove>(new ChessMove(tempPosition, tempPosition.Translate(0, -1), ChessMoveType.PawnPromote));
								}
							}
						}
					}
				}
			}
			return pawnMoves;
		}

		public void ApplyMove(ChessMove m) {
			ChessPiece tempPiece = GetPieceAtPosition(m.StartPosition);
			SetPieceAtPosition(m.EndPosition, tempPiece);
            mMoveHistory.Append<ChessMove>(m);
			//Add to list of moves made later
			//NEEDS TO BE DONE SOON
		}

		public void UndoLastMove() {
			throw new NotImplementedException("You must implement this method.");
		}
		
		/// <summary>
		/// Returns whatever chess piece is occupying the given position.
		/// </summary>
		public ChessPiece GetPieceAtPosition(BoardPosition position) {
            //BoardPosition has row and column
            //Need to use this to get an index in the byte array
            int indexNum = (position.Row * 4) + (position.Col / 2);
			//Check if position is on left or right side of byte
			if (position.Col % 2 == 1)
			{
				//if position is on the right side of the byte
				byte temp = BoardPositions[indexNum];
				var temp2 = temp >> 4;
				int y = (int)(temp2 << 4);
				int x = ((int)(temp)) - y;
				int tempPlayer;
				if (x >= 8)
				{
					tempPlayer = 2;
                    x = x - 8;
				}
				else
				{
					tempPlayer = 1;
				}
				if (x == 0)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Empty, 0);
					return tempPiece;
				}
				else if (x == 1)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Pawn, tempPlayer);
					return tempPiece;
				}
				else if (x == 2)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Rook, tempPlayer);
					return tempPiece;
				}
				else if (x == 3)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Knight, tempPlayer);
					return tempPiece;
				}
				else if (x == 4)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Bishop, tempPlayer);
					return tempPiece;
				}
				else if (x == 5)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Queen, tempPlayer);
					return tempPiece;
				}
					//King is only piece left possible
					ChessPiece tempPiece2 = new ChessPiece(ChessPieceType.King, tempPlayer);
					return tempPiece2;
			}
            else
            {
				//position is on left side of byte
				byte temp = BoardPositions[indexNum];
				int tempPlayer;
				int x = (int)(temp >> 4);
				if (x >= 8)
				{
					tempPlayer = 2;
                    x = x - 8;
				}
				else
				{
					tempPlayer = 1;
				}
				if (x == 0)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Empty, 0);
					return tempPiece;
				}
				else if (x == 1)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Pawn, tempPlayer);
					return tempPiece;
				}
				else if (x == 2)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Rook, tempPlayer);
					return tempPiece;
				}
				else if (x == 3)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Knight, tempPlayer);
					return tempPiece;
				}
				else if (x == 4)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Bishop, tempPlayer);
					return tempPiece;
				}
				else if (x == 5)
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Queen, tempPlayer);
					return tempPiece;
				}
				//King is the only piece left possible
				Console.WriteLine(x);
				ChessPiece tempPiece2 = new ChessPiece(ChessPieceType.King, tempPlayer);
				return tempPiece2;
			}
		}

		/// <summary>
		/// Returns whatever player is occupying the given position.
		/// </summary>
		public int GetPlayerAtPosition(BoardPosition pos) {
            if (GetPieceAtPosition(pos).Player == 0 && GetPieceAtPosition(pos).PieceType != ChessPieceType.Empty)
            {
                return 1;
            }
            else if (GetPieceAtPosition(pos).Player == 1 && GetPieceAtPosition(pos).PieceType != ChessPieceType.Empty)
            {
                return 2;
            }
			return 0;
		}

		/// <summary>
		/// Returns true if the given position on the board is empty.
		/// </summary>
		/// <remarks>returns false if the position is not in bounds</remarks>
		public bool PositionIsEmpty(BoardPosition pos) {
			if (PositionInBounds(pos) == false)
			{
				return false;
			}
			else if (GetPieceAtPosition(pos).PieceType == ChessPieceType.Empty)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the given position contains a piece that is the enemy of the given player.
		/// </summary>
		/// <remarks>returns false if the position is not in bounds</remarks>
		public bool PositionIsEnemy(BoardPosition pos, int player) {
			if (GetPieceAtPosition(pos).Player != player)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the given position is in the bounds of the board.
		/// </summary>
		public static bool PositionInBounds(BoardPosition pos) {
			if (pos.Col < 0 || pos.Col > 7)
			{
				return false;
			}
			if (pos.Row < 0 || pos.Col > 7)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Returns all board positions where the given piece can be found.
		/// </summary>
		public IEnumerable<BoardPosition> GetPositionsOfPiece(ChessPieceType piece, int player) {
			IEnumerable<BoardPosition> piecePositions = new List<BoardPosition>();
			for (int i = 0; i < 8; i++)
			{
				for (int k = 0; k < 8; k++)
				{
					BoardPosition tempPosition = new BoardPosition(i, k);
					if (GetPieceAtPosition(tempPosition).Player == player && GetPieceAtPosition(tempPosition).PieceType == piece)
					{
						piecePositions.Append(tempPosition);
					}
				}
			}
			return piecePositions;
		}

		/// <summary>
		/// Returns true if the given player's pieces are attacking the given position.
		/// </summary>
		public bool PositionIsAttacked(BoardPosition position, int byPlayer) {
			throw new NotImplementedException("You must implement this method.");
		}

		/// <summary>
		/// Returns a set of all BoardPositions that are attacked by the given player.
		/// </summary>
		public ISet<BoardPosition> GetAttackedPositions(int byPlayer) {
			throw new NotImplementedException("You must implement this method.");
		}
		#endregion

		#region Private methods.
		/// <summary>
		/// Mutates the board state so that the given piece is at the given position.
		/// </summary>
		private void SetPieceAtPosition(BoardPosition position, ChessPiece piece) {
			int indexNum = (position.Row * 4) + (position.Col / 2);
			if (position.Col % 2 == 1)
			{
				//if position is on the right side of the byte
				byte temp = BoardPositions[indexNum];
				var temp2 = temp >> 4;
				int leftSide = (int)(temp2 << 4);
				int rightSide = ((int)(temp)) - leftSide;
				int tempPlayer;
				byte tempByte;
				int newByte;
				if (rightSide >= 8)
				{
					tempPlayer = 8;
				}
				else
				{
					tempPlayer = 0;
				}
				if (piece.PieceType == ChessPieceType.Empty)
				{
					//take int of left side and add to right, then convert to binary
					newByte = leftSide + tempPlayer + 0;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
					//Create new byte and write to byte array
				}
				else if (piece.PieceType == ChessPieceType.Pawn)
				{
					newByte = leftSide + tempPlayer + 1;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Rook)
				{
					newByte = leftSide + tempPlayer + 2;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Knight)
				{
					newByte = leftSide + tempPlayer + 3;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Bishop)
				{
					newByte = leftSide + tempPlayer + 4;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Queen)
				{
					newByte = leftSide + tempPlayer + 5;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				//King is only piece left possible
				newByte = leftSide + tempPlayer + 6;
				tempByte = Convert.ToByte(newByte);
				BoardPositions[indexNum] = tempByte;
			}
			else
			{
				//position is on left side of byte
				byte temp = BoardPositions[indexNum];
				var temp2 = temp >> 4;
				int leftSide = (int)(temp2 << 4);
				int rightSide = ((int)(temp)) - leftSide;
				int tempPlayer;
				byte tempByte;
				int newByte;
				if (temp2 >= 8)
				{
					tempPlayer = 8;
				}
				else
				{
					tempPlayer = 0;
				}
				if (piece.PieceType == ChessPieceType.Empty)
				{
					newByte = leftSide + tempPlayer + 0;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
					//Create new byte and write to byte array
				}
				else if (piece.PieceType == ChessPieceType.Pawn)
				{
					newByte = leftSide + tempPlayer + 1;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Rook)
				{
					newByte = leftSide + tempPlayer + 2;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Knight)
				{
					newByte = leftSide + tempPlayer + 3;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Bishop)
				{
					newByte = leftSide + tempPlayer + 4;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Queen)
				{
					newByte = leftSide + tempPlayer + 5;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				//King is the only piece left possible
				newByte = leftSide + tempPlayer + 6;
				tempByte = Convert.ToByte(newByte);
				BoardPositions[indexNum] = tempByte;
			}
		}

		#endregion

		#region Explicit IGameBoard implementations.
		IEnumerable<IGameMove> IGameBoard.GetPossibleMoves() {
			return GetPossibleMoves();
		}
		void IGameBoard.ApplyMove(IGameMove m) {
			ApplyMove(m as ChessMove);
		}
		IReadOnlyList<IGameMove> IGameBoard.MoveHistory => mMoveHistory;
		#endregion

		// You may or may not need to add code to this constructor.
		public ChessBoard() {

		}

		public ChessBoard(IEnumerable<Tuple<BoardPosition, ChessPiece>> startingPositions)
			: this() {
			var king1 = startingPositions.Where(t => t.Item2.Player == 1 && t.Item2.PieceType == ChessPieceType.King);
			var king2 = startingPositions.Where(t => t.Item2.Player == 2 && t.Item2.PieceType == ChessPieceType.King);
			if (king1.Count() != 1 || king2.Count() != 1) {
				throw new ArgumentException("A chess board must have a single king for each player");
			}

			foreach (var position in BoardPosition.GetRectangularPositions(8, 8)) {
				SetPieceAtPosition(position, ChessPiece.Empty); 
			}

			int[] values = { 0, 0 };
			foreach (var pos in startingPositions) {
				SetPieceAtPosition(pos.Item1, pos.Item2);
				// TODO: you must calculate the overall advantage for this board, in terms of the pieces
				// that the board has started with. "pos.Item2" will give you the chess piece being placed
				// on this particular position.
			}
		}
	}
}
