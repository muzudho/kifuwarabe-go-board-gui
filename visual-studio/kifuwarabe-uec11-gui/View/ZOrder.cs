namespace KifuwarabeGoBoardGui
{
    /// <summary>
    /// Zインデックスのことだぜ☆（＾～＾）名前被りを避けた☆（＾～＾）
    /// </summary>
    public enum ZOrder
    {
        None,

        /// <summary>
        /// 盤上の線だぜ☆（＾～＾）
        /// </summary>
        Line = 110,

        /// <summary>
        /// 盤上の星だぜ☆（＾～＾）
        /// </summary>
        Star = 115,

        /// <summary>
        /// 盤上の符号だぜ☆（＾～＾） A とか 19 とか☆（＾～＾）
        /// </summary>
        LineNumber = 116,

        /// <summary>
        /// 石だぜ☆（＾～＾）
        /// </summary>
        Stone = 120,

        /// <summary>
        /// 着手のマーカーだぜ☆（＾～＾）
        /// </summary>
        MoveMarker = 125,

        /// <summary>
        /// 画面の表示物☆（＾～＾）
        /// </summary>
        UI = 510,

        /// <summary>
        /// 画面の表示物☆（＾～＾）
        /// </summary>
        InfoCanvas = 520,
    }
}
