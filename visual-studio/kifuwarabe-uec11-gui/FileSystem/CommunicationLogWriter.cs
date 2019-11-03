namespace KifuwarabeUec11Gui
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// 特に通信ログを書き込むことを想定したロガー。
    /// In/Out の区別を付けるメソッドとか　あとでほしくなるかも知らんからラッピングしておくぜ☆（＾～＾）
    /// </summary>
    public sealed class CommunicationLogWriter : IDisposable
    {
        /// <summary>
        /// 書込み用ストリーム☆（＾～＾）
        /// </summary>
        private StreamWriter StreamWriter { get; set; }

        public CommunicationLogWriter(string file)
        {
            // 追加書き込みモードでファイルを開きます。
            this.StreamWriter = new StreamWriter(file, true, Encoding.UTF8);
        }

        public void WriteLine(string text)
        {
            this.StreamWriter.WriteLine(text);
        }

        public void Flush()
        {
            this.StreamWriter.Flush();
        }

        /// <summary>
        /// 破棄。
        /// </summary>
        public void Dispose()
        {
            this.StreamWriter?.Close();
            this.StreamWriter = null;
        }
    }
}
