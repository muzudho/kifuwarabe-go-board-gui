namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class NewsInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(NewsInstructionArgumentDto matched, int curr);
        public delegate int NoneCallback();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns>Next.</returns>
        public static int Parse(
            string text,
            int start,
            SomeCallback someCallback,
            NoneCallback noneCallback)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            NewsInstructionArgumentDto newsInstructionArgument = null;

            // Trace.WriteLine($"Text            | [{text}]");
            var curr = WhiteSpaceParser.Parse(
                text,
                start,
                (_, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）
                    return curr;
                },
                ()=>
                {
                    return start;
                });

            var objectName = string.Empty;

            // 次のコロンの手前までを読み取るぜ☆（＾～＾）
            curr = WordUpToDelimiterParser.Parse(
                ":",
                text,
                curr,
                (leftSide, curr) =>
                {
                    // Trace.WriteLine($"Left side       | [{leftSide.Text}], curr={curr}");
                    objectName = leftSide.Text.Trim();

                    // コロンは読み飛ばすぜ☆（＾～＾）
                    curr++;

                    // 次のスペースは読み飛ばすぜ☆（＾～＾）
                    curr = WhiteSpaceParser.Parse(
                        text,
                        curr,
                        (_, curr) =>
                        {
                            return curr;
                        },
                        () =>
                        {
                            return curr;
                        });

                    // 行の残り全部を読み取るぜ☆（＾～＾）
                    string value = text.Substring(curr);
                    // Trace.WriteLine($"value           | {value}");

                    // 列と行の両方マッチ☆（＾～＾）
                    newsInstructionArgument = new NewsInstructionArgumentDto(objectName, value.Trim());
                    return curr + value.Length;
                },
                () =>
                {
                    // パースエラー☆（＾～＾）
                    return curr;
                });


            if (newsInstructionArgument == null || string.IsNullOrWhiteSpace(newsInstructionArgument.InstanceName))
            {
                return noneCallback();
            }
            else
            {
                return someCallback(newsInstructionArgument, curr);
            }
        }
    }
}
