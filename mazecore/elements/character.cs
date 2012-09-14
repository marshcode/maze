
namespace mazecore.elements {

    using System;
    using mazecore.direction;

    public class Character {
        protected int x, y;
        protected Maze maze;
        protected Navigation navigation;
        protected Direction orientation;

        public Character(Maze maze, int x, int y) {
            maze.set_character(this, x, y);
            this.x = x;
            this.y = y;
            this.orientation = Direction.North;

            this.maze = maze;
            this.navigation = new Navigation(this);

        }

        public Direction get_orientation(){
            return this.orientation;
        }public void set_orientation(Direction orientation){
            this.orientation = orientation;
        }
        
        public Wall get_facing_wall() {
            return this.get_maze().get_wall(this.get_x(), this.get_y(), this.get_orientation());
        }
        public Tile get_tile() {
            return this.get_maze().get_tile(this.get_x(), this.get_y());
        }




        public bool move(Direction direction) {
            bool can_move = this.navigation.can_move(direction);

            if(!can_move){
                return false;
            }

            DirectionControl.move(ref this.x, ref this.y, direction, 1);
            this.maze.set_character(this, this.x, this.y);

            return true;

        }

        public Maze get_maze() {
            return this.maze;
        }public int get_x() {
            return this.x;
        }public int get_y() {
            return this.y;
        }

    }



    public class Navigation {
        //private helper class used for characters.  Determines how the character moves.
        //this can be subclasses to implement a ghost (for example: travel through walls, stand on un-standable tiles.)
        //this handles adjacent tile navigation: it is not a path-finder.  

        protected Character character;

        public Navigation(Character character) {
            this.character = character;
        }

        public bool can_stand_on(Tile tile) {
            return tile == null || !tile.can_stand() || tile.is_occupied();
        }

        public bool can_pass_through(Wall wall) {
            return wall != null && !wall.can_pass();
        }


        public bool can_move(Direction direction){
            Maze maze = this.character.get_maze();
            Tile tile = maze.get_tile( this.character.get_x(), this.character.get_y() );
            Wall wall = tile.get_wall(direction);              //potential wall
            tile = tile.get_neighbor_tile(direction); //potential tile            

            //we tried to move through a wall and it said no
            if (this.can_pass_through(wall)) { 
                return false;
            }

            //there is no tile for us to stand on or there is but we can't or it is occupied.
            if ( this.can_stand_on(tile) ) { 
                return false;
            }
            return true;
        }
    }
}