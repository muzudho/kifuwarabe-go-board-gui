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
        public delegate int SomeCallback(StartsWithKeyword matched, int curr);
        public delegate int NoneCallback();

        public static int Parse(
            string keyword,
            string text,
            int start,
            SomeCallback someCallback,
            NoneCallback noneCallback
            )
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (keyword == null || text == null || text.Length < start + keyword.Length)
            {
                return noneCallback();
            }

            if (text.Substring(start).StartsWith(keyword, StringComparison.Ordinal))
            {
                // 一致。
                // Trace.WriteLine($"ExactlyKeyword  | keyword=[{keyword}] text=[{text}] start={start}");
                return someCallback(new StartsWithKeyword(keyword), start + keyword.Length);
            }
            else
            {
                // Trace.WriteLine($"NoExactlyKeyword| keyword=[{keyword}] text=[{text}] start={start}");
                return noneCallback();
            }
        }
    }
}
