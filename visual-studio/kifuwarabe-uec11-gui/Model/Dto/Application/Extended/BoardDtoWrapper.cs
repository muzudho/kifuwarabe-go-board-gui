namespace KifuwarabeGoBoardGui.Model.Dto
{
    using System.Collections.Generic;
    using KifuwarabeGoBoardGui.Model.Dto.Application.Standard;

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
        public List<PieceDto> Layer1
        {
            get
            {
                return this.BoardModel.Layer1;
            }
            set
            {
                this.BoardModel.Layer1 = value;
            }
        }

        public void Resize(int rowSize, int columnSize)
        {
            this.BoardModel.Resize(rowSize, columnSize);
        }

        public void SetPiece(int zShapedIndex, PieceDto piece)
        {
            this.Layer1[zShapedIndex] = piece;
        }

        public PieceDto GetPiece(int zShapedIndex)
        {
            return this.Layer1[zShapedIndex];
        }
    }
}
