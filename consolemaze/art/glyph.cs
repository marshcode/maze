namespace consolemaze.art {

    using System.Collections.Generic;

    class Glyph<T>{

        protected Dictionary<T, char> mapping;
        protected char default_value;

        public Glyph(char default_value) {
            this.mapping = new Dictionary<T, char>();
            this.default_value = default_value;
        }

        public void add_character(T key, char c) {
            this.remove_character(key);
            this.mapping.Add(key, c);
        }
        public char get_character(T key) { 
            try{
                return this.mapping[key];
            }catch(KeyNotFoundException){
                return this.get_default();
            }
        }
        public void remove_character(T key) {
            try {
                this.mapping.Remove(key);
            }catch(KeyNotFoundException){}
        }
        
        public char get_default() {
            return this.default_value;
        }
        
    }
}