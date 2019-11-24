namespace KifuwarabeUec11Gui.InputScript
{
    /// <summary>
    /// 次のようなコマンド☆（＾～＾）
    /// 
    /// `set b-name = Kifuwarabe`
    /// `set b-name.visible = true`
    /// 
    /// 構造としては
    /// 
    /// `set {name}.{property} = {value}`
    /// 
    /// だぜ☆（＾～＾）`.{property}` が省略されている場合、`.value` を補うぜ☆（＾～＾）
    /// しかし☆
    /// 
    /// `set b-name = 1.5`
    /// 
    /// イコールの後ろにドットが現れたら嫌だよな☆（＾～＾）
    /// </summary>
    public class SetsInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Property { get; private set; }

        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Value { get; private set; }

        public SetsInstructionArgument(string name, string property, string value)
        {
            this.Name = name;
            this.Property = property;
            this.Value = value;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Name}.{this.Property} = {this.Value}";
        }
    }
}
