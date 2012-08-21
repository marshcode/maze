using mazecore.elements;
using NUnit.Framework;
using System;

namespace mazecore.elements.test {

    [TestFixture]
    class TestTileStorage {

        /************************
         * Static Data and factories
        ************************/
        static int x_range = 10;
        static int y_range = 15;

        static TileStorage create_storage() {
            return new TileStorage(TestTileStorage.x_range, TestTileStorage.y_range);
        }
        static Tile create_tile() {
            return new Tile();
        }
        /***************************
         * Test Cases
        ****************************/

        [Test]
        public void test_init(){
            TileStorage ts = TestTileStorage.create_storage();
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
                    delegate { new TileStorage(x_range, y_range); });

        }

        [Test]
        public void test_get_set_remove_tile() {
            //test in and out of tiles
            TileStorage tile_storage = TestTileStorage.create_storage();
            Tile tile = TestTileStorage.create_tile();

            Assert.Null(tile_storage.get_tile(1, 1));
            tile_storage.set_tile(tile, 1, 1);
            Assert.AreEqual(tile, tile_storage.get_tile(1, 1));
            tile_storage.remove_tile(1, 1);
            Assert.Null(tile_storage.get_tile(1, 1)); 

        }

        [Test]
        public void test_tile_override() {
            TileStorage tile_storage = TestTileStorage.create_storage();
            Tile tile1, tile2;
            tile1 = TestTileStorage.create_tile();
            tile2 = TestTileStorage.create_tile();
            tile_storage.set_tile(tile1, 1, 1);
            Assert.AreEqual(tile1, tile_storage.get_tile(1, 1));
            tile_storage.set_tile(tile2, 1, 1);
            Assert.AreEqual(tile2, tile_storage.get_tile(1, 1));

 
        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase( 0, -1)]
        [TestCase(-1,  0)]
        [TestCase( 0, 16)]
        [TestCase(11,  0)]
        public void test_set_tile_out_of_range(int x, int y) {
            TileStorage tile_storage = TestTileStorage.create_storage();
            Tile tile = TestTileStorage.create_tile();
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { tile_storage.set_tile(tile, x, y); });
            
            
            



        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, 16)]
        [TestCase(11, 0)]
        public void test_get_tile_out_of_range(int x, int y) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestTileStorage.create_storage().get_tile(x, y); });


        }

        [TestCase(-1, -1)]
        [TestCase(11, 16)]
        [TestCase(0, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, 16)]
        [TestCase(11, 0)]
        public void test_remove_tile_out_of_range(int x, int y) {
            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestTileStorage.create_storage().get_tile(x, y); });
        }


    }
}