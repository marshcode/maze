using mazecore.elements;
using NUnit.Framework;

namespace mazecore.elements.test {

    [TestFixture]
    class TestTile {

        public static Maze create_maze() {
            return new Maze(10, 15);
        }
        public static Tile create_tile(Maze maze, int x, int y){
            return new Tile(maze, x, y);
        }
        public static Character create_character(Maze maze, int x, int y) {
            return new Character(maze, x, y);
        }


        [Test]
        public void test_get_positions(){
            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, 2, 3);
            
            Assert.AreEqual(tile.get_x(), 2);
            Assert.AreEqual(tile.get_y(), 3);
            Assert.AreEqual(maze.get_tile(2, 3), tile);  

        }

        [Test]
        public void test_is_occupied() {

            Maze maze = TestTile.create_maze();
            Tile tile_occupied = TestTile.create_tile(maze, 1, 1);
            Tile tile_not_occupied = TestTile.create_tile(maze, 1, 2);
            Character character = TestTile.create_character(maze, 1, 1);

            Assert.True(tile_occupied.is_occupied());
            Assert.False(tile_not_occupied.is_occupied());

        }

        [Test]
        public void get_neighbor_tile_good() {
            int x = 2, y = 2;
            int north_x = DirectionControl.adjust(x, Direction.North, 1);
            int south_x = DirectionControl.adjust(x, Direction.South, 1);
            int east_y  = DirectionControl.adjust(y, Direction.East, 1);
            int west_y  = DirectionControl.adjust(y, Direction.West, 1);


            Maze maze = TestTile.create_maze();
            Tile center = TestTile.create_tile(maze, 2, 2);
            Tile north_tile = TestTile.create_tile(maze, north_x, y);
            Tile south_tile = TestTile.create_tile(maze, south_x, y);
            Tile east_tile = TestTile.create_tile(maze, x, east_y);
            Tile west_tile = TestTile.create_tile(maze, x, west_y);


            Assert.AreEqual(center.get_neighbor_tile(Direction.North), north_tile);
            Assert.AreEqual(center.get_neighbor_tile(Direction.South), south_tile);
            Assert.AreEqual(center.get_neighbor_tile(Direction.East), east_tile);
            Assert.AreEqual(center.get_neighbor_tile(Direction.West), west_tile);
        }

 

        [TestCase(2, 2)] //check null tiles
        [TestCase(0, 0)] //check out of bounds
        [TestCase(9, 14)]
        public void get_neighbor_tile_bad(int x, int y) {

            Maze maze = TestTile.create_maze();
            Tile center = TestTile.create_tile(maze, x, y);
            Assert.Null(center.get_neighbor_tile(Direction.North), null);
            Assert.Null(center.get_neighbor_tile(Direction.South), null);
            Assert.Null(center.get_neighbor_tile(Direction.East), null);
            Assert.Null(center.get_neighbor_tile(Direction.West), null);

        }


    }

}