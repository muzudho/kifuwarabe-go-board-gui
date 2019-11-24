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
    public class CellRangeListArgument
    {
        /// <summary>
        /// セル範囲のリスト☆（＾～＾）
        /// </summary>
        public List<CellRange> CellRanges { get; private set; }

        public CellRangeListArgument(List<CellRange> cellRanges)
        {
            this.CellRanges = cellRanges;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModelWrapper model)
        {
            // CellRange型のリストを、CellRangeを.ToDisplay()した結果のリストに変換するぜ☆（＾～＾） S -> T は、こういうやつを言う☆（＾～＾）
            var displays = this.CellRanges.ConvertAll(s => s.ToDisplay(model));

            // スペース区切りで返すぜ☆（＾～＾）
            return string.Join(' ', displays);
        }
    }
}
