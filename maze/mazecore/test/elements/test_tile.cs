using mazecore.elements;
using NUnit.Framework;

namespace mazecore.elements.test {

    [TestFixture]
    class TestTile {

        public static Maze create_maze() {
            return new Maze(10, 15);
        }

        public static Tile create_tile(Maze maze){
            return new Tile(maze, 2, 3);
        }

        [Test]
        public void test_get_positions(){
            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze);
            
            Assert.AreEqual(tile.get_x(), 2);
            Assert.AreEqual(tile.get_y(), 3);
            Assert.AreEqual(maze.get_tile(2, 3), tile);  

        }

    }

}