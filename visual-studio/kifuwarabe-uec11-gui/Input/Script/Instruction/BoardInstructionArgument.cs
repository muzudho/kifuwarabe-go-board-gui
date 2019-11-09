namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using KifuwarabeUec11Gui.Model;

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
        public static (BoardInstructionArgument, int) Parse(string text, int start, ApplicationObjectModel model)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            // 行番号を読めだぜ☆（＾～＾）数字とは限らないからな☆ｍ９（＾～＾）
            BoardInstructionArgument boardInstructionArgument = null;

            // 最初のスペースは読み飛ばすぜ☆（＾～＾）
            var next = WhiteSpace.Parse2(text, start,
                (_, curr) =>
                {
                    RowAddress rowAddress = null;
                    (rowAddress, curr) = RowAddress.Parse(text, curr, model);

                    if (rowAddress == null)
                    {
                        // 不一致☆（＾～＾）
                        return start;
                    }
                    else
                    {
                        // 途中のスペースは読み飛ばすぜ☆（＾～＾）
                        return WhiteSpace.Parse2(text, curr,
                            (_, curr) =>
                            {
                                // 行の残り全部を読み取るぜ☆（＾～＾）
                                string columns = text.Substring(curr);

                                // 列と行の両方マッチ☆（＾～＾）
                                boardInstructionArgument = new BoardInstructionArgument(rowAddress, columns.Trim());
                                return curr + columns.Length;
                            });
                    }
                });

            return (boardInstructionArgument, next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModel model)
        {
            return $"{this.RowAddress.ToDisplayTrimed(model)} {this.Columns}";
        }
    }
}
