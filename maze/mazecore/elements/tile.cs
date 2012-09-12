
namespace mazecore.elements
{
    using System;
    using mazecore.direction;

    public class Tile{
        

        int x, y;
        Maze maze;
        bool standable;

        public Tile(Maze maze, int x, int y) {

            this.x = x;
            this.y = y;
            this.maze = maze;
            this.standable = true;
            
            maze.set_tile(this, x, y);

        }

        public int get_x() { return this.x; }
        public int get_y() { return this.y; }
        public Maze get_maze() { return this.maze; }

        public bool is_occupied() { return this.maze.get_character(this.x, this.y) != null; }
        public bool can_stand() { return this.standable; }
        public void set_can_stand(bool standable) { this.standable = standable; }


        public Tile get_neighbor_tile(Direction direction) {

            int x = this.x, y = this.y;

            DirectionControl.move(ref x, ref y, direction, 1);
            try{
                return this.maze.get_tile(x, y);
            }catch(ArgumentOutOfRangeException){
                return null;
            }

        }
        public Wall get_wall(Direction direction) {
            try {
                return this.maze.get_wall(this.x, this.y, direction);
            }catch (ArgumentOutOfRangeException) {
                return null;
            }

        }

        //internally visible methods.  Determine the best usage for these two methods:
        //should the character be provided to the tile or should the character be able to obtain
        //the character from the maze
        void action_step_on() { }
        void action_stop_off() { }

    }

}