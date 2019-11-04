namespace KifuwarabeUec11Gui.Output
{
    using System.Collections.Generic;
    using KifuwarabeUec11Gui.InputScript;

    /// <summary>
    /// 盤だぜ☆（＾～＾）
    /// </summary>
    public class BoardModel
    {
        /// <summary>
        /// 置いている石☆（＾～＾）
        /// </summary>
        public List<Stone> Stones { get; private set; }

        public BoardModel()
        {
            this.Stones = new List<Stone>();
            for (int i=0; i<InputScriptDocument.CellCount; i++)
            {
                this.Stones.Add(Stone.Black);
            }
        }

        public void SetStone(int zShapedIndex, Stone stone)
        {
            this.Stones[zShapedIndex] = stone;
        }
    }
}
