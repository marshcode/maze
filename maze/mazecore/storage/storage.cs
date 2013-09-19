namespace mazecore.storage {

    using System;
    using System.Collections.Generic;

    using mazecore.elements;
    using mazecore.direction;

    abstract class Storage<T> {

        public Storage(int x_range, int y_range) {
            if (x_range <= 0 || y_range <= 0) {
                throw new ArgumentOutOfRangeException(string.Format("({0}, {1}) is an invalid range", x_range, y_range));
            }
        }

        public abstract int get_x_range();
        public abstract int get_y_range();

        //range check
        protected void check_range(Position p) {
            if (p.x < 0 || p.x >= this.get_x_range() || p.y < 0 || p.y >= this.get_y_range()) {
                throw new ArgumentOutOfRangeException(string.Format("({0}, {1}) is out of range: ({2}, {3})",
                                                                       p.x, p.y, this.get_x_range(), this.get_y_range()));
            }
        }
    }


    class GridStorage<T> : Storage<T> {
        private T[,] storage;

        public GridStorage(int x_range, int y_range)
            : base(x_range, y_range) {

            this.storage = new T[x_range, y_range];
        }

        public Position get_position(T item) {
            T temp;

            for (int x = 0; x < this.get_x_range(); x++) {
                for (int y = 0; y < this.get_y_range(); y++) {
                    temp = this.storage[x, y];//this.get_item(x, y);
                    if (temp != null && temp.Equals(item)) {
                        return new Position(x, y);
                    }
                }
            }
            return null;
        }

        //getters and setters
        public override int get_x_range() {
            return this.storage.GetLength(0);
        }
        public override int get_y_range() {
            return this.storage.GetLength(1);
        }

        public void move(T item, Position new_) {
            Position old = this.get_position(item);
            if (old != null) {
                this.remove_item(old);
            }
            this.set_item(item, new_);

        }

        public void set_item(T item, Position p) {
            this.check_range(p);
            this.storage[p.x, p.y] = item;
        }
        public T get_item(Position p) {
            this.check_range(p);
            return this.storage[p.x, p.y];
        }
        public void remove_item(Position p) {
            this.check_range(p);
            this.storage[p.x, p.y] = default(T);
        }
    }

    class SharedEdgeStorage<T> : Storage<T> {

        //wall storage uses North-East-Notation (trac.davemarsh.webfactional.com\maze).

        private int x_range, y_range;
        private Dictionary<string, T> storage;

        public SharedEdgeStorage(int x_range, int y_range)
            : base(x_range, y_range) {
            this.x_range = x_range;
            this.y_range = y_range;
            this.storage = new Dictionary<string, T>();
        }

        //helper methods

        private string get_key(Position p, Direction direction) {
            Tuple<Position, Direction> t = this.position_correct(p, direction);
            return string.Format("{0}-{1}-{2}", t.Item1.x, t.Item1.y, t.Item2);
        }
        private Tuple<Position, Direction> position_correct(Position p, Direction direction) {
            if (direction == Direction.South) {
                p = p.move(direction, 1);
                direction = Direction.North;
            }
            else if (direction == Direction.West) {
                p = p.move(direction, 1);
                direction = Direction.East;
            }
            return new Tuple<Position, Direction>(p, direction);

        }
        //public interface
        public override int get_x_range() {
            return this.x_range;
        }
        public override int get_y_range() {
            return this.y_range;
        }

        public void set_item(T item, Position p, Direction direction) {
            this.check_range(p);
            string key = this.get_key(p, direction);
            this.storage[key] = item;
        }
        public T get_item(Position p, Direction direction) {
            this.check_range(p);
            string key = this.get_key(p, direction);
            T value;

            if (!this.storage.TryGetValue(key, out value)) {
                value = default(T);
            }
            return value;
        }
        public void remove_item(Position p, Direction direction) {
            this.check_range(p);
            string key = this.get_key(p, direction);
            this.storage.Remove(key);

        }
    }
}