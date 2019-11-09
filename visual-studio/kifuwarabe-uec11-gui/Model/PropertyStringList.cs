namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyStringList : PropertyValue
    {
        /// <summary>
        /// Valueのセッターの後で☆（＾～＾）
        /// </summary>
        /// <param name="value">代入されてきた値のコピー。</param>
        public delegate void AfterSetsValueCallbackType(List<string> value);

        private List<string> innerValue;

        public PropertyStringList()
        {
            this.Value = new List<string>();
            this.Visible = true;
        }

        public PropertyStringList(List<string> value, AfterSetsValueCallbackType afterSetsValueCallback = null)
        {
            // Value より先に このコールバックをセットしておくこと。
            this.AfterSetsValueCallback = afterSetsValueCallback;

            this.Value = value;
            this.Visible = true;
        }

        private AfterSetsValueCallbackType AfterSetsValueCallback { get; set; }

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

                if (value is List<string>)
                {
                    this.innerValue = (List<string>)value;
                }
                else
                {
                    this.innerValue = ColumnNumbersController.FromString(value.ToString());
                }

                if (this.AfterSetsValueCallback != null)
                {
                    this.AfterSetsValueCallback(new List<string>(this.innerValue));
                }
            }
        }

        public override bool ToBool()
        {
            return false;
        }

        public override double ToNumber()
        {
            return 0.0;
        }

        public override string ToText()
        {
            return string.Empty;
        }

        public override List<string> ToTextList()
        {
            return this.innerValue;
        }
    }
}
