namespace textmaze.art{

    using System;
    using mazecore.elements;

    public class ASCIIRendererCamera : ASCIIRenderer{

        protected int x_range;
        protected int y_range;

        protected ASCIIRenderer renderer;

        protected Func<ASCIIRendererCamera, Position> get_center_point;

        public ASCIIRendererCamera(ASCIIRenderer renderer, Func< ASCIIRendererCamera, Position> get_center_point){
            this.renderer = renderer;
            this.get_center_point = get_center_point;
            this.reset_range();
        }

        public override Position maze_to_render_coords(Position p) {


            return this.renderer.maze_to_render_coords(p);
        }

        public override char[][] render_char_array() {
            Position center_point = this.get_center_point(this);
            char[][] full_char_map = renderer.render_char_array();
            char[][] clipped_char_map;

            int max_x_idx = full_char_map[0].Length - 1;
            int max_y_idx = full_char_map.Length - 1;
            int center_x = center_point.x;
            int center_y = center_point.y;

            
            int x_range = full_char_map[0].Length;
            int d_x = x_range;
            if (this.x_range >= 0) {
                x_range = this.x_range;
                d_x = (x_range * 2) + 1;
            }

            int y_range = full_char_map.Length;
            int d_y = y_range;
            if (this.y_range >= 0) {
                y_range = this.y_range;
                d_y = (y_range * 2) + 1;
            }


            if (center_x < 0 || center_x > max_x_idx){
                throw new mazecore.elements.MazeException(string.Format("Bad center x: {0}", center_x));
            }else if (center_y < 0 || center_y > max_y_idx){
                throw new mazecore.elements.MazeException(string.Format("Bad center y: {0}", center_y));
            }


            int x_min_idx = Math.Max(center_x - x_range, 0);
            int x_max_idx = Math.Min(center_x + x_range, max_x_idx);

            int y_min_idx = Math.Max(center_y - y_range, 0);
            int y_max_idx = Math.Min(center_y + y_range, max_y_idx);

            char[] default_line = new String(' ', d_x).ToCharArray();
            clipped_char_map = new char[d_y][];
            for (int i = 0; i < d_y; i++) {
                clipped_char_map[i] = new char[d_x];
                Array.Copy(default_line, clipped_char_map[i], d_x);
            }

            for (int f_y = y_min_idx, c_y = 0; f_y <= y_max_idx; f_y++, c_y++){
                Array.Copy(full_char_map[f_y], x_min_idx, clipped_char_map[c_y], 0, x_max_idx - x_min_idx +1);

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