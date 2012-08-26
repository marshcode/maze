namespace mazecore.elements
{
    
    public class Tile{
        internal Tile() { }

        public Tile(Maze maze, int x, int y, Direction Orientation = Direction.North) {

        }

        public int get_x() { return 0; }
        public int get_y() { return 0; }

        //internally visible methods.  Determine the best usage for these two methods:
        //should the character be provided to the tile or should the character be able to obtain
        //the character from the maze
        void action_step_on() { }
        void action_stop_off() { }

        public bool is_occupied() { return false;  }
        public Tile get_neighbor_tile(Direction direction) { return null; }
        public Wall get_wall(Direction direction) { return null; }

    }

}