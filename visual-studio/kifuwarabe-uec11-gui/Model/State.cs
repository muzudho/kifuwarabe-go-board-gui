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
        public PropertyWidgetModel Ply { get; set; }

        /// <summary>
        /// 最後の着手点。
        /// </summary>
        public PropertyWidgetModel Move { get; set; }

        /// <summary>
        /// 黒の選手名。
        /// </summary>
        public PropertyWidgetModel BlackName { get; set; }

        /// <summary>
        /// 黒の残り時間。
        /// </summary>
        public PropertyWidgetModel BlackTime { get; set; }

        /// <summary>
        /// 黒のアゲハマ。
        /// 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
        /// でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
        /// </summary>
        public PropertyWidgetModel BlackHama { get; set; }

        /// <summary>
        /// 白の選手名。
        /// </summary>
        public PropertyWidgetModel WhiteName { get; set; }

        /// <summary>
        /// 白の残り時間。
        /// </summary>
        public PropertyWidgetModel WhiteTime { get; set; }

        /// <summary>
        /// 白のアゲハマ。
        /// </summary>
        public PropertyWidgetModel WhiteHama { get; set; }

        /// <summary>
        /// 白のコミ。
        /// </summary>
        public PropertyWidgetModel Komi { get; set; }

        /// <summary>
        /// GUIの画面上にメッセージを表示するぜ☆（＾～＾）
        /// 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
        /// </summary>
        public PropertyWidgetModel Info { get; set; }

        public State()
        {
            // 初期値は 2 秒☆（＾～＾）
            this.IntervalMsec = 2000;

            this.Ply = new PropertyWidgetModel();
            this.Move = new PropertyWidgetModel();
            this.BlackName = new PropertyWidgetModel();
            this.BlackTime = new PropertyWidgetModel();
            this.BlackHama = new PropertyWidgetModel();
            this.WhiteName = new PropertyWidgetModel();
            this.WhiteTime = new PropertyWidgetModel();
            this.WhiteHama = new PropertyWidgetModel();
            this.Komi = new PropertyWidgetModel();
            this.Info = new PropertyWidgetModel();
        }
    }
}
