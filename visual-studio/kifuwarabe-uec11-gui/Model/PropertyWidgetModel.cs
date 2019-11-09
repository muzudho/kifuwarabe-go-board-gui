namespace KifuwarabeUec11Gui.Model
{
    /// <summary>
    /// 名前ラベルが付いていて、値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// </summary>
    public class PropertyWidgetModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Visible { get; set; }

        public PropertyWidgetModel()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
            this.Visible = true;
        }
    }
}
