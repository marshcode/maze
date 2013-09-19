namespace windowmaze
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.maze_label = new System.Windows.Forms.Label();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuMain_File = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_New_Block = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_New_Wall = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_File_Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_View = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_View_Size = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_View_Size_Value = new System.Windows.Forms.ToolStripComboBox();
            this.mnuMain_View_Camera = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain_View_Camera_Value = new System.Windows.Forms.ToolStripComboBox();
            this.update_timer = new System.Windows.Forms.Timer(this.components);
            this.mnuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // maze_label
            // 
            this.maze_label.AutoSize = true;
            this.maze_label.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.maze_label.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maze_label.Location = new System.Drawing.Point(3, 30);
            this.maze_label.Margin = new System.Windows.Forms.Padding(0);
            this.maze_label.Name = "maze_label";
            this.maze_label.Size = new System.Drawing.Size(0, 13);
            this.maze_label.TabIndex = 0;
            this.maze_label.Resize += new System.EventHandler(this.maze_resized);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File,
            this.mnuMain_View});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(309, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuMain_File
            // 
            this.mnuMain_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File_New,
            this.mnuMain_File_Quit});
            this.mnuMain_File.Name = "mnuMain_File";
            this.mnuMain_File.Size = new System.Drawing.Size(37, 20);
            this.mnuMain_File.Text = "&File";
            // 
            // mnuMain_File_New
            // 
            this.mnuMain_File_New.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File_New_Block,
            this.mnuMain_File_New_Wall});
            this.mnuMain_File_New.Name = "mnuMain_File_New";
            this.mnuMain_File_New.Size = new System.Drawing.Size(98, 22);
            this.mnuMain_File_New.Text = "&New";
            // 
            // mnuMain_File_New_Block
            // 
            this.mnuMain_File_New_Block.Name = "mnuMain_File_New_Block";
            this.mnuMain_File_New_Block.Size = new System.Drawing.Size(134, 22);
            this.mnuMain_File_New_Block.Text = "Block Maze";
            this.mnuMain_File_New_Block.Click += new System.EventHandler(this.menu_new_block_maze);
            // 
            // mnuMain_File_New_Wall
            // 
            this.mnuMain_File_New_Wall.Name = "mnuMain_File_New_Wall";
            this.mnuMain_File_New_Wall.Size = new System.Drawing.Size(134, 22);
            this.mnuMain_File_New_Wall.Text = "Wall Maze";
            this.mnuMain_File_New_Wall.Click += new System.EventHandler(this.menu_new_wall_maze);
            // 
            // mnuMain_File_Quit
            // 
            this.mnuMain_File_Quit.Name = "mnuMain_File_Quit";
            this.mnuMain_File_Quit.Size = new System.Drawing.Size(98, 22);
            this.mnuMain_File_Quit.Text = "&Quit";
            this.mnuMain_File_Quit.Click += new System.EventHandler(this.menu_application_quit);
            // 
            // mnuMain_View
            // 
            this.mnuMain_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_View_Size,
            this.mnuMain_View_Camera});
            this.mnuMain_View.Name = "mnuMain_View";
            this.mnuMain_View.Size = new System.Drawing.Size(44, 20);
            this.mnuMain_View.Text = "&View";
            // 
            // mnuMain_View_Size
            // 
            this.mnuMain_View_Size.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_View_Size_Value});
            this.mnuMain_View_Size.Name = "mnuMain_View_Size";
            this.mnuMain_View_Size.Size = new System.Drawing.Size(119, 22);
            this.mnuMain_View_Size.Text = "Text Size";
            // 
            // mnuMain_View_Size_Value
            // 
            this.mnuMain_View_Size_Value.AutoCompleteCustomSource.AddRange(new string[] {
            "8",
            "10",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30"});
            this.mnuMain_View_Size_Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mnuMain_View_Size_Value.Items.AddRange(new object[] {
            "8",
            "10",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30"});
            this.mnuMain_View_Size_Value.Name = "mnuMain_View_Size_Value";
            this.mnuMain_View_Size_Value.Size = new System.Drawing.Size(152, 23);
            this.mnuMain_View_Size_Value.SelectedIndexChanged += new System.EventHandler(this.menu_change_font_size);
            // 
            // mnuMain_View_Camera
            // 
            this.mnuMain_View_Camera.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_View_Camera_Value});
            this.mnuMain_View_Camera.Name = "mnuMain_View_Camera";
            this.mnuMain_View_Camera.Size = new System.Drawing.Size(119, 22);
            this.mnuMain_View_Camera.Text = "Camera";
            // 
            // mnuMain_View_Camera_Value
            // 
            this.mnuMain_View_Camera_Value.Items.AddRange(new object[] {
            "Reset",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.mnuMain_View_Camera_Value.Name = "mnuMain_View_Camera_Value";
            this.mnuMain_View_Camera_Value.Size = new System.Drawing.Size(121, 23);
            this.mnuMain_View_Camera_Value.SelectedIndexChanged += new System.EventHandler(this.menu_change_camera_size);
            // 
            // update_timer
            // 
            this.update_timer.Enabled = true;
            this.update_timer.Tick += new System.EventHandler(this.update_timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 346);
            this.Controls.Add(this.maze_label);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "Form1";
            this.Text = "Beautiful Mazes";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.handle_user_input);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label maze_label;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_Quit;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_New;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_New_Block;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_File_New_Wall;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_View;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_View_Size;
        private System.Windows.Forms.ToolStripComboBox mnuMain_View_Size_Value;
        private System.Windows.Forms.Timer update_timer;
        private System.Windows.Forms.ToolStripMenuItem mnuMain_View_Camera;
        private System.Windows.Forms.ToolStripComboBox mnuMain_View_Camera_Value;
    }
}

