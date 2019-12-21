namespace KifuwarabeGoBoardGui.Model
{
    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyString : IPropertyValue
    {
        public PropertyString()
        {
            this.Title = string.Empty;
            this.Value = string.Empty;
            this.Visible = true;
        }

        public PropertyString(string title, string value = "")
        {
            this.Title = title;
            this.Value = value;
            this.Visible = true;
        }

        public string Title { get; set; }

        public string Value { get; set; }

        public bool Visible { get; set; }

        /// <summary>
        /// JSONで出力されないようにメソッドにしているんだぜ☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ValueAsText()
        {
            return this.Value;
        }
    }
}
