using System.Collections.Generic;

namespace puzzle_game
{
    public partial class Board
    {
        
        // Checks if the board is correct
        public bool IsCorrect()
        {
            int previous = this.board[0].Index;
            
            foreach (Tile tile in this.board)
            {
                if (tile.Index < previous && tile.Type == TileType.FULL)
                    return false;
                previous = tile.Index;
            }

            return (previous == 0);
        }
        
        private int GetInvCount()
        {
            int[] board1 = new int[this.size];
            List<int> board2 = new List<int>();
            
            for (int i = 0; i < this.size; i++)
                board1[i] = this.board[i].Index;
            
            
            for (int y = 0; y < this.width; y++)
                for (int x = 0; x < this.width; x++)
                    board2.Add(board1[x * width + y]);

            board1 = board2.ToArray();
            
            int result = 0;
            for (int i = 0; i < this.width * this.width - 1; i++)
            for(int j = i + 1; j < this.width * this.width; j++)
                    if (board1[i] != 0 && board1[j] != 0 &&
                        board1[i] > board1[j])
                        result++;
            return result;
        }

        private int FindEmptyPos()
        {
            for (int i = 0; i < this.size; i++)
                if (this.board[i].Type == TileType.EMPTY)
                    return i;
            return -1;
        }

        // Returns true if the board is solvable
        public bool IsSolvable()
        {
            int invCount = GetInvCount();

            // If grid is odd
            // return true if invCount is even
            if (this.width % 2 == 1)
                return invCount % 2 == 0;
            // grid is even
            int pos = FindEmptyPos();
            
            // If pos is even
            if (pos % 2 == 1)
                return invCount % 2 == 0;
            
            return invCount % 2 == 1;
        }
    }
}