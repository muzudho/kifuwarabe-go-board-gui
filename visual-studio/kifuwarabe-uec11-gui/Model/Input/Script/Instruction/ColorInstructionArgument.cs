namespace KifuwarabeUec11Gui.InputScript
{
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.Model;


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
        public List<CellRange> CellRanges { get; private set; }

        public ColorInstructionArgument(List<CellRange> cellRanges)
        {
            this.CellRanges = cellRanges;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModelWrapper model)
        {
            // Python言語の mapコンビネーター とかあれば１行で書けるんだが、無いからforeachループで回そうぜ☆（＾～＾）
            var tokens = new List<string>();
            foreach (var cellRange in this.CellRanges)
            {
                tokens.Add(cellRange.ToDisplay(model));
            }

            return $"{string.Join(' ', tokens)}";
        }
    }
}
