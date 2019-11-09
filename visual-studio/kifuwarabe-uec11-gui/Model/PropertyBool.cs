namespace KifuwarabeUec11Gui.Model
{
    using System;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyBool : IPropertyValue
    {
        public bool Value { get; set; }
        public bool Visible { get; set; }

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

        public void SetValue(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value is bool)
            {
                this.Value = (bool)value;
            }
            else if (bool.TryParse(value.ToString(), out bool outValue))
            {
                this.Value = outValue;
            }
        }
    }
}
