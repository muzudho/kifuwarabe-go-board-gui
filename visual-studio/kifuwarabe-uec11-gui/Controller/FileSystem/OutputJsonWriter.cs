namespace KifuwarabeGoBoardGui
{
    using System;
    using System.IO;

    /// <summary>
    /// `output.txt` ファイル用のライター☆（＾～＾）
    /// あとで何かほしくなるかも知らんからラッピングしておくぜ☆（＾～＾）
    /// </summary>
    public sealed class OutputJsonWriter : IDisposable
    {
        /// <summary>
        /// 書込み用ストリーム☆（＾～＾）
        /// </summary>
        private StreamWriter StreamWriter { get; set; }

        public OutputJsonWriter(string file)
        {
            // 上書きモードでファイルを開けるぜ☆（＾～＾）
            // Encoding.UTF8 を付けると BOM有り になってしまう☆（＾～＾）省略すれば BOM無し☆（＾～＾）
            this.StreamWriter = new StreamWriter(file, false);
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
