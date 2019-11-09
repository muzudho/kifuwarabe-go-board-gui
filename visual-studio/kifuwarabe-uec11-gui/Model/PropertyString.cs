namespace KifuwarabeUec11Gui.Model
{
    using System;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyString : IPropertyValue
    {
        /// <summary>
        /// JSON用の入出力だぜ☆（＾～＾）
        /// </summary>
        public string Value { get; set; }

        public bool Visible { get; set; }

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

        public void SetValue(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.Value = value.ToString();
        }
    }
}
