using System;
namespace mazecore.elements {

    public class Character {
        protected int x, y;
        protected Maze maze;
        protected Navigation navigation;


        public Character(Maze maze, int x, int y) {
            maze.set_character(this, x, y);
            this.x = x;
            this.y = y;
            this.maze = maze;
            this.navigation = new Navigation(this);

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
        }
        public int get_x() {
            return this.x;
        }
        public int get_y() {
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
        public bool can_move(Direction direction){
            Maze maze = this.character.get_maze();
            Tile tile = maze.get_tile( this.character.get_x(), this.character.get_y() );
            Wall wall = tile.get_wall(direction);              //potential wall
            tile = tile.get_neighbor_tile(direction); //potential tile            

            if (wall != null && !wall.can_pass()) { //we tried to move through a wall and it said no
                return false;
            }

            if (tile == null || !tile.can_stand()) { //there is no tile for us to stand on or there is but we can't.
                return false;
            }
            return true;
        }
    
    }


    public class Controller {
        //private helper class for characters.  Controls the character actions

        public Controller(Character character) { }
        public void set_active(bool is_active){}
        public void update(int tick) { }


    }


}