﻿using mazecore.test;
using mazecore.elements;
using NUnit.Framework;
using System;
namespace mazecore.elements.test {

    [TestFixture]
    class TestCharacter : TestBaseClass {

        [TestCase(Direction.North, true,  0, 2)]
        [TestCase(Direction.East,  false, 1, 2)]
        [TestCase(Direction.South, false, 1, 2)]
        [TestCase(Direction.West,  false, 1, 2)]
        
        public void test_move(Direction direction, bool expected_result, int exp_x, int exp_y) {
            /* Test Move.  Character movement scenario in the following maze:
              O
             XO| 
              X
             
             Start in the center.  Available tile to the north, wall to the west.  Null tiles to the south and east
             */
            int x = 1, y = 2;
            int t_x, t_y;

            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, x, y);

            t_x = x; t_y = y;
            DirectionControl.move(ref t_x, ref t_y, Direction.North, 1);
            Tile tile_north = TestCharacter.create_tile(maze, t_x, t_y);

            Wall west_wall = TestCharacter.create_wall(maze, x, y, Direction.West);
            Character character = TestCharacter.create_character(maze, 1, 2);

            Assert.AreEqual(character.move(direction), expected_result);
            Assert.AreEqual(character.get_x(), exp_x);
            Assert.AreEqual(character.get_y(), exp_y);


        }


        [Test]
        public void test_init_registration(){
            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, 1, 2);
            Character character = TestCharacter.create_character(maze, 1, 2);

            Assert.AreEqual(maze, character.get_maze());
        }
        [Test]
        public void test_get_x_get_y() {
            Maze maze = TestCharacter.create_maze();
            Tile tile = TestCharacter.create_tile(maze, 1, 2);
            Character character = TestCharacter.create_character(maze, 1, 2);

            Assert.AreEqual(character.get_x(), 1);
            Assert.AreEqual(character.get_y(), 2);

        }


    }



    [TestFixture]
    class TestNavigation : TestBaseClass {

        public static Navigation create_navigation(Character character) {
            return new Navigation(character);
        }
        //not safe for boundaries
        public static Tile[] create_tile_neighbors(Maze maze, Tile t) {
            int x = t.get_x();
            int y = t.get_y();

            Tile tile_north = TestNavigation.create_tile(maze, DirectionControl.adjust(x, Direction.North, 1), y);
            Tile tile_east = TestNavigation.create_tile(maze, x, DirectionControl.adjust(y, Direction.East, 1));
            Tile tile_south = TestNavigation.create_tile(maze, DirectionControl.adjust(x, Direction.South, 1), y);
            Tile tile_west = TestNavigation.create_tile(maze, x, DirectionControl.adjust(y, Direction.West, 1));


            return new Tile[] { tile_north, tile_east, tile_south, tile_west };
        
        }

        [TestCase(Direction.North)]
        [TestCase(Direction.East)]
        [TestCase(Direction.South)]
        [TestCase(Direction.West)]
        public void test_can_move_uninhibited(Direction direction) {
            int x = 2, y = 3;

            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, x, y);
            TestNavigation.create_tile_neighbors(maze, tile);
            Character character = TestNavigation.create_character(maze, x, y);

            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.True(navigation.can_move(direction));

        }

        [TestCase(Direction.North)]
        [TestCase(Direction.East)]
        [TestCase(Direction.South)]
        [TestCase(Direction.West)]
        public void test_cannot_move_null_tiles(Direction direction) {
            int x = 2, y = 3;
            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, x, y);
            Character character = TestNavigation.create_character(maze, x, y);
            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.False(navigation.can_move(direction));

        }

        [TestCase(Direction.North)]
        [TestCase(Direction.East)]
        [TestCase(Direction.South)]
        [TestCase(Direction.West)]
        public void test_cannot_move_at_all_walled_in(Direction direction) {
            int x = 2, y = 3;
            Maze maze = TestNavigation.create_maze();
            Tile tile = TestNavigation.create_tile(maze, x, y);
            TestNavigation.create_wall(maze, x, y, Direction.North);
            TestNavigation.create_wall(maze, x, y, Direction.South);
            TestNavigation.create_wall(maze, x, y, Direction.East);
            TestNavigation.create_wall(maze, x, y, Direction.West);

            Character character = TestNavigation.create_character(maze, x, y);
            Navigation navigation = TestNavigation.create_navigation(character);

            Assert.False(navigation.can_move(direction));

        }

    }

}