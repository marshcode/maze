
namespace mazecore.elements {

    using System;

    using mazecore.storage;
    using mazecore.direction;

    public class MazeException : Exception {
        public MazeException(string message)
            : base(message) {
        }
    }

    public class Position {
        public readonly int x;
        public readonly int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
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

        public bool in_range(Position p) {
            if (p.x < 0 || p.x > this.get_x_range() || p.y < 0 || p.y > this.get_y_range()) {
                return false;
            }
            return true;
        }

        public int get_x_range() { return this.tile_storage.get_x_range(); }
        public int get_y_range() { return this.tile_storage.get_y_range(); }

        public void set_tile(Tile tile, Position p) { this.tile_storage.set_item(tile, p); }
        public Tile get_tile(Position p) { return this.tile_storage.get_item(p); }
        public void remove_tile(Position p) { this.tile_storage.remove_item(p); }

        //should you be allowed to set walls where there are no tiles?  Nobody would really notice.
        public void set_wall(Wall wall, Position p, Direction direction) { this.wall_storage.set_item(wall, p, direction); }
        public Wall get_wall(Position p, Direction direction) { return this.wall_storage.get_item(p, direction); }
        public void remove_wall(Position p, Direction direction) { this.wall_storage.remove_item(p, direction); }

        public void set_character(Character character, Position p) {
            if (this.tile_storage.get_item(p) == null) {
                throw new MazeException(string.Format("Cannot set character: tile at {0}, {1} is null", p.x, p.y));
            }else if (this.character_storage.get_item(p) != null) {
                throw new MazeException(string.Format("Cannot set character on {0}, {1}.  Tile is already occupied", p.x, p.y));
            }

            this.character_storage.move(character, p);

            //maze will be responsible for tile events
        
        }
        public Character get_character(Position p) { return this.character_storage.get_item(p); }
        public void remove_character(Position p) { this.character_storage.remove_item(p); }

    }



}