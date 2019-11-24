using KifuwarabeUec11Gui.Model;

namespace KifuwarabeUec11Gui.InputScript
{
    /// <summary>
    /// 次のようなコマンド☆（＾～＾）
    /// 
    /// `put black to K10`
    /// `put white to L11`
    /// 
    /// 構造としては
    /// 
    /// `put {name} to {cellAddress}`
    /// 
    /// だぜ☆（＾～＾）
    /// </summary>
    public class PutsInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public CellRangeListArgument Destination { get; private set; }

        public PutsInstructionArgument(string name, CellRangeListArgument destination)
        {
            this.Name = name;
            this.Destination = destination;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModelWrapper appModel)
        {
            return $"{this.Name} to {this.Destination.ToDisplay(appModel)}";
        }
    }
}
