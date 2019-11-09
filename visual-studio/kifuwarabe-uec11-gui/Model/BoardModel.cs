namespace KifuwarabeUec11Gui.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 盤だぜ☆（＾～＾）　
    /// 大きなオブジェクトのわりに頻繁に更新されるぜ☆（＾～＾）
    /// 規定値は、国際式の囲碁盤にしておくぜ☆（＾～＾）I列がないから気を付けろよ☆（＾～＾）
    /// </summary>
    public class BoardModel
    {
        public BoardModel()
        {
            this.Stones = new List<Stone>();
            for (int i = 0; i < this.GetCellCount(); i++)
            {
                // 初期値は 空点 で☆（＾～＾）
                this.Stones.Add(Stone.None);
            }

            this.StarCellAddresses = new List<string>()
            {
                 "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4"
            };
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
        /// 星の番地☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<string> StarCellAddresses { get; set; }

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

        public void SetStarCellAddresses(List<string> cellAddresses)
        {
            this.StarCellAddresses = cellAddresses;
        }
    }
}
