using NUnit.Framework;
using mazecore.elements;

namespace mazegen.test {

    [TestFixture]
    class TestDepthFirstMazeGenerator {

        [Test]
        public void test_generate_fill_area() {
            DepthFirstMazeGenerator dfmg = new DepthFirstMazeGenerator();
            int x_range = 5, y_range = 10;

            Maze maze = dfmg.generate(x_range, y_range);

            Assert.AreEqual(maze.get_x_range(), x_range);
            Assert.AreEqual(maze.get_y_range(), y_range);


            for (int x = 0; x < x_range; x++) {
                for (int y = 0; y < y_range; y++) {
                    Assert.NotNull(maze.get_tile(x, y));
                }
            }


        }

    }
}
