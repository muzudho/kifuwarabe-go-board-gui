using KifuwarabeUec11Gui.Script.ExcelGo;

namespace KifuwarabeUec11Gui.Script.InternationalGo
{
    /// <summary>
    /// 国際囲碁のセル番地表記☆（＾～＾）
    /// 
    /// このオブジェクトは、Excel式で使い回せるものは　どんどん使い回せだぜ☆（＾～＾）
    /// </summary>
    public class InternationalCellAddress : CellAddress
    {
        public InternationalCellAddress(InternationalRowAddress rowAddress, InternationalColumnAddress columnAddress)
            :base(rowAddress, columnAddress)
        {
        }

        public new static (InternationalCellAddress, int) Parse(string text, int start)
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

        public new static int ToIndex(int rowNumber, int columnNumber)
        {
            return InternationalRowAddress.ToIndex(rowNumber) * 19 + columnNumber;
        }

        public new static InternationalCellAddress FromIndex(int rowNumber, int columnNumber)
        {
            return new InternationalCellAddress(new InternationalRowAddress(rowNumber), new InternationalColumnAddress(columnNumber));
        }

        public new static InternationalCellAddress FromIndex(int index)
        {
            var rowNumber = index / 19;
            var columnNumber = index % 19;
            return new InternationalCellAddress(new InternationalRowAddress(rowNumber), new InternationalColumnAddress(columnNumber));
        }

        public override int ToIndex()
        {
            return ToIndex(this.RowAddress.Number, this.ColumnAddress.Number);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public override string ToDisplay()
        {
            return $"{this.ColumnAddress.ToDisplay()}{this.RowAddress.ToDisplay()}";
        }
    }
}
