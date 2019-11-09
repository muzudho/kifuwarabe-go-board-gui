namespace KifuwarabeUec11Gui.Model
{
    /// <summary>
    /// 内部状態。
    /// </summary>
    public class PropertiesModel
    {
        /// <summary>
        /// 何ミリ秒ごとに `input.txt` を確認するか（＾～＾）
        /// </summary>
        public PropertyModel IntervalMsec { get; set; }

        /// <summary>
        /// 何手目か。
        /// </summary>
        public PropertyModel Ply { get; set; }

        /// <summary>
        /// 最後の着手点。
        /// </summary>
        public PropertyModel Move { get; set; }

        /// <summary>
        /// 黒の選手名。
        /// </summary>
        public PropertyModel BlackName { get; set; }

        /// <summary>
        /// 黒の残り時間。
        /// </summary>
        public PropertyModel BlackTime { get; set; }

        /// <summary>
        /// 黒のアゲハマ。
        /// 囲碁の白石がハマグリで作られているから石のことをハマと呼ぶが、取り揚げた石はアゲハマと呼ぶ☆（＾～＾）
        /// でもアゲハマは、略してハマと呼ばれる☆（＾～＾）
        /// </summary>
        public PropertyModel BlackHama { get; set; }

        /// <summary>
        /// 白の選手名。
        /// </summary>
        public PropertyModel WhiteName { get; set; }

        /// <summary>
        /// 白の残り時間。
        /// </summary>
        public PropertyModel WhiteTime { get; set; }

        /// <summary>
        /// 白のアゲハマ。
        /// </summary>
        public PropertyModel WhiteHama { get; set; }

        /// <summary>
        /// 白のコミ。
        /// </summary>
        public PropertyModel Komi { get; set; }

        /// <summary>
        /// GUIの画面上にメッセージを表示するぜ☆（＾～＾）
        /// 改行は "\n" にだけ対応☆（＾～＾） 代わりに "\v" （垂直タブ）は使えなくなった☆（＾～＾）
        /// </summary>
        public PropertyModel Info { get; set; }

        public PropertiesModel()
        {
            // 初期値は 2 秒☆（＾～＾）
            this.IntervalMsec = new PropertyModel("interval-msec", "2000");

            this.Ply = new PropertyModel("ply", "0");
            this.Move = new PropertyModel("move", "0");
            this.BlackName = new PropertyModel("b-name", "player1");
            this.BlackTime = new PropertyModel("b-time", "00:00");
            this.BlackHama = new PropertyModel("b-hama", "0");
            this.WhiteName = new PropertyModel("w-name", "player2");
            this.WhiteTime = new PropertyModel("w-time", "00:00");
            this.WhiteHama = new PropertyModel("w-hama", "0");
            this.Komi = new PropertyModel("komi", "6.5");
            this.Info = new PropertyModel("info", "");
        }
    }
}
