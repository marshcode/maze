using mazecore.control;
using NUnit.Framework;
using System;

namespace mazecore.control.test {

    class Trap<T> {
        public Trap() { }

        public T val = default(T);
        public void spring(T val) { this.val = val; }
    }


    class ExceptionTrap<T> {
        public ExceptionTrap() { }

        public bool tripped = false;
        public void spring(T val) {
            this.tripped = true; throw new NotImplementedException(">:(");
        }

    [TestFixture(typeof(int))]
    class TestEventManager {


            [Test]
            public void test_single_event() {
                EventManager<int> event_manager = new EventManager<int>();
                Trap<int> trap = new Trap<int>();

                Assert.AreEqual(trap.val, 0);
                event_manager.register(trap.spring);
                event_manager.announce(1);
                Assert.AreEqual(trap.val, 1);
                event_manager.unregister(trap.spring);
                event_manager.announce(2);
                Assert.AreEqual(trap.val, 1);
            }

            [Test]
            public void test_no_registered_events() {
                EventManager<int> event_manager = new EventManager<int>();
                event_manager.announce(2);
                //I guess we're just testing that no exceptions happen.

            }


            [Test]
            public void test_multiple_events_with_middle_exception(){

            EventManager<int> event_manager = new EventManager<int>();
            Trap<int> trap1 = new Trap<int>(), trap3 = new Trap<int>();
            ExceptionTrap<int> trap2 = new ExceptionTrap<int>();


            Assert.AreEqual(trap1.val, 0);
            Assert.False(trap2.tripped);
            Assert.AreEqual(trap3.val, 0);

            event_manager.register(trap1.spring);
            event_manager.register(trap2.spring);
            event_manager.register(trap3.spring);

            event_manager.announce(2);
            Assert.AreEqual(trap1.val, 2);
            Assert.True(trap2.tripped);
            Assert.AreEqual(trap3.val, 2);

            }
        }
    }
}