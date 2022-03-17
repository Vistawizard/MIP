using System;

namespace puzzle_game
{
    public enum TileType
    {
        EMPTY,
        FULL
    }
    
    public class Tile
    {
        
        private int index;
        
        private TileType type;

        public TileType Type => type;

        public int Index => index;

        public Tile(int index, bool empty) {
            this.type = empty ? TileType.EMPTY : TileType.FULL;
            this.index = index;
        }

        public Tile DeepCopy() {
            Tile copy = (Tile) this.MemberwiseClone();
            copy.type = this.type;
            return copy;
        }
    }
}