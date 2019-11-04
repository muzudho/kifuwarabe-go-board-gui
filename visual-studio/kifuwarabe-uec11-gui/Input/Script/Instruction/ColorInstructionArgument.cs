namespace KifuwarabeUec11Gui.InputScript
{
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript.InternationalGo;


    /// <summary>
    /// black命令などの引数の部分☆（＾～＾）
    /// つまり `black k10 k11 k12` の中の `k10 k11 k12` をパースするぜ☆（＾～＾）
    /// だから white 命令にも使い回せるぜ☆（＾～＾）
    /// 
    /// `black m10 n1:n3 o11` のような混合型にも対応させようぜ☆（＾～＾）
    /// </summary>
    public class ColorInstructionArgument
    {
        /// <summary>
        /// セル範囲のリスト☆（＾～＾）
        /// </summary>
        public List<InternationalCellRange> CellRanges { get; private set; }

        public ColorInstructionArgument(List<InternationalCellRange> cellRanges)
        {
            this.CellRanges = cellRanges;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (ColorInstructionArgument, int) Parse(string text, int start)
        {
            var cellRanges = new List<InternationalCellRange>();
            var next = start;

            // リスト☆（＾～＾）最初のスペースは読み飛ばすぜ☆（＾～＾）
            for (; ; )
            {
                WhiteSpace whiteSpace;
                {
                    (whiteSpace, next) = WhiteSpace.Parse(text, next);
                    if (whiteSpace == null)
                    {
                        // おわり☆（＾～＾）
                        break;
                    }
                }

                InternationalCellRange cellRange;
                {
                    (cellRange, next) = InternationalCellRange.Parse(text, next);
                    if (cellRange == null)
                    {
                        // おわり☆（＾～＾）
                        break;
                    }
                }

                // マッチ☆（＾～＾）
                cellRanges.Add(cellRange);
            }


            // 列と行の両方マッチ☆（＾～＾）
            return (new ColorInstructionArgument(cellRanges), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            // Python言語の mapコンビネーター とかあれば１行で書けるんだが、無いからforeachループで回そうぜ☆（＾～＾）
            var tokens = new List<string>();
            foreach (var cellRange in this.CellRanges)
            {
                tokens.Add(cellRange.ToDisplay());
            }

            return $"{string.Join(' ', tokens)}";
        }
    }
}
