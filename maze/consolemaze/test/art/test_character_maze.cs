namespace consolemaze.art.test {
    using NUnit.Framework;
    
    using consolemaze.art;
    using mazecore.elements;

    [TestFixture]
    class TestCharacterMaze {


        protected bool render_compare(string one, string two) {
            return one.Trim().Equals( two.Trim()  );
        }

        [Test]
        public void test_one_room() {
            Maze maze = new Maze(10, 10);
            Tile tile = new Tile(maze, 0, 0);

            CharacterMaze character_maze = new CharacterMaze(maze);
            string actual = character_maze.render();
            string expected = @"
┌─┐
│ |
└─┘
";
            Assert.True( this.render_compare(expected, actual) );
        }

        [Test]
        public void test_two_rooms() {
            Maze maze = new Maze(10, 10);
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 1, 0);
            CharacterMaze character_maze = new CharacterMaze(maze);
            string actual = character_maze.render();
            string expected = @"
┌─┬─┐
│ │ │
└─┴─┘
";
            Assert.True(this.render_compare(expected, actual));          
        
        }

        [Test]
        public void test_two_rooms_no_wall() {
            //note that the removal of the wall changes the ticks above and below it
            Maze maze = new Maze(10, 10);
            Tile tile1 = new Tile(maze, 0, 0);
            Tile tile2 = new Tile(maze, 1, 0);
            CharacterMaze character_maze = new CharacterMaze(maze);
            string actual = character_maze.render();
            string expected = @"
┌───┐
│   │
└───┘
";
            Assert.True(this.render_compare(expected, actual));

        }


    }


}