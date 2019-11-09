namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// Z字方向式セル番地☆（＾～＾） A1 とか T19 みたいなやつだぜ☆（＾～＾）左上端が A1 ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class CellAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellAddress"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(CellAddress cellAddress, int curr);

        public ColumnAddress ColumnAddress { get; private set; }
        public RowAddress RowAddress { get; private set; }

        public CellAddress(RowAddress rowAddress, ColumnAddress columnAddress)
        {
            this.RowAddress = rowAddress;
            this.ColumnAddress = columnAddress;
        }

        public static int Parse(string text, int start, ApplicationObjectModel appModel, ParsesCallback callback)
        {
            ColumnAddress columnAddress;
            var next = 0;
            {
                (columnAddress, next) = ColumnAddress.Parse(text, start, appModel);
                if (columnAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return callback(null, start);
                }
            }

            // 列はマッチ☆（＾～＾）

            RowAddress rowAddress;
            {
                (rowAddress, next) = RowAddress.Parse(text, next, appModel);
                if (rowAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return callback(null, start);
                }
            }

            // 列と行の両方マッチ☆（＾～＾）
            return callback(new CellAddress(rowAddress, columnAddress), next);
        }

        public static int ToIndex(int rowNumberO0, int columnNumberO0, ApplicationObjectModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return rowNumberO0 * model.Board.ColumnSize + columnNumberO0;
        }

        public static CellAddress FromIndex(int indexO0, ApplicationObjectModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var rowNumberO0 = indexO0 / model.Board.ColumnSize;
            var columnNumberO0 = indexO0 % model.Board.ColumnSize;
            return new CellAddress(new RowAddress(rowNumberO0), new ColumnAddress(columnNumberO0));
        }

        public int ToIndex(ApplicationObjectModel model)
        {
            return ToIndex(this.RowAddress.NumberO0, this.ColumnAddress.NumberO0, model);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayNoTrim(ApplicationObjectModel model)
        {
            return $"{this.ColumnAddress.ToDisplay(model)}{this.RowAddress.ToDisplayNoTrim(model)}";
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayTrimed(ApplicationObjectModel model)
        {
            return $"{this.ColumnAddress.ToDisplay(model)}{this.RowAddress.ToDisplayTrimed(model)}";
        }
    }
}
