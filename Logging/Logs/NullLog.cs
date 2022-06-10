using System.IO;

namespace Logging.Logs {

    public class NullLog : Log {

        public NullLog() : base ("null") {}
    
        protected override void write (int pLevel, string msg) {
            // do nothing
        }

        protected override void writeStream (int pLevel, string msg, Stream streamData) {
            // do nothing
        }

        public override void Close () {
            // do nothing
        }
    }
}