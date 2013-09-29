using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mazetextart.art
{

    using mazecore.direction;
    using mazecore.elements;

    /* ASCII Characters and thier codes:
    
     * Characters
     * ►, 16
     * ◄, 17
     * ▲, 30
     * ▼, 31
     
     * Wall Joints
     *  , 32 (technically)
     * ┼, 197
     * ┘, 217
     * └, 192
     * ┌, 218
     * ┐, 191
     * ┴, 193
     * ┬, 194
     * ├, 195
     * ┤, 180
     
     * Walls
     * ─, 196
     * │, 179
       
     */

    public interface IASCIIMazeStyle
    {
        char get_tile_char(Tile tile);
        char get_character_char(Character character);
        char get_wall_char(Tile tile, Direction direction);
        char get_wall_joint_char(int joint_id);

    }

    public class ASCIIMazeGlyphStyle : IASCIIMazeStyle
    {


        static protected void create_default(ASCIIMazeGlyphStyle maze_style)
        {
            maze_style.character_glyph = new Glyph<Direction>('?');
            maze_style.character_glyph.add_character(Direction.North, '▲');
            maze_style.character_glyph.add_character(Direction.South, '▼');
            maze_style.character_glyph.add_character(Direction.East, '►');
            maze_style.character_glyph.add_character(Direction.West, '◄');

            maze_style.wall_glyph = new Glyph<Direction>('?');
            maze_style.wall_glyph.add_character(Direction.North, '─');
            maze_style.wall_glyph.add_character(Direction.South, '─');
            maze_style.wall_glyph.add_character(Direction.East, '│');
            maze_style.wall_glyph.add_character(Direction.West, '│');

            maze_style.tile_glyph = new Glyph<Type>('?');
            maze_style.tile_glyph.add_character(typeof(Tile), ' ');
            maze_style.tile_glyph.add_character(typeof(Block), '█');


            maze_style.wall_joint_glyph = new Glyph<int>('?');
            //                         0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15
            char[] wall_joint_chr = { ' ', '─', '│', '┐', '─', '─', '┌', '┬', '│', '┘', '│', '┤', '└', '┴', '├', '┼' };
            for (int i = 0; i < wall_joint_chr.Length; i++)
            {
                maze_style.wall_joint_glyph.add_character(i, wall_joint_chr[i]);
            }

        }

        protected Glyph<Direction> character_glyph;
        protected Glyph<Direction> wall_glyph;
        protected Glyph<int> wall_joint_glyph;
        protected Glyph<Type> tile_glyph;

        public ASCIIMazeGlyphStyle(){ 
            ASCIIMazeGlyphStyle.create_default(this);
        }

        public ASCIIMazeGlyphStyle(Glyph<Direction> character_glyph = null, Glyph<Direction> wall_glyph = null,
                              Glyph<int> wall_joint_glyph = null, Glyph<Type> tile_glyph = null)
        {

            ASCIIMazeGlyphStyle.create_default(this);

            this.character_glyph = character_glyph == null ? this.character_glyph : character_glyph;
            this.wall_glyph = wall_glyph == null ? this.wall_glyph : wall_glyph;
            this.wall_joint_glyph = wall_joint_glyph == null ?  this.wall_joint_glyph: wall_joint_glyph;
            this.tile_glyph = tile_glyph == null ? this.tile_glyph : tile_glyph;

        }

        public void set_tile_char(Type tile, char ch){
            this.tile_glyph.add_character(tile, ch);
        }
        public char get_tile_char(Tile tile){
            return this.tile_glyph.get_character(tile.GetType());
        }

        public void set_character_char(Direction direction, char ch) {
            this.character_glyph.add_character(direction, ch);
        }
        public char get_character_char(Character character){
            return this.character_glyph.get_character(character.get_orientation());
        }


        public void set_wall_char(Tile tile, Direction direction, char ch) {
            this.wall_glyph.add_character(direction, ch);
        }
        public char get_wall_char(Tile tile, Direction direction){
            return this.wall_glyph.get_character(direction);
        }

        public void set_wall_joint_char(int joint_id, char ch) {
            this.wall_joint_glyph.add_character(joint_id, ch);
        }
        public char get_wall_joint_char(int joint_id){
            return this.wall_joint_glyph.get_character(joint_id);
        }

    }
}
