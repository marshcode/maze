using System;
namespace mazecore.elements {

    public enum Direction { North, East, South, West };

    class TileStorage {

        private int x_range, y_range;
        private ITile[,] storage;
        

        public TileStorage(int x_range, int y_range){
            this.x_range = x_range;
            this.y_range = y_range;
            this.storage = new ITile[this.x_range, this.y_range];
        }

        //range check
        private void check_range(int x, int y) {
            if ( x <  0 | x > this.get_x_range() | y < 0 | y > this.get_y_range()) {
                throw new ArgumentOutOfRangeException( string.Format("({0}, {1}) is out of range: ({2}, {3})", 
                                                                       x, y, this.get_x_range(), this.get_y_range() ) );
            }
        }

        //getters and setters
        public int get_x_range() {
            return this.x_range;
        }
        public int get_y_range() {
            return this.y_range;
        }

        public void set_tile(ITile tile, int x, int y){
            this.check_range(x, y);
            this.storage[x, y] = tile;
        }

        public ITile get_tile(int x, int y) {
            this.check_range(x, y);
            return this.storage[x, y];
        }

        public void remove_tile(int x, int y){
            this.check_range(x, y);
            this.storage[x, y] = null;

        }

    }

}