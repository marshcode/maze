using System;
using System.Collections.Generic;

namespace mazecore.control {

    class SignalHandler {

        public enum SignalState { Off, Rising, On, Falling };

        private Dictionary<string, SignalState> signals;
        public SignalHandler() {
            this.signals = new Dictionary<string, SignalState>();
        }

        public void activate_signal(string signal) {
            SignalState signal_state = this.get_signal_state(signal);
            if (signal_state == SignalState.Rising || signal_state == SignalState.On) {
                return;
            }
            this.signals[signal] = SignalHandler.SignalState.Rising;
        }

        public void deactivate_signal(string signal) {
            SignalState signal_state = this.get_signal_state(signal);
            if (signal_state == SignalState.Off || signal_state == SignalState.Falling) {
                return;
            }
            this.signals[signal] = SignalHandler.SignalState.Falling;
        }

        public void step() {

            SignalState the_state;
            string signal;

            string[] keys = new string[this.signals.Count];
            this.signals.Keys.CopyTo(keys, 0);

            for (int i=0; i < keys.Length;i++ ) {
                signal = keys[i];

                the_state = this.signals[signal];
                if (the_state == SignalState.Rising) {
                    this.signals[signal] = SignalState.On;
                }else if (the_state == SignalState.Falling) {
                    this.signals[signal] = SignalState.Off;
                }

            }
        
        
        }


        public SignalState get_signal_state(string signal) {
            if (this.signals.ContainsKey(signal)) {
                return this.signals[signal];
            }
            return SignalState.Off;

        }

    }

}