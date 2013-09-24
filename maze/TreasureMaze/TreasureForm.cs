using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using treasuremaze.core;
using mazecore.direction;

namespace treasuremaze.window {
    public partial class TreasureForm : Form {

        TreasureMazeFactory factory;
        TreasureMazeStruct treasure_maze;
        TreasureMazeFactory.MazeType maze_type = TreasureMazeFactory.MazeType.Wall;

        public TreasureForm() {
            InitializeComponent();
            factory = new TreasureMazeFactory();
        }
        

        ///////////////////////////////
        //Difficulty
        //////////////////////////////

        private void easyToolStripMenuItem_Click(object sender, EventArgs e) {
            this.treasure_maze = factory.create(1, TreasureMazeFactory.Difficulty.Easy, this.maze_type);

            this.draw_maze();
            this.resize();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e) {
            this.treasure_maze = factory.create(1, TreasureMazeFactory.Difficulty.Medium, this.maze_type);

            this.draw_maze();
            this.resize();
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e) {
            this.treasure_maze = factory.create(1, TreasureMazeFactory.Difficulty.Hard, this.maze_type);

            this.draw_maze();
            this.resize();
        }

        ///////////////////////////////
        //Maze Type
        //////////////////////////////
        private void wallMazeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.maze_type = TreasureMazeFactory.MazeType.Wall;

            this.wallMazeToolStripMenuItem.Checked = true;
            this.blockMazeToolStripMenuItem.Checked = false;
        }
        private void blockMazeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.maze_type = TreasureMazeFactory.MazeType.Block;

            this.wallMazeToolStripMenuItem.Checked = false;
            this.blockMazeToolStripMenuItem.Checked = true;
        }

        ///////////////////////////////
        //Maze Utilities
        //////////////////////////////
        private void draw_maze() {
            if (this.treasure_maze == null) { 
                return; 
            }

            this.maze_label.Text = this.treasure_maze.final_renderer.render_string();
        }


        /////////////////////////////////
        //Maze/Application Interaction
        /////////////////////////////////
        private void handle_input(object sender, KeyPressEventArgs e) {
            char input = char.ToLower(e.KeyChar);
            Nullable<Direction> direction = null;

            if (input.Equals('w')) {
                direction = Direction.North;
            }
            else if (input.Equals('a')) {
                direction = Direction.West;
            }
            else if (input.Equals('s')) {
                direction = Direction.South;
            }
            else if (input.Equals('d')) {
                direction = Direction.East;
            }

            if (direction == null) {
                return;
            }

            this.treasure_maze.character.move(direction.Value);
            this.treasure_maze.character.set_orientation(direction.Value);
        }

        private void update(object sender, EventArgs e) {
            this.draw_maze();
        }

        private void resize(object sender=null, EventArgs e=null) {
            double height_padding_gain = 1.0;
            double width_padding_gain = 1.0;



            int height_padding_offset = 50 + (int)this.maze_label.Font.Size,
                width_padding_offset = 20;



            this.Size = new Size((int)(this.maze_label.Size.Width * width_padding_gain) + width_padding_offset,
                                 (int)(this.maze_label.Size.Height * height_padding_gain) + height_padding_offset);
        }

        ////////////////////////////
        //Application
        ////////////////////////////
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }


    }
}
