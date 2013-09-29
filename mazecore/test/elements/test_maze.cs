namespace mazecore.elements.test {
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;
    using mazecore.direction;
    using mazecore.elements;
    using mazecore.test;

    class TileEventCount : Tile {

        public List<MovementEvent> on_events;
        public List<MovementEvent> off_events;

        public TileEventCount(Maze maze, Position p)
            : base(maze, p) {
                this.on_events = new List<MovementEvent>();
                this.off_events = new List<MovementEvent>();
        }

        public MovementEvent get_last_on_event(){
            return this.on_events[this.on_events.Count-1];
        }
        public override void action_step_on(MovementEvent me) { 
            this.on_events.Add(me);
        }

        public MovementEvent get_last_off_event() {
            return this.off_events[this.off_events.Count-1];
        }
        public override void action_step_off(MovementEvent me) { 
            this.off_events.Add(me);
        }

    }


    [TestFixture]
    class TestMaze : TestBaseClass {
        /********************
         * Tests
         * 
         * Not sure if it is worth it to re-test the corner cases that I've already tested below.
         * For now, do simple get/set/remove tests
         * ******************/
        [Test]
        public void test_init(){
            Maze maze = TestMaze.create_maze();
            Assert.AreEqual(maze.get_x_range(), 10);
            Assert.AreEqual(maze.get_y_range(), 15);
        }

        [TestCase(0, 0, true),
         TestCase(2, 2, true),
         TestCase(5, 5, true),
         TestCase(8, 8, true),
         TestCase(10, 12, true),
         TestCase(10, 15, true),
         TestCase(0, 15, true),
         TestCase(2, 12, true),
         TestCase(5, 8, true),
         TestCase(8, 5, true),
         TestCase(10, 2, true),
         TestCase(10, 0, true),
         TestCase(-1, -1, false),
         TestCase(11, 16, false),
         TestCase(0, -1, false),
         TestCase(-1, 0, false)
        ]
        public void test_in_range(int x, int y, bool expected) {
            Maze maze = TestMaze.create_maze();
            Assert.AreEqual(maze.in_range(new Position(x, y)), expected);

        }

        [Test]
        public void test_tile() {

            Position p = new Position(1, 1);

            Maze maze = TestMaze.create_maze();
            Assert.Null(maze.get_tile(p));
            Tile tile = TestMaze.create_tile(maze, p);


            maze.set_tile(tile, p);
            Assert.AreEqual(tile, maze.get_tile(p));
            maze.remove_tile(p);
            Assert.Null(maze.get_tile(p));
        }

        [TestCase(-1, -1)]
        public void test_tile_out_of_bounds(int x, int y) {

            Position p = new Position(x, y);

            Maze maze = TestMaze.create_maze();
            Tile tile = TestMaze.create_tile(maze, new Position(1, 1));

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_tile(p); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_tile(p); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.set_tile(tile, p); }); //not usually supported


        }

        [Test]
        public void test_wall() {

            Position p = new Position(1, 1);
            Position p2 = new Position(1, 2);

            Maze maze = TestMaze.create_maze();
            Assert.Null(maze.get_wall(p, Direction.North));
            Wall wall = TestMaze.create_wall(maze, p, Direction.North);
            maze.set_wall(wall, p, Direction.North);
            //should I fail if there is no tile to set the wall on?
            //it won't matter, I guess.  No tiles means nobody can see it.  

            Assert.AreEqual(wall, maze.get_wall(p, Direction.North));
            Assert.AreEqual(wall, maze.get_wall(p2, Direction.South));
            maze.remove_wall(p, Direction.North);
            Assert.Null(maze.get_wall(p, Direction.North));

        }

        [TestCase(-1, -1)]
        public void test_wall_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();
            Position p = new Position(x, y);

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_wall(p, Direction.North); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_wall(p, Direction.North); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestMaze.create_wall(maze, p, Direction.North); });


        }


        [Test]
        public void test_character() {

            Maze maze = TestMaze.create_maze();

            Position p1 = new Position(1, 1);
            Position p2 = new Position(2, 3);

            Tile tile = TestMaze.create_tile(maze, p1);
            Tile tile2 = TestMaze.create_tile(maze, p2);

            Assert.Null(maze.get_character(p1));
            Character character = TestMaze.create_character(maze, p1);
            Assert.AreEqual(character, maze.get_character(p1));

            maze.set_character(character, p2);
            Assert.AreEqual(character, maze.get_character(p2));
            Assert.Null(maze.get_character(p1));

            maze.remove_character(p1);
            Assert.Null(maze.get_character(p1));


        }

        [Test]
        public void test_characte_no_overwrite() {

            Maze maze = TestMaze.create_maze();
            Character c1;

            Position p1 = new Position(1, 1);
            Tile tile = TestMaze.create_tile(maze, p1);

            Assert.Null(maze.get_character(p1));

            c1 = TestMaze.create_character(maze, p1);
            Assert.AreEqual(maze.get_character(p1), c1);

            Assert.Throws<MazeException>(
                    delegate { TestMaze.create_character(maze, p1); });
            Assert.AreEqual(maze.get_character(p1), c1);

        }

        [Test]
        public void test_character_set_null_tile() {
            Maze maze = TestMaze.create_maze();
            Position p1 = new Position(1, 1);

            Assert.Throws<MazeException>(
                      delegate { TestMaze.create_character(maze, p1); });
            

        }


        [TestCase(-1, -1)]
        public void test_character_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();
            Position p1 = new Position(x, y);

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_character(p1); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_character(p1); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestMaze.create_character(maze, p1); });
        }


        [Test]
        public void test_normal_event() {            
            Maze maze = new Maze(1, 2);

            TileEventCount t00 = new TileEventCount(maze, new Position(0, 0));
            TileEventCount t01 = new TileEventCount(maze, new Position(0, 1));
            Character c;

            Action<MovementEvent, Character, Position, Position> assert_movement_event = 
                delegate( MovementEvent me, Character character,
                          Position moved_from, Position moved_to) {

                Assert.AreEqual(me.character, character, "Character failure.");
                Assert.AreEqual(me.character.get_position(), moved_to, "Character position does not match given destination.");
                Assert.AreEqual(me.moved_from, moved_from, "Position: Moved from failure.");
                Assert.AreEqual(me.moved_to, moved_to, "Position: Moved to failure.");

            };


            int t0_event_count = 0;
            int t1_event_count = 0;

            Action<MovementEvent> ft0 = delegate(MovementEvent evt) {
                t0_event_count += 1;
            };
            Action<MovementEvent> ft1 = delegate(MovementEvent evt) {
                t1_event_count += 1;
            };


            maze.get_movement_event(t00.get_position()).register(ft0);
            maze.get_movement_event(t01.get_position()).register(ft1);
            //TODO: test the actual Character and positions being provided with the MovementEvent objects
            Assert.AreEqual(t00.on_events.Count, 0);
            Assert.AreEqual(t00.off_events.Count, 0);
            Assert.AreEqual(t01.on_events.Count, 0);
            Assert.AreEqual(t01.off_events.Count, 0);
            Assert.AreEqual(t0_event_count, 0);
            Assert.AreEqual(t1_event_count, 0);

            c = new Character(maze, t00.get_position());
            Assert.AreEqual(t00.on_events.Count, 1);
            Assert.AreEqual(t00.off_events.Count, 0);
            Assert.AreEqual(t01.on_events.Count, 0);
            Assert.AreEqual(t01.off_events.Count, 0);
            Assert.AreEqual(t0_event_count, 1);
            Assert.AreEqual(t1_event_count, 0);

            assert_movement_event(t00.get_last_on_event(), c, null, t00.get_position());

            c.move(Direction.North);
            Assert.AreEqual(t00.on_events.Count, 1);
            Assert.AreEqual(t00.off_events.Count, 1);
            Assert.AreEqual(t01.on_events.Count, 1);
            Assert.AreEqual(t01.off_events.Count, 0);
            Assert.AreEqual(t0_event_count, 1);
            Assert.AreEqual(t1_event_count, 1);

            assert_movement_event(t00.get_last_off_event(), c, t00.get_position(), t01.get_position());
            assert_movement_event(t01.get_last_on_event(), c, t00.get_position(), t01.get_position());

            c.move(Direction.South);
            Assert.AreEqual(t00.on_events.Count, 2);
            Assert.AreEqual(t00.off_events.Count, 1);
            Assert.AreEqual(t01.on_events.Count, 1);
            Assert.AreEqual(t01.off_events.Count, 1);
            Assert.AreEqual(t0_event_count, 2);
            Assert.AreEqual(t1_event_count, 1);

            assert_movement_event(t01.get_last_off_event(), c, t01.get_position(), t00.get_position());
            assert_movement_event(t00.get_last_on_event(), c, t01.get_position(), t00.get_position());

            //for the next two sections, tile events keep firing but we are unregistering ourselves
            //from the custom event
            maze.get_movement_event(t01.get_position()).unregister(ft1);
            c.move(Direction.North);
            Assert.AreEqual(t00.on_events.Count, 2);
            Assert.AreEqual(t00.off_events.Count, 2); 
            Assert.AreEqual(t01.on_events.Count, 2);
            Assert.AreEqual(t01.off_events.Count, 1);
            Assert.AreEqual(t0_event_count, 2);
            Assert.AreEqual(t1_event_count, 1);

            maze.get_movement_event(t00.get_position()).unregister(ft0);
            c.move(Direction.South);
            Assert.AreEqual(t00.on_events.Count, 3);
            Assert.AreEqual(t00.off_events.Count, 2);
            Assert.AreEqual(t01.on_events.Count, 2);
            Assert.AreEqual(t01.off_events.Count, 2);
            Assert.AreEqual(t0_event_count, 2);
            Assert.AreEqual(t1_event_count, 1);

        }
    }
}