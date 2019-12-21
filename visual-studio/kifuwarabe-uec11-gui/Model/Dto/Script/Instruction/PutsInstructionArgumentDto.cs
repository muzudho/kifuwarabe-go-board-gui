namespace KifuwarabeGoBoardGui.Model.Dto
{
    using KifuwarabeGoBoardGui.Model;

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
    public class PutsInstructionArgumentDto
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public CellRangeListArgumentDto Destination { get; private set; }

        public PutsInstructionArgumentDto(string name, CellRangeListArgumentDto destination)
        {
            this.Name = name;
            this.Destination = destination;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectDtoWrapper appModel)
        {
            return $"{this.Name} to {this.Destination.ToDisplay(appModel)}";
        }
    }
}
