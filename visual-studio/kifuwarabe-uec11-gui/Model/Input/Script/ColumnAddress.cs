namespace KifuwarabeUec11Gui.InputScript
{
    using System;
    using System.Globalization;
    using KifuwarabeUec11Gui.Model;

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
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay(ApplicationObjectModelWrapper model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var columnNumbers = model.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).Value;

            if (this.NumberO0 < 0 || columnNumbers.Count <= this.NumberO0)
            {
                return "#Error#";
            }
            else
            {
                return columnNumbers[this.NumberO0];
            }
        }
    }
}
