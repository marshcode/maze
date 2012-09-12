namespace consolemaze.art.test {
    using NUnit.Framework;

    using System;

    using consolemaze.art;
    using mazecore.elements;
    using mazecore.direction;

    using mazecore.elements.test;


    [TestFixture]
    class TestASCIIMazeStyle {

        [Test]
        public void you_fail(){
            Assert.True(false);
        }
    }


    class ASCIIMazeTest {

        protected bool render_compare(ASCIIRenderer character_maze, string expected) {
            string actual = character_maze.render_string();

            //Console.WriteLine("---");
            //Console.WriteLine(actual);
            //Console.WriteLine("---");
            //Console.WriteLine(expected);
            //Console.WriteLine("---");

            return actual.Trim().Equals(expected.Trim());
        }

    }

    [TestFixture]
    class TestASCIIWallMaze : ASCIIMazeTest {

        protected void create_walls(Tile tile, Direction[] directions) {

            int x = tile.get_x(), y = tile.get_y();
            foreach (Direction dir in directions) {
                Wall wall = new Wall(tile.get_maze(), x, y, dir); 
            }

        }


        [Test]
        public void test_complex() {
            Maze maze = new Maze(6, 3);

            Tile t = new Tile(maze, 0, 2);
            Wall w = new Wall(maze, 0, 2, Direction.North);
            w = new Wall(maze, 0, 2, Direction.West);
            w = new Wall(maze, 0, 2, Direction.East);
            Character character = new Character(maze, 0, 2);

            t = new Tile(maze, 0, 1);
            w = new Wall(maze, 0, 1, Direction.West);

            t = new Tile(maze, 1, 1);
            w = new Wall(maze, 1, 1, Direction.North);
            w = new Wall(maze, 1, 1, Direction.East);
            w = new Wall(maze, 1, 1, Direction.South);

            t = new Tile(maze, 0, 0);
            w = new Wall(maze, 0, 0, Direction.West);
            w = new Wall(maze, 0, 0, Direction.South);

            t = new Tile(maze, 1, 0);
            w = new Wall(maze, 1, 0, Direction.South);

            t = new Tile(maze, 2, 0);
            w = new Wall(maze, 2, 0, Direction.South);
            w = new Wall(maze, 2, 0, Direction.North);


            t = new Tile(maze, 3, 0);
            w = new Wall(maze, 3, 0, Direction.South);
            w = new Wall(maze, 3, 0, Direction.North);

            t = new Tile(maze, 4, 0);
            w = new Wall(maze, 4, 0, Direction.South);
            w = new Wall(maze, 4, 0, Direction.North);


            t = new Tile(maze, 5, 0);
            w = new Wall(maze, 5, 0, Direction.South);
            w = new Wall(maze, 5, 0, Direction.East);
            w = new Wall(maze, 5, 0, Direction.North);


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
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 0, 1);

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
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 1, 0);

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
            Tile tile = new Tile(maze, 0, 0);
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
            Tile tile1 = new Tile(maze, 0, 0);
            this.create_walls(tile1, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Tile tile2 = new Tile(maze, 1, 0);
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
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 0, 1);
            Tile tile3 = new Tile(maze, 1, 0);
            Tile tile4 = new Tile(maze, 1, 1);

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
            Tile tile = new Tile(maze, 0, 0);
            this.create_walls(tile, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Character character = new Character(maze, 0, 0);
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
            Tile tile = new Tile(maze, 0, 0);
            this.create_walls(tile, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Glyph<Type> tile_glyph = new Glyph<Type>('#');
            ASCIIMazeStyle style = new ASCIIMazeStyle(tile_glyph: tile_glyph);
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
            Tile tile_ll = new Tile(maze, 0, 0);
            Tile tile_ur = new Tile(maze, 1, 1);

            Tile block_lr = new Block(maze, 1, 0);
            Tile block_ul = new Block(maze, 0, 1);

            string expected = "█ \n" +
                              " █";

            ASCIIBlockMaze maze_render = new ASCIIBlockMaze(maze);
            Assert.True(this.render_compare(maze_render, expected));

        }


        [Test]
        public void with_character() {

            Maze maze = new Maze(2, 2);
            Tile tile_ll = new Tile(maze, 0, 0);
            Tile tile_ur = new Tile(maze, 1, 1);

            Tile block_lr = new Block(maze, 1, 0);
            Tile block_ul = new Block(maze, 0, 1);

            Character character = new Character(maze, 0, 0);

            string expected = "█ \n" +
                              "▲█";

            ASCIIBlockMaze maze_render = new ASCIIBlockMaze(maze);
            Assert.True(this.render_compare(maze_render, expected));

        }

    }


}