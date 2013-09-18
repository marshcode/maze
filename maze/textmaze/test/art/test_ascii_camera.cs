namespace textmaze.art.test
{

    using NUnit.Framework;
    using textmaze.art;
    using System;

    using mazecore.elements;

    class PointStorage
    {

        public int center_x, center_y;
        public PointStorage(int center_x, int center_y) {
            this.set_center(center_x, center_y);
        }

        public void set_center(int center_x, int center_y)
        {
            this.center_x = center_x;
            this.center_y = center_y;
        }

        public Position get_center_point(ASCIIRendererCamera camera){
            return new Position(this.center_x, this.center_y);
        }

    }

    class MyAsciiRenderer : ASCIIRenderer{
        public override Position maze_to_render_coords(Maze maze, Position p) {
            return p;
        }

        public override char[][] render_char_array(){

            char[][] char_map = new char[5][];
            char_map[0] = new char[] { 'a', 'b', 'c', 'd', 'e' };
            char_map[1] = new char[] { 'f', 'g', 'h', 'i', 'j' };
            char_map[2] = new char[] { 'k', 'l', 'm', 'n', 'o', };
            char_map[3] = new char[] { 'p', 'q', 'r', 's', 't' };
            char_map[4] = new char[] { 'u', 'v', 'w', 'x', 'y' };

            return char_map;
        }
    }

    [TestFixture]
    class TestASCIIRendererCamera : ASCIIMazeTest
    {
        public static ASCIIRendererCamera create_camera(ASCIIRenderer renderer, int center_x, int center_y) {
            PointStorage ps = new PointStorage(center_x, center_y);
            ASCIIRendererCamera camera = new ASCIIRendererCamera(renderer, ps.get_center_point);
            return camera;
        }

        [Test]
        public void test_render_corner()
        {
            MyAsciiRenderer mar = new MyAsciiRenderer();
            ASCIIRendererCamera arc = create_camera(mar, 0, 0);
            arc.set_range(2, 2);

            string expected = "abc\n" +
                              "fgh\n" +
                              "klm";

            Assert.True(this.render_compare(arc, expected));
        }
        
        [Test]
        public void test_range_full() {
            MyAsciiRenderer mar = new MyAsciiRenderer();
            ASCIIRendererCamera arc = create_camera(mar, 2, 2);
            //NOTE: not setting render range should capture the entire rendered scene

            string expected = "abcde\n" +
                              "fghij\n" +
                              "klmno\n" +
                              "pqrst\n" +
                              "uvwxy\n";

            Assert.True(this.render_compare(arc, expected));
        }

        [TestCase(10, 10)]
        [TestCase(-1, 2)]
        [TestCase( 2,-1)]
        [TestCase(10, 2)]
        [TestCase(2, 10)]
        public void test_bad_center_point(int center_x, int center_y)
        {
            MyAsciiRenderer mar = new MyAsciiRenderer();
            ASCIIRendererCamera arc = create_camera(mar, center_x, center_y);

            Assert.Throws<MazeException>(
                    delegate { arc.render_string(); });

        }

        [Test]
        public void test_range_too_big()
        {
            MyAsciiRenderer mar = new MyAsciiRenderer();
            ASCIIRendererCamera arc = create_camera(mar, 2, 2);
            arc.set_range(10, 10);

            string expected = "abcde\n" +
                              "fghij\n" +
                              "klmno\n" +
                              "pqrst\n" +
                              "uvwxy\n";

            Assert.True(this.render_compare(arc, expected));
        }

        [Test]
        public void test_range_negative()
        {
            MyAsciiRenderer mar = new MyAsciiRenderer();
            ASCIIRendererCamera arc = create_camera(mar, 2, 2);
            

            string expected = "ghi\n" +
                              "lmn\n" +
                              "qrs\n";
            arc.set_range(1, 1);
            Assert.True(this.render_compare(arc, expected));

            arc.set_range(-1, -1);
            Assert.True(this.render_compare(arc, expected));
        }


        [Test]
        public void test_range_one() {
            MyAsciiRenderer mar = new MyAsciiRenderer();
            ASCIIRendererCamera arc = create_camera(mar, 2, 2);
            arc.set_range(1, 1);

            string expected = "ghi\n" +
                              "lmn\n" +
                              "qrs\n";

            Assert.True(this.render_compare(arc, expected));
        }

        [Test]
        public void test_range_zero() {
            MyAsciiRenderer mar = new MyAsciiRenderer();
            ASCIIRendererCamera arc = create_camera(mar, 2, 2);
            arc.set_range(0, 0);

            string expected = "m";

            Assert.True(this.render_compare(arc, expected));
        }




    }
}