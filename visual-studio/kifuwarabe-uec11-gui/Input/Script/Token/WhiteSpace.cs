namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// １個以上の空白☆（＾～＾）
    /// </summary>
    public class WhiteSpace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whiteSpace"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(WhiteSpace whiteSpace, int curr);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="callback"></param>
        /// <returns>Next.</returns>
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
                var whiteSpaces = m.Groups[1].Value;
                return callback(new WhiteSpace(whiteSpaces), start + whiteSpaces.Length);
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
