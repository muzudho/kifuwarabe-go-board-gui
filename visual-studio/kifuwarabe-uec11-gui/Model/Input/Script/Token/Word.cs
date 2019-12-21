namespace KifuwarabeGoBoardGui.InputScript
{
    /// <summary>
    /// 次の空白までの文字列☆（＾～＾）
    /// </summary>
    public class Word
    {
        /// <summary>
        /// マッチングした文字☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public Word(string text)
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
