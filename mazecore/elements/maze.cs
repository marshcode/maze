using System;
using System.Collections.Generic;
namespace mazecore.elements {

    public enum Direction { North, East, South, West };

    abstract class Storage {

        public Storage(int x_range, int y_range) {
            if (x_range <= 0 || y_range <= 0) {
                throw new ArgumentOutOfRangeException(string.Format("({0}, {1}) is an invalid range", x_range, y_range));
            }
        }

        public abstract int get_x_range();
        public abstract int get_y_range();

        //range check
        protected void check_range(int x, int y) {
            if (x < 0 || x > this.get_x_range() || y < 0 || y > this.get_y_range()) {
                throw new ArgumentOutOfRangeException(string.Format("({0}, {1}) is out of range: ({2}, {3})",
                                                                       x, y, this.get_x_range(), this.get_y_range()));
            }
        }

    }

    class TileStorage : Storage {
        private Tile[,] storage;
        
        public TileStorage(int x_range, int y_range) : base(x_range, y_range){
            
            this.storage = new Tile[x_range, y_range];    
        }


        //getters and setters
        public override int get_x_range() {
            return this.storage.GetLength(0);
        }
        public override int get_y_range() {
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

    class WallStorage : Storage {
        
        //wall storage uses North-East-Notation (trac.davemarsh.webfactional.com\maze).

        private int x_range, y_range;
        private Dictionary<string, Wall> storage;

        public WallStorage(int x_range, int y_range) : base(x_range, y_range){
            this.x_range = x_range;
            this.y_range = y_range;
            this.storage = new Dictionary<string, Wall>();
        }
   
        //helper methods

        private string get_key(int x, int y, Direction direction) {
            this.position_correct(ref x, ref y, ref direction);
            return string.Format("{0}-{1}-{2}", x, y, direction);
        }
        private void position_correct(ref int x, ref int y, ref Direction direction) {

            if (direction == Direction.South) {
                x += 1;
                direction = Direction.North;
            }else if (direction == Direction.West) {
                y -= 1;
                direction = Direction.East;
            }
        }

        public override int get_x_range(){
            return this.x_range;
        }
        public override int get_y_range(){
            return this.y_range;
        }

        public void set_wall(Wall wall, int x, int y, Direction direction){
            string key = this.get_key(x, y, direction);
            this.storage[key] = wall;
        }
        public Wall get_wall(int x, int y, Direction direction){
            string key = this.get_key(x, y, direction);
            Wall value;
            
            if (!this.storage.TryGetValue(key, out value)){
                value = null;
            }
            return value;
        }
        public void remove_wall(int x, int y, Direction direction){
            string key = this.get_key(x, y, direction);
            this.storage.Remove(key);

        }
    }
}