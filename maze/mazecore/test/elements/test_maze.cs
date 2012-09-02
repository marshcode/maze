﻿namespace mazecore.elements.test {
    using System;
    using NUnit.Framework;
    using mazecore.direction;
    using mazecore.elements;
    using mazecore.test;

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
        [Test]
        public void test_tile() {
            Maze maze = TestMaze.create_maze();
            Assert.Null(maze.get_tile(1, 1));
            Tile tile = TestMaze.create_tile(maze, 1, 1);

            
            maze.set_tile(tile, 1, 1);
            Assert.AreEqual(tile, maze.get_tile(1,1));
            maze.remove_tile(1, 1);
            Assert.Null(maze.get_tile(1,1));
        }

        [TestCase(-1, -1)]
        public void test_tile_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();
            Tile tile = TestMaze.create_tile(maze, 1, 1);

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_tile(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_tile(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.set_tile(tile, x, y); }); //not usually supported


        }

        [Test]
        public void test_wall() {
            Maze maze = TestMaze.create_maze();
            Assert.Null(maze.get_wall(1, 1, Direction.North));
            Wall wall  = TestMaze.create_wall(maze, 1, 1, Direction.North);
            maze.set_wall(wall, 1,1, Direction.North);
            //should I fail if there is no tile to set the wall on?
            //it won't matter, I guess.  No tiles means nobody can see it.  

            Assert.AreEqual(wall, maze.get_wall(1, 1, Direction.North));
            Assert.AreEqual(wall, maze.get_wall(1, 2, Direction.South));
            maze.remove_wall(1, 1, Direction.North);
            Assert.Null(maze.get_wall(1, 1, Direction.North));

        }

        [TestCase(-1, -1)]
        public void test_wall_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_wall(x, y, Direction.North); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_wall(x, y, Direction.North); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestMaze.create_wall(maze, x, y, Direction.North); });


        }


        [Test]
        public void test_character() {

            Maze maze = TestMaze.create_maze();
            
            Tile tile = TestMaze.create_tile(maze, 1, 1);
            Tile tile2 = TestMaze.create_tile(maze, 2, 3);

            Assert.Null(maze.get_character(1,1));
            Character character = TestMaze.create_character(maze, 1, 1);
            Assert.AreEqual(character, maze.get_character(1, 1));

            maze.set_character(character, 2, 3);
            Assert.AreEqual(character, maze.get_character(2, 3));
            Assert.Null(maze.get_character(1, 1));

            maze.remove_character(1, 1);
            Assert.Null(maze.get_character(1, 1));


        }

        [Test]
        public void test_characte_no_overwrite() {

            Maze maze = TestMaze.create_maze();
            Character c1;
            Tile tile = TestMaze.create_tile(maze, 1, 1);

            Assert.Null(maze.get_character(1, 1));

            c1 = TestMaze.create_character(maze, 1, 1);
            Assert.AreEqual(maze.get_character(1, 1), c1);

            Assert.Throws<MazeException>(
                    delegate {TestMaze.create_character(maze, 1, 1); });
            Assert.AreEqual(maze.get_character(1, 1), c1);

        }

        [Test]
        public void test_character_set_null_tile() {
            Maze maze = TestMaze.create_maze();
            Assert.Throws<MazeException>(
                      delegate { TestMaze.create_character(maze, 1, 1); });
            

        }


        [TestCase(-1, -1)]
        public void test_character_out_of_bounds(int x, int y) {
            Maze maze = TestMaze.create_maze();

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.get_character(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { maze.remove_character(x, y); });

            Assert.Throws<ArgumentOutOfRangeException>(
                    delegate { TestMaze.create_character(maze, x, y); });
        }
    }
}