namespace mazecore.elements.test {

    using NUnit.Framework;
    using mazecore.elements;
    using mazecore.test;
    using mazecore.direction;

    [TestFixture]
    class TestWall : TestBaseClass {

        [Test]
        public void test_init_create() {
            Maze maze = TestWall.create_maze();
            Position p = new Position(2, 3);


            Tile tile = TestWall.create_tile(maze, p);
            Wall wall = TestWall.create_wall(maze, p, Direction.North);

            Assert.AreEqual(wall, maze.get_wall(p, Direction.North));
            Assert.AreEqual(maze, wall.get_maze());
        }

        [Test]
        public void test_can_pass() {
            Position p = new Position(2, 3);

            Maze maze = TestWall.create_maze();
            Tile tile = TestWall.create_tile(maze, p);
            Wall wall = TestWall.create_wall(maze, p, Direction.North);

            Assert.False(wall.can_pass());

        }
    }

}