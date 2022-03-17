using System;

namespace puzzle_game
{
    public partial class Board
    {

        public string PadCenter(string s, int width, char c)
        {
            if (s == null || width <= s.Length) return s;

            int padding = width - s.Length;
            return s.PadRight(s.Length + padding / 2, c).PadLeft(width, c);
        }
        
        private void PrintLine(int i, int width, int longest_number)
        {
            if (i == 0)
                Console.Write('╔');
            else if (i == width )
                Console.Write("╚");
            else
                Console.Write('╠');
            
            for (int ligne_index = 0; ligne_index < width; ligne_index++)
            {

                for (int p = 0; p < longest_number + 2; p++)
                    Console.Write("═");

                if (ligne_index == width - 1)
                    break;

                if (i == width)
                    Console.Write("╩");
                else if (i == 0)
                    Console.Write("╦");
                else
                    Console.Write("╬");
            }
            
            
            if (i == 0)
                Console.WriteLine('╗');
            else if (i == width )
                Console.WriteLine("╝");
            else
                Console.WriteLine('╣');
        }

        // Pretty print the board
        public void Print()
        {
            // Determining the longest index
            int longest_number = 1;

            foreach (Tile tile in this.board)
            {
                int length_of_elt = (int) Math.Floor(Math.Log10(tile.Index)) + 1;
                longest_number = (length_of_elt > longest_number) ? length_of_elt : longest_number;
            }


            for (int i = 0; i < this.width; i++)
            {

                PrintLine(i, this.width, longest_number);


                for (int j = 0; j < this.width; j++)
                {

                    string index = (this.board[i * this.width + j].Type == TileType.FULL)
                        ? this.board[i * this.width + j].Index.ToString()
                        : "X";

                    Console.Write("║" + PadCenter(index, (longest_number + 2), ' '));
                    if (!(j < this.width - 1))
                        Console.Write("║\n");
                }
            }

            PrintLine(this.width, this.width, longest_number);
        }
        
    }
}