namespace consolemaze.art {

    using mazecore.elements;
    using mazecore.direction;
    using mazecore.storage;

    using System;

    /* ASCII Characters and thier codes:
    
     * Characters
     * ►, 16
     * ◄, 17
     * ▲, 30
     * ▼, 31
     
     * Wall Joints
     *  , 32 (technically)
     * ┼, 197
     * ┘, 217
     * └, 192
     * ┌, 218
     * ┐, 191
     * ┴, 193
     * ┬, 194
     * ├, 195
     * ┤, 180
     
     * Walls
     * ─, 196
     * │, 179
       
     */


    class CharacterMaze {

        static protected Glyph<Direction> character_glyph;
        static protected Glyph<Direction> wall_glyph;
        static protected Glyph<int> wall_joint;

        static CharacterMaze() {
            character_glyph = new Glyph<Direction>('?');
            character_glyph.add_character(Direction.North, '▲');
            character_glyph.add_character(Direction.South, '▼');
            character_glyph.add_character(Direction.East,  '►');
            character_glyph.add_character(Direction.West,  '◄');

            wall_glyph = new Glyph<Direction>('?');
            wall_glyph.add_character(Direction.North, '─');
            wall_glyph.add_character(Direction.South, '─');
            wall_glyph.add_character(Direction.East, '│');
            wall_glyph.add_character(Direction.West, '│');

            wall_joint = new Glyph<int>('?');
            //                        0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15
            char[] wall_joint_chr = {' ', '─', '│', '┐', '─', '─', '┌', '┬', '│', '┘', '│', '┤', '└', '┴', '├', '┼'};
            for(int i=0;i<wall_joint_chr.Length;i++){
                wall_joint.add_character(i, wall_joint_chr[i]);
            }
            

        }

        protected Maze maze;
        public CharacterMaze(Maze maze) {
            this.maze = maze;
        }


        protected void process_tile(Tile tile, string[,] char_map) {
            if (tile == null) {
                return;
            }

            int chr_x_idx = tile.get_x() * 2 + 1;
            int chr_y_idx = tile.get_y() * 2 + 1;

            

        }

        public string render() {
            string[,] char_map = new string[ (this.maze.get_x_range()*2) + 1, (this.maze.get_y_range()*2) + 1];
            


            //process tiles
            for (int x = 0; x < maze.get_x_range(); x++) {
                for (int y = 0; y < maze.get_y_range(); y++) {
                    this.process_tile(maze.get_tile(x, y), char_map); 
                }
            }
            return "";
        }

        

    }
}