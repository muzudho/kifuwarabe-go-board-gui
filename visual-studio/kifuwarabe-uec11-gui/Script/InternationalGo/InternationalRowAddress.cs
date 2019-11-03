namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    using System.Globalization;
    using KifuwarabeUec11Gui.Script.ExcelGo;

    /// <summary>
    /// 国際囲碁の行番号表記☆（＾～＾）
    /// 行番号は下端が 1。
    /// 
    /// このオブジェクトは、Excel式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalRowAddress : RowAddress
    {
        public InternationalRowAddress(int rowNumber)
            : base(rowNumber)
        {
        }

        public new static (InternationalRowAddress, int) Parse(string text, int start)
        {
            var (rowAddress, next) = RowAddress.Parse(text, start);

            // 内部的には Excel表記で持つ☆（＾～＾）
            return (new InternationalRowAddress(19 - rowAddress.Number), next);
        }

        public static int ToIndex(int number)
        {
            return (19 - number);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public override string ToDisplay()
        {
            return ToIndex(this.Number).ToString(CultureInfo.CurrentCulture);
        }
    }
}
