namespace KifuwarabeGoBoardGui.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 盤だぜ☆（＾～＾）　
    /// 大きなオブジェクトのわりに頻繁に更新されるぜ☆（＾～＾）
    /// </summary>
    public class BoardModelWrapper
    {
        public BoardModelWrapper(BoardModel boardModel)
        {
            this.BoardModel = boardModel;
        }

        public BoardModel BoardModel { get; private set; }

        public int RowSize
        {
            get
            {
                return this.BoardModel.RowSize;
            }
            set
            {
                this.BoardModel.RowSize = value;
            }
        }

        public int ColumnSize
        {
            get
            {
                return this.BoardModel.ColumnSize;
            }
            set
            {
                this.BoardModel.ColumnSize = value;
            }
        }

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

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetRowDiv()
        {
            return RowSize + MainWindow.SignLen + 1;
        }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetColumnDiv()
        {
            return ColumnSize + MainWindow.SignLen + 1;
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetRowLastO0()
        {
            return RowSize - 1;
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetColumnLastO0()
        {
            return ColumnSize - 1;
        }

        /// <summary>
        /// 石を置ける場所の数☆（＾～＾）
        /// </summary>
        public int GetCellCount()
        {
            return RowSize * ColumnSize;
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
