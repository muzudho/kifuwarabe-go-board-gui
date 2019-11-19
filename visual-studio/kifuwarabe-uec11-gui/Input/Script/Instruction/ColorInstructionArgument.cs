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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (ColorInstructionArgument, int) Parse(string text, int start, ApplicationObjectModelForCSharp model)
        {
            var cellRanges = new List<CellRange>();
            var nextSum = start;

            // リスト☆（＾～＾）
            bool repeatsColor = true;
            while (repeatsColor)
            {
                nextSum = WhiteSpace.Parse(text, nextSum,
                    (whiteSpace, curr) =>
                    {
                        if (whiteSpace == null)
                        {
                            // 最初にスペースなんか無かった☆（＾～＾）ここで成功終了☆（＾～＾）
                            repeatsColor = false;
                            return curr;
                        }

                        // 最初のスペースを読み飛ばしたぜ☆（＾～＾）
                        return CellRange.Parse(text, curr, model, (cellRange, curr) =>
                        {
                            if (cellRange == null)
                            {
                                    // セル番地指定なんて無かった☆（＾～＾）ここで成功終了☆（＾～＾）
                                    repeatsColor = false;
                            }
                            else
                            {
                                    // セル番地指定があった☆（＾～＾）マッチで成功終了☆（＾～＾）
                                    cellRanges.Add(cellRange);
                            }

                            return curr;
                        });
                    });
            }

            // 列と行の両方マッチ☆（＾～＾）
            return (new ColorInstructionArgument(cellRanges), nextSum);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModelForCSharp model)
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
