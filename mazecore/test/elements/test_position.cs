namespace mazecore.elements.test {
    using System;
    using System.Collections.Generic;

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



        [TestCase(0, 0, 0, 0, true),
         TestCase(0, 0, 1, 0, false),
         TestCase(0, 0, 0, 1, false)]
        public void test_equals(int p1x, int p1y, int p2x, int p2y, bool expected) {

            Position p1 = new Position(p1x, p1y);
            Position p2 = new Position(p2x, p2y);

            Assert.AreEqual(p1.Equals(p2), expected);

        }

        public void test_equal_null() {
            Position p1 = new Position(0, 0);

            Assert.False(p1.Equals(null));
        }
        

        [Test]
        public void test_hash() {

            Position p1 = new Position(0, 0);
            Position p2 = new Position(0, 0);
            Position p3 = new Position(1, 0);
            Position p4 = new Position(0, 1);

            HashSet<Position> the_hash = new HashSet<Position>();
            Assert.AreEqual(the_hash.Count, 0);

            the_hash.Add(p1);
            Assert.AreEqual(the_hash.Count, 1);

            the_hash.Add(p2);
            Assert.AreEqual(the_hash.Count, 1);

            the_hash.Add(p3);
            Assert.AreEqual(the_hash.Count, 2);

            the_hash.Add(p4);
            Assert.AreEqual(the_hash.Count, 3);

        }

    }
}