namespace textmaze.art.test {

    using NUnit.Framework;
    using textmaze.art;

    [TestFixture]
    class TestGlyph {

        enum Blah { one, two, three };

        [TestCase]
        public void test_with_enum() {
            //silly, but an important part of the class.\
            Glyph<Blah> g = new Glyph<Blah>(' ');

            g.add_character(Blah.one, 'a');
            Assert.AreEqual(g.get_character(Blah.one), 'a');
        }

        [TestCase]
        public void test_get_default() {
            Glyph<int> g = new Glyph<int>(' ');
            Assert.AreEqual(g.get_default(), ' ');
        }

        [TestCase]
        public void test_remove_non_existent_entry() {
            Glyph<int> g = new Glyph<int>(' ');
            Assert.AreEqual(g.get_character(1), ' ');
            g.remove_character(1);
            Assert.AreEqual(g.get_character(1), ' ');
        }


        [TestCase]
        public void test_overwrite() {
            Glyph<int> g = new Glyph<int>(' ');

            g.add_character(1, 'a');
            Assert.AreEqual(g.get_character(1), 'a');

            g.add_character(1, 'b');
            Assert.AreEqual(g.get_character(1), 'b');

        }

        [TestCase]
        public void test_add_get_remove_glyph() {

            Glyph<int> g = new Glyph<int>(' ');
            Assert.AreEqual(g.get_character(1), ' ');
            Assert.AreEqual(g.get_character(2), ' ');

            g.add_character(1, 'q');
            g.add_character(2, 'a');

            Assert.AreEqual(g.get_character(1), 'q');
            Assert.AreEqual(g.get_character(2), 'a');

            g.remove_character(1);
            g.remove_character(2);

            Assert.AreEqual(g.get_character(1), ' ');
            Assert.AreEqual(g.get_character(2), ' ');

        }


    }

}
