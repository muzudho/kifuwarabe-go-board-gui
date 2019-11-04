namespace KifuwarabeUec11Gui.InputScript.InternationalGo
{
    using KifuwarabeUec11Gui.InputScript.Translator;

    /// <summary>
    /// 入力時と、出力時だけ 国際式囲碁の盤の表示に合わせろだぜ☆（＾～＾）
    /// 内部は Z字方向式だぜ☆（＾～＾）
    /// 
    /// このオブジェクトは、Z字方向式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalCellRange : CellRange
    {
        public InternationalCellRange(InternationalCellAddress startsCellAddress, InternationalCellAddress endsCellAddress)
            : base(startsCellAddress, endsCellAddress)
        {
        }

        public new static (InternationalCellRange, int) Parse(string text, int start)
        {
            var next = start;

            InternationalCellAddress startsCellAddress;
            (startsCellAddress, next) = InternationalCellAddress.Parse(text, next);
            if (startsCellAddress == null)
            {
                // 構文不一致☆（＾～＾）
                return (null, start);
            }
            // Trace.WriteLine($"InternationalCellAddress Starts | {startsCellAddress.ToDisplay()}");

            ExactlyKeyword colon;
            (colon, next) = ExactlyKeyword.Parse(":", text, next);
            if (colon == null)
            {
                // 構文不一致☆（＾～＾）

                // ここまで一致していれば、短縮形として確定するぜ☆（＾～＾）
                // 例えば `k10` は、 `k10:k10` と一致したと判定するんだぜ☆（＾～＾）
                return (new InternationalCellRange(startsCellAddress, startsCellAddress), next);
            }

            InternationalCellAddress endsCellAddress;
            (endsCellAddress, next) = InternationalCellAddress.Parse(text, next);
            if (endsCellAddress == null)
            {
                // 構文不一致☆（＾～＾）
                return (null, start);
            }
            // Trace.WriteLine($"InternationalCellAddress Ends | {endsCellAddress.ToDisplay()}");

            return (new InternationalCellRange(startsCellAddress, endsCellAddress), next);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public override string ToDisplay()
        {
            var starts = ZShapedToInternational.ConvertCellAddress(this.StartsCellAddress).ToDisplay();
            var ends = ZShapedToInternational.ConvertCellAddress(this.EndsCellAddress).ToDisplay();

            if (starts == ends)
            {
                return $"{starts}";
            }

            return $"{starts}:{ends}";
        }
    }
}
