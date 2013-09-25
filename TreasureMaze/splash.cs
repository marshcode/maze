namespace treasuremaze.core {

    using mazecore.elements;

    public class SplashMaze {

        public static TreasureMaze splash_maze;

        static SplashMaze() {
            Maze maze = new Maze(45, 15);
            Tile t;

            for (int x = 0; x < maze.get_x_range(); x++) {
                for (int y = 0; y < maze.get_y_range(); y++) {
                    t = new Tile(maze, new Position(x, y));
                }
            }
            


            TreasureMaze tm = new TreasureMaze(maze);
            SplashMaze.splash_maze = tm;
            //T
            tm.set_treasure(new Position(3, 14));
            tm.set_treasure(new Position(3, 13));
            tm.set_treasure(new Position(3, 12));
            tm.set_treasure(new Position(3, 11));
            tm.set_treasure(new Position(3, 10));

            tm.set_treasure(new Position(1, 14));
            tm.set_treasure(new Position(2, 14));
            tm.set_treasure(new Position(4, 14));
            tm.set_treasure(new Position(5, 14));

            //R
            tm.set_treasure(new Position(7, 14));
            tm.set_treasure(new Position(7, 13));
            tm.set_treasure(new Position(7, 12));
            tm.set_treasure(new Position(7, 11));
            tm.set_treasure(new Position(7, 10));

            tm.set_treasure(new Position(8, 14));
            tm.set_treasure(new Position(9, 14));
            tm.set_treasure(new Position(10, 14));
            tm.set_treasure(new Position(10, 13));
            tm.set_treasure(new Position(10, 12));
            tm.set_treasure(new Position(8, 12));
            tm.set_treasure(new Position(9, 12));
            tm.set_treasure(new Position(8, 11));
            tm.set_treasure(new Position(9, 10));

            //E
            tm.set_treasure(new Position(12, 14));
            tm.set_treasure(new Position(12, 13));
            tm.set_treasure(new Position(12, 12));
            tm.set_treasure(new Position(12, 11));
            tm.set_treasure(new Position(12, 10));

            tm.set_treasure(new Position(13, 14));
            tm.set_treasure(new Position(14, 14));
            tm.set_treasure(new Position(15, 14));

            tm.set_treasure(new Position(13, 12));
            tm.set_treasure(new Position(14, 12));
            tm.set_treasure(new Position(15, 12));

            tm.set_treasure(new Position(13, 10));
            tm.set_treasure(new Position(14, 10));
            tm.set_treasure(new Position(15, 10));

            //A
            tm.set_treasure(new Position(17, 14));
            tm.set_treasure(new Position(17, 13));
            tm.set_treasure(new Position(17, 12));
            tm.set_treasure(new Position(17, 11));
            tm.set_treasure(new Position(17, 10));

            //tm.set_treasure(new Position(17, 14));
            tm.set_treasure(new Position(18, 14));
            tm.set_treasure(new Position(19, 14));
            tm.set_treasure(new Position(20, 14));
            tm.set_treasure(new Position(21, 14));

            //tm.set_treasure(new Position(17, 12));
            tm.set_treasure(new Position(18, 12));
            tm.set_treasure(new Position(19, 12));
            tm.set_treasure(new Position(20, 12));
            tm.set_treasure(new Position(21, 12));

            //tm.set_treasure(new Position(21, 14));
            tm.set_treasure(new Position(21, 13));
            //tm.set_treasure(new Position(21, 12));
            tm.set_treasure(new Position(21, 11));
            tm.set_treasure(new Position(21, 10));


            //S
            tm.set_treasure(new Position(23, 14));
            tm.set_treasure(new Position(24, 14));
            tm.set_treasure(new Position(25, 14));
            tm.set_treasure(new Position(26, 14));

            tm.set_treasure(new Position(23, 13));
            tm.set_treasure(new Position(23, 12));
            tm.set_treasure(new Position(24, 12));
            tm.set_treasure(new Position(25, 12));
            tm.set_treasure(new Position(26, 12));
            tm.set_treasure(new Position(26, 11));
            tm.set_treasure(new Position(26, 10));
            tm.set_treasure(new Position(25, 10));
            tm.set_treasure(new Position(24, 10));
            tm.set_treasure(new Position(23, 10));

            //U
            tm.set_treasure(new Position(28, 10));
            tm.set_treasure(new Position(29, 10));
            tm.set_treasure(new Position(30, 10));
            tm.set_treasure(new Position(31, 10));
            tm.set_treasure(new Position(32, 10));

            tm.set_treasure(new Position(28, 11));
            tm.set_treasure(new Position(28, 12));
            tm.set_treasure(new Position(28, 13));
            tm.set_treasure(new Position(28, 14));

            tm.set_treasure(new Position(32, 11));
            tm.set_treasure(new Position(32, 12));
            tm.set_treasure(new Position(32, 13));
            tm.set_treasure(new Position(32, 14));

            //R
            tm.set_treasure(new Position(34, 14));
            tm.set_treasure(new Position(34, 13));
            tm.set_treasure(new Position(34, 12));
            tm.set_treasure(new Position(34, 11));
            tm.set_treasure(new Position(34, 10));

            tm.set_treasure(new Position(35, 14));
            tm.set_treasure(new Position(36, 14));
            tm.set_treasure(new Position(37, 14));
            tm.set_treasure(new Position(37, 13));
            tm.set_treasure(new Position(37, 12));
            tm.set_treasure(new Position(35, 12));
            tm.set_treasure(new Position(36, 12));
            tm.set_treasure(new Position(35, 11));
            tm.set_treasure(new Position(36, 10));

            //E
            tm.set_treasure(new Position(39, 14));
            tm.set_treasure(new Position(39, 13));
            tm.set_treasure(new Position(39, 12));
            tm.set_treasure(new Position(39, 11));
            tm.set_treasure(new Position(39, 10));

            tm.set_treasure(new Position(40, 14));
            tm.set_treasure(new Position(41, 14));
            tm.set_treasure(new Position(42, 14));

            tm.set_treasure(new Position(40, 12));
            tm.set_treasure(new Position(41, 12));
            tm.set_treasure(new Position(42, 12));

            tm.set_treasure(new Position(40, 10));
            tm.set_treasure(new Position(41, 10));
            tm.set_treasure(new Position(42, 10));

            //M
            tm.set_treasure(new Position(11, 8));
            tm.set_treasure(new Position(12, 7));
            tm.set_treasure(new Position(13, 6));
            tm.set_treasure(new Position(14, 7));
            tm.set_treasure(new Position(15, 8));

            tm.set_treasure(new Position(11, 4));
            tm.set_treasure(new Position(11, 5));
            tm.set_treasure(new Position(11, 6));
            tm.set_treasure(new Position(11, 7));
            //tm.set_treasure(new Position(11, 8));

            tm.set_treasure(new Position(15, 4));
            tm.set_treasure(new Position(15, 5));
            tm.set_treasure(new Position(15, 6));
            tm.set_treasure(new Position(15, 7));
            //tm.set_treasure(new Position(15, 8));

            //A
            tm.set_treasure(new Position(17, 8));
            tm.set_treasure(new Position(17, 7));
            tm.set_treasure(new Position(17, 6));
            tm.set_treasure(new Position(17, 5));
            tm.set_treasure(new Position(17, 4));

            //tm.set_treasure(new Position(17, 8));
            tm.set_treasure(new Position(18, 8));
            tm.set_treasure(new Position(19, 8));
            tm.set_treasure(new Position(20, 8));
            //tm.set_treasure(new Position(21, 8));

            //tm.set_treasure(new Position(17, 6));
            tm.set_treasure(new Position(18, 6));
            tm.set_treasure(new Position(19, 6));
            tm.set_treasure(new Position(20, 6));
            //tm.set_treasure(new Position(21, 6));

            tm.set_treasure(new Position(21, 8));
            tm.set_treasure(new Position(21, 7));
            tm.set_treasure(new Position(21, 6));
            tm.set_treasure(new Position(21, 5));
            tm.set_treasure(new Position(21, 4));
            
            //Z
            tm.set_treasure(new Position(23, 8));
            tm.set_treasure(new Position(24, 8));
            tm.set_treasure(new Position(25, 8));
            tm.set_treasure(new Position(26, 8));

            tm.set_treasure(new Position(26, 7));

            tm.set_treasure(new Position(24, 6));
            tm.set_treasure(new Position(25, 6));

            tm.set_treasure(new Position(23, 5));
            tm.set_treasure(new Position(26, 4));
            tm.set_treasure(new Position(25, 4));
            tm.set_treasure(new Position(24, 4));
            tm.set_treasure(new Position(23, 4));

            //E
            tm.set_treasure(new Position(28, 8));
            tm.set_treasure(new Position(28, 7));
            tm.set_treasure(new Position(28, 6));
            tm.set_treasure(new Position(28, 5));
            tm.set_treasure(new Position(28, 4));

            tm.set_treasure(new Position(29, 8));
            tm.set_treasure(new Position(30, 8));
            tm.set_treasure(new Position(31, 8));

            tm.set_treasure(new Position(29, 6));
            tm.set_treasure(new Position(30, 6));
            tm.set_treasure(new Position(31, 6));

            tm.set_treasure(new Position(29, 4));
            tm.set_treasure(new Position(30, 4));
            tm.set_treasure(new Position(31, 4));


        }


    }

}