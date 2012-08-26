namespace mazecore.elements {
    public class Character {
        public Character(Maze maze, int x, int y) {
            maze.set_character(this, x, y);
        }

    }
}