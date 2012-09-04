namespace consolemaze.art.test {
    using NUnit.Framework;

    using System;

    using consolemaze.art;
    using mazecore.elements;
    using mazecore.direction;

    [TestFixture]
    class TestCharacterMaze {


        protected bool render_compare(CharacterMaze character_maze, string expected) {
            string actual = character_maze.render();

            Console.WriteLine("---");
            Console.WriteLine(actual);
            Console.WriteLine("---");

            return actual.Trim().Equals(expected.Trim());
        }

        protected void create_walls(Tile tile, Direction[] directions) {

            int x = tile.get_x(), y = tile.get_y();
            foreach (Direction dir in directions) {
                Wall wall = new Wall(tile.get_maze(), x, y, dir); 
            }

        }


        [Test]
        public void test_two_rooms_vertical() {
            Maze maze = new Maze(10, 10);
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 0, 1);

            this.create_walls(tile1, new Direction[] { Direction.South, Direction.East, Direction.West });
            this.create_walls(tile2, new Direction[] { Direction.North, Direction.East, Direction.West });
            CharacterMaze character_maze = new CharacterMaze(maze);
            string expected = @"
┌─┐
│ │
│ │
│ │
└─┘
";
            Assert.True(this.render_compare(character_maze, expected));

        }

        [Test]
        public void test_two_rooms_horizontal() {
            Maze maze = new Maze(10, 10);
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 1, 0);

            this.create_walls(tile1, new Direction[] { Direction.North, Direction.South, Direction.West });
            this.create_walls(tile2, new Direction[] { Direction.North, Direction.South, Direction.East });
            CharacterMaze character_maze = new CharacterMaze(maze);
            string expected = @"
┌───┐
│   │
└───┘
";
            Assert.True(this.render_compare(character_maze, expected));

        }

        [Test]
        public void test_one_room() {
            Maze maze = new Maze(10, 10);
            Tile tile = new Tile(maze, 0, 0);
            this.create_walls(tile, new Direction[]{Direction.North, Direction.East, Direction.South, Direction.West});

            CharacterMaze character_maze = new CharacterMaze(maze);
            string expected = @"
┌─┐
│ │
└─┘
";
            Assert.True(this.render_compare(character_maze, expected));
        }

        [Test]
        public void test_two_rooms() {
            Maze maze = new Maze(10, 10);
            Tile tile1 = new Tile(maze, 0, 0);
            this.create_walls(tile1, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Tile tile2 = new Tile(maze, 1, 0);
            this.create_walls(tile2, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            
            CharacterMaze character_maze = new CharacterMaze(maze);
            string expected = @"
┌─┬─┐
│ │ │
└─┴─┘
";
            Assert.True(this.render_compare(character_maze, expected));          
        
        }

        [Test]
        public void test_four_tiles() {

            Maze maze = new Maze(10, 10);
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 0, 1);
            Tile tile3 = new Tile(maze, 1, 0);
            Tile tile4 = new Tile(maze, 1, 1);

            Direction[] four_walls = {Direction.North, Direction.East, Direction.South, Direction.West};
            this.create_walls(tile1, four_walls);
            this.create_walls(tile2, four_walls);
            this.create_walls(tile3, four_walls);
            this.create_walls(tile4, four_walls);

            string expected = @"
┌─┬─┐
│ │ │
├─┼─┤
│ │ │
└─┴─┘
";
            CharacterMaze character_maze = new CharacterMaze(maze);
            Assert.True( this.render_compare(character_maze, expected) );
        }


        [TestCase(Direction.North, "▲")]
        [TestCase(Direction.East,  "►")]
        [TestCase(Direction.South, "▼")]
        [TestCase(Direction.West,  "◄")]
        public void test_character_direction(Direction orientation, string char_glyph) {
            Maze maze = new Maze(10, 10);
            Tile tile = new Tile(maze, 0, 0);
            this.create_walls(tile, new Direction[] { Direction.North, Direction.East, Direction.South, Direction.West });

            Character character = new Character(maze, 0, 0);
            character.set_orientation(orientation);

            CharacterMaze character_maze = new CharacterMaze(maze);
            string expected = @"
┌─┐
│{0}|
└─┘
";
            Assert.True(this.render_compare(character_maze, string.Format(expected, char_glyph)));
        }


    }


}