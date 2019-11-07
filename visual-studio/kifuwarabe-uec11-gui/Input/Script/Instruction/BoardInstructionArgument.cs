namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using KifuwarabeUec11Gui.Output;

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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (BoardInstructionArgument, int) Parse(string text, int start, BoardModel boardModel)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var next = start;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            {
                (_, next) = WhiteSpace.Parse(text, next);
            }

            // 行番号を読めだぜ☆（＾～＾）数字とは限らないからな☆ｍ９（＾～＾）
            RowAddress rowAddress;
            (rowAddress, next) = RowAddress.Parse(text, next, boardModel);
            if (rowAddress == null)
            {
                // 不一致☆（＾～＾）
                return (null, start);
            }

            // 途中のスペースは読み飛ばすぜ☆（＾～＾）
            {
                (_, next) = WhiteSpace.Parse(text, next);
            }

            // 行の残り全部を読み取るぜ☆（＾～＾）
            string columns = text.Substring(next);

            // 列と行の両方マッチ☆（＾～＾）
            return (new BoardInstructionArgument(rowAddress, columns.Trim()), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(BoardModel model)
        {
            return $"{this.RowAddress.ToDisplayTrimed(model)} {this.Columns}";
        }
    }
}
