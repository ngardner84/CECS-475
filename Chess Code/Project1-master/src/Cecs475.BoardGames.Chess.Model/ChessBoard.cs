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
                {0b_0010_0011, 0b_0100_0101, 0b_0110_0100, 0b_0011_0010,
                0b_0001_0001, 0b_0001_0001, 0b_0001_0001, 0b_0001_0001,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_0000_0000, 0b_0000_0000, 0b_0000_0000, 0b_0000_0000,
                0b_1001_1001, 0b_1001_1001, 0b_1001_1001, 0b_1001_1001,
                0b_1010_1011, 0b_1100_1101, 0b_1110_1100, 0b_1011_1010};




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

		public bool IsFinished { get { throw new NotImplementedException("You must implement this property."); } }

		public int CurrentPlayer { get { throw new NotImplementedException("You must implement this property."); } }

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
			throw new NotImplementedException("You must implement this method.");
		}

		public IEnumerable<ChessMove> GetPawnMoves(int player)
		{
			List<ChessMove> pawnMoves = new List<ChessMove>();
			//Add en passant later
			for (int i = 1; i < 7; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					if (GetPieceAtPosition(new BoardPosition(i, j)).PieceType == ChessPieceType.Pawn)
					{
						if (GetPlayerAtPosition(new BoardPosition(i, j)) == 1)
						{
							//player 1

						}
						else
						{
							//player 2
						}
					}
				}
			}
			return pawnMoves;
		}

		public void ApplyMove(ChessMove m) {
			// STRONG RECOMMENDATION: any mutation to the board state should be run
			// through the method SetPieceAtPosition.
			throw new NotImplementedException("You must implement this method.");
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
            int indexNum = ((((position.Col - 1) * 4) + ((position.Row - 1) * 32)) / 4);
			//Check if position is on left or right side of byte
			if (position.Col % 2 == 1)
			{
				//if position is on the left side of the byte
				byte temp = BoardPositions[indexNum];
				byte y = (byte)(temp >> 4);
				string tempString = y.ToString();
				int tempPlayer;
				if (tempString[0] == '0')
				{
					tempPlayer = 1;
				}
				else
				{
					tempPlayer = 2;
				}
				if (tempString.Substring(1,3) == "000")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Empty, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1,3) == "001")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Pawn, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "010")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Rook, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "011")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Knight, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "100")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Bishop, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "101")
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
				//position is on right side of byte
				byte temp = BoardPositions[indexNum];
				string tempString = temp.ToString();
				int tempPlayer;
				if (tempString[0] == '0')
				{
					tempPlayer = 1;
				}
				else
				{
					tempPlayer = 2;
				}
				if (tempString.Substring(1, 3) == "000")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Empty, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "001")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Pawn, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "010")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Rook, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "011")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Knight, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "100")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Bishop, tempPlayer);
					return tempPiece;
				}
				else if (tempString.Substring(1, 3) == "101")
				{
					ChessPiece tempPiece = new ChessPiece(ChessPieceType.Queen, tempPlayer);
					return tempPiece;
				}
				//King is the only piece left possible
				ChessPiece tempPiece2 = new ChessPiece(ChessPieceType.King, tempPlayer);
				return tempPiece2;
			}
		}

		/// <summary>
		/// Returns whatever player is occupying the given position.
		/// </summary>
		public int GetPlayerAtPosition(BoardPosition pos) {
			return GetPieceAtPosition(pos).Player;
		}

		/// <summary>
		/// Returns true if the given position on the board is empty.
		/// </summary>
		/// <remarks>returns false if the position is not in bounds</remarks>
		public bool PositionIsEmpty(BoardPosition pos) {
			if (GetPieceAtPosition(pos).PieceType == ChessPieceType.Empty)
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
			if (pos.Col < 0 || pos.Col > 8)
			{
				return false;
			}
			if (pos.Row < 0 || pos.Col > 8)
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
			int indexNum = ((((position.Col - 1) * 4) + ((position.Row - 1) * 32)) / 4);
			if (position.Col % 2 == 1)
			{
				//if position is on the right side of the byte
				byte temp = BoardPositions[indexNum];
				byte y = (byte)temp;
				string tempString = y.ToString();
				string leftString = tempString.Substring(0, 4);
				y = (byte)(temp << 4);
				tempString = y.ToString();
				string rightString = tempString.Substring(0, 3);
				string tempPlayer;
				string newByte;
				byte tempByte;
				if (tempString[0] == '0')
				{
					tempPlayer = "0";
				}
				else
				{
					tempPlayer = "1";
				}
				if (piece.PieceType == ChessPieceType.Empty)
				{
					newByte = leftString + "_" + tempPlayer + "000";
					tempByte = Convert.ToByte(newByte, 2);
					BoardPositions[indexNum] = tempByte;
					//Create new byte and write to byte array
				}
				else if (piece.PieceType == ChessPieceType.Pawn)
				{
					newByte = leftString + "_" + tempPlayer + "001";
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Rook)
				{
					newByte = leftString + "_" + tempPlayer + "010";
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Knight)
				{
					newByte = leftString + "_" + tempPlayer + "011";
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Bishop)
				{
					newByte = leftString + "_" + tempPlayer + "100";
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (piece.PieceType == ChessPieceType.Queen)
				{
					newByte = leftString + "_" + tempPlayer + "101";
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				//King is only piece left possible
				newByte = leftString + "_" + tempPlayer + "110";
				tempByte = Convert.ToByte(newByte);
				BoardPositions[indexNum] = tempByte;
			}
			else
			{
				//position is on right side of byte
				byte temp = BoardPositions[indexNum];
				string tempString = temp.ToString();
				string tempPlayer;
				string rightString = tempString.Substring(4, 4);
				string newByte;
				byte tempByte;
				if (tempString[0] == '0')
				{
					tempPlayer = "0";
				}
				else
				{
					tempPlayer = "1";
				}
				if (piece.PieceType == ChessPieceType.Empty)
				{
					newByte = tempPlayer + "000" + rightString;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
					//Create new byte and write to byte array
				}
				else if (tempString.Substring(1, 3) == "001")
				{
					newByte = tempPlayer + "001" + rightString;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (tempString.Substring(1, 3) == "010")
				{
					newByte = tempPlayer + "010" + rightString;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (tempString.Substring(1, 3) == "011")
				{
					newByte = tempPlayer + "011" + rightString;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (tempString.Substring(1, 3) == "100")
				{
					newByte = tempPlayer + "100" + rightString;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				else if (tempString.Substring(1, 3) == "101")
				{
					newByte = tempPlayer + "101" + rightString;
					tempByte = Convert.ToByte(newByte);
					BoardPositions[indexNum] = tempByte;
				}
				//King is the only piece left possible
				newByte = tempPlayer + "110" + rightString;
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
