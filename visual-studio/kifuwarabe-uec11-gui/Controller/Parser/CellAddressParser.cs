namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    public static class CellAddressParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(CellAddress matched, int curr);
        public delegate int NoneCallback();

        public static int Parse(
            string text,
            int start,
            ApplicationObjectDtoWrapper appModel,
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

            CellAddress cellAddress = null;

            var next = ColumnAddressParser.Parse(
                text,
                start,
                appModel,
                (columnAddress, curr) =>
                {
                    // 列はマッチ☆（＾～＾）
                    return RowAddressParser.Parse(
                        text,
                        curr,
                        appModel,
                        (rowAddress, curr) =>
                        {
                            if (rowAddress == null)
                            {
                                // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                                return start;
                            }

                            // 列と行の両方マッチ☆（＾～＾）
                            cellAddress = new CellAddress(rowAddress, columnAddress);
                            return curr;
                        },
                        ()=>
                        {
                            return curr;
                        });
                },
                ()=>
                {
                    // 片方でもマッチしなければ、非マッチ☆（＾～＾）
                    return start;
                });

            if (cellAddress!=null)
            {
                return someCallback(cellAddress, next);
            }
            else
            {
                return noneCallback();
            }
        }
    }
}
