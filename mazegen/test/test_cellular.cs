using NUnit.Framework;
using mazecore.elements;

namespace mazegen.test {

    [TestFixture]
    class TestCellulartMazeGenerator {

        [Test]
        public void test_generate_fill_area() {
            CellulartMazeGenerator cmg = new CellulartMazeGenerator();
            int x_range = 5, y_range = 10;

            Maze maze = cmg.generate(x_range, y_range);
            //this algorithm works on odd shapes
            Assert.AreEqual(maze.get_x_range(), x_range);
            Assert.AreEqual(maze.get_y_range(), y_range+1);


            for (int x = 0; x < x_range; x++) {
                for (int y = 0; y < y_range; y++) {
                    Assert.NotNull(maze.get_tile(x, y));
                }
            }






        }

    }
}
