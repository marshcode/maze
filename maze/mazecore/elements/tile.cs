
namespace mazecore.elements
{
    using System;
    using mazecore.direction;

    public class Block : Tile {

        public Block(Maze maze, Position p) : base(maze, p) { }
        public override bool can_stand(){return false;}
    }

    public class Tile{


        Position position;
        Maze maze;

        public Tile(Maze maze, Position p) {

            this.position = p;
            this.maze = maze;
            
            maze.set_tile(this, p);

        }

        //Virtual Methods that can be subclassed if desired
        public virtual bool can_stand() {
            return true;
        }


        public Position get_position() { return this.position; }
        public Maze get_maze() { return this.maze; }

        public bool is_occupied() { 
            return this.maze.get_character(this.get_position()) != null; 
        }

        public Tile get_neighbor_tile(Direction direction) {

            Position p = this.get_position().move(direction, 1);
            try{
                return this.maze.get_tile(p);
            }catch(ArgumentOutOfRangeException){
                return null;
            }

        }
        public Wall get_wall(Direction direction) {
            try {
                return this.maze.get_wall(this.get_position(), direction);
            }catch (ArgumentOutOfRangeException) {
                return null;
            }

        }

        public virtual void action_step_on(MovementEvent e) { }
        public virtual void action_step_off(MovementEvent e) { }

    }

}