namespace KifuwarabeUec11Gui.Model
{
    using System;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyNumber : IPropertyValue
    {
        public double Value { get; set; }
        public bool Visible { get; set; }

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

        public void SetValue(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value is double)
            {
                this.Value = (double)value;
            }
            else if (double.TryParse(value.ToString(), out double outValue))
            {
                this.Value = outValue;
            }
        }
    }
}
