namespace mazecore.elements {
    public class Wall {

        public Wall(Maze maze, int x, int y, Direction direction) {
            maze.set_wall(this, x, y, direction);
        }
    }
}