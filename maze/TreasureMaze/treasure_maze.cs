namespace treasuremaze.core {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using mazecore.elements;
    using mazecore.direction;
    using mazegen;
    using mazetextart.art;

    /////////////////////////////////
    //Game Mechanics 
    /////////////////////////////////


    class TreasureTileStyle : IASCIIMazeStyle {

        private ASCIIMazeGlyphStyle default_style;
        private TreasureMaze treasure_maze;


        public TreasureTileStyle(TreasureMaze treasure_maze) {
            this.default_style = new ASCIIMazeGlyphStyle();
            this.default_style.set_tile_char(typeof(TreasureTile), ' ');


            this.treasure_maze = treasure_maze;

            }


        public char get_tile_char(Tile tile) {

            char letter = this.default_style.get_tile_char(tile);               
            if(this.treasure_maze.has_treasure(tile.get_position())){
                letter = '#';
            }

            return letter;
            
        }
        public char get_character_char(Character character) {
            return this.default_style.get_character_char(character);
        }
        public char get_wall_char(Tile tile, Direction direction) {
            return this.default_style.get_wall_char(tile, direction);
        }
        public char get_wall_joint_char(int joint_id) {
            return this.default_style.get_wall_joint_char(joint_id);
        }

    }

    public class TreasureTile : Tile {

        private bool treasure;

        public TreasureTile(Maze maze, Position position)
            : base(maze, position) {
                this.treasure = true;
        }

        public bool has_treasure() {
            return this.treasure;
        }

        public override void action_step_on(MovementEvent e) {
            this.treasure = false;
        }
    }
    public class TreasureMaze {

        private HashSet<TreasureTile> tiles;
        public Maze maze;

        public TreasureMaze(Maze maze){
                this.tiles = new HashSet<TreasureTile>();
                this.maze = maze;
        }

        public void set_treasure(Position p) {
            this.maze.remove_tile(p);
            this.tiles.Add(new TreasureTile(maze, p));
        }

        public bool has_treasure(Position p) {

            bool result = false;
            foreach (TreasureTile t in this.tiles) {
                if (t.get_position().Equals(p) && t.has_treasure()) {
                    result = true;
                }
            }
            return result;
        }

        public int get_treasures_remaining() {
            int treasure_count = 0;
            foreach (TreasureTile t in this.tiles) {
                if (t.has_treasure()) {
                    treasure_count += 1;
                }
            }
            return treasure_count;
        }

    }

    /////////////////////////////////
    //Factory
    /////////////////////////////////
    public class TreasureMazeStruct {

        public readonly TreasureMaze maze;
        public readonly ASCIIRenderer final_renderer;
        public readonly Character character;

        public TreasureMazeStruct(TreasureMaze maze, ASCIIRenderer renderer, Character character) {
            this.maze = maze;
            this.final_renderer = renderer;
            this.character = character;
        }
    }

    public class TreasureMazeFactory {


        private static TreasureMaze splash_maze;
        static TreasureMazeFactory() {

            Maze maze = new Maze(35, 15);
            Block b;
            Tile t;

            for (int x = 0; x < maze.get_x_range(); x++) {
                for (int y = 0; y < maze.get_y_range(); y++) {
                    //b = new Block(maze, new Position(x, y));
                    t = new Tile(maze, new Position(x, y));
                }
            }

            

            TreasureMaze tm = new TreasureMaze(maze);
            TreasureMazeFactory.splash_maze = tm;

            tm.set_treasure(new Position(1, 14));
            tm.set_treasure(new Position(1, 13));
            tm.set_treasure(new Position(1, 12));
            tm.set_treasure(new Position(1, 11));
            tm.set_treasure(new Position(1, 10));
        
        
        }



        public enum Difficulty { Easy, Medium, Hard };
        public enum MazeType { Wall, Block, Splash };

        public TreasureMazeFactory() {

        }
        //place the character starting at the bottom left corner.
        private Character place_character(Maze maze) {
            Character character = null;
            for (int x = 0; x < maze.get_x_range() && character == null; x++) {
                for (int y = 0; y < maze.get_y_range() && character == null; y++) {
                    try {
                        character = new Character(maze, new Position(x, y));
                    }
                    catch (MazeException) {
                    }
                }
            }
            return character;
        }

        private void place_tiles(TreasureMaze treasure_maze, int num_treasures) {

            Tile temp_tile;
            Position p;

            Random rng = new Random();
            int x_range = treasure_maze.maze.get_x_range(),
                y_range = treasure_maze.maze.get_y_range();

            while (treasure_maze.get_treasures_remaining() != num_treasures) {
                p = new Position(rng.Next(1, x_range-1),
                                 rng.Next(1, y_range-1));
                temp_tile = treasure_maze.maze.get_tile(p);
                if (temp_tile.can_stand()) {
                    treasure_maze.set_treasure(p);
                }

            }
        }


        public TreasureMazeStruct create(int num_treasures, Difficulty difficulty, MazeType maze_type) {


            Maze maze;
            int x_range, y_range, camera_range;
            ///////////////////////
            //Set the difficulty
            ///////////////////////
            if(difficulty == Difficulty.Easy){
                x_range = 10;
                y_range = 10;
                camera_range = -1;
            }
            else if (difficulty == Difficulty.Medium) {
                x_range = 15;
                y_range = 15;
                camera_range = 5;
            }
            else {
                x_range = 20;
                y_range = 20;
                camera_range = 4;
            }

            

            ///////////////////////////////
            //Generate maze and renderers
            ///////////////////////////////
            ASCIIMazeRenderer renderer;
            TreasureMaze treasure_maze;

            if (maze_type == MazeType.Block) {
                maze = new CellularMazeGenerator().generate(x_range*2, y_range*2);
                renderer = new ASCIIBlockMaze(maze);
                treasure_maze = new TreasureMaze(maze);
                this.place_tiles(treasure_maze, num_treasures);
            }
            else if(maze_type == MazeType.Wall){
                maze = new DepthFirstMazeGenerator().generate(x_range, y_range);
                renderer = new ASCIIWallMaze(maze);
                treasure_maze = new TreasureMaze(maze);
                this.place_tiles(treasure_maze, num_treasures);
            }else{
                treasure_maze = TreasureMazeFactory.splash_maze;
                maze = treasure_maze.maze;
                renderer = new ASCIIBlockMaze(maze); 
            }
            ////////////////////////////////
            //Maze Placement
            ////////////////////////////////
            Character character = this.place_character(maze);
            
            
            TreasureTileStyle tts = new TreasureTileStyle(treasure_maze);
            renderer.set_style(tts); 

            Func<ASCIIRendererCamera, Position> center = delegate(ASCIIRendererCamera camera){
                return renderer.maze_to_render_coords( character.get_position());
            };


            ASCIIRendererCamera camera_renderer = new ASCIIRendererCamera(renderer, center);
            camera_renderer.set_range(camera_range, camera_range);
            return new TreasureMazeStruct(treasure_maze, camera_renderer, character);

        }
    }
}