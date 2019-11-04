namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.Output;

    /// <summary>
    /// 行番号だぜ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class RowAddress
    {
        /// <summary>
        /// 0から始まる（Origin 0）列番号☆（＾～＾）
        /// </summary>
        public int NumberO0 { get; private set; }

        public RowAddress(int numberO0)
        {
            this.NumberO0 = numberO0;
        }

        public static (RowAddress, int) Parse(string text, int start, BoardModel model)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            // 入出力時では行番号は 1 から（1 Origin）持つぜ☆（＾～＾）
            var rowO1 = 0;
            {
                if (int.TryParse(text[start].ToString(CultureInfo.CurrentCulture), out int outRow))
                {
                    rowO1 = outRow;

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
                    rowO1 *= 10;
                    rowO1 += outRow;

                    // 先頭から2桁目は確定☆（＾～＾）
                    start++;
                }
            }

            // 1文字以上のヒットがある場合☆（＾～＾）
            // 内部的には行番号は 0 から持つぜ☆（＾～＾）
            return (new RowAddress(rowO1 - 1), start);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public virtual string ToDisplay(BoardModel model)
        {
            // 入出力時では行番号は 1 から（1 Origin）持つぜ☆（＾～＾）
            return (this.NumberO0 + 1).ToString(CultureInfo.CurrentCulture);
        }
    }
}
