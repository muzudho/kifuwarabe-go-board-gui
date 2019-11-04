namespace KifuwarabeUec11Gui.InputScript
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// １個以上の空白☆（＾～＾）
    /// </summary>
    public class WhiteSpace
    {
        /// <summary>
        /// ホワイト・スペース☆（＾～＾）
        /// </summary>
        private static Regex regex = new Regex("^(\\s+)", RegexOptions.Compiled);

        /// <summary>
        /// マッチングした文字☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public WhiteSpace(string text)
        {
            this.Text = text;
        }

        public static (WhiteSpace, int) Parse(string text, int start)
        {
            if (text == null || text.Length <= start)
            {
                return (null, start);
            }

            var m = regex.Match(text.Substring(start));
            if (m.Success)
            {
                // 一致。
                var whiteSpaces = m.Groups[1].Value;
                return (new WhiteSpace(whiteSpaces), start + whiteSpaces.Length);
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
