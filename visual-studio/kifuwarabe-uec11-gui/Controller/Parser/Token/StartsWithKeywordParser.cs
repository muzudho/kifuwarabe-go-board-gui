namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using KifuwarabeUec11Gui.InputScript;

    public static class StartsWithKeywordParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(StartsWithKeyword matched, int curr);

        public static int Parse(string keyword, string text, int start, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (keyword == null || text == null || text.Length < start + keyword.Length)
            {
                return callback(null, start);
            }

            if (text.Substring(start).StartsWith(keyword, StringComparison.Ordinal))
            {
                // 一致。
                // Trace.WriteLine($"ExactlyKeyword  | keyword=[{keyword}] text=[{text}] start={start}");
                return callback(new StartsWithKeyword(keyword), start + keyword.Length);
            }

            // Trace.WriteLine($"NoExactlyKeyword| keyword=[{keyword}] text=[{text}] start={start}");
            return callback(null, start);
        }
    }
}
