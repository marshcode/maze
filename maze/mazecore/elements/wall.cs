namespace mazecore.elements {
    using mazecore.direction;

    public class Wall {

        Maze maze;

        public Wall(Maze maze, int x, int y, Direction direction) {
            maze.set_wall(this, x, y, direction);
            this.maze = maze;
        }

        public bool can_pass() {
            return false;
        }
        public Maze get_maze() {
            return this.maze;
        }

    }
}