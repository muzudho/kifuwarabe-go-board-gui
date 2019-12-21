namespace KifuwarabeGoBoardGui.InputScript
{
    /// <summary>
    /// 指定ミリ秒☆（＾～＾）
    /// </summary>
    public class SleepsInstructionArgument
    {
        /// <summary>
        /// ミリ秒☆（＾～＾）
        /// </summary>
        public int MilliSeconds { get; private set; }

        public SleepsInstructionArgument(int milliSeconds)
        {
            this.MilliSeconds = milliSeconds;
        }

        /// <summary>
        /// デバッグ表示用☆（＾～＾）
        /// </summary>
        /// <returns></returns>
        public string ToDisplay()
        {
            return $"{this.MilliSeconds}";
        }
    }
}
