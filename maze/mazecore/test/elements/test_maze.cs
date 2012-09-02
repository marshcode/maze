using mazecore.elements;
using mazecore.test;
using NUnit.Framework;
using System;

namespace mazecore.elements.test {

    [TestFixture]
    class TestDirectionControl {

        [TestCase(1, Direction.North, 2)]
        [TestCase(1, Direction.West, 0)]
        //non-sensical coordinate but ensure that we can produce it all the same
        [TestCase(0, Direction.North, 1)] 
        [TestCase(0, Direction.West, -1)]
        
        [TestCase(1, Direction.South, 0)]
        [TestCase(1, Direction.East, 2)]
        public void test_adjust(int coord, Direction direction, int expected){
            Assert.AreEqual(DirectionControl.adjust(coord, direction, 1), expected);
        }

        [TestCase(1, 1, Direction.North, 1, 2)]
        [TestCase(1, 1, Direction.South, 1, 0)]
        [TestCase(1, 1, Direction.East, 2, 1)]
        [TestCase(1, 1, Direction.West, 0, 1)]
        [TestCase(3, 2, Direction.West, 2, 2)]//symmetrical test vectors are, of course, the devil.  This will fix that.
        public void test_move(int x, int y, Direction direction, int exp_x, int exp_y) {
            DirectionControl.move(ref x, ref y, direction, 1);
            Assert.AreEqual(x, exp_x);
            Assert.AreEqual(y, exp_y);

        }

    }
    
    [TestFixture]
    class TestMaze : TestBaseClass {
        /********************
         * Tests
         * 
         * Not sure if it is worth it to re-test the corner cases that I've already tested below.
         * For now, do simple get/set/remove tests
         * ******************/
        [Test]
        public void test_init(){
            Maze maze = TestMaze.create_maze();
            Assert.AreEqual(maze.get_x_range(), 10);
            Assert.AreEqual(maze.get_y_range(), 15);
        }
        [Test]
        public void test_tile() {
            Maze maze = TestMaze.create_maze();
            Assert.Null(maze.get_tile(1, 1));
            Tile tile = TestMaze.create_tile(maze, 1, 1);

            
            maze.set_tile(tile, 1, 1);
            Assert.AreEqual(tile, maze.get_tile(1,1));
            maze.remove_tile(1, 1);
            Assert.Null(maze.get_tile(1,1));
        }

        [TestCase(-1, -1)]
        public void test_tile_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();
            Tile tile = TestMaze.create_tile(maze, 1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_tile(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_tile(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.set_tile(tile, x, y); }); //not usually supported


        }

        [Test]
        public void test_wall() {
            Maze maze = TestMaze.create_maze();
            Assert.Null(maze.get_wall(1, 1, Direction.North));
            Wall wall  = TestMaze.create_wall(maze, 1, 1, Direction.North);
            maze.set_wall(wall, 1,1, Direction.North);
            //should I fail if there is no tile to set the wall on?
            //it won't matter, I guess.  No tiles means nobody can see it.  

            Assert.AreEqual(wall, maze.get_wall(1, 1, Direction.North));
            Assert.AreEqual(wall, maze.get_wall(1, 2, Direction.South));
            maze.remove_wall(1, 1, Direction.North);
            Assert.Null(maze.get_wall(1, 1, Direction.North));

        }

