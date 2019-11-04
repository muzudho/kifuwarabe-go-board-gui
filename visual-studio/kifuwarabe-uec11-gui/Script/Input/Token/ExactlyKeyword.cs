namespace KifuwarabeUec11Gui.Script
{
    using System;

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

        public static (ExactlyKeyword, int) Parse(string word, string text, int start)
        {
            if (word == null || text == null || text.Length < start + word.Length)
            {
                return (null, start);
            }

            if (text.Substring(start).StartsWith(word, StringComparison.Ordinal))
            {
                // 一致。
                return (new ExactlyKeyword(word), start + word.Length);
            }

            return (null, start);
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
