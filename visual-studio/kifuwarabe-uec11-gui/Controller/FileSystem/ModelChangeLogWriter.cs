namespace KifuwarabeUec11Gui
{
    using System;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// モデルの変更を書き込むことを想定したロガー。
    /// デバッグ目的☆（＾～＾）
    /// 自動テストのときはオフにしておかないと、ファイルを書き込みあってエラーになるぜ☆（＾～＾）
    /// </summary>
    public sealed class ModelChangeLogWriter : IDisposable
    {
        /// <summary>
        /// 書込み用ストリーム☆（＾～＾）
        /// </summary>
        private StreamWriter StreamWriter { get; set; }

        public ModelChangeLogWriter(string file)
        {
            // 並列処理でロックしてしまうので初期値は false にしろだぜ☆（＾～＾）
            this.Enable = false;

            this.File = file;
        }

        public bool Enable { get; set; }

        private string File { get; set; }

        private void Activate()
        {
            if (this.Enable)
            {
                if (this.StreamWriter == null)
                {
                    // 追加書き込みモードでファイルを開けるぜ☆（＾～＾）
                    // Encoding.UTF8 を付けると BOM有り になってしまう☆（＾～＾）省略すれば BOM無し☆（＾～＾）
                    this.StreamWriter = new StreamWriter(this.File, true);
                }
            }
        }

        /// <summary>
        /// TODO ログが大量になってしまうので、変化してない場合はログに書き込まないぜ☆（＾～＾）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void WriteLine(string name, string oldValue, string newValue)
        {
            if (this.Enable)
            {
                this.Activate();

                if (string.Compare(oldValue, newValue, false, CultureInfo.CurrentCulture) != 0)
                {
                    this.StreamWriter.WriteLine($"Change          | [{name}] = [{oldValue}] --> [{newValue}].");
                }
                //else
                //{
                //    Trace.WriteLine($"NoChange        | [{name}] = [{oldValue}] --> [{newValue}].");
                //}
            }
        }

        public void WriteLine(string text)
        {
            if (this.Enable)
            {
                this.Activate();

                this.StreamWriter.WriteLine(text);
            }
        }

        public void Flush()
        {
            if (this.Enable)
            {
                this.Activate();

                this.StreamWriter.Flush();
            }
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
