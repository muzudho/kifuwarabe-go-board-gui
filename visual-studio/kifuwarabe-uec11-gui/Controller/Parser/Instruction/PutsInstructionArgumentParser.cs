namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class PutsInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(PutsInstructionArgument matched, int curr);

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
            ParsesCallback doneCallback
            )
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            PutsInstructionArgument putsInstructionArgument = null;

            // Trace.WriteLine($"Text            | [{text}]");
            var next = WhiteSpaceParser.Parse(text, start,
                (_, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）

                    var objectName = string.Empty;

                    // 次の `to` の手前までを読み取るぜ☆（＾～＾）
                    return WordUpToDelimiterParser.Parse("to", text, curr, (leftSide, curr) =>
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
                            (arg, curr) => {
                                putsInstructionArgument = new PutsInstructionArgument(objectName, arg);
                                return curr;
                            });
                    });
                });

            return doneCallback(putsInstructionArgument, next);
        }
    }
}
