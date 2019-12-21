namespace KifuwarabeGoBoardGui.Model.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// 盤だぜ☆（＾～＾）　
    /// 大きなオブジェクトのわりに頻繁に更新されるぜ☆（＾～＾）
    /// </summary>
    public class BoardDtoWrapper
    {
        public BoardDtoWrapper(BoardDto boardModel)
        {
            this.BoardModel = boardModel;
        }

        public BoardDto BoardModel { get; private set; }

        /// <summary>
        /// 置いている石☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<Stone> Stones
        {
            get
            {
                return this.BoardModel.Stones;
            }
            set
            {
                this.BoardModel.Stones = value;
            }
        }

        public void Resize(int rowSize, int columnSize)
        {
            this.BoardModel.Resize(rowSize, columnSize);
        }

        public void SetStone(int zShapedIndex, Stone stone)
        {
            this.Stones[zShapedIndex] = stone;
        }

        public Stone GetStone(int zShapedIndex)
        {
            return this.Stones[zShapedIndex];
        }
    }
}
