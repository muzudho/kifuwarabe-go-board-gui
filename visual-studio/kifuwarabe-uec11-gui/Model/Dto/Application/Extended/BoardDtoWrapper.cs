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
        public BoardLayerDto[] Layers
        {
            get
            {
                return this.BoardModel.Layers;
            }
            set
            {
                this.BoardModel.Layers = value;
            }
        }

        public void Resize(int rowSize, int columnSize)
        {
            this.BoardModel.Resize(rowSize, columnSize);
        }

        public void SetPiece(int layerIndex, int zShapedIndex, PieceDto piece)
        {
            this.Layers[layerIndex].Pieces[zShapedIndex] = piece;
        }

        public PieceDto GetPiece(int layerIndex, int zShapedIndex)
        {
            return this.Layers[layerIndex].Pieces[zShapedIndex];
        }
    }
}
