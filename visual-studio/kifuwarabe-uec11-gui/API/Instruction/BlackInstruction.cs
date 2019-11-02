using System.Collections.Generic;
using System.Text;

namespace kifuwarabe_uec11_gui.API
{
    /// <summary>
    /// black命令☆（＾～＾）
    /// `black k10 k11 k12` みたいにして黒石を置いていこうぜ☆（＾～＾）
    /// </summary>
    public class BlackInstruction
    {
        /// <summary>
        /// セル番地のリスト☆（＾～＾）
        /// </summary>
        public List<CellAddress> CellAddressList { get; private set; }

        public BlackInstruction(List<CellAddress> cellAddressList)
        {
            this.CellAddressList = cellAddressList;
        }

        public static (BlackInstruction, int) Parse(string text, int start)
        {
            var cellAddressList = new List<CellAddress>();
            var next = start;

            ExactlyKeyword black;
            {
                (black, next) = ExactlyKeyword.Parse("black", text, next);
                if (black == null)
                {
                    // 非マッチ☆（＾～＾）
                    return (null, start);
                }
            }

            // あとはリスト☆（＾～＾）
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
            return (new BlackInstruction(cellAddressList), next);
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
