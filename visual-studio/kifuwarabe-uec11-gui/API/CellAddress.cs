namespace kifuwarabe_uec11_gui.API
{
    /// <summary>
    /// セル番地☆（＾～＾） A1 とか T19 みたいなやつだぜ☆（＾～＾）
    /// </summary>
    public class CellAddress
    {
        public ColumnAddress ColumnAddress { get; set; }
        public RowAddress RowAddress { get; set; }

        public CellAddress(ColumnAddress columnAddress, RowAddress rowAddress)
        {
            this.ColumnAddress = columnAddress;
            this.RowAddress = rowAddress;
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
            return (new CellAddress(columnAddress, rowAddress), next);
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
