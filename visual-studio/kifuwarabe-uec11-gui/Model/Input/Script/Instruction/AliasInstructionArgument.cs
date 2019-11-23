namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 次のようなコマンド☆（＾～＾）
    /// 
    /// `alias top1 = move sasite`
    /// `alias right1 = b-name black-name player1name`
    /// 
    /// 構造としては
    /// 
    /// `alias {realName} = {別名の空白区切りのリスト}`
    /// </summary>
    public class AliasInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public RealName RealName { get; private set; }

        /// <summary>
        /// 別名の空白区切りのリスト☆（＾～＾） 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public List<AliasName> AliasList { get; private set; }

        public AliasInstructionArgument(RealName realName, List<AliasName> aliasList)
        {
            this.RealName = realName;
            this.AliasList = aliasList;
        }

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

            var next = WhiteSpace.Parse(line, start,
                (_, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）

                    // 次のイコールの手前までを読み取るぜ☆（＾～＾）
                    return WordUpToDelimiter.Parse("=", line, curr, (leftSide, curr) =>
                    {
                        // イコールの手前は、本名☆（＾～＾）
                        var realName = new RealName(leftSide.Text.Trim());

                        // イコールは読み飛ばすぜ☆（＾～＾）
                        curr++;

                        // 次のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpace.Parse(line, curr,
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

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.RealName.Value} = {string.Join(' ', this.AliasList.ConvertAll(s=>s.Value))}";
        }
    }
}
