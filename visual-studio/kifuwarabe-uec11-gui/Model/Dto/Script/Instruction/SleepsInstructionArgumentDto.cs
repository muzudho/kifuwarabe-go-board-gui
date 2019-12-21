namespace KifuwarabeGoBoardGui.Model.Dto
{
    /// <summary>
    /// 指定ミリ秒☆（＾～＾）
    /// </summary>
    public class SleepsInstructionArgumentDto
    {
        /// <summary>
        /// ミリ秒☆（＾～＾）
        /// </summary>
        public int MilliSeconds { get; private set; }

        public SleepsInstructionArgumentDto(int milliSeconds)
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
