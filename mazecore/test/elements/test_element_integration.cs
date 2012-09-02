namespace mazecore.elements.tests {
    
    using NUnit.Framework;
    using mazecore.direction;
    using mazecore.elements;
    using mazecore.test;
    
    [TestFixture]
    class TestElementIntegration : TestBaseClass{

        /*
         * TestMaze that looks like this:
          
         
           _
       2  |*|_
       1  |  _|____
       0  |________| 
       Y X 0 1 2345
         * marks the start location of the character 
          
         */

        public Maze create_standard_maze(){

            Maze maze = TestElementIntegration.create_maze();
            
            TestElementIntegration.create_tile(maze, 0, 2);
            TestElementIntegration.create_wall(maze, 0, 2, Direction.North);
            TestElementIntegration.create_wall(maze, 0, 2, Direction.West);
            TestElementIntegration.create_wall(maze, 0, 2, Direction.East);
            Character character = TestElementIntegration.create_character(maze, 0, 2);

            TestElementIntegration.create_tile(maze, 0, 1);
            TestElementIntegration.create_wall(maze, 0, 1, Direction.West);
            
            TestElementIntegration.create_tile(maze, 1, 1);
            TestElementIntegration.create_wall(maze, 1, 1, Direction.North);
            TestElementIntegration.create_wall(maze, 1, 1, Direction.East);
            TestElementIntegration.create_wall(maze, 1, 1, Direction.South);

            TestElementIntegration.create_tile(maze, 0, 0);
            TestElementIntegration.create_wall(maze, 0, 0, Direction.West);
            TestElementIntegration.create_wall(maze, 0, 0, Direction.South);

            TestElementIntegration.create_tile(maze, 1, 0);
            TestElementIntegration.create_wall(maze, 1, 0, Direction.South);

            TestElementIntegration.create_tile(maze, 2, 0);
            TestElementIntegration.create_wall(maze, 2, 0, Direction.South);
            TestElementIntegration.create_wall(maze, 2, 0, Direction.North);


            TestElementIntegration.create_tile(maze, 3, 0);
            TestElementIntegration.create_wall(maze, 3, 0, Direction.South);
            TestElementIntegration.create_wall(maze, 3, 0, Direction.North);

            TestElementIntegration.create_tile(maze, 4, 0);
            TestElementIntegration.create_wall(maze, 4, 0, Direction.South);
            TestElementIntegration.create_wall(maze, 4, 0, Direction.North);


            TestElementIntegration.create_tile(maze, 5, 0);
            TestElementIntegration.create_wall(maze, 5, 0, Direction.South);
            TestElementIntegration.create_wall(maze, 5, 0, Direction.East);
            TestElementIntegration.create_wall(maze, 5, 0, Direction.North);

            return maze;

        }

        [Test]
        public void test_into_cubby() {

            Maze maze = this.create_standard_maze();
            Character character = maze.get_character(0, 2);

            Assert.True( character.move(Direction.South) );
            Assert.True( character.move(Direction.East)  );

            Assert.False(character.move(Direction.North));
            Assert.False(character.move(Direction.East));
            Assert.False(character.move(Direction.South));

        }

        [Test]
        public void test_in_an_L(){

            Maze maze = this.create_standard_maze();
            Character character = maze.get_character(0, 2);

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