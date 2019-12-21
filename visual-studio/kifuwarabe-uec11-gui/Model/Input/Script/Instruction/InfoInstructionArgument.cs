namespace KifuwarabeGoBoardGui.InputScript
{
    /// <summary>
    /// `info I have a banana.` みたいなコマンド☆（＾～＾）
    /// </summary>
    public class InfoInstructionArgument
    {
        /// <summary>
        /// 前後の空白はトリムするぜ☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public InfoInstructionArgument(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.Text}";
        }
    }
}
