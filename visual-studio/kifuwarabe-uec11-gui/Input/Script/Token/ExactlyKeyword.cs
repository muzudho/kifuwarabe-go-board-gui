namespace KifuwarabeUec11Gui.InputScript
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exactlyKeyword"></param>
    /// <param name="curr">Current.</param>
    /// <returns>Next.</returns>
    public delegate int ParsesExactlyKeywordCallbackType(ExactlyKeyword exactlyKeyword, int curr);

    /// <summary>
    /// キーワードの完全一致☆（＾～＾）
    /// </summary>
    public class ExactlyKeyword
    {
        /// <summary>
        /// キーワード☆（＾～＾）
        /// </summary>
        private string Word { get; set; }

        public ExactlyKeyword(string word)
        {
            this.Word = word;
        }

        public static int Parse(string word, string text, int start, ParsesExactlyKeywordCallbackType callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (word == null || text == null || text.Length < start + word.Length)
            {
                return callback(null, start);
            }

            if (text.Substring(start).StartsWith(word, StringComparison.Ordinal))
            {
                // 一致。
                return callback(new ExactlyKeyword(word), start + word.Length);
            }

            return callback(null, start);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return this.Word;
        }
    }
}
