namespace textmaze.art{

    using System;
    using mazecore.elements;

    public class ASCIIRendererCamera : ASCIIRenderer{

        protected int x_range;
        protected int y_range;

        protected ASCIIRenderer renderer;

        protected Func<ASCIIRendererCamera, Tuple<int, int>> get_center_point;

        public ASCIIRendererCamera(ASCIIRenderer renderer, Func< ASCIIRendererCamera, Tuple<int, int>> get_center_point){
            this.renderer = renderer;
            this.get_center_point = get_center_point;
            this.reset_range();
        }

        public override Tuple<int, int> maze_to_render_coords(Maze maze, Position p) {
            return this.renderer.maze_to_render_coords(maze, p);
        }

        public override char[][] render_char_array() {
            Tuple<int, int> center_point = this.get_center_point(this);
            char[][] full_char_map = renderer.render_char_array();
            char[][] clipped_char_map;

            int max_x_idx = full_char_map[0].Length - 1;
            int max_y_idx = full_char_map.Length - 1;
            int center_x = center_point.Item1;
            int center_y = center_point.Item2;

            int x_range = this.x_range >= 0 ? this.x_range : full_char_map[0].Length;
            int y_range = this.y_range >= 0 ? this.y_range : full_char_map.Length;

            if (center_x < 0 || center_x > max_x_idx){
                throw new mazecore.elements.MazeException(string.Format("Bad center x: {0}", center_x));
            }else if (center_y < 0 || center_y > max_y_idx){
                throw new mazecore.elements.MazeException(string.Format("Bad center y: {0}", center_y));
            }


            int x_min_idx = Math.Max(center_x - x_range, 0);
            int x_max_idx = Math.Min(center_x + x_range, max_x_idx);

            int y_min_idx = Math.Max(center_y - y_range, 0);
            int y_max_idx = Math.Min(center_y + y_range, max_y_idx);
            
            clipped_char_map = new char[y_max_idx - y_min_idx + 1][];

            for (int f_y = y_min_idx, c_y = 0; f_y <= y_max_idx; f_y++, c_y++){
                clipped_char_map[c_y] = new char[x_max_idx - x_min_idx + 1];
                for (int f_x = x_min_idx, c_x=0; f_x <= x_max_idx; f_x++, c_x++){
                    clipped_char_map[c_y][c_x] = full_char_map[f_y][f_x];
                }
            }
            return clipped_char_map; 
        }

        public void set_range(int x_range = -1, int y_range = -1){
            if (x_range >= 0) this.x_range = x_range;
            if (y_range >= 0) this.y_range = y_range;
        }

        public void reset_range() {
            this.x_range = -1;
            this.y_range = -1;
        }

    }


}