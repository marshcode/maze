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

            Position p1 = new Position(0, 2);
            Position p2 = new Position(0, 1);
            Position p3 = new Position(1, 1);
            Position p4 = new Position(0, 0);
            Position p5 = new Position(1, 0);
            Position p6 = new Position(2, 0);
            Position p7 = new Position(3, 0);
            Position p8 = new Position(4, 0);
            Position p9 = new Position(5, 0);


            TestElementIntegration.create_tile(maze, p1);
            TestElementIntegration.create_wall(maze, p1, Direction.North);
            TestElementIntegration.create_wall(maze, p1, Direction.West);
            TestElementIntegration.create_wall(maze, p1, Direction.East);
            Character character = TestElementIntegration.create_character(maze, p1);

            TestElementIntegration.create_tile(maze, p2);
            TestElementIntegration.create_wall(maze, p2, Direction.West);

            TestElementIntegration.create_tile(maze, p3);
            TestElementIntegration.create_wall(maze, p3, Direction.North);
            TestElementIntegration.create_wall(maze, p3, Direction.East);
            TestElementIntegration.create_wall(maze, p3, Direction.South);

            TestElementIntegration.create_tile(maze, p4);
            TestElementIntegration.create_wall(maze, p4, Direction.West);
            TestElementIntegration.create_wall(maze, p4, Direction.South);

            TestElementIntegration.create_tile(maze, p5);
            TestElementIntegration.create_wall(maze, p5, Direction.South);

            TestElementIntegration.create_tile(maze, p6);
            TestElementIntegration.create_wall(maze, p6, Direction.South);
            TestElementIntegration.create_wall(maze, p6, Direction.North);


            TestElementIntegration.create_tile(maze, p7);
            TestElementIntegration.create_wall(maze, p7, Direction.South);
            TestElementIntegration.create_wall(maze, p7, Direction.North);

            TestElementIntegration.create_tile(maze, p8);
            TestElementIntegration.create_wall(maze, p8, Direction.South);
            TestElementIntegration.create_wall(maze, p8, Direction.North);


            TestElementIntegration.create_tile(maze, p9);
            TestElementIntegration.create_wall(maze, p9, Direction.South);
            TestElementIntegration.create_wall(maze, p9, Direction.East);
            TestElementIntegration.create_wall(maze, p9, Direction.North);

            return maze;

        }

        [Test]
        public void test_into_cubby() {

            Position p = new Position(0, 2);

            Maze maze = this.create_standard_maze();
            Character character = maze.get_character(p);

            Assert.True( character.move(Direction.South) );
            Assert.True( character.move(Direction.East)  );

            Assert.False(character.move(Direction.North));
            Assert.False(character.move(Direction.East));
            Assert.False(character.move(Direction.South));

        }

        [Test]
        public void test_in_an_L(){

            Maze maze = this.create_standard_maze();
            Character character = maze.get_character(new Position(0, 2));

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