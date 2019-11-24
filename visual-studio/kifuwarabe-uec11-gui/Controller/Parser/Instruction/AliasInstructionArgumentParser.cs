namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class AliasInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (AliasInstructionArgument, int) Parse(string line, int start)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            AliasInstructionArgument aliasInstructionArgument = null;

            var next = WhiteSpaceParser.Parse(line, start,
                (_, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）

                    // 次のイコールの手前までを読み取るぜ☆（＾～＾）
                    return WordUpToDelimiterParser.Parse("=", line, curr, (leftSide, curr) =>
                    {
                        // イコールの手前は、本名☆（＾～＾）
                        var realName = new RealName(leftSide.Text.Trim());

                        // イコールは読み飛ばすぜ☆（＾～＾）
                        curr++;

                        // 次のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpaceParser.Parse(line, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string value = line.Substring(curr);

                                // 空白が連続していたら空文字列とか拾ってしまうが……☆（＾～＾）
                                var aliasListAsString = new List<string>(value.Split(' '));

                                // 空白要素は削除しようぜ☆（＾～＾）
                                aliasListAsString.RemoveAll(s => string.IsNullOrWhiteSpace(s));

                                var aliasList = aliasListAsString.ConvertAll(s => new AliasName(s));

                                aliasInstructionArgument = new AliasInstructionArgument(realName, aliasList);
                                return curr + value.Length;
                            });
                    });
                });

            return (aliasInstructionArgument, next);
        }
    }
}
