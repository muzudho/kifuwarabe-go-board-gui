using System.Collections.Generic;

namespace kifuwarabe_uec11_gui.API
{
    /// <summary>
    /// black命令などの引数の部分☆（＾～＾）
    /// つまり `black k10 k11 k12` の中の `k10 k11 k12` をパースするぜ☆（＾～＾）
    /// だから white 命令にも使い回せるぜ☆（＾～＾）
    /// </summary>
    public class ColorInstructionParameter
    {
        /// <summary>
        /// セル番地のリスト☆（＾～＾）
        /// </summary>
        public List<CellAddress> CellAddressList { get; private set; }

        public ColorInstructionParameter(List<CellAddress> cellAddressList)
        {
            this.CellAddressList = cellAddressList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (ColorInstructionParameter, int) Parse(string text, int start)
        {
            var cellAddressList = new List<CellAddress>();
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

                CellAddress cellAddress;
                {
                    (cellAddress, next) = CellAddress.Parse(text, next);
                    if (cellAddress == null)
                    {
                        // おわり☆（＾～＾）
                        break;
                    }
                }

                // マッチ☆（＾～＾）
                cellAddressList.Add(cellAddress);
            }


            // 列と行の両方マッチ☆（＾～＾）
            return (new ColorInstructionParameter(cellAddressList), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            // Python言語の mapコンビネーター とかあれば１行で書けるんだが、無いからforeachループで回そうぜ☆（＾～＾）
            var tokens = new List<string>();
            foreach (var cellAddress in this.CellAddressList)
            {
                tokens.Add(cellAddress.ToDisplay());
            }

            return $"{string.Join(' ', tokens)}";
        }
    }
}
