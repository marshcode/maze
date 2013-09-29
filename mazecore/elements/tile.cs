
namespace mazecore.elements
{
    using System;
    using mazecore.direction;

    /// <summary>
    /// Built-in Block can be used just like a tile.  It represents a tile that a normal Navivgation <see cref="Navigation"/>
    /// object will not allow the character to stand on.
    /// </summary>
    public class Block : Tile {

        public Block(Maze maze, Position p) : base(maze, p) { }
        public override bool can_stand(){return false;}
    }

    /// <summary>
    /// Tiles are one of the fundamental elements that comprise a maze.  They define the behavior of the location
    /// provided in the constructor.  Tile positions are not modifiable.  
    /// </summary>
    public class Tile{


        Position position;
        Maze maze;

        public Tile(Maze maze, Position p) {

            this.position = p;
            this.maze = maze;
            
            maze.set_tile(this, p);

        }

        /// <summary>
        /// Determines whether a normal character can stand on the tile.  If a character has a non-standard 
        /// Navigation <see cref="Navigation"/> strategy then this can be overwritten or ignored. 
        /// </summary>
        /// <returns></returns>
        public virtual bool can_stand() {
            return true;
        }


        public Position get_position() { 
            return this.position; 
        }
        public Maze get_maze() { 
            return this.maze; 
        }

        /// <summary>
        /// Convenience function to check whether the maze is currently occupied at this location.
        /// </summary>
        /// <returns></returns>
        public bool is_occupied() { 
            //tiles do not (and should not) store character references themselves.  Therefore, we ask the maze.
            return this.maze.get_character(this.get_position()) != null; 
        }

        /// <summary>
        /// Convenience function to return the neighboring tile relative to this current tile.
        /// </summary>
        /// <param name="direction">Direction of the neighboring tile</param>
        /// <returns></returns>
        public Tile get_neighbor_tile(Direction direction) {

            Position p = this.get_position().move(direction, 1);
            try{
                return this.maze.get_tile(p);
            }catch(ArgumentOutOfRangeException){
                return null;
            }
        }

        /// <summary>
        /// Returns the shared wall between this tile and:
        /// <code>
        /// tile.get_neighbor_tile(direction)
        /// </code>
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Wall get_wall(Direction direction) {
            try {
                return this.maze.get_wall(this.get_position(), direction);
            }catch (ArgumentOutOfRangeException) {
                return null;
            }

        }
        /// <summary>
        /// Callback that the maze activates when a character leaves the tile.  The movement has already happend
        /// and we are just being informed about it.
        /// </summary>
        /// <param name="e">MovementEvent with all details about the movement</param>
        public virtual void action_step_on(MovementEvent e) { }

        /// <summary>
        /// Callback that the maze activates when a character step on to the tile.The movement has already happend
        /// and we are just being informed about it.
        /// </summary>
        /// <param name="e">MovementEvent with all details about the movement</param>
        public virtual void action_step_off(MovementEvent e) { }

    }

}