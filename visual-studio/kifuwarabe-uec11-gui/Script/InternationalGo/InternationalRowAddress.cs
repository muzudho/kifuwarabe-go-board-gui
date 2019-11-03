namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    using System.Globalization;
    using KifuwarabeUec11Gui.Script.ExcelGo;

    /// <summary>
    /// 国際囲碁の行番号表記☆（＾～＾）
    /// 行番号は下端が 1。
    /// 
    /// 無理に継承するより、無関係な別オブジェクトとして作った方が混乱がないぜ☆（＾～＾）
    /// このオブジェクトは、Excel式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalRowAddress
    {
        public int Number { get; private set; }

        public InternationalRowAddress(int rowNumber)
        {
            this.Number = rowNumber;
        }

        public static (InternationalRowAddress, int) Parse(string text, int start)
        {
            var (rowAddress, next) = RowAddress.Parse(text, start);

            // 内部的には Excel表記で持つ☆（＾～＾）
            return (new InternationalRowAddress(19 - rowAddress.Number), next);
        }

        /// <summary>
        /// 下に進んだ結果を返すぜ☆（＾～＾）
        /// </summary>
        /// <param name="down"></param>
        /// <returns>行番号。</returns>
        public int ToDownMoved(int down)
        {
            return this.Number - down;
        }

        public static int ToIndex(int number)
        {
            return (19 - number);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return ToIndex(this.Number).ToString(CultureInfo.CurrentCulture);
        }
    }
}
