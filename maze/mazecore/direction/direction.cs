namespace mazecore.direction {


    public enum Direction { North, East, South, West };
    public class DirectionControl {

        public static int NORTH_OFFSET = 0;
        public static int SOUTH_OFFSET = 0;
        public static int EAST_OFFSET  = 0;
        public static int WEST_OFFSET  = 0;

        static DirectionControl() {

            NORTH_OFFSET = adjust(0, Direction.North, 1);
            SOUTH_OFFSET = adjust(0, Direction.South, 1);
            EAST_OFFSET  = adjust(0, Direction.East,  1);
            WEST_OFFSET  = adjust(0, Direction.West,  1);

        }

        
        //err, x and y are reversed.  Whoopsie.  I guess just consider x as a row and y as a column.
        //that way, depicting (row, column) isn't as goofy.  Fix this by branching first.  
        //keep all direction manipulation in one place, so we can define
        public static int adjust(int coord, Direction direction, int by) {
            if (direction == Direction.South || direction == Direction.West) {
                coord -= by;
            }
            else
                coord += by;
            return coord;
        }

        public static void move(ref int x, ref int y, Direction direction, int by) {

            if (direction == Direction.North || direction == Direction.South) {
                y = DirectionControl.adjust(y, direction, by);
            }
            else {
                x = DirectionControl.adjust(x, direction, by);
            }
        }
    }


}