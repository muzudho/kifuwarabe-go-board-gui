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
        /// 置いている石の色☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<ColorDto> Colors
        {
            get
            {
                return this.BoardModel.Colors;
            }
            set
            {
                this.BoardModel.Colors = value;
            }
        }

        public void Resize(int rowSize, int columnSize)
        {
            this.BoardModel.Resize(rowSize, columnSize);
        }

        public void SetColor(int zShapedIndex, ColorDto stoneColor)
        {
            this.Colors[zShapedIndex] = stoneColor;
        }

        public ColorDto GetColor(int zShapedIndex)
        {
            return this.Colors[zShapedIndex];
        }
    }
}
