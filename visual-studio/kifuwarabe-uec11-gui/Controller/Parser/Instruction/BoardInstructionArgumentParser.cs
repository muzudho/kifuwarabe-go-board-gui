namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class BoardInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (BoardInstructionArgument, int) Parse(string text, int start, ApplicationObjectModelWrapper model)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            // 行番号を読めだぜ☆（＾～＾）数字とは限らないからな☆ｍ９（＾～＾）
            BoardInstructionArgument boardInstructionArgument = null;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            var next = WhiteSpaceParser.Parse(text, start,
                (_, curr) =>
                {
                    return RowAddressParser.Parse(text, curr, model, (rowAddress, curr) =>
                    {
                        if (rowAddress == null)
                        {
                            // 不一致☆（＾～＾）
                            return start;
                        }

                        // 途中のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpaceParser.Parse(text, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string columns = text.Substring(curr);

                                // 列と行の両方マッチ☆（＾～＾）
                                boardInstructionArgument = new BoardInstructionArgument(rowAddress, columns.Trim());
                                return curr + columns.Length;
                            });
                    });
                });

            return (boardInstructionArgument, next);
        }
    }
}
