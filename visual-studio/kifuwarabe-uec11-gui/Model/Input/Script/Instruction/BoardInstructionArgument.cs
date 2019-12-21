namespace KifuwarabeGoBoardGui.InputScript
{
    using System;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model;

    /// <summary>
    /// `board 19 .www.bbb...........` みたいなコマンドの引数☆（＾～＾）
    /// `board {row-number} {column-chars}` みたいな感じだな☆（＾～＾）
    /// </summary>
    public class BoardInstructionArgument
    {
        /// <summary>
        /// 行番号だぜ☆（＾～＾）
        /// </summary>
        public RowAddress RowAddress { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Columns { get; private set; }

        public BoardInstructionArgument(RowAddress rowAddress, string columns)
        {
            this.RowAddress = rowAddress;
            this.Columns = columns;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModelWrapper appModel)
        {
            return $"{this.RowAddress.ToDisplayTrimed(appModel)} {this.Columns}";
        }
    }
}
