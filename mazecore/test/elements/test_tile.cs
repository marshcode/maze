namespace mazecore.elements.test {
    
    using NUnit.Framework;
    using mazecore.direction;
    using mazecore.elements;
    using mazecore.test;

    [TestFixture]
    class TestTile : TestBaseClass {

        [Test]
        public void test_block_can_stand() {
            Maze maze = TestNavigation.create_maze();
            Block b = new Block(maze, 1, 1);
            Assert.False(b.can_stand());
        }

        [Test]
        public void test_init_registration() {

            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, 2, 3);

            Assert.AreEqual(maze.get_tile(2, 3), tile);
            Assert.AreEqual(maze, tile.get_maze());

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
        public void test_can_stand() {

            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, 1, 1);
            Assert.True(tile.can_stand());
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
            int x = 2, y = 3;

            int east_x = DirectionControl.adjust(x, Direction.East, 1);
            int west_x = DirectionControl.adjust(x, Direction.West, 1);
            int north_y = DirectionControl.adjust(y, Direction.North, 1);
            int south_y = DirectionControl.adjust(y, Direction.South, 1);



            Maze maze = TestTile.create_maze();
            Tile center = TestTile.create_tile(maze, x, y);
            Tile north_tile = TestTile.create_tile(maze, x, north_y);
            Tile south_tile = TestTile.create_tile(maze, x, south_y);
            Tile east_tile = TestTile.create_tile(maze, east_x, y);
            Tile west_tile = TestTile.create_tile(maze, west_x, y);


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
        [Test]
        public void get_wall_good() {
            int x = 2, y = 2;
            int west_x = DirectionControl.adjust(x, Direction.West, 1);

            Maze maze = TestTile.create_maze();
            Tile a = TestTile.create_tile(maze, x, y);
            Tile b = TestTile.create_tile(maze, west_x, y);
            Wall north = TestTile.create_wall(maze, x, y, Direction.North);
            Wall south = TestTile.create_wall(maze, x, y, Direction.South);
            Wall east = TestTile.create_wall(maze, x, y, Direction.East);
            Wall west = TestTile.create_wall(maze, x, y, Direction.West);


            Assert.AreEqual(a.get_wall(Direction.North), north);
            Assert.AreEqual(a.get_wall(Direction.South), south);
            Assert.AreEqual(a.get_wall(Direction.East), east);
            Assert.AreEqual(a.get_wall(Direction.West), west);

            Assert.AreEqual(b.get_wall(Direction.East), west);

        }

        [Test]
        public void get_wall_null() {
            int x = 2, y = 2;
            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, x, y);

            Assert.Null(tile.get_wall(Direction.North));
            Assert.Null(tile.get_wall(Direction.South));
            Assert.Null(tile.get_wall(Direction.East));
            Assert.Null(tile.get_wall(Direction.West));




        }


    }

}