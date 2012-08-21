using System;
namespace mazecore.elements {

    public enum Direction { North, East, South, West };

    class TileStorage {
        private Tile[,] storage;
        

        public TileStorage(int x_range, int y_range){
            this.storage = new Tile[x_range, y_range];
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
            return this.storage.GetLength(0);
        }
        public int get_y_range() {
            return this.storage.GetLength(1);
        }

        public void set_tile(Tile tile, int x, int y) {
            this.check_range(x, y);
            this.storage[x, y] = tile;
        }

        public Tile get_tile(int x, int y) {
            this.check_range(x, y);
            return this.storage[x, y];
        }

        public void remove_tile(int x, int y){
            this.check_range(x, y);
            this.storage[x, y] = null;

        }

    }

}