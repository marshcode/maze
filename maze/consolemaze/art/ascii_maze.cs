namespace consolemaze.art {

    using mazecore.elements;
    using mazecore.direction;

    using System;
    using System.Collections.Generic;

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


    class ASCIIMaze {
        //NOTE, all x and y's are reversed here.  
        static protected Glyph<Direction> character_glyph;
        static protected Glyph<Direction> wall_glyph;
        
        static protected Glyph<int> wall_joint_glyph;
        // I don't want to allow the real direction to be combined so I'm using a static mapping
        [Flags]
        enum WallJoint { North = 8, East = 4, South = 2, West = 1 };
        static Dictionary<Direction, WallJoint> joint_direction_map;


        static ASCIIMaze() {
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

            wall_joint_glyph = new Glyph<int>('?');
            //                        0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15
            char[] wall_joint_chr = {' ', '─', '│', '┐', '─', '─', '┌', '┬', '│', '┘', '│', '┤', '└', '┴', '├', '┼'};
            for(int i=0;i<wall_joint_chr.Length;i++){
                wall_joint_glyph.add_character(i, wall_joint_chr[i]);
            }
            joint_direction_map = new Dictionary<Direction, WallJoint>();
            joint_direction_map[Direction.North] = WallJoint.North;
            joint_direction_map[Direction.South] = WallJoint.South;
            joint_direction_map[Direction.East] = WallJoint.East;
            joint_direction_map[Direction.West] = WallJoint.West;

        }

        protected Maze maze;
        public ASCIIMaze(Maze maze) {
            this.maze = maze;
        }


        protected void process_tile(Tile tile, char[][] char_map) {
            if (tile == null) {
                return;
            }
            
            int chr_x_idx = (tile.get_x() * 2) + 1;
            int chr_y_idx = (tile.get_y() * 2) + 1;

            //character
            if (tile.is_occupied()) {
                //TODO: tile needs a 'get_character' method
                Character character = tile.get_maze().get_character(tile.get_x(), tile.get_y());
                char_map[chr_y_idx][chr_x_idx] = character_glyph.get_character(character.get_orientation());
            }

            //shouldn't have to worry about out of bounds
            int tmp_x, tmp_y;
            foreach(Direction dir in new Direction[]{Direction.North, Direction.East, Direction.South, Direction.West}){
                if (tile.get_wall(dir) == null) {
                    continue;
                }
                
                tmp_x = chr_x_idx; tmp_y = chr_y_idx;
                DirectionControl.move(ref tmp_x, ref tmp_y, dir, 1);
                char_map[tmp_y][tmp_x] = wall_glyph.get_character(dir);
            }
        }

        protected void process_walls(char[][] char_map) {

            int tmp_x, tmp_y;

            for (int y = 0; y < char_map.Length; y += 2) {
                for (int x = 0; x < char_map[y].Length; x += 2) {
                    WallJoint wall_joint = 0;
                    foreach (Direction dir in new Direction[]{Direction.North, Direction.East, Direction.South, Direction.West}) {
                        tmp_x = x; tmp_y = y;
                        DirectionControl.move(ref tmp_x, ref tmp_y, dir, 1);
                        try {
                            if (char_map[tmp_y][tmp_x] != ' ') {
                                wall_joint = wall_joint | joint_direction_map[dir];
                            }
                        }catch (Exception) { continue; }  
                     }
                    char_map[y][x] = wall_joint_glyph.get_character((int)wall_joint);
                }
            }
        }



        public char[][] render_char_array() {
            int char_x_range = (this.maze.get_x_range()*2) + 1;
            int char_y_range = (this.maze.get_y_range()*2) + 1;
            //initialize
            char[][] char_map = new char[ char_y_range ][];
            for (int i = 0; i < char_map.Length; i++) {
                char_map[i] = new char[char_x_range];
                for (int j = 0; j < char_map[i].Length;j++ ) {
                    char_map[i][j] = ' ';//why does this NEED to happen?
                }
            }

            //process tiles
            for (int x = 0; x < maze.get_x_range(); x++) {
                for (int y = 0; y < maze.get_y_range(); y++) {
                    this.process_tile(maze.get_tile(x, y), char_map); 
                }
            }
            //process walls
            this.process_walls(char_map);

            return char_map;
        }

        public string[] render_string_array(){
            char[][] char_map = this.render_char_array();

            //create string representation
            string[] maze_lines = new string[ char_map.Length ];
            for (int i = 0; i < char_map.Length; i++) {
                maze_lines[i] = new string(char_map[i]);
            }
            Array.Reverse(maze_lines);
            return maze_lines;
        }

        public string render_string() {
            return string.Join("\n", this.render_string_array());
        }

    }
}