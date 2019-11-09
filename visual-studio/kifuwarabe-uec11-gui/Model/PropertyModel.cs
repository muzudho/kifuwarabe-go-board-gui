namespace KifuwarabeUec11Gui.Model
{
    /// <summary>
    /// 名前ラベルが付いていて、値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// </summary>
    public class PropertyModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Visible { get; set; }

        public PropertyModel()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
            this.Visible = true;
        }

        public PropertyModel(string name, string value)
        {
            this.Name = name;
            this.Value = value;
            this.Visible = true;
        }
    }
}
