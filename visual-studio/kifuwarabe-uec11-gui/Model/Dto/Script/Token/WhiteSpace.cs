namespace KifuwarabeGoBoardGui.Model.Dto
{
    /// <summary>
    /// １個以上の空白☆（＾～＾）
    /// </summary>
    public class WhiteSpace
    {
        /// <summary>
        /// マッチングした文字☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public WhiteSpace(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return this.Text;
        }
    }
}
