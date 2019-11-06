namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.Output;

    /// <summary>
    /// Z字方向式の列アドレスだぜ☆（＾～＾） A ～ T みたいなやつだぜ☆（＾～＾）
    /// 
    /// このオブジェクトは、国際式囲碁のことは知らなくていいように作れだぜ☆（＾～＾）
    /// </summary>
    public class ColumnAddress
    {
        /// <summary>
        /// 0から始まる（Origin 0）列番号☆（＾～＾）
        /// </summary>
        public int NumberO0 { get; private set; }

        public ColumnAddress(int numberO0)
        {
            this.NumberO0 = numberO0;
        }

        /// <summary>
        /// 列番号の文字をパースして数にするぜ☆（＾～＾）
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static (ColumnAddress, int) Parse(string text, int start, BoardModel model)
        {
            if (text == null || text.Length < start + 1 || model == null)
            {
                return (null, start);
            }

            var oneChar = text[start].ToString(CultureInfo.CurrentCulture);
            var index = model.ColumnNumbers.IndexOf(oneChar);

            if (index < 0)
            {
                // 該当なし☆（＾～＾）
                return (null, start);
            }

            return (new ColumnAddress(index), start + 1);
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(BoardModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (this.NumberO0 < 0 || model.ColumnNumbers.Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return model.ColumnNumbers[this.NumberO0];
            }
        }
    }
}
