namespace mazetextart.art {

    using mazecore.elements;
    using mazecore.direction;

    using System;
    using System.Collections.Generic;

    public abstract class ASCIIRenderer{
        //NOTE, all x and y's are reversed here.  

        abstract public char[][] render_char_array();
        abstract public Position maze_to_render_coords(Position p);

        public string[] render_string_array() {
            char[][] char_map = this.render_char_array();

            //create string representation
            string[] maze_lines = new string[char_map.Length];
            for (int i = 0; i < char_map.Length; i++) {
                maze_lines[i] = new string(char_map[i]);
            }
            return maze_lines;
        }

        public string render_string() {
            return string.Join("\n", this.render_string_array());
        }
    
    
    }
    //inbetween abstract class that takes a maze as part of the constructor.
    public abstract class ASCIIMazeRenderer : ASCIIRenderer{

        protected Maze maze;
        protected IASCIIMazeStyle style;

        public ASCIIMazeRenderer(Maze maze, IASCIIMazeStyle style = null){
            this.maze = maze;
            if (style == null){
                style = new ASCIIMazeGlyphStyle();
            }
            this.style = style;
        }
        public override char[][] render_char_array()
        {
            char[][] char_map = this.do_render_char_array();
            Array.Reverse(char_map);
            return char_map;
        }
        //mazes are build upside down because that makes more sense
        abstract protected char[][] do_render_char_array();
    
    }

    public class ASCIIWallMaze : ASCIIMazeRenderer{

        public ASCIIWallMaze(Maze maze, IASCIIMazeStyle style = null) : base(maze, style) { }

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

        public override Position maze_to_render_coords(Position p) {

            if (!this.maze.in_range(p)) {
                throw new MazeException(String.Format("Position is out of range: ({0}, {1})", p.x, p.y));
            }

            int converted_y  = (p.y * 2) + 1;
            int maze_ceiling = (this.maze.get_y_range() * 2) + 1;

            return new Position((p.x * 2) + 1, maze_ceiling - converted_y);
        }

        protected void process_tile(Tile tile, char[][] char_map) {
            if (tile == null) {
                return;
            }
            Position p = tile.get_position();

            int chr_x_idx = (p.x * 2) + 1;
            int chr_y_idx = (p.y * 2) + 1;

            //character
            char tile_char = this.style.get_tile_char( tile );
            if (tile.is_occupied()) {
                //TODO: tile needs a 'get_character' method
                Character character = tile.get_maze().get_character(tile.get_position());
                tile_char = style.get_character_char(character);  
            }
            char_map[chr_y_idx][chr_x_idx] = tile_char;


            //shouldn't have to worry about out of bounds
            foreach(Direction dir in new Direction[]{Direction.North, Direction.East, Direction.South, Direction.West}){
                if (tile.get_wall(dir) == null) {
                    continue;
                }
                p = new Position(chr_x_idx, chr_y_idx);
                p = p.move(dir, 1);
                char_map[p.y][p.x] = style.get_wall_char(tile, dir);
            }
        }

        protected void process_walls(char[][] char_map) {

            Position p;

            for (int y = 0; y < char_map.Length; y += 2) {
                for (int x = 0; x < char_map[y].Length; x += 2) {
                    WallJoint wall_joint = 0;
                    
                    foreach (Direction dir in new Direction[]{Direction.North, Direction.East, Direction.South, Direction.West}) {
                        p = new Position(x, y);
                        p = p.move(dir, 1);
                        try {
                            if (char_map[p.y][p.x] != style.get_wall_joint_char(0)) {
                                wall_joint = wall_joint | joint_direction_map[dir];
                            }
                        }catch (Exception) { continue; }  
                     }
                    char_map[y][x] = style.get_wall_joint_char((int)wall_joint);
                }
            }
        }



        protected override char[][] do_render_char_array()
        {
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
                    this.process_tile(maze.get_tile(new Position(x, y)), char_map); 
                }
            }
            //process walls
            this.process_walls(char_map);

            return char_map;
        }
    }

    public class ASCIIBlockMaze : ASCIIMazeRenderer
    {

        public ASCIIBlockMaze(Maze maze, IASCIIMazeStyle style = null) : base(maze, style) { }

        public override Position maze_to_render_coords(Position p) {
            if (!this.maze.in_range(p)) {
                throw new MazeException(String.Format("Position is out of range: ({0}, {1})", p.x, p.y));
            }

            return new Position(p.x, this.maze.get_y_range() - p.y);
        }

        protected override char[][] do_render_char_array()
        {

            char[][] char_map = new char[maze.get_y_range()][];
            
            Tile tile;
            char tile_char;
            Character character;

            for (int y = 0; y < maze.get_y_range(); y++) {
                char_map[y] = new char[maze.get_x_range()];
                for (int x = 0; x < maze.get_x_range(); x++) {

                    tile = maze.get_tile(new Position(x, y));
                    tile_char = style.get_tile_char(tile);
                    if (tile.is_occupied()) {
                        character = maze.get_character(tile.get_position());
                        tile_char = style.get_character_char(character); 
                    }
                    char_map[y][x] = tile_char;
                }
            }
            return char_map;
        }
    }

}