namespace kifuwarabe_uec11_gui
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// `input.txt` の読取☆（＾～＾）
    /// </summary>
    public sealed class InputTextReader : IDisposable
    {
        private StreamReader StreamReader { get; set; }

        public InputTextReader(string file)
        {
            this.StreamReader = new StreamReader(file, Encoding.UTF8);
        }

        /// <summary>
        /// 行読込。
        /// </summary>
        /// <returns>読み込んだ行、またはヌル。</returns>
        public string Read()
        {
            return this.StreamReader.ReadLine();
        }

        /// <summary>
        /// 破棄。
        /// </summary>
        public void Dispose()
        {
            this.StreamReader?.Close();
            this.StreamReader = null;
        }
    }
}
