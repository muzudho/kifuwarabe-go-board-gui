namespace KifuwarabeUec11Gui.Model
{
    /// <summary>
    /// 内部状態。
    /// </summary>
    public class State
    {
        /// <summary>
        /// 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
        /// </summary>
        public int IntervalMsec { get; set; }

        /// <summary>
        /// 何手目か。
        /// </summary>
        public CanvasWidgetModel Ply { get; set; }

        /// <summary>
        /// 最後の着手点。
        /// </summary>
        public CanvasWidgetModel Move { get; set; }

        /// <summary>
        /// 黒の選手名。
        /// </summary>
        public CanvasWidgetModel BlackName { get; set; }

        /// <summary>
        /// 黒の残り時間。
        /// </summary>
        public CanvasWidgetModel BlackTime { get; set; }

        /// <summary>
        /// 黒のアゲハマ。
        /// 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
        /// でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
        /// </summary>
        public CanvasWidgetModel BlackHama { get; set; }

        /// <summary>
        /// 白の選手名。
        /// </summary>
        public CanvasWidgetModel WhiteName { get; set; }

        /// <summary>
        /// 白の残り時間。
        /// </summary>
        public CanvasWidgetModel WhiteTime { get; set; }

        /// <summary>
        /// 白のアゲハマ。
        /// </summary>
        public CanvasWidgetModel WhiteHama { get; set; }

        /// <summary>
        /// 白のコミ。
        /// </summary>
        public CanvasWidgetModel Komi { get; set; }

        /// <summary>
        /// GUIの画面上にメッセージを表示するぜ☆（＾～＾）
        /// 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
        /// </summary>
        public string Info { get; set; }

        public State()
        {
            // 初期値は 2 秒☆（＾～＾）
            this.IntervalMsec = 2000;

            this.Ply = new CanvasWidgetModel();
            this.Move = new CanvasWidgetModel();
            this.BlackName = new CanvasWidgetModel();
            this.BlackTime = new CanvasWidgetModel();
            this.BlackHama = new CanvasWidgetModel();
            this.WhiteName = new CanvasWidgetModel();
            this.WhiteTime = new CanvasWidgetModel();
            this.WhiteHama = new CanvasWidgetModel();
            this.Komi = new CanvasWidgetModel();
            this.Info = string.Empty;
        }
    }
}
