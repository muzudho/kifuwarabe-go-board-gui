namespace KifuwarabeGoBoardGui
{
    using System;
    using System.IO;
    using System.Text;
    using KifuwarabeGoBoardGui.Controller.Parser;

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

        private InputTextReader(string file)
        {
            this.File = file;
        }

        public static InputTextReader OpenOrCreate(string file)
        {
            var instance = new InputTextReader(file)
            {
                FileStreamR = new System.IO.FileStream(
                file,
                FileMode.OpenOrCreate,
                FileAccess.Read,
                FileShare.ReadWrite)
            };
            // Encoding.UTF8 を指定すると BOM付きUTF8、無指定だと BOM無しUTF8 だぜ☆（＾～＾）
            instance.StreamReader = new System.IO.StreamReader(instance.FileStreamR);

            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">読み込んだ行</param>
        public delegate void CallbackDone(string text);
        public delegate void CallbackErr(System.IO.IOException e);
        /// <summary>
        /// ファイル全部読み込む。
        /// </summary>
        /// <param name="callbackDone"></param>
        /// <param name="callbackErr"></param>
        public void ReadToEnd(CallbackDone callbackDone, CallbackErr callbackErr)
        {
            if (callbackDone == null)
            {
                throw new ArgumentNullException(nameof(callbackDone));
            }

            if (callbackErr == null)
            {
                throw new ArgumentNullException(nameof(callbackErr));
            }

            try
            {
                var sourceText = this.StreamReader.ReadToEnd();
                var sleeped = false;
                var topText = new StringBuilder();
                var bottomText = new StringBuilder();

                foreach (var line in sourceText.Split(Environment.NewLine))
                {
                    if (sleeped)
                    {
                        bottomText.AppendLine(line);
                    }
                    else
                    {
                        // 特殊処理： ざっくり sleep で始まる行は、 sleep コマンドとします。
                        if (line.TrimStart().StartsWith(InputLineParserLv1.SleepsCommand, StringComparison.CurrentCulture))
                        {
                            sleeped = true;
                        }

                        topText.AppendLine(line);
                    }
                }


                // ファイルの先頭に読込位置を戻す。
                this.FileStreamR.Position = 0;

                // 書込み用ストリーム☆（＾～＾）
                // Encoding.UTF8 を指定すると BOM付きUTF8、無指定だと BOM無しUTF8 だぜ☆（＾～＾）
                using (var writer = new StreamWriter(this.File, false))
                {
                    // sleep 行より下で上書き、あるいはファイルを空にするぜ☆（＾～＾）
                    writer.Write(bottomText.ToString());
                    writer.Flush();
                }

                callbackDone(topText.ToString());
            }
            catch (System.IO.IOException e)
            {
                // ファイルのロックにでも引っかかったんだろ☆（＾～＾）ミリ秒でアクセスを繰り返せば よくある☆（＾～＾）
                // 無視するぜ☆（＾～＾）また次のループで読みにくるだろ☆（＾～＾）
                callbackErr(e);
            }
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
