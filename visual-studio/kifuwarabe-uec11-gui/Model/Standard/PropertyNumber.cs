namespace KifuwarabeUec11Gui.Model
{
    using System.Globalization;

    /// <summary>
    /// 値テキストがあって、表示・非表示を切り替えられるものは　これだぜ☆（＾～＾）
    /// 名前プロパティは持つなだぜ☆（＾～＾） JSONの出力書式が　イケてなくなるぜ☆（＾～＾）
    /// </summary>
    public class PropertyNumber : IPropertyValue
    {
        public PropertyNumber()
        {
            // this.Value = 0.0;
            this.Visible = true;
        }

        public PropertyNumber(double value)
        {
            this.Value = value;
            this.Visible = true;
        }

        /// <summary>
        /// JSON用の入出力だぜ☆（＾～＾）
        /// </summary>
        public double Value { get; set; }

        public bool Visible { get; set; }

        public string ToText()
        {
            return this.Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
