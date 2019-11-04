namespace KifuwarabeUec11Gui
{
    /// <summary>
    /// 決め打ちで固定のものだぜ☆（＾～＾）
    /// </summary>
    public static class HyperParameter
    {
        /// <summary>
        /// 19路盤以上には　大きくできないぜ☆（＾～＾）
        /// </summary>
        public static int MaxRowSize => 19;

        /// <summary>
        /// 19路盤以上には　大きくできないぜ☆（＾～＾）
        /// </summary>
        public static int MaxColumnSize => 19;

        /// <summary>
        /// 19路盤以上には　大きくできないぜ☆（＾～＾）
        /// </summary>
        public static int MaxCellCount => MaxRowSize * MaxColumnSize;

        /// <summary>
        /// 星は9個まで☆（＾～＾）
        /// </summary>
        public static int MaxStarCount => 9;
    }
}
