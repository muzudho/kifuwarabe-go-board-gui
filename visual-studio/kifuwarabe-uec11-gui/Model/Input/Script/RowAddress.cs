namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.Model;
    using KifuwarabeUec11Gui.Controller;

    /// <summary>
    /// 行番号だぜ☆（＾～＾）
    /// 
    /// 実質、1～2桁の数字だぜ☆（＾～＾）
    /// </summary>
    public class RowAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matched"></param>
        /// <param name="curr">Current.</param>
        /// <returns>Next.</returns>
        public delegate int ParsesCallback(RowAddress matched, int curr);

        /// <summary>
        /// 0から始まる（Origin 0）列番号☆（＾～＾）
        /// </summary>
        public int NumberO0 { get; private set; }

        public RowAddress(int numberO0)
        {
            this.NumberO0 = numberO0;
        }

        public static int Parse(string text, int start, ApplicationObjectModelWrapper appModel, ParsesCallback callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (text.Length < start + 1 || appModel == null)
            {
                return callback(null, start);
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
                    return callback(null, start);
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
                return callback(null, start);
            }

            // 内部的には行番号は 0 から持つぜ☆（＾～＾）
            return callback(new RowAddress(index), start);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayNoTrim(ApplicationObjectModelWrapper model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var rowNumbers = model.GetStringList(ApplicationObjectModel.RowNumbersRealName).Value;

            if (this.NumberO0 < 0 || rowNumbers.Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return rowNumbers[this.NumberO0];
            }
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplayTrimed(ApplicationObjectModelWrapper model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.NumberO0 < 0 || model.GetRowNumbersTrimed().Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return model.GetRowNumbersTrimed()[this.NumberO0];
            }
        }
    }
}
