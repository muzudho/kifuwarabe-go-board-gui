namespace KifuwarabeGoBoardGui.Model.Dto.Application.Standard
{
    /// <summary>
    /// ピース１個分☆（＾～＾）
    /// </summary>
    public class PieceDto
    {
        public PieceDto()
        {
            this.Color = ColorDto.Transparent;
            this.Shape = PieceShapes.None;
        }

        public PieceDto(ColorDto color, PieceShapes piece)
        {
            this.Color = color;
            this.Shape = piece;
        }

        /// <summary>
        /// 色☆（＾～＾）
        /// </summary>
        public ColorDto Color { get; set; }

        /// <summary>
        /// 形状☆（＾～＾）
        /// </summary>
        public PieceShapes Shape { get; set; }
    }
}
