using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace windowmaze
{

    using mazecore.elements;
    using mazecore.direction;
    using mazegen;
    using textmaze.art;
    

    public partial class Form1 : Form
    {

        static Character PlaceCharacter(Maze maze){
            Character character = null;
            for (int x = 0; x < maze.get_x_range() && character == null; x++) {
                for (int y = 0; y < maze.get_y_range() && character == null; y++) {
                    try {
                        character = new Character(maze, x, y);
                    }catch(MazeException){
                    }
                 }
            }
            return character;
        }

        /********************************
         * Constructor
         *******************************/

        Maze maze;
        ASCIIRenderer maze_renderer;
        Character character;

        public Form1()
        {
            InitializeComponent();
            this.maze = null;
            this.character = null;
            this.maze_renderer = null;

        }

        /*******************************
         * Menus
         *******************************/
        private void menu_application_quit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menu_new_block_maze(object sender, EventArgs e)
        {
            CellulartMazeGenerator cmg = new CellulartMazeGenerator();
            Maze maze = cmg.generate(40, 30);
            IASCIIMazeStyle style = new ASCIIMazeGlyphStyle();
            ASCIIBlockMaze maze_renderer = new ASCIIBlockMaze(maze, style);

            this.initialize_maze(maze, maze_renderer);

        }

        private void menu_new_wall_maze(object sender, EventArgs e)
        {
            DepthFirstMazeGenerator dfmg = new DepthFirstMazeGenerator();
            Maze maze = dfmg.generate(20, 15);
            IASCIIMazeStyle style = new ASCIIMazeGlyphStyle();
            ASCIIWallMaze maze_renderer = new ASCIIWallMaze(maze, style);

            this.initialize_maze(maze, maze_renderer);
        }

        private void menu_change_font_size(object sender, EventArgs e)
        {

            String item = (string)this.mnuMain_Style_Size_Value.Items[this.mnuMain_Style_Size_Value.SelectedIndex];
            int new_size = int.Parse( item );
            this.set_font_size( new_size );
        }


        /********************************
         * maze functions
         ********************************/

        private void initialize_maze(Maze maze, ASCIIRenderer maze_renderer){
            this.maze = maze;
            this.maze_renderer = maze_renderer;
            this.character = PlaceCharacter(this.maze);

            this.draw_maze();
            this.maze_resized();
            

        }

        private void draw_maze(){
            if(maze == null){return;}

            this.maze_label.Text = this.maze_renderer.render_string();
        }


         /*******************************
         * Control Methods
         *******************************/
        private void handle_user_input(object sender, KeyPressEventArgs e)
        {

            char input = char.ToLower(e.KeyChar);
            Nullable<Direction> direction = null;

            if (input.Equals('w')){
                direction = Direction.North;
            }
            else if (input.Equals('a')){
                direction = Direction.West;
            }
            else if (input.Equals('s')){
                direction = Direction.South;
            }
            else if (input.Equals('d')){
                direction = Direction.East;
            }

            if (direction == null){
                return;
            }

            character.move(direction.Value);
            character.set_orientation(direction.Value);
        }

        private void update_timer_Tick(object sender, EventArgs e)
        {
            this.draw_maze();
        }
        

        /******************************
         * Form Functions
         *****************************/
        private void maze_resized(object sender=null, EventArgs e=null)
        {
            double height_padding_offset = 1.10;
            double width_padding_offset = 1.05;


            this.Size = new Size((int)(this.maze_label.Size.Width * width_padding_offset),
                                 (int)(this.maze_label.Size.Height * height_padding_offset) + this.mnuMain.Size.Height);
        }

        private void set_font_size(int new_size)
        {
            this.maze_label.Font = new Font(this.maze_label.Font.FontFamily, new_size);
        }

    }
}
