
namespace mazecore.elements.test {

    using System;
    using NUnit.Framework;
    using mazecore.test;
    using mazecore.elements;
    using mazecore.direction;

    [TestFixture]
    class TestCharacter : TestBaseClass {

        [Test]
        public void test_get_tile() {
            Position p = new Position(1, 1);


            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, p);
            Character character = TestCharacter.create_character(maze, p);


            p = p.move(Direction.South, 1);
            Tile tile_south = TestCharacter.create_tile(maze, p);

            Assert.AreEqual(character.get_tile(), tile);
            character.move(Direction.South);
            Assert.AreEqual(character.get_tile(), tile_south);

        
        }

        [Test]
        public void test_init_tile_occupied() {
            Position p = new Position(1, 1);
            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, p);
            TestCharacter.create_character(maze, p);

            Assert.Throws<MazeException>(
                delegate { TestCharacter.create_character(maze, p); });
        
        }


        [Test]
        public void test_init_tile_unoccupiable() {
            Position p = new Position(1, 1);
            Maze maze = TestCharacter.create_maze();
            Block block = new Block(maze, p);

            Assert.Throws<MazeException>(
                delegate { TestCharacter.create_character(maze, p); });

        }


        [Test]
        public void test_get_facing_wall() {
            Position p = new Position(1, 1);
            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, p);

            Wall wall_north = TestCharacter.create_wall(maze, p, Direction.North);
            Wall wall_south = TestCharacter.create_wall(maze, p, Direction.South);


            Character character = TestCharacter.create_character(maze, p);

            Assert.AreEqual(character.get_facing_wall(), wall_north);

            character.set_orientation(Direction.South);
            Assert.AreEqual(character.get_facing_wall(), wall_south);

            character.set_orientation(Direction.East);
            Assert.Null(character.get_facing_wall());

            character.set_orientation(Direction.West);
            Assert.Null(character.get_facing_wall());

        }

        [TestCase(Direction.North, true,  1, 3)]
        [TestCase(Direction.East,  false, 1, 2)]
        [TestCase(Direction.South, false, 1, 2)]
        [TestCase(Direction.West,  false, 1, 2)]
        public void test_move(Direction direction, bool expected_result, int exp_x, int exp_y) {
            /* Test Move.  Character movement scenario in the following maze:
              O
             X*| 
              *
             
             Start in the center.  Available tile to the north, wall to the west.  Null tiles to the east and occupied to the south
             */
            Position p = new Position(1, 2);
            Position p_t = null;

            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, p);

            p_t = p.move(Direction.North, 1);
            Tile tile_north = TestCharacter.create_tile(maze, p_t);

            p_t = p.move(Direction.South, 1);
            Tile tile_south = TestCharacter.create_tile(maze, p_t);
            Character character_south = TestCharacter.create_character(maze, p_t);

            Wall west_wall = TestCharacter.create_wall(maze, p, Direction.West);
            Character character_main = TestCharacter.create_character(maze, p);

            Assert.AreEqual(character_main.move(direction), expected_result);
            Assert.AreEqual(character_main.get_position().x, exp_x);
            Assert.AreEqual(character_main.get_position().y, exp_y);

            Assert.AreEqual(maze.get_character(character_main.get_position()), character_main);

        }

        [Test]
        public void test_orientation() {
            Position p = new Position(1, 2);
            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, p);
            Character character = TestCharacter.create_character(maze, p);

            Assert.AreEqual(Direction.North, character.get_orientation());
            character.set_orientation(Direction.South);
            Assert.AreEqual(Direction.South, character.get_orientation());

        }

        [Test]
        public void test_init_registration(){
            Position p = new Position(1, 2);
            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, p);
            Character character = TestCharacter.create_character(maze, p);

            Assert.AreEqual(maze, character.get_maze());
        }
        [Test]
        public void test_get_x_get_y() {
            Position p = new Position(1, 2);
            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, p);
            Character character = TestCharacter.create_character(maze, p);

            Assert.AreEqual(character.get_position().x, 1);
            Assert.AreEqual(character.get_position().y, 2);

        }


    }



    [TestFixture]
    class TestNavigation : TestBaseClass {

        public static Navigation create_navigation(Character character) {
            return new Navigation(character);
        }
        //not safe for boundaries
        public static Tile[] create_tile_neighbors(Maze maze, Tile t) {
            Position p = t.get_position();

            Tile tile_north = TestNavigation.create_tile(maze, p.move(Direction.North, 1));
            Tile tile_east = TestNavigation.create_tile(maze, p.move(Direction.East, 1));
            Tile tile_south = TestNavigation.create_tile(maze, p.move(Direction.South, 1));
            Tile tile_west = TestNavigation.create_tile(maze, p.move(Direction.West, 1));


            return new Tile[] { tile_north, tile_east, tile_south, tile_west };
        
        }


        [TestCase(Direction.North)]
        [TestCase(Direction.East)]
        [TestCase(Direction.South)]
        [TestCase(Direction.West)]
        public void test_cannot_move_tiles_occupied(Direction direction) {
            Position p = new Position(2, 3);

            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, p);
            Tile[] neighbor_tiles = TestNavigation.create_tile_neighbors(maze, tile);
            Character character = TestNavigation.create_character(maze, p);

            foreach (Tile t in neighbor_tiles) {
                TestNavigation.create_character(maze, t.get_position());
            }

            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.False(navigation.can_move(direction));

        }

        [Test]
        public void test_cannot_move_cannot_stand() {
            Position p = new Position(2, 3);
            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, p);
            TestNavigation.create_tile_neighbors(maze, tile);
            
            Tile north_tile = tile.get_neighbor_tile(Direction.North);
            Block b = new Block(maze, north_tile.get_position());


            Character character = TestNavigation.create_character(maze, p);
            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.False(navigation.can_move(Direction.North));
        }


        [TestCase(Direction.North)]
        [TestCase(Direction.East)]
        [TestCase(Direction.South)]
        [TestCase(Direction.West)]
        public void test_can_move_uninhibited(Direction direction) {
            Position p = new Position(2, 3);

            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, p);
            TestNavigation.create_tile_neighbors(maze, tile);
            Character character = TestNavigation.create_character(maze, p);

            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.True(navigation.can_move(direction));

        }

        [TestCase(Direction.North)]
        [TestCase(Direction.East)]
        [TestCase(Direction.South)]
        [TestCase(Direction.West)]
        public void test_cannot_move_null_tiles(Direction direction) {
            Position p = new Position(2, 3);
            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, p);
            Character character = TestNavigation.create_character(maze, p);
            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.False(navigation.can_move(direction));

        }

        [TestCase(Direction.North)]
        [TestCase(Direction.East)]
        [TestCase(Direction.South)]
        [TestCase(Direction.West)]
        public void test_cannot_move_at_all_walled_in(Direction direction) {
            Position p = new Position(2, 3);
            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, p);
            TestNavigation.create_wall(maze, p, Direction.North);
            TestNavigation.create_wall(maze, p, Direction.South);
            TestNavigation.create_wall(maze, p, Direction.East);
            TestNavigation.create_wall(maze, p, Direction.West);

            Character character = TestNavigation.create_character(maze, p);
            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.False(navigation.can_move(direction));

        }

    }

}