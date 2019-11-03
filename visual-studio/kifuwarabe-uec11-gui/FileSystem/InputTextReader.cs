namespace KifuwarabeUec11Gui
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// `input.txt` の読取☆（＾～＾）
    /// TODO `input.txt` ファイルが必要☆（＾～＾）
    /// ファイルにロックを掛けずに開くことが重要☆（＾～＾）
    /// </summary>
    public sealed class InputTextReader : IDisposable
    {
        /// <summary>
        /// 読込用ファイル・ストリーム☆（＾～＾）
        /// </summary>
        private FileStream FileStreamR { get; set; }

        /// <summary>
        /// 読込用ストリーム・リーダー☆（＾～＾）
        /// </summary>
        private StreamReader StreamReader { get; set; }

        /// <summary>
        /// ファイル名☆（＾～＾）
        /// </summary>
        private string File { get; set; }

        public InputTextReader(string file)
        {
            this.File = file;
            this.FileStreamR = new System.IO.FileStream(
                file,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite);
            // Encoding.UTF8 を指定すると BOM付きUTF8、無指定だと BOM無しUTF8 だぜ☆（＾～＾）
            this.StreamReader = new System.IO.StreamReader(this.FileStreamR);
        }

        /// <summary>
        /// ファイル全部読み込む。
        /// </summary>
        /// <returns>読み込んだ行、またはヌル。</returns>
        public string ReadToEnd()
        {
            var text = this.StreamReader.ReadToEnd();

            // ファイルの先頭に読込位置を戻す。
            this.FileStreamR.Position = 0;

            // 書込み用ストリーム☆（＾～＾）
            // Encoding.UTF8 を指定すると BOM付きUTF8、無指定だと BOM無しUTF8 だぜ☆（＾～＾）
            using (var writer = new StreamWriter(this.File, false))
            {
                // ファイルを空にするぜ☆（＾～＾）
                writer.Write("");
                writer.Flush();
            }

            return text;
        }

        /// <summary>
        /// 破棄。
        /// </summary>
        public void Dispose()
        {
            this.FileStreamR?.Close();
            this.FileStreamR = null;

            this.StreamReader?.Close();
            this.StreamReader = null;
        }
    }
}
