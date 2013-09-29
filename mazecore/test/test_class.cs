

namespace mazecore.test {
    using mazecore.elements;
    using mazecore.direction;

    public class TestBaseClass {

        public static Maze create_maze() {
            return new Maze(10, 15);
        }
        public static Tile create_tile(Maze maze, Position p) {
            return new Tile(maze, p);
        }
        public static Wall create_wall(Maze maze, Position p, Direction direction) {
            return new Wall(maze, p, direction);
        }
        public static Character create_character(Maze maze, Position p) {
            return new Character(maze, p);
        }

    }

}