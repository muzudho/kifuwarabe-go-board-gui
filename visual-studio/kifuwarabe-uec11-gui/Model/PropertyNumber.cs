namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Globalization;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyNumber : PropertyValue
    {
        private double innerValue;

        public PropertyNumber()
        {
            this.Value = 0.0;
            this.Visible = true;
        }

        public PropertyNumber(double value)
        {
            this.Value = value;
            this.Visible = true;
        }

        /// <summary>
        /// JSON用の入出力だぜ☆（＾～＾）
        /// </summary>
        public override object Value
        {
            get
            {
                return this.innerValue;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (value is double)
                {
                    this.innerValue = (double)value;
                }
                else if (double.TryParse(value.ToString(), out double outValue))
                {
                    this.innerValue = outValue;
                }
            }
        }

        public override bool ToBool()
        {
            if (innerValue == 0.0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override double ToNumber()
        {
            return this.innerValue;
        }

        public override string ToText()
        {
            return this.innerValue.ToString(CultureInfo.CurrentCulture);
        }
    }
}
