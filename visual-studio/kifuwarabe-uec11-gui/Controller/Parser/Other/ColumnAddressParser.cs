namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using System.Globalization;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class ColumnAddressParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(ColumnAddress matched, int curr);
        public delegate int NoneCallback();

        /// <summary>
        /// 列番号の文字をパースして数にするぜ☆（＾～＾）
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int Parse(
            string text,
            int start,
            ApplicationObjectDtoWrapper appModel,
            SomeCallback someCallback,
            NoneCallback noneCallback
            )
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (text.Length < start + 1 || appModel == null)
            {
                return noneCallback();
            }

            var oneChar = text[start].ToString(CultureInfo.CurrentCulture);
            var index = appModel.GetStringList(ApplicationDto.ColumnNumbersRealName).Value.IndexOf(oneChar);

            if (index < 0)
            {
                // 該当なし☆（＾～＾）
                return noneCallback();
            }

            return someCallback(new ColumnAddress(index), start + 1);
        }
    }
}
