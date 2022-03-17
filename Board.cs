using System;
using System.Collections.Generic;
using System.Linq;

namespace puzzle_game
{
    public partial class Board
    {
        private int size;
        private int width;
        private bool solved = false;
        private Tile[] board;


        public bool Solved => solved;

        public int Size => size;

        public int Width => width;

        public Tile[] Board1 => board;

        // Board constructor
        public Board(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Size needs to be positive !");

            // Checking if size is a perfect square
            if ( ! (Math.Sqrt(size) % 1 == 0) )
                throw new ArgumentException("Size needs to be a perfect square !");

            this.size = size;
            this.width = Convert.ToInt32(Math.Sqrt(this.size));
            this.board = new Tile[size];
            this.solved = false;
        }

        public Board DeepCopy()
        {
            Board copy = (Board) this.MemberwiseClone();

            copy.board = new Tile[copy.size];

            for (int i = 0; i < copy.size; i++)
                copy.board[i] = this.board[i].DeepCopy();

            return copy;
        }

        private bool AreConsecutive(int []arr)
        {
                if (arr.Length < 1)
                    return false;

                int min = arr.Min();
                int max = arr.Max();

                if (max - min + 1 != arr.Length)
                    return false;

                bool[] visited = new bool[arr.Length];

                for (int i = 0; i < arr.Length; i++) {
                    if (visited[arr[i] - min])
                        return false;
                    visited[arr[i] - min] = true;
                }
                return true;
        }


        public void Fill(int[] array)
        {
            // The 0 represent the empty TILE
            if (array.Length != size)
                throw new ArgumentException("Size need to match !");

            if (! AreConsecutive(array))
                throw new ArgumentException("Array should be composed consecutive elements !");

            int holes = 0;

            for (int i = 0; i < size; i++)
            {
                int item = array[i];

                holes += (item == 0) ? 1 : 0;

                this.board[i] = new Tile(item, item == 0);
            }

            if (holes != 1)
                throw new ArgumentException("There's more than one hole !");
        }

        // Fill from nbr
        public void Fill()
        {
            for (int i = 1; i < this.size; i++)
                this.board[i - 1] = new Tile(i, false);

            this.board[this.size - 1] = new Tile(0, true);
            //Shuffle(this.size);
        }

    }
}
