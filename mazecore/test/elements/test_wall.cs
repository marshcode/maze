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
            Tile tile = TestWall.create_tile(maze, 2, 3);
            Wall wall = TestWall.create_wall(maze, 2, 3, Direction.North);

            Assert.AreEqual(wall, maze.get_wall(2,3,Direction.North));
            Assert.AreEqual(maze, wall.get_maze());
        }

        [Test]
        public void test_can_pass() {

            Maze maze = TestWall.create_maze();
            Tile tile = TestWall.create_tile(maze, 2, 3);
            Wall wall = TestWall.create_wall(maze, 2, 3, Direction.North);

            Assert.False(wall.can_pass());

        }
    }

}