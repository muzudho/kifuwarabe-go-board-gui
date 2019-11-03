namespace KifuwarabeUec11Gui.Script.ExcelGo
{
    /// <summary>
    /// Excel式セル番地☆（＾～＾） A1 とか T19 みたいなやつだぜ☆（＾～＾）左上端が A1 ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class CellAddress
    {
        public ColumnAddress ColumnAddress { get; private set; }
        public RowAddress RowAddress { get; private set; }

        public CellAddress(RowAddress rowAddress, ColumnAddress columnAddress)
        {
            this.RowAddress = rowAddress;
            this.ColumnAddress = columnAddress;
        }

        public static (CellAddress, int) Parse(string text, int start)
        {
            ColumnAddress columnAddress;
            var next = 0;
            {
                (columnAddress, next) = ColumnAddress.Parse(text, start);
                if (columnAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return (null, start);
                }
            }

            // 列はマッチ☆（＾～＾）

            RowAddress rowAddress;
            {
                (rowAddress, next) = RowAddress.Parse(text, next);
                if (rowAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return (null, start);
                }
            }

            // 列と行の両方マッチ☆（＾～＾）
            return (new CellAddress(rowAddress, columnAddress), next);
        }

        public static int ToIndex(int rowNumber, int columnNumber)
        {
            return rowNumber * 19 + columnNumber;
        }

        public static CellAddress FromIndex(int rowNumber, int columnNumber)
        {
            return new CellAddress(new RowAddress(rowNumber), new ColumnAddress(columnNumber));
        }

        public static CellAddress FromIndex(int index)
        {
            var rowNumber = index / 19;
            var columnNumber = index % 19;
            return new CellAddress(new RowAddress(rowNumber), new ColumnAddress(columnNumber));
        }

        public int ToIndex()
        {
            return ToIndex(this.RowAddress.Number, this.ColumnAddress.Number);
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
