using System;

using mazecore.direction;
using mazecore.elements;

namespace mazegen {

    public abstract class MazeGenerator{

        protected void encircle_tile(Tile t) {
            Maze maze = t.get_maze();
            Position p = t.get_position();

            Wall north_wall = new Wall(maze, p, Direction.North);
            Wall south_wall = new Wall(maze, p, Direction.South);
            Wall east_wall = new Wall(maze, p, Direction.East);
            Wall west_wall = new Wall(maze, p, Direction.West);
        }

        public abstract Maze generate(int x_range, int y_range);

    }

}
