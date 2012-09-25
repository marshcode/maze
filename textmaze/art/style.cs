using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace textmaze.art
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

        //These are all class defaults.  They can be overridden with the style class. 
        //NOTE, all x and y's are reversed here.  
        static protected Glyph<Direction> default_character_glyph;
        static protected Glyph<Direction> default_wall_glyph;

        static protected Glyph<Type> default_tile_glyph;

        static protected Glyph<int> default_wall_joint_glyph;
        // I don't want to allow the real direction to be combined so I'm using a static mapping


        static ASCIIMazeGlyphStyle()
        {
            default_character_glyph = new Glyph<Direction>('?');
            default_character_glyph.add_character(Direction.North, '▲');
            default_character_glyph.add_character(Direction.South, '▼');
            default_character_glyph.add_character(Direction.East, '►');
            default_character_glyph.add_character(Direction.West, '◄');

            default_wall_glyph = new Glyph<Direction>('?');
            default_wall_glyph.add_character(Direction.North, '─');
            default_wall_glyph.add_character(Direction.South, '─');
            default_wall_glyph.add_character(Direction.East, '│');
            default_wall_glyph.add_character(Direction.West, '│');

            default_tile_glyph = new Glyph<Type>('?');
            default_tile_glyph.add_character(typeof(Tile), ' ');
            default_tile_glyph.add_character(typeof(Block), '█');


            default_wall_joint_glyph = new Glyph<int>('?');
            //                        0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15
            char[] wall_joint_chr = { ' ', '─', '│', '┐', '─', '─', '┌', '┬', '│', '┘', '│', '┤', '└', '┴', '├', '┼' };
            for (int i = 0; i < wall_joint_chr.Length; i++)
            {
                default_wall_joint_glyph.add_character(i, wall_joint_chr[i]);
            }
        }

        protected Glyph<Direction> character_glyph;
        protected Glyph<Direction> wall_glyph;
        protected Glyph<int> wall_joint_glyph;
        protected Glyph<Type> tile_glyph;

        public ASCIIMazeGlyphStyle() : this(default_character_glyph, default_wall_glyph, default_wall_joint_glyph, default_tile_glyph) { }

        public ASCIIMazeGlyphStyle(Glyph<Direction> character_glyph = null, Glyph<Direction> wall_glyph = null,
                              Glyph<int> wall_joint_glyph = null, Glyph<Type> tile_glyph = null)
        {
            this.character_glyph = character_glyph == null ? default_character_glyph : character_glyph;
            this.wall_glyph = wall_glyph == null ? default_wall_glyph : wall_glyph;
            this.wall_joint_glyph = wall_joint_glyph == null ? default_wall_joint_glyph : wall_joint_glyph;
            this.tile_glyph = tile_glyph == null ? default_tile_glyph : tile_glyph;

        }


        public char get_tile_char(Tile tile){
            return this.tile_glyph.get_character(tile.GetType());
        }
        public char get_character_char(Character character){
            return this.character_glyph.get_character(character.get_orientation());
        }
        public char get_wall_char(Tile tile, Direction direction){
            return this.wall_glyph.get_character(direction);
        }
        public char get_wall_joint_char(int joint_id){
            return this.wall_joint_glyph.get_character(joint_id);
        }

    }
}
