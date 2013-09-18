namespace mazecore.direction.test {
    using NUnit.Framework;
    using mazecore.elements;

    [TestFixture]
    class TestDirectionControl {

        [Test]
        public void test_static_offsets() {

            Assert.AreEqual(DirectionControl.NORTH_OFFSET, 1);
            Assert.AreEqual(DirectionControl.SOUTH_OFFSET, -1);

            Assert.AreEqual(DirectionControl.EAST_OFFSET, 1);
            Assert.AreEqual(DirectionControl.WEST_OFFSET, -1);

        }

        [TestCase(1, 1, Direction.North, 1, 2)]
        [TestCase(1, 1, Direction.South, 1, 0)]
        [TestCase(1, 1, Direction.East, 2, 1)]
        [TestCase(1, 1, Direction.West, 0, 1)]
        [TestCase(3, 2, Direction.West, 2, 2)]//symmetrical test vectors are, of course, the devil.  This will fix that.
        public void test_move(int x, int y, Direction direction, int exp_x, int exp_y) {
            Position p = new Position(x, y);
            Position p_adj;

            p_adj = DirectionControl.move(p, direction, 1);
            Assert.AreEqual(p_adj.x, exp_x);
            Assert.AreEqual(p_adj.y, exp_y);
        }
    }

}