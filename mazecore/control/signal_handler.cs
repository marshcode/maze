using System;

namespace mazecore.control {

    class SignalHandler {

        public enum SignalState { Off, Rising, On, Falling };

        public void activate_signal(string signal) { }
        public void deactivate_signal(string signal) { }
        public void step() { }


        public SignalState get_signal_state(string signal) { throw new NotImplementedException(">:("); }

    }

}