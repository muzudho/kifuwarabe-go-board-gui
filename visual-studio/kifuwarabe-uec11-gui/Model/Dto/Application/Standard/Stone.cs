namespace KifuwarabeGoBoardGui.Model
{
    /// <summary>
    /// 石だぜ☆（＾～＾）
    /// </summary>
    public enum Stone
    {
        /// <summary>
        /// 空点だぜ☆（＾～＾） 置いていないときはこれを使う☆（＾～＾）
        /// 列挙型の最初は None を用意しておくのがコツだぜ☆（＾～＾）
        /// </summary>
        None,

        /// <summary>
        /// 黒石☆（＾～＾）
        /// </summary>
        Black,

        /// <summary>
        /// 白石☆（＾～＾）
        /// </summary>
        White,

        /// <summary>
        /// 壁……、というか枠☆（＾～＾）
        /// </summary>
        Wall
    }
}
