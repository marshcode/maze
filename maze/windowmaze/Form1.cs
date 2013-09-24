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
    using mazetextart.art;
    

    public partial class Form1 : Form
    {

        static Character PlaceCharacter(Maze maze){
            Character character = null;
            for (int x = 0; x < maze.get_x_range() && character == null; x++) {
                for (int y = 0; y < maze.get_y_range() && character == null; y++) {
                    try {
                        character = new Character(maze, new Position(x, y));
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
        ASCIIRenderer final_renderer;
        ASCIIRendererCamera camera;
        ASCIIRenderer first_renderer;

        Character character;

        public Form1()
        {
            InitializeComponent();
            this.maze         = null;
            this.character    = null;
            this.final_renderer = null;
            this.first_renderer = null;

            this.menu_new_wall_maze(null, null);
            this.set_font_size(10); //any smaller and it doesn't look right.
        }

        /*******************************
         * Menus
         *******************************/
        private void menu_application_quit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //the front end should have a few utility methods that encapsulate this.
        private void menu_new_block_maze(object sender, EventArgs e)
        {
            CellularMazeGenerator cmg = new CellularMazeGenerator();
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

            String item = (string)this.mnuMain_View_Size_Value.Items[this.mnuMain_View_Size_Value.SelectedIndex];
            int new_size = int.Parse( item );
            this.set_font_size( new_size );
        }

        private void menu_change_camera_size(object sender, EventArgs e) {
            String item = (string)this.mnuMain_View_Camera_Value.Items[ this.mnuMain_View_Camera_Value.SelectedIndex ];

            //TODO: allow the camera to be reset by negative numbers
            if(item.ToLower().Equals("reset")){
                this.camera.reset_range();
            }else{
                this.set_camera_view( int.Parse(item));
            }
        }


        /********************************
         * maze functions
         ********************************/
        //this should also be put into some kind of controller class.  It still belongs in Window maze
        //because this is the top level controller
        public Position get_char_position(ASCIIRendererCamera camera) {

            return first_renderer.maze_to_render_coords( this.character.get_position());
        }
        private void initialize_maze(Maze maze, ASCIIRenderer maze_renderer){
            this.maze = maze;

            Action<MovementEvent> f = delegate(MovementEvent evt) {
                MessageBox.Show(String.Format("Previous: {0}, {1}\nCurrent: {2}, {3}", evt.moved_from.x, evt.moved_from.y,
                                                                                       evt.moved_to.x, evt.moved_to.y));


            };

            //this.maze.get_movement_event(new Position(0, 0)).register(f);
            //this.maze.get_movement_event(new Position(0, 1)).register(f);
            //this.maze.get_movement_event(new Position(1, 0)).register(f);

            this.character = PlaceCharacter(this.maze);

            ASCIIRendererCamera camera = new ASCIIRendererCamera(maze_renderer, this.get_char_position);
            this.first_renderer = maze_renderer;
            this.camera = camera;
            this.final_renderer = camera;

            this.draw_maze();
            this.maze_resized();
            

        }

        private void draw_maze(){
            if(maze == null){return;}

            this.maze_label.Text = this.final_renderer.render_string();
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
            double height_padding_gain = 1.0;
            double width_padding_gain = 1.0;

            int height_padding_offset = 50 + this.mnuMain.Size.Height,
                width_padding_offset = 20;



            this.Size = new Size((int)(this.maze_label.Size.Width * width_padding_gain) + width_padding_offset,
                                 (int)(this.maze_label.Size.Height * height_padding_gain) + height_padding_offset);
        }

        private void set_font_size(int new_size)
        {
            this.maze_label.Font = new Font(this.maze_label.Font.FontFamily, new_size);
        }

        private void set_camera_view(int new_view) {
            this.camera.set_range(new_view, new_view);
            this.draw_maze();
            this.maze_resized();
        }

    }
}
