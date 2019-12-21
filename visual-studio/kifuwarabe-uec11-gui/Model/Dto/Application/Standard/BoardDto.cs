﻿namespace KifuwarabeGoBoardGui.Model.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// Resize してから使ってください。
    /// </summary>
    public class BoardDto
    {
        public BoardDto()
        {
            this.Stones = new List<Stone>();
            this.Marks = new List<Mark>();
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
                this.Stones.RemoveRange(newSerialLength, this.Stones.Count - newSerialLength);
                this.Marks.RemoveRange(newSerialLength, this.Marks.Count - newSerialLength);
            }
            else if(this.SerialLength < newSerialLength)
            {
                // 長くなったのなら、要素を足します。
                var extend = newSerialLength - this.SerialLength;
                for (int i = 0; i < extend; i++)
                {
                    // 増えたところは 空点 で☆（＾～＾）
                    this.Stones.Add(Stone.None);
                    this.Marks.Add(Mark.None);
                }
            }
        }

        /// <summary>
        /// 置いている石☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<Stone> Stones { get; set; }

        /// <summary>
        /// 置いている石☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<Mark> Marks { get; set; }
    }
}