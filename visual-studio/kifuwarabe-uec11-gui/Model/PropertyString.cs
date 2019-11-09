namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyString : PropertyValue
    {
        private string innerValue;

        public PropertyString()
        {
            this.Value = string.Empty;
            this.Visible = true;
        }

        public PropertyString(string value)
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

                this.innerValue = value.ToString();
            }
        }

        public override bool ToBool()
        {
            if (bool.TryParse(this.innerValue, out bool outValue))
            {
                return outValue;
            }

            return false;
        }

        public override double ToNumber()
        {
            if (double.TryParse(this.innerValue, out double outValue))
            {
                return outValue;
            }

            return 0.0;
        }

        public override string ToText()
        {
            return this.innerValue;
        }

        public override List<string> ToTextList()
        {
            return new List<string>()
            {
                this.ToString()
            };
        }
    }
}
