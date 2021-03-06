﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using treasuremaze.core;
using mazecore.direction;

using mazecore.elements;

namespace treasuremaze.window {
    public partial class TreasureForm : Form {

        TreasureMazeFactory factory;
        TreasureMazeStruct treasure_maze;
        TreasureMazeFactory.MazeType maze_type = TreasureMazeFactory.MazeType.Wall;
        int seconds_remaining = 0;
        bool game_timer_enabled = true;

        public TreasureForm() {
            InitializeComponent();
            factory = new TreasureMazeFactory();
            this.set_size(14);
            this.treasure_maze = factory.create(0, TreasureMazeFactory.Difficulty.Easy, TreasureMazeFactory.MazeType.Splash);
            this.reset(0);
            this.set_game_timer_enabled(false);
        }
        

        ///////////////////////////////
        //Difficulty
        //////////////////////////////

        private void easyToolStripMenuItem_Click(object sender, EventArgs e) {
            this.treasure_maze = factory.create(3, TreasureMazeFactory.Difficulty.Easy, this.maze_type);
            this.reset(60);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e) {
            this.treasure_maze = factory.create(5, TreasureMazeFactory.Difficulty.Medium, this.maze_type);
            this.reset(60*3);
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e) {
            this.treasure_maze = factory.create(7, TreasureMazeFactory.Difficulty.Hard, this.maze_type);
            this.reset(60 * 5);
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

        private void stop() {
            this.sim_timer.Stop();
            this.game_timer.Stop();
        }
        private void reset(int seconds_remaining) {

            this.draw_maze();
            this.resize();

            this.seconds_remaining = seconds_remaining;
            this.sim_timer.Start();
            this.game_timer.Start();
            this.set_game_timer_enabled(true);
        }


        private void set_game_timer_enabled(bool status) {
            this.game_timer_enabled = status;
        }
        private bool get_game_timer_status() {
            return this.game_timer_enabled;
        }

        /////////////////////////////////
        //Maze/Application Interaction
        /////////////////////////////////
        private void handle_input(object sender, KeyPressEventArgs e) {
            
            if(this.treasure_maze == null){
                return;
             }
            
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

        private void update(object sender=null, EventArgs e=null) {
            this.draw_maze();

            if (this.treasure_maze == null) {
                return;
            }
            int remaining_treasures = this.treasure_maze.maze.get_treasures_remaining();

            //remaining treasures
            this.statusStrip1.Items[0].Text = String.Format("Remaining: {0}", remaining_treasures);
            
            if(!this.get_game_timer_status()){
                return;
            }
            //remaining time
            int minutes = this.seconds_remaining / 60;
            int seconds = this.seconds_remaining - (minutes * 60);
            this.statusStrip1.Items[1].Text = String.Format("{0}:{1}", minutes.ToString("0"), seconds.ToString("00"));

            if (remaining_treasures == 0 ) {
                this.stop();
                MessageBox.Show("Try a harder setting", "You Win!");
            }
            
        }

        private void resize(object sender=null, EventArgs e=null) {
            double height_padding_gain = 1.0;
            double width_padding_gain = 1.0;



            int height_padding_offset = 50 + (int)this.maze_label.Font.Size + this.statusStrip1.Size.Height,
                width_padding_offset = 20;


            int min_width = 0;
            foreach (ToolStripItem tsi in this.statusStrip1.Items) {
                min_width += tsi.Size.Width;

            }
            
            int height = (int)(this.maze_label.Size.Height * height_padding_gain) + height_padding_offset;
            int width = (int)(this.maze_label.Size.Width * width_padding_gain) + width_padding_offset;


            this.Size = new Size(width < min_width ? min_width: width,
                                 height);
        }


        private void game_timer_Tick(object sender, EventArgs e) {
            this.seconds_remaining -= 1;

            if (this.seconds_remaining == 0) {
                this.update();
                this.stop();
                MessageBox.Show("Try Again", "You Lose!");
            }
        }


        ////////////////////////////
        //OPTIONS
        ////////////////////////////

        private void set_size(int size){
            this.maze_label.Font = new Font(this.maze_label.Font.FontFamily, size);
            this.resize();
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e) {
            this.smallToolStripMenuItem.Checked = true;
            this.mediumToolStripMenuItem1.Checked = false;
            this.largeToolStripMenuItem.Checked = false;
            this.set_size(12);
        }

        private void mediumToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.smallToolStripMenuItem.Checked = false;
            this.mediumToolStripMenuItem1.Checked = true;
            this.largeToolStripMenuItem.Checked = false;
            this.set_size(16);
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.smallToolStripMenuItem.Checked = false;
            this.mediumToolStripMenuItem1.Checked = false;
            this.largeToolStripMenuItem.Checked = true;
            this.set_size(23);
        }
        ////////////////////////////
        //Application
        ////////////////////////////
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }



    }
}
