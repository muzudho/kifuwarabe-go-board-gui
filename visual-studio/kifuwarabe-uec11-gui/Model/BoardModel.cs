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
            for (int i = 0; i < HyperParameter.MaxCellCount; i++)
            {
                // 初期値は 空点 で☆（＾～＾）
                this.Stones.Add(Stone.None);
            }
        }

        /// <summary>
        /// 置いている石☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<Stone> Stones { get; set; }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetRowDiv(MetricsModel metrics)
        {
            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            return metrics.RowSize + MainWindow.SignLen + 1;
        }

        /// <summary>
        /// 19本の線を引くから20分割だが、符号の列を1つ足すぜ☆（＾～＾）
        /// </summary>
        public int GetColumnDiv(MetricsModel metrics)
        {
            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            return metrics.ColumnSize + MainWindow.SignLen + 1;
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetRowLastO0(MetricsModel metrics)
        {
            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            return metrics.RowSize - 1;
        }

        /// <summary>
        /// 19路盤の最終行のインデックス 0 から始まる（0 Origin）ので、 -1 する☆（＾～＾）
        /// </summary>
        public int GetColumnLastO0(MetricsModel metrics)
        {
            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            return metrics.ColumnSize - 1;
        }

        /// <summary>
        /// 石を置ける場所の数☆（＾～＾）
        /// </summary>
        public int GetCellCount(MetricsModel metrics)
        {
            if (metrics == null)
            {
                throw new ArgumentNullException(nameof(metrics));
            }

            return metrics.RowSize * metrics.ColumnSize;
        }

        public void SetStone(int zShapedIndex, Stone stone)
        {
            this.Stones[zShapedIndex] = stone;
        }
    }
}
