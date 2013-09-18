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
            Block b = new Block(maze, new Position(1, 1));
            Assert.False(b.can_stand());
        }

        [Test]
        public void test_init_registration() {

            Position p = new Position(2, 3);
            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, p);

            Assert.AreEqual(maze.get_tile(p), tile);
            Assert.AreEqual(maze, tile.get_maze());

        }

        [Test]
        public void test_get_positions(){
            Position p = new Position(2, 3);
            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, p);
            
            Assert.AreEqual(tile.get_position().x, p.x);
            Assert.AreEqual(tile.get_position().y, p.y);
            Assert.AreEqual(maze.get_tile(p), tile);  

        }

        [Test]
        public void test_can_stand() {
            Position p = new Position(1, 1);
            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, p);
            Assert.True(tile.can_stand());
        }

        [Test]
        public void test_is_occupied() {

            Position p1 = new Position(1, 1);
            Position p2 = new Position(1, 2);

            Maze maze = TestTile.create_maze();
            Tile tile_occupied = TestTile.create_tile(maze, p1);
            Tile tile_not_occupied = TestTile.create_tile(maze, p2);
            Character character = TestTile.create_character(maze, p1);

            Assert.True(tile_occupied.is_occupied());
            Assert.False(tile_not_occupied.is_occupied());

        }

        [Test]
        public void get_neighbor_tile_good() {
            Position p = new Position(2, 3);

            Position east_p = DirectionControl.move(p, Direction.East, 1);
            Position west_p = DirectionControl.move(p, Direction.West, 1);
            Position north_p = DirectionControl.move(p, Direction.North, 1);
            Position south_p = DirectionControl.move(p, Direction.South, 1);


            Maze maze = TestTile.create_maze();
            Tile center = TestTile.create_tile(maze, p);
            Tile north_tile = TestTile.create_tile(maze, north_p);
            Tile south_tile = TestTile.create_tile(maze, south_p);
            Tile east_tile = TestTile.create_tile(maze, east_p);
            Tile west_tile = TestTile.create_tile(maze, west_p);


            Assert.AreEqual(center.get_neighbor_tile(Direction.North), north_tile);
            Assert.AreEqual(center.get_neighbor_tile(Direction.South), south_tile);
            Assert.AreEqual(center.get_neighbor_tile(Direction.East), east_tile);
            Assert.AreEqual(center.get_neighbor_tile(Direction.West), west_tile);
        }

 

        [TestCase(2, 2)] //check null tiles
        [TestCase(0, 0)] //check out of bounds
        [TestCase(9, 14)]
        public void get_neighbor_tile_bad(int x, int y) {

            Position p = new Position(x, y);

            Maze maze = TestTile.create_maze();
            Tile center = TestTile.create_tile(maze, p);
            Assert.Null(center.get_neighbor_tile(Direction.North), null);
            Assert.Null(center.get_neighbor_tile(Direction.South), null);
            Assert.Null(center.get_neighbor_tile(Direction.East), null);
            Assert.Null(center.get_neighbor_tile(Direction.West), null);

        }
        [Test]
        public void get_wall_good() {
            Position p = new Position(2, 2);
            Position west_p = DirectionControl.move(p, Direction.West, 1);

            Maze maze = TestTile.create_maze();
            Tile a = TestTile.create_tile(maze, p);
            Tile b = TestTile.create_tile(maze, west_p);
            Wall north = TestTile.create_wall(maze, p, Direction.North);
            Wall south = TestTile.create_wall(maze, p, Direction.South);
            Wall east = TestTile.create_wall(maze, p, Direction.East);
            Wall west = TestTile.create_wall(maze, p, Direction.West);


            Assert.AreEqual(a.get_wall(Direction.North), north);
            Assert.AreEqual(a.get_wall(Direction.South), south);
            Assert.AreEqual(a.get_wall(Direction.East), east);
            Assert.AreEqual(a.get_wall(Direction.West), west);

            Assert.AreEqual(b.get_wall(Direction.East), west);

        }

        [Test]
        public void get_wall_null() {
            Position p = new Position(2, 2);

            Maze maze = TestTile.create_maze();
            Tile tile = TestTile.create_tile(maze, p);

            Assert.Null(tile.get_wall(Direction.North));
            Assert.Null(tile.get_wall(Direction.South));
            Assert.Null(tile.get_wall(Direction.East));
            Assert.Null(tile.get_wall(Direction.West));




        }


    }

}