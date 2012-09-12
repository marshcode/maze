using mazecore.direction;
using mazecore.elements;

using System;
using System.Collections.Generic;
using System.Linq;


namespace mazegen {
    //http://en.wikipedia.org/wiki/Maze_generation_algorithm#Depth-first_search
    public class DepthFirstMazeGenerator : MazeGenerator {

        protected class CellInfo {

            protected static Random rng = new Random();

            public Tile tile;
            private List<Direction> direction_list;

            public CellInfo(Tile tile) {
                this.tile = tile;
                this.direction_list = new List<Direction>( new Direction[]{ Direction.North, Direction.East, Direction.South, Direction.West } );


                Direction tmp_d;
                int k;
                for (int n = this.direction_list.Count-1; n > 1; n--) {
                    k = rng.Next(n + 1);
                    tmp_d = this.direction_list[k];
                    this.direction_list[k] = this.direction_list[n];
                    this.direction_list[n] = tmp_d;
                }
            }

            public Nullable<Direction> consume_direction() {
                
                if( this.direction_list.Count == 0 )
                    return null;

                Direction dir = this.direction_list[0];
                this.direction_list.RemoveAt(0);
                return dir;
            }
        
        }


        public override Maze generate(int x_range, int y_range) {
            int x, y;
            Maze maze = new Maze(x_range, y_range);
            Tile t;
            for (x = 0; x < x_range; x++) {
                for (y = 0; y < y_range; y++) {
                    t = new Tile(maze, x, y);
                    this.encircle_tile(t);
                }
            }

            //keep track of the visited cells
            bool[,] visited = new bool[x_range, y_range];
            Stack<CellInfo> stack = new Stack<CellInfo>();

            //choose a random tile to start with
            Random rng = new Random();
            x = rng.Next(1, x_range-1); y = rng.Next(1, y_range-1);//reduce the starting range
            CellInfo current_cell = new CellInfo(maze.get_tile(x, y));
            visited[x, y] = true;

            //while there are unvisited cells
            Tile next_cell;
            Direction? next_direction;
            while ( !visited.Cast<bool>().ToList().TrueForAll(v => v) ) {
                bool keep_searching;
                do {
                    next_direction = current_cell.consume_direction(); next_cell = null;
                    if (!next_direction.HasValue)
                        break;
                    next_cell = current_cell.tile.get_neighbor_tile(next_direction.Value);
                    keep_searching =  next_cell != null ? visited[next_cell.get_x(), next_cell.get_y()] == true : true;
                } while (keep_searching);

                if (next_cell != null) {//current cell has cells which have not been visited
                    stack.Push(current_cell);
                    maze.remove_wall(current_cell.tile.get_x(), current_cell.tile.get_y(), next_direction.Value); 
                    current_cell = new CellInfo(next_cell);
                    visited[current_cell.tile.get_x(), current_cell.tile.get_y()] = true;
                }else if (stack.Count > 0) {
                    current_cell = stack.Pop();
                }else {
                    break;
                }
            }


            for (int i = 0; i < (int)x_range*y_range * 0.1; i++) {
                x = rng.Next(1, x_range-1);y=rng.Next(1, y_range-1);
                maze.remove_wall(x, y, Direction.North);
            }

            return maze;
        }

    }
}
