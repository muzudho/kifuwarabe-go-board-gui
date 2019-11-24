namespace KifuwarabeUec11Gui.InputScript
{
    /// <summary>
    /// 区切り記号までの単語☆（＾～＾）
    /// </summary>
    public class WordUpToDelimiter
    {
        /// <summary>
        /// マッチングした文字☆（＾～＾）
        /// </summary>
        public string Text { get; private set; }

        public WordUpToDelimiter(string text)
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
