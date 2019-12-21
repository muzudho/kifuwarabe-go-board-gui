namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    public static class CellRangeParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(CellRange matched, int curr);
        public delegate int NoneCallback();

        public static int Parse(
            string text,
            int start,
            ApplicationObjectModelWrapper appModel,
            SomeCallback someCallback,
            NoneCallback noneCallback
            )
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            CellRange cellRange = null;
            int curr = start;

            var next = CellAddressParser.Parse(
                text,
                curr,
                appModel,
                (startsCellAddress, curr) =>
                {
                    // Trace.WriteLine($"startsCellAddres| {startsCellAddress.ToDisplay()}");

                    return StartsWithKeywordParser.Parse(
                        ":",
                        text,
                        curr,
                        (colon, curr) =>
                        {
                            return CellAddressParser.Parse(
                                text,
                                curr,
                                appModel,
                                (endsCellAddress, curr) =>
                                {
                                    // Trace.WriteLine($"endsCellAddress | {endsCellAddress.ToDisplay()}");

                                    cellRange = new CellRange(startsCellAddress, endsCellAddress);
                                    return curr;
                                },
                                () =>
                                {
                                    // 構文不一致☆（＾～＾）コロンが付いていて尻切れトンボなら不一致、諦めろだぜ☆（＾～＾）パース失敗☆（＾～＾）
                                    return start;
                                });
                        },
                        ()=>
                        {
                            // 構文不一致、パース成功☆（＾～＾）

                            // ここまで一致していれば、短縮形として確定するぜ☆（＾～＾）
                            // 例えば `k10` は、 `k10:k10` と一致したと判定するんだぜ☆（＾～＾）
                            cellRange = new CellRange(startsCellAddress, startsCellAddress);
                            return curr;
                        });
                },
                () =>
                {
                    // 構文不一致☆（＾～＾）パース失敗☆（＾～＾）
                    return start;
                });

            if (cellRange != null)
            {
                return someCallback(cellRange, next);
            }
            else
            {
                return noneCallback();
            }
        }
    }
}
