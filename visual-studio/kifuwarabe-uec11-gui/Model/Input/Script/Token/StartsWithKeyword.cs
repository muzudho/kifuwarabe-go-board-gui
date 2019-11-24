namespace KifuwarabeUec11Gui.InputScript
{
    /// <summary>
    /// キーワードの完全一致☆（＾～＾）
    /// </summary>
    public class StartsWithKeyword
    {
        /// <summary>
        /// キーワード☆（＾～＾）
        /// </summary>
        private string Word { get; set; }

        public StartsWithKeyword(string word)
        {
            this.Word = word;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return this.Word;
        }
    }
}
