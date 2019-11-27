namespace KifuwarabeUec11Gui.Controller.Parser
{
    using System;
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
        public delegate int SomeCallback(CellRangeListArgument matched, int curr);
        public delegate int NoneCallback();

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
            SomeCallback someCallback,
            NoneCallback noneCallback)
        {
            if (someCallback==null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            var cellRanges = new List<CellRange>();
            var curr = start;

            // リスト☆（＾～＾）
            bool isRepeats = true;
            while (isRepeats)
            {
                // 最初のスペースを読み飛ばすぜ☆（＾～＾）
                curr = WhiteSpaceParser.Parse(
                    text,
                    curr,
                    (whiteSpace, curr) =>
                    {
                        return curr;
                    },
                    ()=>
                    {
                        return curr;
                    });

                // 最初のスペースを読み飛ばしたぜ☆（＾～＾）
                curr = CellRangeParser.Parse(
                    text,
                    curr,
                    appModel,
                    (cellRange, curr) =>
                    {
                        // セル番地指定があった☆（＾～＾）続行☆（＾～＾）
                        cellRanges.Add(cellRange);
                        return curr;
                    },
                    () =>
                    {
                        // セル番地指定なんて無かった☆（＾～＾）ここで成功終了☆（＾～＾）
                        isRepeats = false;
                        return curr;
                    });
            }

            if (cellRanges.Count<1)
            {
                return noneCallback();
            }
            else
            {
                // 列と行の両方マッチ☆（＾～＾）
                return someCallback(new CellRangeListArgument(cellRanges), curr);
            }
        }
    }
}
