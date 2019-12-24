namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class PutsInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(PutsInstructionArgumentDto matched, int curr);
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
            ApplicationObjectDtoWrapper appModel,
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

            PutsInstructionArgumentDto putsInstructionArgument = null;

            // Trace.WriteLine($"Text            | [{text}]");
            var curr1 = WhiteSpaceParser.Parse(
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

            var colorName = string.Empty;

            // 次の `to` の手前までを読み取るぜ☆（＾～＾）
            var curr4 = WordUpToDelimiterParser.Parse(
                "to",
                text,
                curr1,
                (leftSide, curr2) =>
                {
                    // Trace.WriteLine($"Left side       | [{leftSide.Text}], curr={curr}");

                    // 左辺の、次のドットの手前までを読み取るぜ☆（＾～＾）
                    colorName = leftSide.Text.Trim();
                    // Trace.WriteLine($"objectName      | {objectName}");

                    // `to` は読み飛ばすぜ☆（＾～＾）
                    curr2 += "to".Length;
                    // Trace.WriteLine($"curr            | {curr}");

                    // 残りはセル範囲のリストだぜ☆（＾～＾）
                    return CellRangeListArgumentParser.Parse(
                        text,
                        curr2,
                        appModel,
                        (arg, curr3) =>
                        {
                            putsInstructionArgument = new PutsInstructionArgumentDto(colorName, arg);
                            return curr3;
                        },
                        () =>
                        {
                            // パース失敗☆（＾～＾）
                            return curr2;
                        });
                },
                () =>
                {
                    // パース失敗☆（＾～＾）
                    return curr1;
                });

            if (putsInstructionArgument != null)
            {
                return someCallback(putsInstructionArgument, curr4);
            }
            else
            {
                return noneCallback();
            }
        }
    }
}
