namespace KifuwarabeUec11Gui.Script
{
    using System;

    /// <summary>
    /// 区切り記号までの単語☆（＾～＾）
    /// </summary>
    public class WordUpToDelimiter
    {
        /// <summary>
        /// マッチングした文字☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public WordUpToDelimiter(string text)
        {
            this.Text = text;
        }

        public static (WordUpToDelimiter, int) Parse(string delimiter, string text, int start)
        {
            if (delimiter == null || text == null || text.Length <= start)
            {
                return (null, start);
            }

            var next = text.IndexOf(delimiter, start, StringComparison.Ordinal);
            if (-1 < next)
            {
                // 一致。
                var word = text.Substring(start, next - start);
                return (new WordUpToDelimiter(word), next);
            }

            return (null, start);
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
