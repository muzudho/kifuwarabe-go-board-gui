namespace KifuwarabeUec11Gui
{
    /// <summary>
    /// 内部状態。
    /// </summary>
    public class State
    {
        /// <summary>
        /// 何手目か。
        /// </summary>
        public int Ply { get; set; }

        /// <summary>
        /// 最後の着手点。
        /// </summary>
        public int LastMoveIndex { get; set; }

        /// <summary>
        /// 黒の選手名。
        /// </summary>
        public string BlackName { get; set; }

        /// <summary>
        /// 黒の残り時間。
        /// </summary>
        public string BlackTime { get; set; }

        /// <summary>
        /// 黒のアゲハマ。
        /// 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
        /// でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
        /// </summary>
        public string BlackHama { get; set; }

        /// <summary>
        /// 白の選手名。
        /// </summary>
        public string WhiteName { get; set; }

        /// <summary>
        /// 白の残り時間。
        /// </summary>
        public string WhiteTime { get; set; }

        /// <summary>
        /// 白のアゲハマ。
        /// </summary>
        public string WhiteHama { get; set; }

        /// <summary>
        /// 白のコミ。
        /// </summary>
        public double Komi { get; set; }

        /// <summary>
        /// コメント☆（＾～＾）画面にメッセージを表示するぜ☆（＾～＾）
        /// 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
        /// </summary>
        public string Comment { get; set; }

        public State()
        {
            this.LastMoveIndex = -1;
            this.BlackName = string.Empty;
            this.BlackTime = string.Empty;
            this.BlackHama = string.Empty;
            this.WhiteName = string.Empty;
            this.WhiteTime = string.Empty;
            this.WhiteHama = string.Empty;
            this.Comment = string.Empty;
        }
    }
}
