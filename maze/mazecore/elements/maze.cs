using System;
using mazecore.storage;

namespace mazecore.elements {

    public enum Direction { North, East, South, West };
    public class DirectionControl{
        //err, x and y are reversed.  Whoopsie.  I guess just consider x as a row and y as a column.
        //that way, depicting (row, column) isn't as goofy.  Fix this by branching first.  
        //keep all direction manipulation in one place, so we can define
        public static int adjust(int coord, Direction direction, int by) {
            if (direction == Direction.South || direction == Direction.West) {
                coord -= by;
            }else 
                coord += by;
            return coord;
        }

        public static void move(ref int x, ref int y, Direction direction, int by) {

            if (direction == Direction.North || direction == Direction.South) {
                y = DirectionControl.adjust(y, direction, by);
            }else {
                x = DirectionControl.adjust(x, direction, by);
            }

        }

    }


    public class MazeException : Exception {
        public MazeException(string message)
            : base(message) {
        }
    }

    public class Maze {


        private GridStorage<Tile> tile_storage;
        private SharedEdgeStorage<Wall> wall_storage;
        private GridStorage<Character> character_storage;

        public Maze(int x_range, int y_range) {
            this.tile_storage = new GridStorage<Tile>(x_range, y_range);
            this.wall_storage = new SharedEdgeStorage<Wall>(x_range, y_range);
            this.character_storage = new GridStorage<Character>(x_range, y_range);
        }
        
        public int get_x_range() { return this.tile_storage.get_x_range(); }
        public int get_y_range() { return this.tile_storage.get_y_range(); }

        public void set_tile(Tile tile, int x, int y) { this.tile_storage.set_item(tile, x, y); }
        public Tile get_tile(int x, int y) { return this.tile_storage.get_item(x, y); }
        public void remove_tile(int x, int y) { this.tile_storage.remove_item(x, y); }

        //should you be allowed to set walls where there are no tiles?  Nobody would really notice.
        public void set_wall(Wall wall, int x, int y, Direction direction) { this.wall_storage.set_item(wall, x, y, direction); }
        public Wall get_wall(int x, int y, Direction direction) { return this.wall_storage.get_item(x, y, direction); }
        public void remove_wall(int x, int y, Direction direction) { this.wall_storage.remove_item(x, y, direction); }

        public void set_character(Character character, int x, int y) {
            if (this.tile_storage.get_item(x, y) == null) {
                throw new MazeException(string.Format("Cannot set character: tile at {0}, {1} is null", x, y));
            }else if (this.character_storage.get_item(x, y) != null) {
                throw new MazeException(string.Format("Cannot set character on {0}, {1}.  Tile is already occupied", x, y));
            }

            this.character_storage.move(character, x, y);

            //maze will be responsible for tile events
        
        }
        public Character get_character(int x, int y) { return this.character_storage.get_item(x, y); }
        public void remove_character(int x, int y) { this.character_storage.remove_item(x, y); }

    }



}