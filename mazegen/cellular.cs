namespace mazegen {
    using System;
    using System.Collections.Generic;
    using mazecore.elements;

    public class CellulartMazeGenerator : MazeGenerator {
        //http://en.wikipedia.org/wiki/Maze_generation_algorithm#Cellular_automaton_algorithms

        protected double complexity;
        protected double density;

        public CellulartMazeGenerator(double complexity = 0.75, double density = 0.75) {
            this.complexity = complexity;
            this.density = density;
        }


        public override Maze generate(int x_range, int y_range) {

            int x_shape = (x_range / 2) * 2 + 1;
            int y_shape = (y_range / 2) * 2 + 1;

            int complexity = (int)( this.complexity * (5*(x_shape+y_shape)) );
            int density    = (int)( this.density    * (x_shape/2*y_shape/2) );

            bool[,] block_map = new bool[x_shape, y_shape];
            //initialize block_map
            for (int x = 0; x < x_shape; x++) {
                for (int y = 0; y < y_shape; y++) {
                    block_map[x, y] = x==0 || x==x_shape-1 || y==0 || y==y_shape-1 ? true:false;
                }
            }

            //populate the block map
            Random rng = new Random();
            List<Tuple<int, int>> neighbors = new List<Tuple<int, int>>();
            Tuple<int, int> temp;

            for (int i = 0; i < density; i++) {
                int x = rng.Next(0, (x_range/2) )*2, y = rng.Next(0, (y_range/2) )*2;
                block_map[x, y] = true;
                for (int j = 0; j < complexity; j++) {
                    neighbors.Clear();
                    if (x > 1) neighbors.Add(  new Tuple<int, int>(x-2, y) );
                    if (x < x_range-2) neighbors.Add(new Tuple<int, int>(x+2, y));
                    if (y > 1) neighbors.Add(new Tuple<int, int>(x, y-2));
                    if (y < y_range - 2) neighbors.Add(new Tuple<int, int>(x, y+ 2));

                    if(neighbors.Count > 0){
                        temp = neighbors[rng.Next(0, neighbors.Count-1)];

                        if (block_map[temp.Item1, temp.Item2] == false) {
                            block_map[temp.Item1, temp.Item2] = true;
                            block_map[temp.Item1 + (x - temp.Item1) / 2, temp.Item2 + (y - temp.Item2) / 2] = true;
                            x = temp.Item1; y = temp.Item2;
                        }
                    
                    }
                
                }
            }


            //create maze
            Maze maze = new Maze(x_shape, y_shape);
            for (int x = 0; x < x_shape; x++) {
                for (int y = 0; y < y_shape; y++) {

                    if(block_map[x,y]){
                        new Block(maze, x, y);
                    }else{
                        new Tile(maze, x, y);
                    }                
                }

            }

            return maze;


        }


    }

}