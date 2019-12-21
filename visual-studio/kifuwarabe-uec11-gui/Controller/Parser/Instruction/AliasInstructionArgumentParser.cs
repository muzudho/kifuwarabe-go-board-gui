namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    public static class AliasInstructionArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(AliasInstructionArgument matched, int curr);
        public delegate int NoneCallback();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="start"></param>
        /// <returns>Next.</returns>
        public static int Parse(
            string line,
            int start,
            SomeCallback someCallback,
            NoneCallback noneCallback)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            AliasInstructionArgument aliasInstructionArgument = null;

            var curr = WhiteSpaceParser.Parse(
                line,
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

            // 次のイコールの手前までを読み取るぜ☆（＾～＾）
            curr = WordUpToDelimiterParser.Parse(
                "=",
                line,
                curr,
                (leftSide, curr) =>
                {
                    // イコールの手前は、本名☆（＾～＾）
                    var realName = new RealName(leftSide.Text.Trim());

                    // イコールは読み飛ばすぜ☆（＾～＾）
                    curr++;

                    // 次のスペースは読み飛ばすぜ☆（＾～＾）
                    curr = WhiteSpaceParser.Parse(line, curr,
                        (_matched, curr) =>
                        {
                            return curr;
                        },
                        () =>
                        {
                            return curr;
                        });

                    // 行の残り全部を読み取るぜ☆（＾～＾）
                    string value = line.Substring(curr);

                    // 空白が連続していたら空文字列とか拾ってしまうが……☆（＾～＾）
                    var aliasListAsString = new List<string>(value.Split(' '));

                    // 空白要素は削除しようぜ☆（＾～＾）
                    aliasListAsString.RemoveAll(s => string.IsNullOrWhiteSpace(s));

                    var aliasList = aliasListAsString.ConvertAll(s => new AliasName(s));

                    aliasInstructionArgument = new AliasInstructionArgument(realName, aliasList);
                    return curr + value.Length;
                },
                () =>
                {
                    // パース失敗☆（＾～＾）
                    return curr;
                });

            if (aliasInstructionArgument != null)
            {
                return someCallback(aliasInstructionArgument, curr);
            }
            else
            {
                return noneCallback();
            }
        }
    }
}
