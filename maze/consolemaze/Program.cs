using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using mazecore.direction;
using mazecore.elements;
using consolemaze.art;

namespace consolemaze {
    class Program {

        static Maze CreateMaze() {
            Maze maze = new Maze(6, 3);

            Tile t = new Tile(maze, 0, 2);
            Wall w = new Wall(maze, 0, 2, Direction.North);
            w = new Wall(maze, 0, 2, Direction.West);
            w = new Wall(maze, 0, 2, Direction.East);
            Character character = new Character(maze, 0, 2);

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

        static void Draw(ASCIIMaze character_maze, int left=0, int top=0) {

            string[] maze_lines = character_maze.render_string_array();

            for (int i = 0; i < maze_lines.Length;i++ ) {
                Console.SetCursorPosition(left, top+i);
                Console.WriteLine(maze_lines[i]);
            }

        }

        static void Main(string[] args) {


            Maze maze = Program.CreateMaze();
            ASCIIMaze character_maze = new ASCIIMaze(maze);
            Program.Draw(character_maze, 5, 5);
            
            Console.ReadLine();


        }
    }
}
