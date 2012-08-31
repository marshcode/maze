using mazecore.control;
using NUnit.Framework;

namespace mazecore.control.test {

    [TestFixture]
    class TestSignalHandler {

        [Test]
        public void test_signal_cycle(){
            string signal = "signal";

            SignalHandler signal_handler = new SignalHandler();
            Assert.AreEqual(signal_handler.get_signal_state(signal), SignalHandler.SignalState.Off);

            signal_handler.activate_signal(signal);
            Assert.AreEqual(signal_handler.get_signal_state(signal), SignalHandler.SignalState.Rising);
            
            signal_handler.step();
            Assert.AreEqual(signal_handler.get_signal_state(signal), SignalHandler.SignalState.On);
            
            signal_handler.step();
            Assert.AreEqual(signal_handler.get_signal_state(signal), SignalHandler.SignalState.On);
            
            signal_handler.deactivate_signal(signal);
            Assert.AreEqual(signal_handler.get_signal_state(signal), SignalHandler.SignalState.Falling);
            
            signal_handler.step();
            Assert.AreEqual(signal_handler.get_signal_state(signal), SignalHandler.SignalState.Off);
        }

        [Test]
        public void test_off_already_off(){
            SignalHandler signal_handler = new SignalHandler();
            Assert.AreEqual(signal_handler.get_signal_state("signal_not_there"), SignalHandler.SignalState.Off);
        }

        [Test]
        public void test_off_while_rising() {
            SignalHandler signal_handler = new SignalHandler();
            signal_handler.activate_signal("sig");
            signal_handler.step();
            Assert.AreEqual(signal_handler.get_signal_state("sig"), SignalHandler.SignalState.On);
            signal_handler.deactivate_signal("sig");
            signal_handler.step();
            Assert.AreEqual(signal_handler.get_signal_state("sig"), SignalHandler.SignalState.Off);
        }


        [Test]
        public void test_on_while_falling() {
            SignalHandler signal_handler = new SignalHandler();
            signal_handler.activate_signal("sig");
            signal_handler.step();
            signal_handler.step();
            signal_handler.deactivate_signal("sig");
            Assert.AreEqual(signal_handler.get_signal_state("sig"), SignalHandler.SignalState.Falling);
            signal_handler.activate_signal("sig");
            signal_handler.step();
            Assert.AreEqual(signal_handler.get_signal_state("sig"), SignalHandler.SignalState.On);
        }


        [Test]
        public void test_on_already_on(){
            SignalHandler signal_handler = new SignalHandler();
            signal_handler.activate_signal("signal");
            signal_handler.step();
            signal_handler.step();
            Assert.AreEqual(signal_handler.get_signal_state("signal"), SignalHandler.SignalState.On);
            signal_handler.activate_signal("signal");
            Assert.AreEqual(signal_handler.get_signal_state("signal"), SignalHandler.SignalState.On);
        }
        [Test]
        public void test_get_signal_state_does_not_exist(){
            SignalHandler signal_handler = new SignalHandler();
            Assert.AreEqual(signal_handler.get_signal_state("signal_not_there"), SignalHandler.SignalState.Off);
        }


    }


}