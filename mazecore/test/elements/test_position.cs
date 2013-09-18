namespace mazecore.elements.test {
    using System;
    using NUnit.Framework;
    using mazecore.direction;
    using mazecore.elements;
    using mazecore.test;

    [TestFixture]
    class TestPosition {
        /********************
         * Tests Positions
         * ******************/

        [TestCase(0, 0),
         TestCase(1, 0),
         TestCase(0, 1),
         TestCase(50, 50)]
        public void test_position_good(int x, int y) {
            Position p = new Position(x, y);
        }


    }
}