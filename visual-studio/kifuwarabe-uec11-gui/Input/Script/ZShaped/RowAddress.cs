namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 行番号だぜ☆（＾～＾）
    /// 
    /// 実質、1～2桁の数字だぜ☆（＾～＾）
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

            if (text.Length < start + 1 || model == null)
            {
                return (null, start);
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
                    return (null, start);
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
            int index = model.RowNumbers.IndexOf(oneChar);

            if (index < 0)
            {
                // 該当なし☆（＾～＾）
                return (null, start);
            }

            // 内部的には行番号は 0 から持つぜ☆（＾～＾）
            return (new RowAddress(index), start);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayNoTrim(BoardModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.NumberO0 < 0 || model.GetRowNumbersNoTrim().Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return model.GetRowNumbersNoTrim()[this.NumberO0];
            }
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayTrimed(BoardModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.NumberO0 < 0 || model.RowNumbers.Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return model.RowNumbers[this.NumberO0];
            }
        }
    }
}
