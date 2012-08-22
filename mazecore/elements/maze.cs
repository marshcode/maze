using System;
using System.Collections.Generic;
namespace mazecore.elements {

    public enum Direction { North, East, South, West };

    abstract class Storage<T> {

        public Storage(int x_range, int y_range) {
            if (x_range <= 0 || y_range <= 0) {
                throw new ArgumentOutOfRangeException(string.Format("({0}, {1}) is an invalid range", x_range, y_range));
            }
        }

        public abstract int get_x_range();
        public abstract int get_y_range();

        //range check
        protected void check_range(int x, int y) {
            if (x < 0 || x > this.get_x_range() || y < 0 || y > this.get_y_range()) {
                throw new ArgumentOutOfRangeException(string.Format("({0}, {1}) is out of range: ({2}, {3})",
                                                                       x, y, this.get_x_range(), this.get_y_range()));
            }
        }

    }

    class GridStorage<T> : Storage<T> {
        private T[,] storage;
        
        public GridStorage(int x_range, int y_range) : base(x_range, y_range){
            
            this.storage = new T[x_range, y_range];    
        }


        //getters and setters
        public override int get_x_range() {
            return this.storage.GetLength(0);
        }
        public override int get_y_range() {
            return this.storage.GetLength(1);
        }
        public void set_item(T item, int x, int y) {
            this.check_range(x, y);
            this.storage[x, y] = item;
        }
        public T get_item(int x, int y) {
            this.check_range(x, y);
            return this.storage[x, y];
        }
        public void remove_item(int x, int y){
            this.check_range(x, y);
            this.storage[x, y] = default(T);
        }
    }

    class SharedEdgeStorage<T> : Storage<T> {
        
        //wall storage uses North-East-Notation (trac.davemarsh.webfactional.com\maze).

        private int x_range, y_range;
        private Dictionary<string, T> storage;

        public SharedEdgeStorage(int x_range, int y_range) : base(x_range, y_range){
            this.x_range = x_range;
            this.y_range = y_range;
            this.storage = new Dictionary<string, T>();
        }
   
        //helper methods

        private string get_key(int x, int y, Direction direction) {
            this.position_correct(ref x, ref y, ref direction);
            return string.Format("{0}-{1}-{2}", x, y, direction);
        }
        private void position_correct(ref int x, ref int y, ref Direction direction) {

            if (direction == Direction.South) {
                x += 1;
                direction = Direction.North;
            }else if (direction == Direction.West) {
                y -= 1;
                direction = Direction.East;
            }
        }

        public override int get_x_range(){
            return this.x_range;
        }
        public override int get_y_range(){
            return this.y_range;
        }

        public void set_item(T item, int x, int y, Direction direction){
            this.check_range(x, y);
            string key = this.get_key(x, y, direction);
            this.storage[key] = item;
        }
        public T get_item(int x, int y, Direction direction){
            this.check_range(x, y);
            string key = this.get_key(x, y, direction);
            T value;
            
            if (!this.storage.TryGetValue(key, out value)){
                value = default(T);
            }
            return value;
        }
        public void remove_item(int x, int y, Direction direction){
            this.check_range(x, y);
            string key = this.get_key(x, y, direction);
            this.storage.Remove(key);

        }
    }
}