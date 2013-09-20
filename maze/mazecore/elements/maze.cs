
namespace mazecore.elements {

    using System;
    using System.Collections.Generic;

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
            return other_pos.x == this.x && other_pos.y == this.y;
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
    /// </summary>
    public class MovementEvent {
        public readonly Character character;
        public MovementEvent(Character character) {
            this.character = character;
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

        public void set_tile(Tile tile, Position p) { this.tile_storage.set_item(tile, p); }
        public Tile get_tile(Position p) { return this.tile_storage.get_item(p); }
        public void remove_tile(Position p) { this.tile_storage.remove_item(p); }

        //should you be allowed to set walls where there are no tiles?  Nobody would really notice.
        public void set_wall(Wall wall, Position p, Direction direction) { this.wall_storage.set_item(wall, p, direction); }
        public Wall get_wall(Position p, Direction direction) { return this.wall_storage.get_item(p, direction); }
        public void remove_wall(Position p, Direction direction) { this.wall_storage.remove_item(p, direction); }

        public void set_character(Character character, Position p) {
            if (this.tile_storage.get_item(p) == null) {
                throw new MazeException(string.Format("Cannot set character: tile at {0}, {1} is null", p.x, p.y));
            }else if (this.character_storage.get_item(p) != null) {
                throw new MazeException(string.Format("Cannot set character on {0}, {1}.  Tile is already occupied", p.x, p.y));
            }

            bool do_events = !character.get_position().Equals(p);
            Console.WriteLine(do_events);
            if (do_events) {
                this.get_tile(character.get_position()).action_step_off(character);
            }
            
            this.character_storage.move(character, p);

            if (do_events) {
                this.get_tile(p).action_step_on(character);
                this.get_movement_event(p).announce(new MovementEvent(character));
            }

        }
        public Character get_character(Position p) { return this.character_storage.get_item(p); }
        public void remove_character(Position p) { this.character_storage.remove_item(p); }

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