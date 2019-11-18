namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 盤だぜ☆（＾～＾）　
    /// 大きなオブジェクトのわりに頻繁に更新されるぜ☆（＾～＾）
    /// </summary>
    public class BoardModel
    {
        /// <summary>
        /// その他のデータの名前☆（＾～＾）
        /// </summary>
        public static string RowSizeOutsideName => "row-size";
        public static string ColumnSizeOutsideName => "column-size";

        public BoardModel()
        {
            this.Stones = new List<Stone>();
            for (int i = 0; i < this.GetCellCount(); i++)
            {
                // 初期値は 空点 で☆（＾～＾）
                this.Stones.Add(Stone.None);
            }
        }

        /// <summary>
        /// デフォルトでは 19路盤☆（＾～＾）
        /// </summary>
        public int RowSize { get; set; } = 19;

        /// <summary>
        /// 19路盤☆（＾～＾）
        /// </summary>
        public int ColumnSize { get; set; } = 19;

        /// <summary>
        /// 置いている石☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<Stone> Stones { get; set; }

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
    }
}
