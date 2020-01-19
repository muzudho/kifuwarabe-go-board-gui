namespace KifuwarabeGoBoardGui.Model.Dto
{
    using KifuwarabeGoBoardGui.Model;

    /// <summary>
    /// 次のようなコマンド☆（＾～＾）置くのは色☆（＾～＾）
    /// 
    /// `put black to K10`
    /// `put white to L11`
    /// 
    /// 構造としては
    /// 
    /// `put {colorName} to {cellAddress}`
    /// 
    /// だぜ☆（＾～＾）
    /// </summary>
    public class PutsInstructionArgumentDto
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public ColorNameDto ColorName { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public CellRangeListArgumentDto Destination { get; private set; }

        public PutsInstructionArgumentDto(ColorNameDto colorName, CellRangeListArgumentDto destination)
        {
            this.ColorName = colorName;
            this.Destination = destination;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectDtoWrapper appModel)
        {
            return $"{this.ColorName.Source} to {this.Destination.ToDisplay(appModel)}";
        }
    }
}
