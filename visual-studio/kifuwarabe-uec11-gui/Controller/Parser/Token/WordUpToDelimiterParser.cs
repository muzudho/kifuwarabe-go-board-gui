namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using KifuwarabeUec11Gui.InputScript;

    public static class WordUpToDelimiterParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(WordUpToDelimiter matched, int curr);

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
    }
}
