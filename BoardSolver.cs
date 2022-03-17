using System;
using System.Collections.Generic;
using System.Linq;
using puzzle_game.SimpleGraph;
using static puzzle_game.Board;

namespace puzzle_game
{
    public partial class Board
    {

        private int ManhattanDistance(int i1, int i2)
        {
            int x1 = i1 / width;
            int y1 = i1 - x1 * width;

            int x2 = i2 / width;
            int y2 = i2 - x2 * width;


            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }


        public int CalculateHeuristic()
        {
            int total_heuri = 0;

            for (int i = 1; i < this.Size + 1; i++)
            {
                Tile tile = this.Board1[i - 1];

                if (tile.Index != i && tile.Type == TileType.FULL)
                {
                    int tile_heuristic = ManhattanDistance(i - 1, tile.Index - 1);

                    total_heuri += tile_heuristic;
                }
            }

            return total_heuri;
        }


        private Direction reverseDirection(Direction direction)
        {
            List<Direction> lookUpTable = new List<Direction>
            {
                Direction.DOWN,
                Direction.UP,
                Direction.RIGHT,
                Direction.LEFT,
                Direction.NONE
            };

            return lookUpTable[(int) Convert.ChangeType(direction, direction.GetTypeCode())];
        }

        public List<Direction> SolveBoard()
        {
            Board SolvingBoard = this.DeepCopy();

            //On choisi le minimum a chaque fois et on poursuit

            List<Direction> result = new List<Direction>();
            Direction prevMovement = Direction.NONE;

            // While the board isn't correct we continue... 
            for (int steps = 0; !SolvingBoard.IsCorrect() || SolvingBoard.CalculateHeuristic() != 0; steps++)
            {
                Direction[] possibleDirection = SolvingBoard.GetPossibleDirections();
                
                Direction minDirection = Direction.UP;
                // Maximum value 
                int min_heu = (this.Size * this.Size) * (this.Width + 1);

                // Looping through all the possible directions
                foreach (Direction direction in possibleDirection)
                {
                    Board tmp_board = SolvingBoard.DeepCopy();

                    tmp_board.MoveDirection(direction);

                    //Console.WriteLine($"Heuristic : {tmp_board.CalculateHeuristic()}");

                    int tmpHeuristic = tmp_board.CalculateHeuristic();

                    // Getting the direction of the minimum heuristic value, it can't be the previous part
                    if (tmpHeuristic <= min_heu && prevMovement != direction)
                    {
                        min_heu = tmpHeuristic;
                        minDirection = direction;
                    }
                }

                // Applying the direction to the actual board
                SolvingBoard.MoveDirection(minDirection);
                result.Add(minDirection);
                prevMovement = reverseDirection(minDirection);
            }

            return result;
        }

        public void ApplyMovements(List<Direction> directions)
        {
            foreach (Direction direction in directions)
                MoveDirection(direction);
        }
        
        /* BONUS */
        public List<Direction> SolveBoardBonus()
        {
            Board toSolve = this.DeepCopy();

            SimpleGraph<Board> graph = new SimpleGraph<Board>(toSolve);
            
            MinHeap<SimpleGraph<Board>> minHeap = new MinHeap<SimpleGraph<Board>>(toSolve.size * toSolve.size);
            
            //PriorityQueue<SimpleGraph<Board>, Int32> minHeap = new PriorityQueue<SimpleGraph<Board>, int>();
            
            while (!graph.Value.IsCorrect())
            {
                Direction[] directions = graph.Value.GetPossibleDirections();
                foreach (var possibleDirection in directions)
                {
                    Board tmp = graph.Value.DeepCopy();
                    
                    tmp.MoveDirection(possibleDirection);
                    
                    SimpleGraph<Board> newGraphNode = new SimpleGraph<Board>(tmp);
                    
                    
                    HeapElement<SimpleGraph<Board>> newNode = new HeapElement<SimpleGraph<Board>>(newGraphNode, tmp.CalculateHeuristic() + graph.heightGraph);
                    
                    graph.AddChild(newGraphNode, possibleDirection);

                    minHeap.Enqueue(newNode);
                }
                graph = minHeap.Dequeue().Node;
            }
            
            /* Retrieve Path */

            List<Direction> result = new List<Direction>();
            while (graph != null) {
                result.Insert(0, graph.directionFromParent);
                graph = graph.parent;
                if (graph.directionFromParent == Direction.NONE)
                    break;
            }
            
            return result;
        } 
    }
}