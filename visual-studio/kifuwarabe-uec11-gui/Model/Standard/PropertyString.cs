namespace KifuwarabeUec11Gui.Model
{
    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyString : IPropertyValue
    {
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
        public string Value { get; set; }

        public bool Visible { get; set; }

        public string ToText()
        {
            return this.Value;
        }
    }
}
