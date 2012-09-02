using mazecore.control;
using NUnit.Framework;

namespace mazecore.control.test {

    class TrapController : Controller {

        public bool tripped;
        public int last_tick;
        public TrapController() {
            this.reset();
        }

        public void reset(){
            this.tripped = false;
            this.last_tick = -1;
        }

        protected override void do_update(int tick) {
            this.tripped = true;
            this.last_tick = tick;
        }

    }


    [TestFixture]
    class TestController {

        [Test]
        public void test_get_set_active() {

            TrapController trap_controller = new TrapController();

            Assert.True(trap_controller.get_active());
            trap_controller.set_active(false);
            Assert.False(trap_controller.get_active());
        }

        [Test]
        public void test_tick(){

            TrapController trap_controller = new TrapController();

            Assert.False(trap_controller.tripped);
            Assert.AreEqual(trap_controller.last_tick, -1);

            trap_controller.update(1);
            Assert.True(trap_controller.tripped);
            Assert.AreEqual(trap_controller.last_tick, 1);

            trap_controller.reset();
            trap_controller.set_active(false);
            trap_controller.update(2);
            Assert.False(trap_controller.tripped);
            Assert.AreEqual(trap_controller.last_tick, -1);
        }


    }


}