namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using System.Text.RegularExpressions;
    using KifuwarabeUec11Gui.InputScript;

    public static class WordParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(Word matched, int curr);

        /// <summary>
        /// 英語がいうところの、単語☆（＾～＾）
        /// </summary>
        private static Regex regex = new Regex("^(\\w+)", RegexOptions.Compiled);

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
    }
}
