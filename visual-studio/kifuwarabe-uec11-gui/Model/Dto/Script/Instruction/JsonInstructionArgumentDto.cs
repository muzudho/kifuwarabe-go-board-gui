namespace KifuwarabeGoBoardGui.Model.Dto
{
    /// <summary>
    /// `JSON {ここにJSON}` みたいなコマンド☆（＾～＾）
    /// </summary>
    public class JsonInstructionArgumentDto
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Json { get; private set; }

        public JsonInstructionArgumentDto(string text)
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
