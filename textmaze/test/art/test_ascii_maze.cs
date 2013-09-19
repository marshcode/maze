namespace textmaze.art.test {
    using NUnit.Framework;

    using System;

    using textmaze.art;
    using mazecore.elements;
    using mazecore.direction;

    using mazecore.elements.test;

    class ASCIIMazeTest {

        protected bool render_compare(ASCIIRenderer character_maze, string expected, bool trim=true) {
            string actual = character_maze.render_string();

            Console.WriteLine("---");
            Console.WriteLine(actual);
            Console.WriteLine("---");
            Console.WriteLine(expected);
            Console.WriteLine("---");

            if (trim) {
                expected = expected.Trim();
            }

            return actual.Trim().Equals(expected);
        }

    }

    [TestFixture]
    class TestASCIIWallMaze : ASCIIMazeTest {

        protected void create_walls(Tile tile, Direction[] directions) {

            Position p = tile.get_position();

            foreach (Direction dir in directions) {
                Wall wall = new Wall(tile.get_maze(), p, dir); 
            }

        }

        [TestCase(-1, -1),
         TestCase(11, 16)]
        public void test_maze_to_render_coords_fail(int x, int y) {
            Position original = new Position(x, y);
            Maze maze = new Maze(10, 15);
            ASCIIWallMaze maze_renderer = new ASCIIWallMaze(maze);
            Assert.Throws<MazeException>(
                    delegate { maze_renderer.maze_to_render_coords(original); });
            
           
        }

        [TestCase(0, 0, 1, 30),

         TestCase(0, 1, 1, 28),
         TestCase(0, 2, 1, 26),
        //notice in the last three, a difference of one in the orignal is a difference of two in the new coordiantes
         //this is due to the wall renderng taking up two spaces on either size.
         TestCase(1, 0, 3, 30),
         TestCase(2, 0, 5, 30),
         TestCase(10, 15, 21, 0)]
        public void test_maze_to_render_coords(int x, int y, int exp_x, int exp_y) {

            Position original = new Position(x, y);
            Maze maze = new Maze(10, 15);
            ASCIIWallMaze maze_renderer = new ASCIIWallMaze(maze);
            Position new_ = maze_renderer.maze_to_render_coords(original);

            Assert.AreEqual(new_.x, exp_x);
            Assert.AreEqual(new_.y, exp_y);
        }

        [Test]
        public void test_complex() {
            Maze maze = new Maze(6, 3);

            Position p1 = new Position(0, 2);
            Position p2 = new Position(0, 1);
            Position p3 = new Position(1, 1);
            Position p4 = new Position(0, 0);
            Position p5 = new Position(1, 0);
            Position p6 = new Position(2, 0);
            Position p7 = new Position(3, 0);
            Position p8 = new Position(4, 0);
            Position p9 = new Position(5, 0);

            Tile t = new Tile(maze, p1);
            Wall w = new Wall(maze, p1, Direction.North);
            w = new Wall(maze, p1, Direction.West);
            w = new Wall(maze, p1, Direction.East);
            Character character = new Character(maze, p1);

            t = new Tile(maze, p2);
            w = new Wall(maze, p2, Direction.West);

            t = new Tile(maze, p3);
            w = new Wall(maze, p3, Direction.North);
            w = new Wall(maze, p3, Direction.East);
            w = new Wall(maze, p3, Direction.South);

            t = new Tile(maze, p4);
            w = new Wall(maze, p4, Direction.West);
            w = new Wall(maze, p4, Direction.South);

            t = new Tile(maze, p5);
            w = new Wall(maze, p5, Direction.South);

            t = new Tile(maze, p6);
            w = new Wall(maze, p6, Direction.South);
            w = new Wall(maze, p6, Direction.North);


            t = new Tile(maze, p7);
            w = new Wall(maze, p7, Direction.South);
            w = new Wall(maze, p7, Direction.North);

            t = new Tile(maze, p8);
            w = new Wall(maze, p8, Direction.South);
            w = new Wall(maze, p8, Direction.North);


            t = new Tile(maze, p9);
            w = new Wall(maze, p9, Direction.South);
            w = new Wall(maze, p9, Direction.East);
            w = new Wall(maze, p9, Direction.North);


            string expected = "┌─┐          \n" +
                              "│▲│          \n" +
                              "│ └─┐        \n" +
                              "│   │        \n" +
                              "│ ──┴───────┐\n" +
                              "│           │\n" +
                              "└───────────┘\n";

            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze);
            Assert.True(this.render_compare(character_maze, expected));

        }


        [Test]
        public void test_two_rooms_vertical() {
            Maze maze = new Maze(1, 2);
            Tile tile1 = new Tile(maze, new Position(0, 0));
            Tile tile2 = new Tile(maze, new Position(0, 1));

            this.create_walls(tile1, new Direction[] { Direction.South, Direction.East, Direction.West });
            this.create_walls(tile2, new Direction[] { Direction.North, Direction.East, Direction.West });
            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze);
            string expected = "┌─┐\n" +
                              "│ │\n" +
                              "│ │\n" +
                              "│ │\n" +
                              "└─┘\n";

            Assert.True(this.render_compare(character_maze, expected));

        }

        [Test]
        public void test_two_rooms_horizontal() {
            Maze maze = new Maze(2, 1);
            Tile tile1 = new Tile(maze, new Position(0, 0));
            Tile tile2 = new Tile(maze, new Position(1, 0));

            this.create_walls(tile1, new Direction[] { Direction.North, Direction.South, Direction.West });
            this.create_walls(tile2, new Direction[] { Direction.North, Direction.South, Direction.East });
            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze);
            string expected = "┌───┐\n" +
                              "│   │\n" +
                              "└───┘\n";

            Assert.True(this.render_compare(character_maze, expected));

        }

        

        [Test]
        public void test_one_room() {
            Maze maze = new Maze(1, 1);
            Tile tile = new Tile(maze, new Position(0, 0));
            this.create_walls(tile, new Direction[]{Direction.North, Direction.East, Direction.South, Direction.West});

            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze);
            string expected = "┌─┐\n" +
                              "│ │\n" +
                              "└─┘\n";

            Assert.True(this.render_compare(character_maze, expected));
        }

        [Test]
        public void test_two_rooms() {
            Maze maze = new Maze(2, 1);
            Tile tile1 = new Tile(maze, new Position(0, 0));
            this.create_walls(tile1, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Tile tile2 = new Tile(maze, new Position(1, 0));
            this.create_walls(tile2, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            
            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze);
            string expected = "┌─┬─┐\n" +
                              "│ │ │\n" +
                              "└─┴─┘\n";

            Assert.True(this.render_compare(character_maze, expected));          
        
        }

        [Test]
        public void test_four_tiles() {

            Maze maze = new Maze(2, 2);
            Tile tile1 = new Tile(maze, new Position(0, 0));
            Tile tile2 = new Tile(maze, new Position(0, 1));
            Tile tile3 = new Tile(maze, new Position(1, 0));
            Tile tile4 = new Tile(maze, new Position(1, 1));

            Direction[] four_walls = {Direction.North, Direction.East, Direction.South, Direction.West};
            this.create_walls(tile1, four_walls);
            this.create_walls(tile2, four_walls);
            this.create_walls(tile3, four_walls);
            this.create_walls(tile4, four_walls);

            string expected = "┌─┬─┐\n" +
                              "│ │ │\n" +
                              "├─┼─┤\n" +
                              "│ │ │\n" +
                              "└─┴─┘\n";

            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze);
            Assert.True( this.render_compare(character_maze, expected) );
        }


        [TestCase(Direction.North, "▲")]
        [TestCase(Direction.East,  "►")]
        [TestCase(Direction.South, "▼")]
        [TestCase(Direction.West,  "◄")]
        public void test_character_direction(Direction orientation, string char_glyph) {
            Maze maze = new Maze(1, 1);
            Position p = new Position(0, 0);

            Tile tile = new Tile(maze, p);
            this.create_walls(tile, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Character character = new Character(maze, p);
            character.set_orientation(orientation);

            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze);
            string expected = "┌─┐\n" +
                              "│{0}│\n" +
                              "└─┘\n";

            Assert.True(this.render_compare(character_maze, string.Format(expected, char_glyph)));
        }


        [Test]
        public void test_new_style() {

            Maze maze = new Maze(1, 1);
            Tile tile = new Tile(maze, new Position(0, 0));
            this.create_walls(tile, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Glyph<Type> tile_glyph = new Glyph<Type>('#');
            IASCIIMazeStyle style = new ASCIIMazeGlyphStyle(tile_glyph: tile_glyph);
            ASCIIWallMaze character_maze = new ASCIIWallMaze(maze, style);
            string expected = "┌─┐\n" +
                              "│#│\n" +
                              "└─┘\n";

            Assert.True(this.render_compare(character_maze, expected));

        }

    }

    [TestFixture]
    class TestASCIIBlockMaze : ASCIIMazeTest {

        [Test]
        public void test_four_rooms() {

            Maze maze = new Maze(2, 2);
            Tile tile_ll = new Tile(maze, new Position(0, 0));
            Tile tile_ur = new Tile(maze, new Position(1, 1));

            Tile block_lr = new Block(maze, new Position(1, 0));
            Tile block_ul = new Block(maze, new Position(0, 1));

            string expected = "█ \n" +
                              " █";

            ASCIIBlockMaze maze_render = new ASCIIBlockMaze(maze);
            Assert.True(this.render_compare(maze_render, expected));

        }


        [Test]
        public void with_character() {

            Maze maze = new Maze(2, 2);
            Tile tile_ll = new Tile(maze, new Position(0, 0));
            Tile tile_ur = new Tile(maze, new Position(1, 1));

            Tile block_lr = new Block(maze, new Position(1, 0));
            Tile block_ul = new Block(maze, new Position(0, 1));

            Character character = new Character(maze, tile_ll.get_position());

            string expected = "█ \n" +
                              "▲█";

            ASCIIBlockMaze maze_render = new ASCIIBlockMaze(maze);
            Assert.True(this.render_compare(maze_render, expected));

        }

        [TestCase(-1, -1),
         TestCase(11, 16)]
        public void test_maze_to_render_coords_fail(int x, int y) {
            Position original = new Position(x, y);
            Maze maze = new Maze(10, 15);
            ASCIIBlockMaze maze_renderer = new ASCIIBlockMaze(maze);
            Assert.Throws<MazeException>(
                    delegate { maze_renderer.maze_to_render_coords(original); });


        }

        [TestCase(0, 0, 0, 15),
         TestCase(0, 1, 0, 14),
         TestCase(0, 2, 0, 13),
         //notice these last three: a difference of one in the original is a difference of 1 in the new coordinates
         TestCase(1, 0, 1, 15),
         TestCase(2, 0, 2, 15),
         TestCase(10, 15, 10, 0)]
        public void test_maze_to_render_coords(int x, int y, int exp_x, int exp_y) {

            Position original = new Position(x, y);
            Maze maze = new Maze(10, 15);
            ASCIIBlockMaze maze_renderer = new ASCIIBlockMaze(maze);
            Position new_ = maze_renderer.maze_to_render_coords(original);

            Assert.AreEqual(new_.x, exp_x);
            Assert.AreEqual(new_.y, exp_y);
        }



    }


}