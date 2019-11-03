namespace KifuwarabeUec11Gui.Script.ExcelGo
{
    using System;
    using System.Globalization;

    /// <summary>
    /// 行番号だぜ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class RowAddress
    {
        public int Number { get; private set; }

        public RowAddress(int number)
        {
            this.Number = number;
        }

        public static (RowAddress, int) Parse(string text, int start)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var row = 0;
            {
                if (int.TryParse(text[start].ToString(CultureInfo.CurrentCulture), out int outRow))
                {
                    row = outRow;

                    // 先頭桁目は確定☆（＾～＾）
                    start++;
                }
                else
                {
                    // 1文字もヒットしなかった場合☆（＾～＾）
                    return (null, start);
                }
            }

            if (start < text.Length)
            {
                if (int.TryParse(text[start].ToString(CultureInfo.CurrentCulture), out int outRow))
                {
                    row *= 10;
                    row += outRow;

                    // 先頭から2桁目は確定☆（＾～＾）
                    start++;
                }
            }

            // 1文字以上のヒットがある場合☆（＾～＾）
            return (new RowAddress(row), start);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public virtual string ToDisplay()
        {
            return this.Number.ToString(CultureInfo.CurrentCulture);
        }
    }
}
