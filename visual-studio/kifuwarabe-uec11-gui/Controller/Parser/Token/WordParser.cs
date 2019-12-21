namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using System.Text.RegularExpressions;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class WordParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(Word matched, int curr);
        public delegate int NoneCallback();

        /// <summary>
        /// 英語がいうところの、単語☆（＾～＾）
        /// </summary>
        private static Regex regex = new Regex("^(\\w+)", RegexOptions.Compiled);

        public static int Parse(
            string text,
            int start,
            SomeCallback someCallback,
            NoneCallback noneCallback)
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (text == null || text.Length <= start)
            {
                return noneCallback();
            }

            var m = regex.Match(text.Substring(start));
            if (m.Success)
            {
                // 一致。
                var word = m.Groups[1].Value;
                return someCallback(new Word(word), start + word.Length);
            }

            return noneCallback();
        }
    }
}
