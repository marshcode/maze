using mazecore.elements;
using mazecore.test;
using NUnit.Framework;


namespace mazecore.elements.tests {

    [TestFixture]
    class TestElementIntegration : TestBaseClass{

        /*
         * TestMaze that looks like this:
          
           0 1 2345
           _
       0  |X|_
       1  |  _|____
       2  |________| 
          
         X marks the start location of the character 
          
         */

        public Maze create_standard_maze(){

            Maze maze = TestElementIntegration.create_maze();
            
            TestElementIntegration.create_tile(maze, 0, 0);
            TestElementIntegration.create_wall(maze, 0, 0, Direction.West);
            TestElementIntegration.create_wall(maze, 0, 0, Direction.North);
            TestElementIntegration.create_wall(maze, 0, 0, Direction.East);
            Character character = TestElementIntegration.create_character(maze, 0, 0);


            TestElementIntegration.create_tile(maze, 1, 0);
            TestElementIntegration.create_wall(maze, 1, 0, Direction.West);
            TestElementIntegration.create_wall(maze, 1, 0, Direction.West);
            
            TestElementIntegration.create_tile(maze, 1, 1);
            TestElementIntegration.create_wall(maze, 1, 1, Direction.North);
            TestElementIntegration.create_wall(maze, 1, 1, Direction.East);
            TestElementIntegration.create_wall(maze, 1, 1, Direction.South);

            TestElementIntegration.create_tile(maze, 2, 0);
            TestElementIntegration.create_wall(maze, 2, 0, Direction.West);
            TestElementIntegration.create_wall(maze, 2, 0, Direction.South);

            TestElementIntegration.create_tile(maze, 2, 1);
            TestElementIntegration.create_wall(maze, 2, 1, Direction.South);
            
            TestElementIntegration.create_tile(maze, 2, 2);
            TestElementIntegration.create_wall(maze, 2, 2, Direction.South);

            TestElementIntegration.create_tile(maze, 2, 3);
            TestElementIntegration.create_wall(maze, 2, 3, Direction.South);
            
            TestElementIntegration.create_tile(maze, 2, 4);
            TestElementIntegration.create_wall(maze, 2, 4, Direction.South);

            TestElementIntegration.create_tile(maze, 2, 5);
            TestElementIntegration.create_wall(maze, 2, 5, Direction.South);
            TestElementIntegration.create_wall(maze, 2, 5, Direction.East);

            return maze;

        }

        [Test]
        public void test_into_cubby() {

            Maze maze = this.create_standard_maze();
            Character character = maze.get_character(0, 0);

            Assert.True( character.move(Direction.South) );
            Assert.True( character.move(Direction.East)  );

            Assert.False(character.move(Direction.North));
            Assert.False(character.move(Direction.East));
            Assert.False(character.move(Direction.South));

        }

        [Test]
        public void test_in_an_L(){

            Maze maze = this.create_standard_maze();
            Character character = maze.get_character(0, 0);

            Assert.False(character.move(Direction.North));
            Assert.False(character.move(Direction.West));
            Assert.False(character.move(Direction.East));

            Assert.True(character.move(Direction.South));
            Assert.True(character.move(Direction.South));

            Assert.True(character.move(Direction.East));
            Assert.True(character.move(Direction.East));
            Assert.True(character.move(Direction.East));
            Assert.True(character.move(Direction.East));
            Assert.True(character.move(Direction.East));

            Assert.False(character.move(Direction.North));
            Assert.False(character.move(Direction.East));
            Assert.False(character.move(Direction.South));
        }



    }


}