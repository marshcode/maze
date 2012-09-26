﻿namespace textmaze.art{

    using System;

    public abstract class ASCIIRendererCamera : ASCIIRenderer{

        protected int x_range;
        protected int y_range;

        protected ASCIIRenderer renderer;
        
        public ASCIIRendererCamera(ASCIIRenderer renderer){
            this.x_range = -1;
            this.y_range = -1;
            this.renderer = renderer;
        }
        public override char[][] render_char_array() {

            Tuple<int, int> center_point = this.get_center_point();
            char[][] full_char_map = renderer.render_char_array();
            char[][] clipped_char_map;

            int max_x_idx = full_char_map[0].Length - 1;
            int max_y_idx = full_char_map.Length - 1;
            int center_x = center_point.Item1;
            int center_y = max_y_idx - center_point.Item2;

            Console.WriteLine(center_x);
            Console.WriteLine(center_y);

            Console.WriteLine(max_x_idx);
            Console.WriteLine(max_y_idx);

            int x_range = this.x_range >= 0 ? this.x_range : full_char_map[0].Length;
            int y_range = this.y_range >= 0 ? this.y_range : full_char_map.Length;

            Console.WriteLine(x_range);
            Console.WriteLine(y_range);

            if (center_x < 0 || center_x > max_x_idx){
                throw new mazecore.elements.MazeException(string.Format("Bad center x: {0}", center_x));
            }else if (center_y < 0 || center_y > max_y_idx){
                throw new mazecore.elements.MazeException(string.Format("Bad center y: {0}", center_y));
            }


            int x_min_idx = Math.Max(center_x - x_range, 0);
            int x_max_idx = Math.Min(center_x + x_range, max_x_idx);

            Console.WriteLine(x_min_idx);
            Console.WriteLine(x_max_idx);

            int y_min_idx = Math.Max(center_y - y_range, 0);
            int y_max_idx = Math.Min(center_y + y_range, max_y_idx);

            Console.WriteLine(y_min_idx);
            Console.WriteLine(y_max_idx);
            
            clipped_char_map = new char[y_max_idx - y_min_idx + 1][];

            for (int f_y = y_min_idx, c_y = 0; f_y <= y_max_idx; f_y++, c_y++)
            {
                clipped_char_map[c_y] = new char[x_max_idx - x_min_idx + 1];
                for (int f_x = x_min_idx, c_x=0; f_x <= x_max_idx; f_x++, c_x++)
                {
                    Console.WriteLine(string.Format("***{0}", full_char_map[f_y][f_x]));
                    Console.WriteLine(c_x); Console.WriteLine(c_y);
                    Console.WriteLine(f_x); Console.WriteLine(f_y);
                    clipped_char_map[c_y][c_x] = full_char_map[f_y][f_x];
                }
            }
            return clipped_char_map; 
        }

        public void set_range(int x_range = -1, int y_range = -1){
            if (x_range >= 0) this.x_range = x_range;
            if (y_range >= 0) this.y_range = y_range;
        }

        protected abstract Tuple<int, int> get_center_point();

    }


}