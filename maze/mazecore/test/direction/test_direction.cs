namespace mazecore.direction.test {
    using NUnit.Framework;

    [TestFixture]
    class TestDirectionControl {

        [Test]
        public void test_static_offsets() {

            Assert.AreEqual(DirectionControl.NORTH_OFFSET, 1);
            Assert.AreEqual(DirectionControl.SOUTH_OFFSET, -1);

            Assert.AreEqual(DirectionControl.EAST_OFFSET, 1);
            Assert.AreEqual(DirectionControl.WEST_OFFSET, -1);

        }

        [TestCase(1, Direction.North, 2)]
        [TestCase(1, Direction.West, 0)]
        //non-sensical coordinate but ensure that we can produce it all the same
        [TestCase(0, Direction.North, 1)]
        [TestCase(0, Direction.West, -1)]

        [TestCase(1, Direction.South, 0)]
        [TestCase(1, Direction.East, 2)]
        public void test_adjust(int coord, Direction direction, int expected) {
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

}