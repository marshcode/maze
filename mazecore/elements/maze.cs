
namespace mazecore.elements {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using mazecore.storage;
    using mazecore.direction;
    using mazecore.control;

    /// <summary>
    /// Maze Exception.  Thrown only when maze specific errors are encountered.  Null Pointers and
    /// certain range errors use regular system errors.
    /// </summary>
    public class MazeException : Exception {
        public MazeException(string message)
            : base(message) {
        }
    }
    /// <summary>
    /// 2D cartesian coordinate container class.  This handles any position data contained in the Maze.  Positions
    /// are read only so modifying a position (even if you managed to do it) would not change the system
    /// in a meaninful way.
    /// 
    /// Positions hashed in Sets and Dictionaries are hashed using their X and Y coordinates instead of object equality.
    /// 
    /// </summary>
    public class Position {
        /// <summary>
        /// Cartesian X-Coordinate
        /// </summary>
        public readonly int x;
        /// <summary>
        /// Cartesian Y-Coordinate
        /// </summary>
        public readonly int y;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Cartesian X-Coordinate</param>
        /// <param name="y">Cartesian X-Coordinate</param>
        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a new position relative to this one.  New position is moved in the given 
        /// cardinal direction by the given number of spaces.
        /// </summary>
        /// <param name="direction">Direction enum</param>
        /// <param name="by">How many spaces to move in the given direction</param>
        /// <returns></returns>
        public Position move(Direction direction, int by) {
            return DirectionControl.move(this, direction, by);
        }

        /// <summary>
        /// Position equality calculation.  Two positions are equal if the x and y coordinates match.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            Position other_pos = obj as Position;
            return obj != null && other_pos.x == this.x && other_pos.y == this.y;
        }

