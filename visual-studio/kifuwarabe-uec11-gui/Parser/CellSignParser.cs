namespace kifuwarabe_uec11_gui
{
    using System;
    using System.Globalization;

    /// <summary>
    /// 盤の符号のパーサー。
    /// </summary>
    public static class CellSignParser
    {
        /// <summary>
        /// `A19` を 0、 `B19` を 1、…。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ToIndex(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            // 最初の1文字はアルファベット。
            var column = ((char)text[0]) - 65; // 65はAsciiCodeのA。97はa。コンピューター囲碁では I は抜く慣習。
            if (32 <= column)
            {
                // 小文字から大文字へ変換。
                column -= 32;
            }

            if (8 <= column)
            {
                column--;
            }

            var row = 0;
            if (int.TryParse(text[1].ToString(CultureInfo.CurrentCulture), out int outRow1))
            {
                row = outRow1;
            }

            if (2 < text.Length)
            {
                row *= 10;
                if (int.TryParse(text[2].ToString(CultureInfo.CurrentCulture), out int outRow2))
                {
                    row += outRow2;
                }
            }

            return (19-row)*19+column;
        }
    }
}
