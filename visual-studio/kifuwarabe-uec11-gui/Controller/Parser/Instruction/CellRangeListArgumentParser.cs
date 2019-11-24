namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    public static class CellRangeListArgumentParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(CellRangeListArgument matched, int curr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns>Next.</returns>
        public static int Parse(
            string text,
            int start,
            ApplicationObjectModelWrapper appModel,
            ParsesCallback callbackDone)
        {
            var cellRanges = new List<CellRange>();
            var nextSum = start;

            // リスト☆（＾～＾）
            bool repeatsColor = true;
            while (repeatsColor)
            {
                // 最初のスペースを読み飛ばすぜ☆（＾～＾）
                nextSum = WhiteSpaceParser.Parse(text, nextSum,
                    (whiteSpace, curr) =>
                    {
                        if (whiteSpace == null)
                        {
                            // 最初にスペースなんか無かった☆（＾～＾）ここで成功終了☆（＾～＾）
                            repeatsColor = false;
                            return curr;
                        }

                        // 最初のスペースを読み飛ばしたぜ☆（＾～＾）
                        return CellRangeParser.Parse(text, curr, appModel, (cellRange, curr) =>
                        {
                            if (cellRange == null)
                            {
                                // セル番地指定なんて無かった☆（＾～＾）ここで成功終了☆（＾～＾）
                                repeatsColor = false;
                            }
                            else
                            {
                                // セル番地指定があった☆（＾～＾）マッチで成功終了☆（＾～＾）
                                cellRanges.Add(cellRange);
                            }

                            return curr;
                        });
                    });
            }

            // 列と行の両方マッチ☆（＾～＾）
            return callbackDone(new CellRangeListArgument(cellRanges), nextSum);
        }
    }
}
