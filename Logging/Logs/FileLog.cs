using System;
using System.IO;
using System.Text;

namespace Logging.Logs {

    public class FileLog : Log {

        private const int MAXLOG_SIZE = 10 * 1024 * 1024;
        private readonly string filename;

        public FileLog(string name, string filename) : base(name) {
            this.filename = filename;
        }

        protected override void write(int pLevel, string msg) {
            writeInternal(pLevel, msg, null);
        }

        protected override void writeStream(int pLevel, string msg, Stream streamData) {
            writeInternal(pLevel, msg, streamData);
        }

        public override void Close() { }

        private void writeInternal(int pLevel, string msg, Stream streamData) {
            lock (this) {
                if (File.Exists(filename)) {
                    FileInfo fi = new FileInfo(filename);
                    if (fi.Length > MAXLOG_SIZE) {
                        try {
                            if (File.Exists(filename + ".bak")) {
                                File.Delete(filename + ".bak");
                            }

                            File.Move(filename, filename + ".bak");
                            File.Delete(filename);
                        } catch {
                            // ignored
                        }
                    }
                }

                if (streamData == null) {
                    if (!File.Exists(filename)) {
                        string path = Path.GetDirectoryName(filename);
                        if (path != null) {
                            Directory.CreateDirectory(path);
                        }
                    }

                    StreamWriter wr = new StreamWriter(
                        new FileStream(filename, FileMode.Append),
                        Encoding.Default
                    );

                    wr.WriteLine(getFormatedDate(pLevel) + " " + msg);
                    wr.WriteLine();
                    wr.Flush();
                    wr.Close();
                } else {
                    FileStream fs = new FileStream(filename, FileMode.Append);

                    StreamWriter wrs = new StreamWriter(fs, Encoding.Default);

                    wrs.WriteLine(getFormatedDate(pLevel) + " " + msg + " message writing started");
                    wrs.Flush();
                    streamData.Position = 0;
                    wrs.Flush();
                    wrs.WriteLine();
                    wrs.WriteLine(getFormatedDate(pLevel) + " " + msg + " message writing finished");
                    wrs.WriteLine();
                    wrs.Flush();
                    wrs.Close();
                    streamData.Position = 0;
                }
            }
        }

        private static string getFormatedDate(int level) {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff zzz}] [{convertLevel(level)}]";
        }

    }

}