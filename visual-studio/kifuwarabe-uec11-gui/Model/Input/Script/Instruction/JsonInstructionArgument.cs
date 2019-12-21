namespace KifuwarabeGoBoardGui.InputScript
{
    /// <summary>
    /// `JSON {ここにJSON}` みたいなコマンド☆（＾～＾）
    /// </summary>
    public class JsonInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Json { get; private set; }

        public JsonInstructionArgument(string text)
        {
            this.Json = text;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Json}";
        }
    }
}
