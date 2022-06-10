using System;
using System.IO;

namespace Logging {

    public abstract class ILog {

        public const int DEBUG   = 5;
        protected const int TRACE   = 4;
        protected const int WARNING = 3;
        protected const int ERROR   = 2;
        protected const int WRECK   = 1;

        public abstract string Name {get;}
        public abstract int Level {get;set;}
        public abstract bool ShowStackTrace {get;set;}

        public abstract void debug (string msg);

        public abstract void debugStream (string msg, Stream streamData);

        public abstract void trace (string msg);

        public abstract void warning (string msg);

        public abstract void warning (string msg, Exception e);

        public abstract void error (string msg);

        public abstract void error (string msg, Exception e);

        public abstract void wreck (string msg);

        public abstract void wreck (string msg, Exception e);

        public abstract void Close ();

        protected static string convertLevel (int level) {
            string levelAbbr;
            switch (level) {
                case DEBUG:
                    levelAbbr = "DBG";
                    break;
                case TRACE:
                    levelAbbr = "TRA";
                    break;
                case WARNING:
                    levelAbbr = "WAR";
                    break;
                case ERROR:
                    levelAbbr = "ERR";
                    break;
                case WRECK:
                    levelAbbr = "WRE";
                    break;
                default:
                    levelAbbr = "UNK";
                    break;
            }
            return levelAbbr;
        }
    }
}