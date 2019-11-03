namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    using System.Globalization;
    using KifuwarabeUec11Gui.Script.ZShaped;

    /// <summary>
    /// 国際囲碁の列表記☆（＾～＾）
    /// I列が欠番☆（＾～＾）
    /// 
    /// このオブジェクトは、Z字方向式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalColumnAddress : ColumnAddress
    {
        public InternationalColumnAddress(int columnNumber)
            : base(columnNumber)
        {
        }

        public new static (InternationalColumnAddress, int) Parse(string text, int start)
        {
            var (columnAddress, next) = ColumnAddress.Parse(text, start);

            // 国際式の囲碁では I は抜く慣習☆（＾～＾）
            if (8 == columnAddress.NumberO0)
            {
                // 不一致☆（＾～＾）
                return (null, start);
            }
            else if (8 < columnAddress.NumberO0)
            {
                columnAddress = new ColumnAddress(columnAddress.NumberO0 - 1);
            }

            return (new InternationalColumnAddress(columnAddress.NumberO0), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）国際式囲碁盤表記☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public override string ToDisplay()
        {
            // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
            var number = 65 + (this.NumberO0 < 8 ? this.NumberO0 : this.NumberO0 + 1);
            return ((char)number).ToString(CultureInfo.CurrentCulture);
        }
    }
}
