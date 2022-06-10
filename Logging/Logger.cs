using System.Collections.Generic;

using Logging.Logs;

namespace Logging {

    public class Logger {

        private static readonly object mutex = new object();
        private static Logger instance;

        private readonly IDictionary<string, ILog> logs;
        private readonly ILog nullLog;

        private Logger() {
            logs = new Dictionary<string, ILog>();
            nullLog = new NullLog();
        }

        public static Logger Instance {
            get {
                if (instance == null) {
                    lock (mutex) {
                        if (instance == null) {
                            instance = new Logger();
                        }
                    }
                }
                return instance;
            }
        }

        public ILog GetLog(string logName) {
            return GetLog(logName, true);
        }

        private ILog GetLog(string logName, bool returnNullLog) {
            if (logs.ContainsKey(logName)) {
                return logs[logName];
            }
            return returnNullLog
                ? nullLog
                : null;
        }

        public void RegisterLog(ILog log) {
            logs.Add(log.Name, log);
        }

        public void UnRegisterLog(string logName) {
            if (!logs.ContainsKey(logName)) {
                return;
            }
            logs[logName].Close();
            logs.Remove(logName);
        }
    }
}
