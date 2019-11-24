namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using KifuwarabeUec11Gui.InputScript;

    public static class PutsInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (PutsInstructionArgument, int) Parse(string text, int start)
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

                        // 次のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpaceParser.Parse(text, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string destination = text.Substring(curr);
                                // Trace.WriteLine($"value           | {value}");

                                // 列と行の両方マッチ☆（＾～＾）
                                putsInstructionArgument = new PutsInstructionArgument(objectName, destination.Trim());
                                return curr + destination.Length;
                            });
                    });
                });

            return (putsInstructionArgument, next);
        }
    }
}
