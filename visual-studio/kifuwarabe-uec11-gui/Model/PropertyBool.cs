namespace KifuwarabeUec11Gui.Model
{
    using System.Globalization;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyBool : IPropertyValue
    {
        public PropertyBool()
        {
            // this.Value = false;
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
        public bool Value { get; set; }

        public bool Visible { get; set; }

        public string ToText()
        {
            return this.Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
