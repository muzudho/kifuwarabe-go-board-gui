namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class CellAddressParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(CellAddress matched, int curr);

        public static int Parse(string text, int start, ApplicationObjectModelWrapper appModel, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            CellAddress cellAddress = null;

            var next = ColumnAddressParser.Parse(text, start, appModel, (columnAddress, curr) =>
            {
                if (columnAddress == null)
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return start;
                }

                // 列はマッチ☆（＾～＾）

                return RowAddressParser.Parse(text, curr, appModel, (rowAddress, curr) =>
                {
                    if (rowAddress == null)
                    {
                        // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                        return start;
                    }

                    // 列と行の両方マッチ☆（＾～＾）
                    cellAddress = new CellAddress(rowAddress, columnAddress);
                    return curr;
                });
            });

            return callback(cellAddress, next);
        }
    }
}
