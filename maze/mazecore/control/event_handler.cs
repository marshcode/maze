
namespace mazecore.control {
    using System;

    class EventManager<R> {

        private Action<R> callbacks;

        public EventManager() { }

        public void register(Action<R> callback) {
            this.callbacks += callback;
        }
        public void unregister(Action<R> callback) {
            this.callbacks -= callback;
        }
        public void announce(R event_) {
            if (this.callbacks == null) {
                return;
            }

            foreach(Action<R> callback in this.callbacks.GetInvocationList()) {
                try {
                    callback.DynamicInvoke(new object[]{event_});
                }catch (Exception) {
                    //maybe do some logging?
                }

            }


        }


    }

}