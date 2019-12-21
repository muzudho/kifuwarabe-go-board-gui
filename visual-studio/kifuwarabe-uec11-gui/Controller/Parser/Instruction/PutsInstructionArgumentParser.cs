namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    public static class PutsInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(PutsInstructionArgument matched, int curr);
        public delegate int NoneCallback();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int Parse(
            string text,
            int start,
            ApplicationObjectModelWrapper appModel,
            SomeCallback someCallback,
            NoneCallback noneCallback
            )
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            PutsInstructionArgument putsInstructionArgument = null;

            // Trace.WriteLine($"Text            | [{text}]");
            var curr = WhiteSpaceParser.Parse(
                text,
                start,
                (matched, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）
                    return curr;

                },
                () =>
                {
                    return start;
                });

            var objectName = string.Empty;

            // 次の `to` の手前までを読み取るぜ☆（＾～＾）
            curr = WordUpToDelimiterParser.Parse(
                "to",
                text,
                curr,
                (leftSide, curr) =>
                {
                    // Trace.WriteLine($"Left side       | [{leftSide.Text}], curr={curr}");

                    // 左辺の、次のドットの手前までを読み取るぜ☆（＾～＾）
                    objectName = leftSide.Text.Trim();
                    // Trace.WriteLine($"objectName      | {objectName}");

                    // `to` は読み飛ばすぜ☆（＾～＾）
                    curr += "to".Length;
                    // Trace.WriteLine($"curr            | {curr}");

                    // 残りはセル範囲のリストだぜ☆（＾～＾）
                    return CellRangeListArgumentParser.Parse(
                        text,
                        curr,
                        appModel,
                        (arg, curr) =>
                        {
                            putsInstructionArgument = new PutsInstructionArgument(objectName, arg);
                            return curr;
                        },
                        () =>
                        {
                            // パース失敗☆（＾～＾）
                            return curr;
                        });
                },
                () =>
                {
                    // パース失敗☆（＾～＾）
                    return curr;
                });

            if (putsInstructionArgument != null)
            {
                return someCallback(putsInstructionArgument, curr);
            }
            else
            {
                return noneCallback();
            }
        }
    }
}
