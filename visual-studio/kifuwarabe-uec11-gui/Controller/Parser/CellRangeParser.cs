namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class CellRangeParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(CellRange matched, int curr);

        public static int Parse(string text, int start, ApplicationObjectModelWrapper model, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            CellRange cellRange = null;
            int curr = start;

            var next = CellAddressParser.Parse(text, curr, model, (startsCellAddress, curr) =>
            {
                if (startsCellAddress == null)
                {
                    // 構文不一致☆（＾～＾）
                    return start;
                }
                // Trace.WriteLine($"startsCellAddres| {startsCellAddress.ToDisplay()}");

                return StartsWithKeywordParser.Parse(":", text, curr, (colon, curr) =>
                {
                    if (colon == null)
                    {
                        // 構文不一致☆（＾～＾）

                        // ここまで一致していれば、短縮形として確定するぜ☆（＾～＾）
                        // 例えば `k10` は、 `k10:k10` と一致したと判定するんだぜ☆（＾～＾）
                        cellRange = new CellRange(startsCellAddress, startsCellAddress);
                        return curr;
                    }

                    return CellAddressParser.Parse(text, curr, model, (endsCellAddress, curr) =>
                    {
                        if (endsCellAddress == null)
                        {
                            // 構文不一致☆（＾～＾）コロンが付いていて尻切れトンボなら不一致、諦めろだぜ☆（＾～＾）
                            return start;
                        }
                        // Trace.WriteLine($"endsCellAddress | {endsCellAddress.ToDisplay()}");

                        cellRange = new CellRange(startsCellAddress, endsCellAddress);
                        return curr;
                    });
                });
            });

            return callback(cellRange, next);
        }
    }
}
