using System;
using System.IO;
using System.Runtime.Remoting;
using System.Text;

namespace Logging.Logs {

	public abstract class Log : ILog {

        private readonly string name;
        private int level;
        private bool stackTrace;

	    protected Log (string name) : this (name, ERROR) {}

	    private Log (string name, int level) {
            this.name = name;
            this.level = level;
            stackTrace = true;
		}

        protected abstract void write (int pLevel, string msg);

        protected abstract void writeStream (int pLevel, string msg, Stream streamData);

        public override string Name {
            get { return name; }
        }

        public override int Level {
            get { return level; }
            set { level = value; }
        }

        public override bool ShowStackTrace {
            get { return stackTrace; }
            set { stackTrace = value; }
        }

	    private void write (int pLevel, string msg, Exception e) {
            if (level < pLevel)
                return;

            if (e == null) {
                write (pLevel, msg);
            } else {
                StringBuilder sb = new StringBuilder (msg);
                appendExceptionInfo (sb, e);
                write (pLevel, sb.ToString());
            }
        }

        private void appendExceptionInfo (StringBuilder sb, Exception e) {
            sb.Append ("\n" + e.GetType() + ": " + e.Message);
            if (stackTrace) {
                sb.Append ("\nStackTrace:\n" + e.StackTrace);
            }
            if (e.InnerException == null) {
                return;
            }
            sb.Append ("\n\nINNER EXCEPTION:");
            appendExceptionInfo (sb, e.InnerException);
        }

        public override void debug (string msg) {
            write (DEBUG, msg, null);
        }

        public override void debugStream (string msg, Stream streamData) {
            if (Level >= DEBUG)
                writeStream (DEBUG, msg, streamData);
        }

        public override void trace (string msg) {
            write (TRACE, msg, null);
        }

        public override void warning (string msg) {
            warning (msg, null);
        }

        public override void warning (string msg, Exception e) {
            write (WARNING, msg, e);
        }

        public override void error (string msg) {
            error (msg, null);
        }

        public override void error (string msg, Exception e) {
            write (ERROR, msg, e);
        }

        public override void wreck (string msg) {
            wreck (msg, null);
        }

        public override void wreck (string msg, Exception e) {
            write (WRECK, msg, e);
        }
    }
}
