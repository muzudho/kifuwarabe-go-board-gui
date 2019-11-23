namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Collections.Generic;

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
        public string RealName { get; private set; }

        /// <summary>
        /// 別名の空白区切りのリスト☆（＾～＾） 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public List<string> AliasList { get; private set; }

        public AliasInstructionArgument(string realName, List<string> aliasList)
        {
            this.RealName = realName;
            this.AliasList = aliasList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (AliasInstructionArgument, int) Parse(string text, int start)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            AliasInstructionArgument aliasInstructionArgument = null;

            var next = WhiteSpace.Parse(text, start,
                (_, curr) =>
                {
                    // 最初のスペースは読み飛ばした☆（＾～＾）

                    // 次のイコールの手前までを読み取るぜ☆（＾～＾）
                    return WordUpToDelimiter.Parse("=", text, curr, (leftSide, curr) =>
                    {
                        // イコールの手前は、本名☆（＾～＾）
                        var realName = leftSide.Text.Trim();

                        // イコールは読み飛ばすぜ☆（＾～＾）
                        curr++;

                        // 次のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpace.Parse(text, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string value = text.Substring(curr);

                                // 空白が連続していたら空文字列とか拾ってしまうが……☆（＾～＾）
                                var aliasList = new List<string>(value.Split(' '));

                                // 空白要素は削除しようぜ☆（＾～＾）
                                aliasList.RemoveAll(s => string.IsNullOrWhiteSpace(s));

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
            return $"{this.RealName} = {string.Join(' ', this.AliasList)}";
        }
    }
}
