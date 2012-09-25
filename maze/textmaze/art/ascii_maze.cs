namespace textmaze.art {

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


    public class ASCIIMazeStyle {

        //These are all class defaults.  They can be overridden with the style class. 
        //NOTE, all x and y's are reversed here.  
        static protected Glyph<Direction> default_character_glyph;
        static protected Glyph<Direction> default_wall_glyph;

        static protected Glyph<Type> default_tile_glyph;

        static protected Glyph<int> default_wall_joint_glyph;
        // I don't want to allow the real direction to be combined so I'm using a static mapping


        static ASCIIMazeStyle() {
            default_character_glyph = new Glyph<Direction>('?');
            default_character_glyph.add_character(Direction.North, '▲');
            default_character_glyph.add_character(Direction.South, '▼');
            default_character_glyph.add_character(Direction.East,  '►');
            default_character_glyph.add_character(Direction.West,  '◄');

            default_wall_glyph = new Glyph<Direction>('?');
            default_wall_glyph.add_character(Direction.North, '─');
            default_wall_glyph.add_character(Direction.South, '─');
            default_wall_glyph.add_character(Direction.East, '│');
            default_wall_glyph.add_character(Direction.West, '│');

            default_tile_glyph = new Glyph<Type>('?');
            default_tile_glyph.add_character(typeof(Tile), ' ');
            default_tile_glyph.add_character(typeof(Block), '█');


            default_wall_joint_glyph = new Glyph<int>('?');
            //                        0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15
            char[] wall_joint_chr = {' ', '─', '│', '┐', '─', '─', '┌', '┬', '│', '┘', '│', '┤', '└', '┴', '├', '┼'};
            for(int i=0;i<wall_joint_chr.Length;i++){
               default_wall_joint_glyph.add_character(i, wall_joint_chr[i]);
            }
        }

        public Glyph<Direction> character_glyph;
        public Glyph<Direction> wall_glyph;
        public Glyph<int> wall_joint_glyph;
        public Glyph<Type> tile_glyph;

        public ASCIIMazeStyle():this(default_character_glyph, default_wall_glyph, default_wall_joint_glyph, default_tile_glyph) {}

        public ASCIIMazeStyle(Glyph<Direction> character_glyph = null, Glyph<Direction> wall_glyph = null, 
                              Glyph<int> wall_joint_glyph = null, Glyph<Type> tile_glyph=null) {
            this.character_glyph  =  character_glyph  == null ? default_character_glyph  : character_glyph;
            this.wall_glyph       =  wall_glyph       == null ? default_wall_glyph       : wall_glyph;
            this.wall_joint_glyph = wall_joint_glyph  == null ? default_wall_joint_glyph : wall_joint_glyph;
            this.tile_glyph       = tile_glyph        == null ? default_tile_glyph       : tile_glyph;

        }

    }

    public abstract class ASCIIRenderer{

        protected Maze maze;
        protected ASCIIMazeStyle style;

        public ASCIIRenderer(Maze maze, ASCIIMazeStyle style = null) {
            this.maze = maze;

            if (style == null) {
                style = new ASCIIMazeStyle();
            }

            this.style = style;

        }

        abstract public char[][] render_char_array();

        public string[] render_string_array() {
            char[][] char_map = this.render_char_array();

            //create string representation
            string[] maze_lines = new string[char_map.Length];
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

    public class ASCIIWallMaze : ASCIIRenderer {

        public ASCIIWallMaze(Maze maze, ASCIIMazeStyle style = null) : base(maze, style) { }

        [Flags]
        enum WallJoint { North = 8, East = 4, South = 2, West = 1 };
        static Dictionary<Direction, WallJoint> joint_direction_map;

        static ASCIIWallMaze() {
            joint_direction_map = new Dictionary<Direction, WallJoint>();
            joint_direction_map[Direction.North] = WallJoint.North;
            joint_direction_map[Direction.South] = WallJoint.South;
            joint_direction_map[Direction.East] = WallJoint.East;
            joint_direction_map[Direction.West] = WallJoint.West;
        }

        protected void process_tile(Tile tile, char[][] char_map) {
            if (tile == null) {
                return;
            }
            
            int chr_x_idx = (tile.get_x() * 2) + 1;
            int chr_y_idx = (tile.get_y() * 2) + 1;

            //character
            char tile_char = this.style.tile_glyph.get_character( tile.GetType() );
            if (tile.is_occupied()) {
                //TODO: tile needs a 'get_character' method
                Character character = tile.get_maze().get_character(tile.get_x(), tile.get_y());
                tile_char = style.character_glyph.get_character(character.get_orientation());  
            }
            char_map[chr_y_idx][chr_x_idx] = tile_char;


            //shouldn't have to worry about out of bounds
            int tmp_x, tmp_y;
            foreach(Direction dir in new Direction[]{Direction.North, Direction.East, Direction.South, Direction.West}){
                if (tile.get_wall(dir) == null) {
                    continue;
                }
                
                tmp_x = chr_x_idx; tmp_y = chr_y_idx;
                DirectionControl.move(ref tmp_x, ref tmp_y, dir, 1);
                char_map[tmp_y][tmp_x] = style.wall_glyph.get_character(dir);
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
                            if (char_map[tmp_y][tmp_x] != style.wall_joint_glyph.get_character(0)) {
                                wall_joint = wall_joint | joint_direction_map[dir];
                            }
                        }catch (Exception) { continue; }  
                     }
                    char_map[y][x] = style.wall_joint_glyph.get_character((int)wall_joint);
                }
            }
        }



        public override char[][] render_char_array() {
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
    }

    public class ASCIIBlockMaze : ASCIIRenderer {

        public ASCIIBlockMaze(Maze maze, ASCIIMazeStyle style = null) : base(maze, style) { }

        public override char[][] render_char_array() {

            char[][] char_map = new char[maze.get_y_range()][];
            
            Tile tile;
            char tile_char;
            Character character;

            for (int y = 0; y < maze.get_y_range(); y++) {
                char_map[y] = new char[maze.get_x_range()];
                for (int x = 0; x < maze.get_x_range(); x++) {

                    tile = maze.get_tile(x, y);
                    tile_char = style.tile_glyph.get_character(tile.GetType());
                    if (tile.is_occupied()) {
                        character = maze.get_character(tile.get_x(), tile.get_y());
                        tile_char = style.character_glyph.get_character(character.get_orientation()); 
                    }
                    char_map[y][x] = tile_char;
                }
            }
            return char_map;
        }
    }

}