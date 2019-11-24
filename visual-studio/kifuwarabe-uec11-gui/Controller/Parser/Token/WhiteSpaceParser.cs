namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using System.Text.RegularExpressions;
    using KifuwarabeUec11Gui.InputScript;

    public static class WhiteSpaceParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(WhiteSpace matched, int curr);

        /// <summary>
        /// ホワイト・スペース☆（＾～＾）
        /// </summary>
        private static Regex regex = new Regex("^(\\s+)", RegexOptions.Compiled);

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
    }
}
