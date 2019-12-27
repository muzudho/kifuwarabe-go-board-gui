namespace KifuwarabeGoBoardGui.Model.Dto
{
    using System.Collections.Generic;
    using KifuwarabeGoBoardGui.Model.Dto.Application.Standard;

    /// <summary>
    /// 盤というより、盤の階層のようなもの☆（＾～＾）
    /// Resize してから使ってください。
    /// </summary>
    public class BoardDto
    {
        public BoardDto()
        {
            // とりあえず２階層☆（＾～＾）
            this.Layers = new BoardLayerDto[]{
                new BoardLayerDto(),
                new BoardLayerDto(),
            };
        }

        /// <summary>
        /// Row * Column.
        /// </summary>
        public int SerialLength { get; set; }

        public void Resize(int rowSize, int columnSize)
        {
            var newSerialLength = rowSize * columnSize;

            if (newSerialLength < this.SerialLength)
            {
                // 短くなったのなら、リストを縮めます。
                foreach(var layer in this.Layers)
                {
                    layer.Pieces.RemoveRange(newSerialLength, layer.Pieces.Count - newSerialLength);
                }
            }
            else if(this.SerialLength < newSerialLength)
            {
                // 長くなったのなら、要素を足します。
                var extend = newSerialLength - this.SerialLength;
                foreach (var layer in this.Layers)
                {
                    for (int i = 0; i < extend; i++)
                    {
                        // 増えたところは 空点 で☆（＾～＾）
                        layer.Pieces.Add(new PieceDto()); // 透明
                    }
                }
            }
        }

        /// <summary>
        /// 置いているピース☆（＾～＾）
        /// TODO 着手マークなどを置く予定で複数階層、用意してあるが……☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public BoardLayerDto[] Layers { get; set; }
    }
}
