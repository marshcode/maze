namespace mazecore.storage.test {

    using System;
    using NUnit.Framework;

    using mazecore.storage;
    using mazecore.elements;
    using mazecore.direction;

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
        public void test_init() {
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
            grid_storage.set_item(test_class, new Position(x, y));


            Position position = grid_storage.get_position(test_class);
            Assert.NotNull(position);
            Assert.AreEqual(position.x, x);
            Assert.AreEqual(position.y, y);
        }

        [Test]
        public void test_get_position_bad() {
            GridStorage<TestClass> grid_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            Position position = grid_storage.get_position(test_class);
            Assert.Null(position);
        }

        [Test]
        public void test_get_position_two_objects() {
            GridStorage<TestClass> grid_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            TestClass test_class_2 = TestGridStorage.create_tile();
            grid_storage.set_item(test_class, new Position(1, 1));


            Position position = grid_storage.get_position(test_class_2);
            Assert.Null(position);
        }

        [Test]
        public void test_move() {
            GridStorage<TestClass> grid_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            Position old,
                     p1 = new Position(1, 1),
                     p2 = new Position(1, 2);


            old = grid_storage.move(test_class, p1);
            Assert.AreEqual(grid_storage.get_item(p1), test_class);
            Assert.Null(grid_storage.get_item(p2));
            Assert.Null(old);

            old = grid_storage.move(test_class, p2);
            Assert.AreEqual(grid_storage.get_item(p2), test_class);
            Assert.Null(grid_storage.get_item(p1));
            Assert.True(old.Equals(p1));

        }


        [Test]
        public void test_get_set_remove_tile() {
            //test in and out of tiles
            GridStorage<TestClass> tile_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();

            Position p = new Position(1, 1);

            Assert.Null(tile_storage.get_item(p));
            tile_storage.set_item(test_class, p);
            Assert.AreEqual(test_class, tile_storage.get_item(p));
            tile_storage.remove_item(p);
            Assert.Null(tile_storage.get_item(p));

        }

        [Test]
        public void test_tile_override() {
            GridStorage<TestClass> tile_storage = TestGridStorage.create_storage();
            TestClass tc1, tc2;
            Position p = new Position(1, 1);

            tc1 = TestGridStorage.create_tile();
            tc2 = TestGridStorage.create_tile();
            tile_storage.set_item(tc1, p);
            Assert.AreEqual(tc1, tile_storage.get_item(p));
            tile_storage.set_item(tc2, p);
            Assert.AreEqual(tc2, tile_storage.get_item(p));


        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, 16)]
        [TestCase(11, 0)]
        [TestCase(10, 0)]
        [TestCase(0, 15)]
        public void test_set_tile_out_of_range(int x, int y) {
            GridStorage<TestClass> tile_storage = TestGridStorage.create_storage();
            TestClass test_class = TestGridStorage.create_tile();
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { tile_storage.set_item(test_class, new Position(x, y)); });
        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, 16)]
        [TestCase(11, 0)]
        public void test_get_tile_out_of_range(int x, int y) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestGridStorage.create_storage().get_item(new Position(x, y)); });
        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, 16)]
        [TestCase(11, 0)]
        public void test_remove_tile_out_of_range(int x, int y) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestGridStorage.create_storage().get_item(new Position(x, y)); });
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

        [TestCase(0, 0, Direction.West),
         TestCase(0, 0, Direction.South)]
        public void test_border_tiles(int x, int y, Direction direction) {
            Position p1 = new Position(x, y);
            SharedEdgeStorage<TestClass> wall_storage = TestSharedEdgeStorage.create_storage();
            Assert.Null(wall_storage.get_item(p1, direction));
        }

        [Test]
        public void test_get_set_remove_wall_north_south() {

            SharedEdgeStorage<TestClass> wall_storage = TestSharedEdgeStorage.create_storage();
            TestClass test_class = TestSharedEdgeStorage.create_wall();

            Position p1 = new Position(1, 1);
            Position p2 = new Position(1, 2);


            Assert.Null(wall_storage.get_item(p1, Direction.North));
            Assert.Null(wall_storage.get_item(p2, Direction.South));

            wall_storage.set_item(test_class, p1, Direction.North);

            Assert.AreEqual(wall_storage.get_item(p2, Direction.South), test_class);
            Assert.AreEqual(wall_storage.get_item(p1, Direction.North), test_class);

            wall_storage.remove_item(p1, Direction.North);
            Assert.Null(wall_storage.get_item(p2, Direction.South));
            Assert.Null(wall_storage.get_item(p1, Direction.North));
        }

        [Test]
        public void test_get_set_remove_wall_east_west() {

            SharedEdgeStorage<TestClass> wall_storage = TestSharedEdgeStorage.create_storage();
            TestClass test_class = TestSharedEdgeStorage.create_wall();

            Position p1 = new Position(1, 1);
            Position p2 = new Position(2, 1);

            Assert.Null(wall_storage.get_item(p1, Direction.East));
            wall_storage.set_item(test_class, p1, Direction.East);

            Assert.AreEqual(wall_storage.get_item(p1, Direction.East), test_class);
            Assert.AreEqual(wall_storage.get_item(p2, Direction.West), test_class);

            wall_storage.remove_item(p1, Direction.East);
            Assert.Null(wall_storage.get_item(p1, Direction.East));
            Assert.Null(wall_storage.get_item(p2, Direction.West));
        }

        [Test]
        public void test_tile_override() {
            SharedEdgeStorage<TestClass> shared_edge_storage = TestSharedEdgeStorage.create_storage();
            TestClass tc1, tc2;
            tc1 = TestSharedEdgeStorage.create_wall();
            tc2 = TestSharedEdgeStorage.create_wall();

            Position p1 = new Position(1, 1);

            shared_edge_storage.set_item(tc1, p1, Direction.North);
            Assert.AreEqual(tc1, shared_edge_storage.get_item(p1, Direction.North));
            shared_edge_storage.set_item(tc2, p1, Direction.North);
            Assert.AreEqual(tc2, shared_edge_storage.get_item(p1, Direction.North));


        }
    }
}