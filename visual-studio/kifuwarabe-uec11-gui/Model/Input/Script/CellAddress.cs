namespace KifuwarabeGoBoardGui.InputScript
{
    using System;
    using KifuwarabeGoBoardGui.Model;

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

        public static int ToIndex(int rowNumberO0, int columnNumberO0, ApplicationObjectModelWrapper model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return rowNumberO0 * model.Board.ColumnSize + columnNumberO0;
        }

        public static CellAddress FromIndex(int indexO0, ApplicationObjectModelWrapper model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var rowNumberO0 = indexO0 / model.Board.ColumnSize;
            var columnNumberO0 = indexO0 % model.Board.ColumnSize;
            return new CellAddress(new RowAddress(rowNumberO0), new ColumnAddress(columnNumberO0));
        }

        public int ToIndex(ApplicationObjectModelWrapper model)
        {
            return ToIndex(this.RowAddress.NumberO0, this.ColumnAddress.NumberO0, model);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayNoTrim(ApplicationObjectModelWrapper model)
        {
            return $"{this.ColumnAddress.ToDisplay(model)}{this.RowAddress.ToDisplayNoTrim(model)}";
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayTrimed(ApplicationObjectModelWrapper model)
        {
            return $"{this.ColumnAddress.ToDisplay(model)}{this.RowAddress.ToDisplayTrimed(model)}";
        }
    }
}
