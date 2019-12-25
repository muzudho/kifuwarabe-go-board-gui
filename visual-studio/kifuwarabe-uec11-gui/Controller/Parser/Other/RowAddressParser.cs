namespace KifuwarabeGoBoardGui.Controller.Parser
{
    using System;
    using System.Globalization;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class RowAddressParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int SomeCallback(RowAddress matched, int curr);
        public delegate int NoneCallback();

        public static int Parse(
            string text,
            int start,
            ApplicationObjectDtoWrapper appModel,
            SomeCallback someCallback,
            NoneCallback noneCallback)
        {
            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (text.Length < start + 1 || appModel == null)
            {
                return noneCallback();
            }

            // 複数桁の数字☆（＾～＾）
            var figures = 0;
            {
                if (int.TryParse(text[start].ToString(CultureInfo.CurrentCulture), out int outRow))
                {
                    figures = outRow;

                    // 先頭桁目は確定☆（＾～＾）
                    start++;
                }
                else
                {
                    // 1文字もヒットしなかった場合☆（＾～＾）
                    return noneCallback();
                }
            }

            if (start < text.Length)
            {
                if (int.TryParse(text[start].ToString(CultureInfo.CurrentCulture), out int outRow))
                {
                    figures *= 10;
                    figures += outRow;

                    // 先頭から2桁目は確定☆（＾～＾）
                    start++;
                }
            }

            // 1文字以上のヒットがある場合☆（＾～＾）

            var oneChar = figures.ToString(CultureInfo.CurrentCulture);
            int index = appModel.GetRowNumbersTrimed().IndexOf(oneChar);

            if (index < 0)
            {
                // 該当なし☆（＾～＾）
                return noneCallback();
            }
            else
            {
                // 内部的には行番号は 0 から持つぜ☆（＾～＾）
                return someCallback(new RowAddress(index), start);
            }
        }
    }
}
