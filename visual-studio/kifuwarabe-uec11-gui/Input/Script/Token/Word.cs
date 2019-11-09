namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 次の空白までの文字列☆（＾～＾）
    /// </summary>
    public class Word
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whiteSpace"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(Word word, int curr);

        /// <summary>
        /// 英語がいうところの、単語☆（＾～＾）
        /// </summary>
        private static Regex regex = new Regex("^(\\w+)", RegexOptions.Compiled);

        /// <summary>
        /// マッチングした文字☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public Word(string text)
        {
            this.Text = text;
        }

        public static int Parse(string text, int start, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (text == null || text.Length <= start)
            {
                return callback(null, start);
            }

            var m = regex.Match(text.Substring(start));
            if (m.Success)
            {
                // 一致。
                var word = m.Groups[1].Value;
                return callback(new Word(word), start + word.Length);
            }

            return callback(null, start);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return this.Text;
        }
    }
}
