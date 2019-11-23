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

        public PropertyBool(string title, bool value)
        {
            this.Title = title;
            this.Value = value;
            this.Visible = true;
        }

        public string Title { get; set; }

        public bool Value { get; set; }

        public bool Visible { get; set; }

        public string ValueAsText()
        {
            return this.Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
