namespace KifuwarabeUec11Gui.InputScript
{
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
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.RealName.Value} = {string.Join(' ', this.AliasList.ConvertAll(s=>s.Value))}";
        }
    }
}
