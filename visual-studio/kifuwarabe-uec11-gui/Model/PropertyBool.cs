namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Globalization;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyBool : PropertyValue
    {
        private bool innerValue;

        public PropertyBool()
        {
            this.Value = false;
            this.Visible = true;
        }

        public PropertyBool(bool value)
        {
            this.Value = value;
            this.Visible = true;
        }

        /// <summary>
        /// JSON用の入出力だぜ☆（＾～＾）
        /// </summary>
        public override object Value
        {
            get { return this.innerValue; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (value is bool)
                {
                    this.innerValue = (bool)value;
                }
                else if (bool.TryParse(value.ToString(), out bool outValue))
                {
                    this.innerValue = outValue;
                }
            }
        }

        public override bool ToBool()
        {
            return this.innerValue;
        }

        public override double ToNumber()
        {
            if (this.innerValue)
            {
                return 1.0;
            }
            else
            {
                return 0.0;
            }
        }

        public override string ToText()
        {
            return this.innerValue.ToString(CultureInfo.CurrentCulture);
        }
    }
}
