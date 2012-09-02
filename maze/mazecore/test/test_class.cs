

namespace mazecore.test {
    using mazecore.elements;
    using mazecore.direction;

    class TestBaseClass {

        public static Maze create_maze() {
            return new Maze(10, 15);
        }
        public static Tile create_tile(Maze maze, int x, int y) {
            return new Tile(maze, x, y);
        }
        public static Wall create_wall(Maze maze, int x, int y, Direction direction) {
            return new Wall(maze, x, y, direction);
        }
        public static Character create_character(Maze maze, int x, int y) {
            return new Character(maze, x, y);
        }

    }

}