namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    using KifuwarabeUec11Gui.Script.ExcelGo;
    using KifuwarabeUec11Gui.Script.Translator;

    /// <summary>
    /// セルの範囲指定だぜ☆（＾～＾）
    /// `i7:k9` なら `i7 j7 k7 i8 j8 k8 i9 j9 k9` な☆（＾～＾）
    /// `k7:i9` なら `k7 j7 i7 k8 j8 i8 k9 j9 i9` な☆（＾～＾）　あっ、国際式のコンピューター囲碁に I列 は無いのだった☆（＾～＾）
    /// ただし要素をデータとして持ったらメモリを圧迫するんで、持たないぜ☆（＾～＾）
    /// 
    /// 無理に継承するより、無関係な別オブジェクトとして作った方が混乱がないぜ☆（＾～＾）
    /// このオブジェクトは、Excel式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalCellRange
    {
        public InternationalCellAddress StartsCellAddress { get; private set; }
        public InternationalCellAddress EndsCellAddress { get; private set; }

        public InternationalCellRange(InternationalCellAddress startsCellAddress, InternationalCellAddress endsCellAddress)
        {
            this.StartsCellAddress = startsCellAddress;
            this.EndsCellAddress = endsCellAddress;
        }

        public static (InternationalCellRange, int) Parse(string text, int start)
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
                return (null, start);
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

        public void Foreach(KifuwarabeUec11Gui.Script.ExcelGo.IndexCallback callback)
        {
            // 内部的には Excel式 のデータで持っているんで、Excel 式の Foreach が使えるぜ☆（＾～＾）
            var cellRange = new CellRange(
                InternationalToExcel.ConvertCellAddress(this.StartsCellAddress),
                InternationalToExcel.ConvertCellAddress(this.EndsCellAddress));
            cellRange.Foreach(callback);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.StartsCellAddress.ToDisplay()}:{this.EndsCellAddress.ToDisplay()}";
        }
    }
}
