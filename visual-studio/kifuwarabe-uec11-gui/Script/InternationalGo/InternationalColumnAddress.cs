namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    using System.Globalization;
    using KifuwarabeUec11Gui.Script.ExcelGo;

    /// <summary>
    /// 国際囲碁の列表記☆（＾～＾）
    /// I列が欠番☆（＾～＾）
    /// 
    /// 無理に継承するより、無関係な別オブジェクトとして作った方が混乱がないぜ☆（＾～＾）
    /// このオブジェクトは、Excel式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalColumnAddress
    {
        public int Number { get; private set; }

        public InternationalColumnAddress(int columnNumber)
        {
            this.Number = columnNumber;
        }

        public static (InternationalColumnAddress, int) Parse(string text, int start)
        {
            var (columnAddress, next) = ColumnAddress.Parse(text, start);

            // 国際式の囲碁では I は抜く慣習☆（＾～＾）
            if (8 <= columnAddress.Number)
            {
                columnAddress = new ColumnAddress(columnAddress.Number - 1);
            }

            return (new InternationalColumnAddress(columnAddress.Number), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）国際式囲碁盤表記☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
            var number = 65 + (this.Number < 8 ? this.Number : this.Number + 1);
            return ((char)number).ToString(CultureInfo.CurrentCulture);
        }
    }
}
