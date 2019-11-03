namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    /// <summary>
    /// 国際囲碁のセル番地表記☆（＾～＾）
    /// 
    /// 無理に継承するより、無関係な別オブジェクトとして作った方が混乱がないぜ☆（＾～＾）
    /// このオブジェクトは、Excel式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalCellAddress
    {
        public InternationalColumnAddress ColumnAddress { get; private set; }
        public InternationalRowAddress RowAddress { get; private set; }

        public InternationalCellAddress(InternationalRowAddress rowAddress, InternationalColumnAddress columnAddress)
        {
            this.RowAddress = rowAddress;
            this.ColumnAddress = columnAddress;
        }

        public static (InternationalCellAddress, int) Parse(string text, int start)
        {
            InternationalColumnAddress columnAddress;
            var next = 0;
            {
                (columnAddress, next) = InternationalColumnAddress.Parse(text, start);
                if (columnAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return (null, start);
                }
            }

            // 列はマッチ☆（＾～＾）

            InternationalRowAddress rowAddress;
            {
                (rowAddress, next) = InternationalRowAddress.Parse(text, next);
                if (rowAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return (null, start);
                }
            }

            // 列と行の両方マッチ☆（＾～＾）
            return (new InternationalCellAddress(rowAddress, columnAddress), next);
        }

        public static int ToIndex(int rowNumber, int columnNumber)
        {
            return InternationalRowAddress.ToIndex(rowNumber) * 19 + columnNumber;
        }

        public int ToIndex()
        {
            return ToIndex(this.RowAddress.Number, this.ColumnAddress.Number);
        }

        public static InternationalCellAddress FromIndex(int rowNumber, int columnNumber)
        {
            return new InternationalCellAddress(new InternationalRowAddress(rowNumber), new InternationalColumnAddress(columnNumber));
        }

        public static InternationalCellAddress FromIndex(int index)
        {
            var rowNumber = index / 19;
            var columnNumber = index % 19;
            return new InternationalCellAddress(new InternationalRowAddress(rowNumber), new InternationalColumnAddress(columnNumber));
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.ColumnAddress.ToDisplay()}{this.RowAddress.ToDisplay()}";
        }
    }
}
