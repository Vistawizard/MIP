using System;
using System.Collections.Generic;
using System.Linq;

namespace puzzle_game
{
    public partial class Board
    {
       
        // Returns an array of possible directions
        public Direction[] GetPossibleDirections()
        {
            List<Direction> result = new List<Direction>();
            
            int i = FindEmptyPos();
            
            // 2D coordinates
            int x = i / this.width;
            int y = i - x * this.width;
            
            if (!(x == 0))
                result.Add(Direction.UP);

            if (!(x == width - 1))
                result.Add(Direction.DOWN);
            
            if (!(y == 0)) 
                result.Add(Direction.LEFT);

            if (!(y == width - 1))
                result.Add(Direction.RIGHT);

            return result.ToArray();
        }
        
        // Swap two tiles in board 
        private void SwapTile(int i1, int i2)
        {
            if (this.board[i1].Type == this.board[i2].Type)
                throw new ArgumentException();
            
            Tile tmp = this.board[i1];
            this.board[i1] = this.board[i2];
            this.board[i2] = tmp;
        }
        
        // Moves the tiles by the direction
        // Returns true if it can move and false otherwise
        // If the board is already solved don't touch it
        public bool MoveDirection(Direction direct)
        {
            if (this.solved)
                return false;
            
            Tile tile = this.board[0];

            int i = FindEmptyPos();

            
            // 2D coordinates
            int x = i / this.width;
            int y = i - x * this.width;


            Direction[] directions = this.GetPossibleDirections();
            
            if (! directions.Contains(direct))
                return false;

            y += (direct == Direction.LEFT) ? -1 : (direct == Direction.RIGHT) ? 1 : 0;
            x += (direct == Direction.UP) ? -1 : (direct == Direction.DOWN) ? 1 : 0;
            
            
            // Swaping the tiles
            SwapTile(i, x * this.width + y);

            return true;
        }
        
        
        public void Shuffle(int nbr) 
        {
            if (nbr <= 0)
                throw new ArgumentException("nbr need to be strictly positive !");
            
            Random random = new Random();
            
            for (int i = 0; i < nbr; i++) {
                Direction[] possibleDirections = GetPossibleDirections();
                
                int index = random.Next(0, possibleDirections.Length);

                MoveDirection(possibleDirections[index]);
            }
        }
        
    }
}