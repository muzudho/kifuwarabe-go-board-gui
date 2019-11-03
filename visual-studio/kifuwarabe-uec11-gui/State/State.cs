namespace KifuwarabeUec11Gui
{
    /// <summary>
    /// 内部状態。
    /// </summary>
    public class State
    {
        public int Ply { get; set; }

        /// <summary>
        /// 最後の着手点。
        /// </summary>
        public int LastMoveIndex { get; set; }

        public State()
        {
            this.LastMoveIndex = -1;
        }
    }
}
