using System;
using System.Threading;

using mazecore.direction;
using mazecore.elements;
using textmaze.art;
using mazegen;


namespace consolemaze {
    class Program {

        static bool Running = true;

        static Maze CreateMaze() {
            Maze maze = new Maze(6, 3);

            Tile t = new Tile(maze, 0, 2);
            Wall w = new Wall(maze, 0, 2, Direction.North);
            w = new Wall(maze, 0, 2, Direction.West);
            w = new Wall(maze, 0, 2, Direction.East);

            t = new Tile(maze, 0, 1);
            w = new Wall(maze, 0, 1, Direction.West);

            t = new Tile(maze, 1, 1);
            w = new Wall(maze, 1, 1, Direction.North);
            w = new Wall(maze, 1, 1, Direction.East);
            w = new Wall(maze, 1, 1, Direction.South);

            t = new Tile(maze, 0, 0);
            w = new Wall(maze, 0, 0, Direction.West);
            w = new Wall(maze, 0, 0, Direction.South);

            t = new Tile(maze, 1, 0);
            w = new Wall(maze, 1, 0, Direction.South);

            t = new Tile(maze, 2, 0);
            w = new Wall(maze, 2, 0, Direction.South);
            w = new Wall(maze, 2, 0, Direction.North);


            t = new Tile(maze, 3, 0);
            w = new Wall(maze, 3, 0, Direction.South);
            w = new Wall(maze, 3, 0, Direction.North);

            t = new Tile(maze, 4, 0);
            w = new Wall(maze, 4, 0, Direction.South);
            w = new Wall(maze, 4, 0, Direction.North);


            t = new Tile(maze, 5, 0);
            w = new Wall(maze, 5, 0, Direction.South);
            w = new Wall(maze, 5, 0, Direction.East);
            w = new Wall(maze, 5, 0, Direction.North);

            return maze;
        }

        static Maze CreateMaze2() {
            DepthFirstMazeGenerator dfmg = new DepthFirstMazeGenerator();
            Maze maze = dfmg.generate(20, 15);
            return maze;
        }

        static Maze CreateMaze3() {
            CellulartMazeGenerator cmg = new CellulartMazeGenerator();
            Maze maze = cmg.generate(40, 30);
            return maze;
        }

        static void Draw(ASCIIRenderer character_maze, int left=0, int top=0) {

            string[] maze_lines = character_maze.render_string_array();

            for (int i = 0; i < maze_lines.Length;i++ ) {
                Console.SetCursorPosition(left, top+i);
                Console.WriteLine(maze_lines[i]);
            }

        }

        static void ProcessInput(Character character) {

            if(!Console.KeyAvailable){
                return;
            }

            ConsoleKey input = Console.ReadKey(true).Key;
            Nullable<Direction> direction = null;
            
            if (input.Equals(ConsoleKey.W)) {
                direction = Direction.North;
            }else if(input.Equals(ConsoleKey.A)){
                direction = Direction.West;
            }else if(input.Equals(ConsoleKey.S)){
                direction = Direction.South;
            }else if(input.Equals(ConsoleKey.D)){
                direction = Direction.East;
            }else if (input.Equals(ConsoleKey.Escape)) {
                Program.Running = false;
            }

            if (direction == null) {
                return;
            }

            character.move(direction.Value);
            character.set_orientation(direction.Value);
        }


        static void Main(string[] args) {            
            //maze configuration
            IASCIIMazeStyle maze_style= new ASCIIMazeGlyphStyle();
            Maze maze;
            ASCIIRenderer ascii_maze;

            bool wall_type = false;
            if(wall_type){
                maze = CreateMaze2();
                ascii_maze = new ASCIIWallMaze(maze, maze_style);
            }else{
                maze = CreateMaze3();
                ascii_maze = new ASCIIBlockMaze(maze, maze_style);
            }

            //somewhat random character placement
            Character character = null;
            for (int x = 0; x < maze.get_x_range() && character == null; x++) {
                for (int y = 0; y < maze.get_y_range() && character == null; y++) {
                    try {
                        character = new Character(maze, x, y);
                    }catch(MazeException){
                    }
                 }
            }

            //Character character = maze.get_character(x, y);
            
            while (Running){
                Draw(ascii_maze, 5, 5);
                ProcessInput(character);
                Thread.Sleep(20);
            }


            Console.WriteLine("Press any key to quit ...");
            Console.ReadLine();


        }
    }
}