        public override int GetHashCode() {
            int hash = 17;
            hash = (hash * 31) + this.x.GetHashCode();
            hash = (hash * 31) + this.y.GetHashCode();
            return hash;

        }

    }
    /// <summary>
    /// Movement Event that details a character movement event.
    /// This represents the movement after it has occured.  The character has left the tile it was standing on
    /// When the tile is notified.
    /// </summary>
    public class MovementEvent {
        /// <summary>
        /// Character involved in the movement.
        /// <see cref="Character"/>
        /// </summary>
        public readonly Character character;
        /// <summary>
        /// Position the character moved from
        /// <see cref="Position"/>
        /// </summary>
        public readonly Position moved_from;
        /// <summary>
        /// Position the character moved to.
        /// <see cref="Position"/>
        /// </summary>
        public readonly Position moved_to;

        /// <summary>
        ///
        /// </summary>
        /// <param name="character">Character involved in the movement.</param>
        /// <param name="moved_from">Position the character moved from</param>
        /// <param name="moved_to">Position the character moved to.</param>
        public MovementEvent(Character character, Position moved_from, Position moved_to) {
            this.character = character;
            this.moved_from = moved_from;
            this.moved_to = moved_to;
        }
    }

    /// <summary>
    /// Holds the geometry of the maze. All element creation and movements should be coordinated through this class.
    /// Elements are free to keep any desired data cached and up to date but it must be sent through the
    /// proper maze object first.
    /// </summary>
    public class Maze {
        /// <summary>
        /// Tile storage in a cartesian grid system.  Misses return null<see cref="Tile"/>
        /// </summary>
        private GridStorage<Tile> tile_storage;

        /// <summary>
        /// Shared edge storage ensures that North/South and East/West walls that should be the same
        /// wall actually are.
        /// </summary>
        private SharedEdgeStorage<Wall> wall_storage;

        /// <summary>
        /// Character storage in a cartesian grid.
        /// </summary>
        private GridStorage<Character> character_storage;

        /// <summary>
        /// Dictionary of EventManagers that handle MovementEvents.
        /// </summary>
        private Dictionary<Position, EventManager<MovementEvent>> movement_events;

        /// <summary>
        ///
        /// </summary>
        /// <param name="x_range">Maximum X coordinate value</param>
        /// <param name="y_range">Maximum Y coordinate value</param>
        public Maze(int x_range, int y_range) {
            this.tile_storage = new GridStorage<Tile>(x_range, y_range);
            this.wall_storage = new SharedEdgeStorage<Wall>(x_range, y_range);
            this.character_storage = new GridStorage<Character>(x_range, y_range);
            this.movement_events = new Dictionary<Position, EventManager<MovementEvent>>();
        }

        public bool in_range(Position p) {
            if (p.x < 0 || p.x > this.get_x_range() || p.y < 0 || p.y > this.get_y_range()) {
                return false;
            }
            return true;
        }

        public int get_x_range() { 
            return this.tile_storage.get_x_range(); 
        }

        public int get_y_range() { 
            return this.tile_storage.get_y_range(); 
        }
        /// <summary>
        /// Sets the given tile at the given position.  An existing tile is overwritten.  Care should be taken to
        /// clean up any tile references being held outside of the Maze that are being overwritten.
        /// 
        /// Tiles are not meant to be "moved".  A single tile instance should only be set once.
        /// </summary>
        /// <param name="tile">Tile to set <see cref="Tile"/></param>
        /// <param name="p">New location of the tile <see cref="Position"/></param>
        public void set_tile(Tile tile, Position p) { 
            this.tile_storage.set_item(tile, p); 
        }
        /// <summary>
        /// Returns the Tile at the requested position or null if no tile exists.
        /// Out of range positions will throw ArgumentOutOfRangeExceptions
        /// <exception>ArgumentOutOfRangeExceptions</exception>
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Tile get_tile(Position p) { 
            return this.tile_storage.get_item(p); 
        }

        /// <summary>
        /// Removes the given tile at the given location.  Care should be taken to clean up any tile references
        /// held outside of the Maze.
        /// </summary>
        /// <param name="p">Location at which to remove the tile.</param>
        public void remove_tile(Position p) {
            this.tile_storage.remove_item(p); 
        }

        //should you be allowed to set walls where there are no tiles?  Nobody would really notice.
        //The same warnings about outside references apply to walls.
        public void set_wall(Wall wall, Position p, Direction direction) { 
            this.wall_storage.set_item(wall, p, direction); 
        }
        public Wall get_wall(Position p, Direction direction) { 
            return this.wall_storage.get_item(p, direction); 
        }
        public void remove_wall(Position p, Direction direction) { 
            this.wall_storage.remove_item(p, direction); 
        }

        //characters are handled slightly different than walls and tiles
        //walls and tiles may be overwritten by characters cannot.
        public void set_character(Character character, Position p) {
            if (this.tile_storage.get_item(p) == null) {
                throw new MazeException(string.Format("Cannot set character: tile at {0}, {1} is null", p.x, p.y));
            }else if (this.character_storage.get_item(p) != null) {
                throw new MazeException(string.Format("Cannot set character on {0}, {1}.  Tile is already occupied", p.x, p.y));
            }

            
            Position moved_from = this.character_storage.move(character, p);
            bool do_events = !p.Equals(moved_from);
            //this code will successfully handle having new characters added to the maze
            //the equal comparison catches null points (which is an indication that this character is not present).  
            //If this code is restructured, ensure that the character does NOT have to be in the character_storge.
            if(!do_events){
                return;
            }

            MovementEvent me = new MovementEvent(character, moved_from, p);
            if (moved_from != null) {//do not call the step off function for first timers
                this.get_tile(moved_from).action_step_off(me);
            }
            this.get_tile(p).action_step_on(me);
            this.get_movement_event(p).announce(me);



        }
        public Character get_character(Position p) { 
            return this.character_storage.get_item(p); 
        }
        public void remove_character(Position p) { 
            this.character_storage.remove_item(p); 
        }

        /// <summary>
        /// Returns the EventManager for the given position
        /// MovementEvents are fired for each character that moves in the maze.
        /// </summary>
        /// <param name="p">Location in the maze to monitor</param>
        /// <returns></returns>
        public EventManager<MovementEvent> get_movement_event(Position p){

            EventManager<MovementEvent> event_manager;
            if (!this.movement_events.ContainsKey(p)) {
                event_manager = new EventManager<MovementEvent>();
                this.movement_events.Add(p, event_manager);
            }else {
                event_manager = this.movement_events[p];
            }
            return event_manager;
        }

    }



}