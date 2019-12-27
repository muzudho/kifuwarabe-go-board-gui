namespace KifuwarabeGoBoardGui.Model.Dto.Application.Standard
{
    using System.Collections.Generic;

    public class BoardLayerDto
    {
        public BoardLayerDto()
        {
            this.Pieces = new List<PieceDto>();
        }

        /// <summary>
        /// 置いているピース☆（＾～＾）
        /// </summary>
        public List<PieceDto> Pieces { get; set; }
    }
}
