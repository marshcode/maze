namespace mazecore.control {


public abstract class Controller {
    //private helper class for characters.  Controls the character actions

    protected bool active;
    public Controller() {
        this.active = true;
    }


    public void set_active(bool is_active) {
        this.active = is_active;
    }
    public bool get_active() {
        return this.active;
    }

    public void update(int tick) {
        if (this.get_active()) {
            this.do_update(tick);
        }
    }
    protected abstract void do_update(int tick);

}

}