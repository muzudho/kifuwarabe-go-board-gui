namespace KifuwarabeGoBoardGui.Model.Dto.Application.Standard
{
    /// <summary>
    /// 置くやつの形状☆（＾～＾）
    /// </summary>
    public enum PieceShapes
    {
        /// <summary>
        /// 描かない☆（＾～＾）
        /// </summary>
        None,

        /// <summary>
        /// 碁石☆（＾～＾）
        /// </summary>
        Stone,

        /// <summary>
        /// 将棋の駒で、上向いてるやつ☆（＾～＾）
        /// </summary>
        Upper,

        /// <summary>
        /// 将棋の駒で、下向いてるやつ☆（＾～＾）
        /// </summary>
        Downer,

        /// <summary>
        /// 小さな矩形☆（＾～＾）最後の着手の上に置いたりするのに使う☆（＾～＾）
        /// </summary>
        SmallRect,
    }
}
