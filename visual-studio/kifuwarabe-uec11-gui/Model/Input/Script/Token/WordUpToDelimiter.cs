namespace KifuwarabeUec11Gui.InputScript
{
    using System;

    /// <summary>
    /// 区切り記号までの単語☆（＾～＾）
    /// </summary>
    public class WordUpToDelimiter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(WordUpToDelimiter matched, int curr);

        /// <summary>
        /// マッチングした文字☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public WordUpToDelimiter(string text)
        {
            this.Text = text;
        }

        public static int Parse(string delimiter, string text, int start, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (delimiter == null || text == null || text.Length <= start)
            {
                return callback(null, start);
            }

            var next = text.IndexOf(delimiter, start, StringComparison.Ordinal);
            if (-1 < next)
            {
                // 一致。
                var word = text.Substring(start, next - start);
                return callback(new WordUpToDelimiter(word), next);
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
