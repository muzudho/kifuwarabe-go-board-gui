namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using KifuwarabeUec11Gui.Output;

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

        public static (CellAddress, int) Parse(string text, int start, BoardModel model)
        {
            ColumnAddress columnAddress;
            var next = 0;
            {
                (columnAddress, next) = ColumnAddress.Parse(text, start, model);
                if (columnAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return (null, start);
                }
            }

            // 列はマッチ☆（＾～＾）

            RowAddress rowAddress;
            {
                (rowAddress, next) = RowAddress.Parse(text, next, model);
                if (rowAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return (null, start);
                }
            }

            // 列と行の両方マッチ☆（＾～＾）
            return (new CellAddress(rowAddress, columnAddress), next);
        }

        public static int ToIndex(int rowNumberO0, int columnNumberO0, BoardModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return rowNumberO0 * model.ColumnSize + columnNumberO0;
        }

        public static CellAddress FromIndex(int indexO0, BoardModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var rowNumberO0 = indexO0 / model.ColumnSize;
            var columnNumberO0 = indexO0 % model.ColumnSize;
            return new CellAddress(new RowAddress(rowNumberO0), new ColumnAddress(columnNumberO0));
        }

        public int ToIndex(BoardModel model)
        {
            return ToIndex(this.RowAddress.NumberO0, this.ColumnAddress.NumberO0, model);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayNoTrim(BoardModel model)
        {
            return $"{this.ColumnAddress.ToDisplay(model)}{this.RowAddress.ToDisplayNoTrim(model)}";
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayTrimed(BoardModel model)
        {
            return $"{this.ColumnAddress.ToDisplay(model)}{this.RowAddress.ToDisplayTrimed(model)}";
        }
    }
}
