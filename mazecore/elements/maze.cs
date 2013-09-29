
namespace mazecore.elements {

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using mazecore.storage;
    using mazecore.direction;
    using mazecore.control;

    public class MazeException : Exception {
        public MazeException(string message)
            : base(message) {
        }
    }

    public class Position {
        public readonly int x;
        public readonly int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Position move(Direction direction, int by) {
            return DirectionControl.move(this, direction, by);
        }

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
    /// Movement Event that details the step on and off
    /// This represents the movement after it has occured.  The character has left the tile it was standing on
    /// When the tile is notified.
    /// </summary>
    public class MovementEvent {
        public readonly Character character;
        public readonly Position moved_from;
        public readonly Position moved_to;

        public MovementEvent(Character character, Position moved_from, Position moved_to) {
            this.character = character;
            this.moved_from = moved_from;
            this.moved_to = moved_to;
        }
    }

    /// <summary>
    /// Holds the topology of the maze
    /// </summary>
    public class Maze {

        private GridStorage<Tile> tile_storage;
        private SharedEdgeStorage<Wall> wall_storage;
        private GridStorage<Character> character_storage;
        private Dictionary<Position, EventManager<MovementEvent>> movement_events;

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

        public int get_x_range() { return this.tile_storage.get_x_range(); }
        public int get_y_range() { return this.tile_storage.get_y_range(); }

        public void set_tile(Tile tile, Position p) { 
            this.tile_storage.set_item(tile, p); 
        }
        public Tile get_tile(Position p) { 
            return this.tile_storage.get_item(p); 
        }
        public void remove_tile(Position p) {
            this.tile_storage.remove_item(p); 
        }

        //should you be allowed to set walls where there are no tiles?  Nobody would really notice.
        public void set_wall(Wall wall, Position p, Direction direction) { 
            this.wall_storage.set_item(wall, p, direction); 
        }
        public Wall get_wall(Position p, Direction direction) { 
            return this.wall_storage.get_item(p, direction); 
        }
        public void remove_wall(Position p, Direction direction) { 
            this.wall_storage.remove_item(p, direction); 
        }

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