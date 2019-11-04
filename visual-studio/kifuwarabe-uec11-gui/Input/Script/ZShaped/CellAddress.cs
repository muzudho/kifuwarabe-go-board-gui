using KifuwarabeUec11Gui.Output;

namespace KifuwarabeUec11Gui.InputScript
{
    /// <summary>
    /// Z字方向式セル番地☆（＾～＾） A1 とか T19 みたいなやつだぜ☆（＾～＾）左上端が A1 ☆（＾～＾）
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

        public static int ToIndex(int rowNumberO0, int columnNumberO0)
        {
            return rowNumberO0 * BoardModel.ColumnSize + columnNumberO0;
        }

        /*
        public static CellAddress FromIndex(int rowNumber, int columnNumber)
        {
            return new CellAddress(new RowAddress(rowNumber), new ColumnAddress(columnNumber));
        }
        */

        public static CellAddress FromIndex(int indexO0)
        {
            var rowNumberO0 = indexO0 / BoardModel.ColumnSize;
            var columnNumberO0 = indexO0 % BoardModel.ColumnSize;
            return new CellAddress(new RowAddress(rowNumberO0), new ColumnAddress(columnNumberO0));
        }

        public virtual int ToIndex()
        {
            return ToIndex(this.RowAddress.NumberO0, this.ColumnAddress.NumberO0);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public virtual string ToDisplay()
        {
            return $"{this.ColumnAddress.ToDisplay()}{this.RowAddress.ToDisplay()}";
        }
    }
}