        [TestCase(-1, -1)]
        public void test_wall_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_wall(x, y, Direction.North); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_wall(x, y, Direction.North); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestMaze.create_wall(maze, x, y, Direction.North); });


        }


        [Test]
        public void test_character() {

            Maze maze = TestMaze.create_maze();
            
            Tile tile = TestMaze.create_tile(maze, 1, 1);
            Tile tile2 = TestMaze.create_tile(maze, 2, 3);

            Assert.Null(maze.get_character(1,1));
            Character character = TestMaze.create_character(maze, 1, 1);
            Assert.AreEqual(character, maze.get_character(1, 1));

            maze.set_character(character, 2, 3);
            Assert.AreEqual(character, maze.get_character(2, 3));
            Assert.Null(maze.get_character(1, 1));

            maze.remove_character(1, 1);
            Assert.Null(maze.get_character(1, 1));


        }

        [Test]
        public void test_characte_no_overwrite() {

            Maze maze = TestMaze.create_maze();
            Character c1;
            Tile tile = TestMaze.create_tile(maze, 1, 1);

            Assert.Null(maze.get_character(1, 1));

            c1 = TestMaze.create_character(maze, 1, 1);
            Assert.AreEqual(maze.get_character(1, 1), c1);

            Assert.Throws<MazeException>(
                    delegate {TestMaze.create_character(maze, 1, 1); });
            Assert.AreEqual(maze.get_character(1, 1), c1);

        }

        [Test]
        public void test_character_set_null_tile() {
            Maze maze = TestMaze.create_maze();
            Assert.Throws<MazeException>(
                      delegate { TestMaze.create_character(maze, 1, 1); });
            

        }


        [TestCase(-1, -1)]
        public void test_character_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_character(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_character(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestMaze.create_character(maze, x, y); });
        }
    }



    [TestFixture]
    class TestGridStorage {

        /************************
         * Testing Classes
        ************************/
        class TestClass {
            public TestClass() {
                //define a parameterless constructor that we can instantiate.  This way we can test the 
                //storage module without creating any dependicies of the tile.
            }
        }

        /************************
         * Static Data and factories
        ************************/
        static int x_range = 10;
        static int y_range = 15;

        static GridStorage<TestClass> create_storage() {
            return new GridStorage<TestClass>(TestGridStorage.x_range, TestGridStorage.y_range);
        }
        static TestClass create_tile() {
            return new TestClass();
        }
        /***************************
         * Test Cases
        ****************************/

        [Test]
        public void test_init(){
            GridStorage<TestClass> ts = TestGridStorage.create_storage();
            Assert.AreEqual(ts.get_x_range(), 10);
            Assert.AreEqual(ts.get_y_range(), 15);

        }

        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void test_bad_init(int x_range, int y_range) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { new GridStorage<TestClass>(x_range, y_range); });

        }


        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(9, 14)]
        public void test_get_position_good(int x, int y) {
            GridStorage<TestClass> grid_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            grid_storage.set_item(test_class, x, y);


            int[] position = grid_storage.get_position(test_class);
            Assert.NotNull(position);
            Assert.AreEqual(position.Length, 2);
            Assert.AreEqual(position[0], x);
            Assert.AreEqual(position[1], y);
        }

        [Test]
        public void test_get_position_bad() {
            GridStorage<TestClass> grid_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            int[] position = grid_storage.get_position(test_class);
            Assert.Null(position);
        }

        [Test]
        public void test_get_position_two_objects() {
            GridStorage<TestClass> grid_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            TestClass test_class_2 = TestGridStorage.create_tile();
            grid_storage.set_item(test_class, 1, 1);


            int[] position = grid_storage.get_position(test_class_2);
            Assert.Null(position);
        }

        [Test]
        public void test_move() {
            GridStorage<TestClass> grid_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();

            grid_storage.move(test_class, 1, 1);
            Assert.AreEqual(grid_storage.get_item(1, 1), test_class);
            Assert.Null(grid_storage.get_item(1, 2));
            grid_storage.move(test_class, 1, 2);
            Assert.AreEqual(grid_storage.get_item(1, 2), test_class);
            Assert.Null(grid_storage.get_item(1, 1));

        }


        [Test]
        public void test_get_set_remove_tile() {
            //test in and out of tiles
            GridStorage<TestClass> tile_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();

            Assert.Null(tile_storage.get_item(1, 1));
            tile_storage.set_item(test_class, 1, 1);
            Assert.AreEqual(test_class, tile_storage.get_item(1, 1));
            tile_storage.remove_item(1, 1);
            Assert.Null(tile_storage.get_item(1, 1)); 

        }

        [Test]
        public void test_tile_override() {
            GridStorage<TestClass> tile_storage = TestGridStorage.create_storage();
            TestClass tc1, tc2;
            tc1 = TestGridStorage.create_tile();
            tc2 = TestGridStorage.create_tile();
            tile_storage.set_item(tc1, 1, 1);
            Assert.AreEqual(tc1, tile_storage.get_item(1, 1));
            tile_storage.set_item(tc2, 1, 1);
            Assert.AreEqual(tc2, tile_storage.get_item(1, 1));

 
        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase( 0, -1)]
        [TestCase(-1,  0)]
        [TestCase( 0, 16)]
        [TestCase(11,  0)]
        [TestCase(10, 0)]
        [TestCase(0, 15)]
        public void test_set_tile_out_of_range(int x, int y) {
            GridStorage<TestClass> tile_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { tile_storage.set_item(test_class, x, y); });
        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, 16)]
        [TestCase(11, 0)]
        public void test_get_tile_out_of_range(int x, int y) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestGridStorage.create_storage().get_item(x, y); });
        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, 16)]
        [TestCase(11, 0)]
        public void test_remove_tile_out_of_range(int x, int y) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestGridStorage.create_storage().get_item(x, y); });
        }


    }

    [TestFixture]
    class TestSharedEdgeStorage {

        /*********************
         * Test Classes
         *********************/
        class TestClass {

        }

        /*******************
         * Static Methods
         *******************/
        static int X_RANGE = 10;
        static int Y_RANGE = 15;

        static SharedEdgeStorage<TestClass> create_storage() {
            return new SharedEdgeStorage<TestClass>(TestSharedEdgeStorage.X_RANGE, TestSharedEdgeStorage.Y_RANGE);
        }
        static TestClass create_wall() {
            return new TestClass();
        }
        /***********************
         * Tests
         **********************/


        [Test]
        public void test_init() {
            SharedEdgeStorage<TestClass> wall_storage = TestSharedEdgeStorage.create_storage();
            Assert.AreEqual(wall_storage.get_x_range(), 10);
            Assert.AreEqual(wall_storage.get_y_range(), 15);
        }
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, 1)]
        public void test_bad_init(int x_range, int y_range) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { new SharedEdgeStorage<TestClass>(x_range, y_range); });
        }
        [Test]
        public void test_get_set_remove_wall_north_south() {

            SharedEdgeStorage<TestClass> wall_storage = TestSharedEdgeStorage.create_storage();
            TestClass test_class = TestSharedEdgeStorage.create_wall();

            Assert.Null( wall_storage.get_item(1, 1, Direction.North) );
            wall_storage.set_item(test_class, 1, 1, Direction.North);

            Assert.AreEqual(wall_storage.get_item(1, 2, Direction.South), test_class);
            Assert.AreEqual(wall_storage.get_item(1, 1, Direction.North), test_class);

            wall_storage.remove_item(1, 1, Direction.North);
            Assert.Null(wall_storage.get_item(2, 1, Direction.South));
            Assert.Null(wall_storage.get_item(1, 1, Direction.North));
        }

        [Test]
        public void test_get_set_remove_wall_east_west() {

            SharedEdgeStorage<TestClass> wall_storage = TestSharedEdgeStorage.create_storage();
            TestClass test_class = TestSharedEdgeStorage.create_wall();

            Assert.Null(wall_storage.get_item(1, 1, Direction.East));
            wall_storage.set_item(test_class, 1, 1, Direction.East);

            Assert.AreEqual(wall_storage.get_item(1, 1, Direction.East), test_class);
            Assert.AreEqual(wall_storage.get_item(2, 1, Direction.West), test_class);

            wall_storage.remove_item(1, 1, Direction.East);
            Assert.Null(wall_storage.get_item(1, 1, Direction.East));
            Assert.Null(wall_storage.get_item(2, 1, Direction.West));
        }

        [Test]
        public void test_tile_override() {
            SharedEdgeStorage<TestClass> shared_edge_storage = TestSharedEdgeStorage.create_storage();
            TestClass tc1, tc2;
            tc1 = TestSharedEdgeStorage.create_wall();
            tc2 = TestSharedEdgeStorage.create_wall();
            shared_edge_storage.set_item(tc1, 1, 1, Direction.North);
            Assert.AreEqual(tc1, shared_edge_storage.get_item(1, 1, Direction.North));
            shared_edge_storage.set_item(tc2, 1, 1, Direction.North);
            Assert.AreEqual(tc2, shared_edge_storage.get_item(1, 1, Direction.North));


        }   
    }
}