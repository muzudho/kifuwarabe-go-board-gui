namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class WordUpToDelimiterParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(WordUpToDelimiter matched, int curr);
        public delegate int NoneCallback();

        public static int Parse(
            string delimiter,
            string text,
            int start,
            SomeCallback someCallback,
            NoneCallback noneCallback)
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (delimiter == null || text == null || text.Length <= start)
            {
                return noneCallback();
            }

            var next = text.IndexOf(delimiter, start, StringComparison.Ordinal);
            if (-1 < next)
            {
                // 一致。
                var word = text[start..next];
                return someCallback(new WordUpToDelimiter(word), next);
            }

            return noneCallback();
        }
    }
}
