using System;

using mazecore.direction;
using mazecore.elements;

namespace mazegen {

    abstract class MazeGenerator{

        protected void encircle_tile(Tile t) {
            Maze maze = t.get_maze();
            int x = t.get_x(), y = t.get_y();

            Wall north_wall = new Wall(maze, x, y, Direction.North);
            Wall south_wall = new Wall(maze, x, y, Direction.South);
            Wall east_wall = new Wall(maze, x, y, Direction.East);
            Wall west_wall = new Wall(maze, x, y, Direction.West);
        }


        public abstract Maze generate(int x_range, int y_range);

    }

}
