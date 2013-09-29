using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mazetextart.art.test
{
    using mazecore.test;
    using mazecore.elements;
    using mazecore.direction;

    using NUnit.Framework;
    

    [TestFixture]
    class TestASCIIMazeGlyphStyle : TestBaseClass
    {
        public enum MazeStyles{default_, character, wall, tile, wall_joint};
        static Dictionary<MazeStyles, IASCIIMazeStyle> style_dictionary;
        
        

        static TestASCIIMazeGlyphStyle(){
            Glyph<Direction> new_character_glyph = new Glyph<Direction>('!');
            new_character_glyph.add_character(Direction.North, '^');
            new_character_glyph.add_character(Direction.East, '>');

            Glyph<Direction> new_wall_glyph = new Glyph<Direction>('.');
            new_wall_glyph.add_character(Direction.South, '@');
            new_wall_glyph.add_character(Direction.West, '#');

            Glyph<Type> new_tile_glyph = new Glyph<Type>('?');
            new_tile_glyph.add_character(typeof(Tile), '$');

            Glyph<int> new_wall_joint_glyph = new Glyph<int>('&');
            new_wall_joint_glyph.add_character(0, '*');

            style_dictionary = new Dictionary<MazeStyles, IASCIIMazeStyle>();
            style_dictionary.Add(MazeStyles.default_, new ASCIIMazeGlyphStyle());
            style_dictionary.Add(MazeStyles.character, new ASCIIMazeGlyphStyle(character_glyph: new_character_glyph));
            style_dictionary.Add(MazeStyles.wall, new ASCIIMazeGlyphStyle(wall_glyph: new_wall_glyph));
            style_dictionary.Add(MazeStyles.tile, new ASCIIMazeGlyphStyle(tile_glyph: new_tile_glyph));
            style_dictionary.Add(MazeStyles.wall_joint, new ASCIIMazeGlyphStyle(wall_joint_glyph: new_wall_joint_glyph));
        }

        [Test]
        public void test_set_tile_char() {
            ASCIIMazeGlyphStyle amgz = new ASCIIMazeGlyphStyle();
            Position p = new Position(0, 0);
            Maze maze = TestBaseClass.create_maze();
            Tile t = TestBaseClass.create_tile(maze, p);

            Assert.AreEqual(amgz.get_tile_char(t), ' ');
            amgz.set_tile_char(typeof(Tile), 'X');
            Assert.AreEqual(amgz.get_tile_char(t), 'X');

        }

        [Test]
        public void test_set_wall_char() {
            ASCIIMazeGlyphStyle amgz = new ASCIIMazeGlyphStyle();
            Position p = new Position(0, 0);
            Maze maze = TestBaseClass.create_maze();
            Tile t = TestBaseClass.create_tile(maze, p);
            Character c = new Character(maze, p);

            Assert.AreEqual(amgz.get_character_char(c), '▲');
            amgz.set_character_char(Direction.North, 'X');
            Assert.AreEqual(amgz.get_character_char(c), 'X');

        }

        [Test]
        public void test_set_character_char() {
            ASCIIMazeGlyphStyle amgz = new ASCIIMazeGlyphStyle();
            Position p = new Position(0, 0);
            Maze maze = TestBaseClass.create_maze();
            Tile t = TestBaseClass.create_tile(maze, p);

            Assert.AreEqual(amgz.get_wall_char(t, Direction.North), '─');
            amgz.set_wall_char(t, Direction.North, 'X');
            Assert.AreEqual(amgz.get_wall_char(t, Direction.North), 'X');

        }

        [Test]
        public void test_set_wall_joint_char() {
            ASCIIMazeGlyphStyle amgz = new ASCIIMazeGlyphStyle();

            Assert.AreEqual(amgz.get_wall_joint_char(1), '─');
            amgz.set_wall_joint_char(1, 'X');
            Assert.AreEqual(amgz.get_wall_joint_char(1), 'X');

        }

        [TestCase(Direction.North, '▲', MazeStyles.default_)]
        [TestCase(Direction.East, '►',  MazeStyles.default_)]
        [TestCase(Direction.South, '▼', MazeStyles.default_)]
        [TestCase(Direction.West, '◄',  MazeStyles.default_)]
        //non default tests
        [TestCase(Direction.North, '^', MazeStyles.character)]
        [TestCase(Direction.East, '>',  MazeStyles.character)]
        [TestCase(Direction.South, '!', MazeStyles.character)]
        [TestCase(Direction.West, '!',  MazeStyles.character)]

        public void test_character_value(Direction direction, char expected, MazeStyles style_enum)
        {
            Position p = new Position(0, 0);

            Maze maze = TestBaseClass.create_maze();
            TestBaseClass.create_tile(maze, p);
            Character character = TestBaseClass.create_character(maze, p);
            IASCIIMazeStyle style = style_dictionary[style_enum];

            character.set_orientation(direction);
            Assert.AreEqual(style.get_character_char(character), expected);

        }



        [TestCase(Direction.North, '─', MazeStyles.default_)]
        [TestCase(Direction.East,  '│',  MazeStyles.default_)]
        [TestCase(Direction.South, '─', MazeStyles.default_)]
        [TestCase(Direction.West,  '│',  MazeStyles.default_)]
        //non_default_test
        [TestCase(Direction.North, '.', MazeStyles.wall)]
        [TestCase(Direction.East,  '.', MazeStyles.wall)]
        [TestCase(Direction.South, '@', MazeStyles.wall)]
        [TestCase(Direction.West,  '#', MazeStyles.wall)]
        public void test_wall_value(Direction direction, char expected, MazeStyles style_enum){
            Maze maze = TestBaseClass.create_maze();
            IASCIIMazeStyle style = style_dictionary[style_enum];

            Position p = new Position(0, 0);
            TestBaseClass.create_wall(maze, p, direction);
            Tile tile = maze.get_tile(p);


            Assert.AreEqual(style.get_wall_char(tile, direction), expected);
        }


        [TestCase(' ', '█', MazeStyles.default_)]
        [TestCase('$', '?', MazeStyles.tile)]
        public void test_tile_value(char tile_char, char block_char, MazeStyles style_enum)
        {
            Maze maze = TestBaseClass.create_maze();
            IASCIIMazeStyle style = style_dictionary[style_enum];

            Tile tile = TestBaseClass.create_tile(maze, new Position(0, 0));
            Tile block = new Block(maze, new Position(0, 1));


            Assert.AreEqual(style.get_tile_char(tile), tile_char);
            Assert.AreEqual(style.get_tile_char(block), block_char);
        }

        [TestCase(0, ' ', MazeStyles.default_)]
        [TestCase(1, '─', MazeStyles.default_)]
        [TestCase(2, '│', MazeStyles.default_)]
        [TestCase(3, '┐', MazeStyles.default_)]
        [TestCase(4, '─', MazeStyles.default_)]
        [TestCase(5, '─', MazeStyles.default_)]
        [TestCase(6, '┌', MazeStyles.default_)]
        [TestCase(7, '┬', MazeStyles.default_)]
        [TestCase(8, '│', MazeStyles.default_)]
        [TestCase(9, '┘', MazeStyles.default_)]
        [TestCase(10, '│', MazeStyles.default_)]
        [TestCase(11, '┤', MazeStyles.default_)]
        [TestCase(12, '└', MazeStyles.default_)]
        [TestCase(13, '┴', MazeStyles.default_)]
        [TestCase(14, '├', MazeStyles.default_)]
        [TestCase(15, '┼', MazeStyles.default_)]
        [TestCase(16, '?', MazeStyles.default_)]
        //non_default_tests
        [TestCase(0,  '*', MazeStyles.wall_joint)]
        [TestCase(8,  '&', MazeStyles.wall_joint)]
        [TestCase(16, '&', MazeStyles.wall_joint)]
        public void test_wall_joint_value(int joint_id, char expected, MazeStyles style_enum = MazeStyles.default_)
        {
            IASCIIMazeStyle style = style_dictionary[style_enum];
            Assert.AreEqual(style.get_wall_joint_char(joint_id), expected);
        }
    }
}
