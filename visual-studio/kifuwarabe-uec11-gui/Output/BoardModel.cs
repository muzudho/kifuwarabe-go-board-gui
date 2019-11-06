namespace KifuwarabeUec11Gui.Output
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

            // 1桁の数は、文字位置の調整がうまく行かないので勘で調整☆（＾～＾）盤の上側から順に並べろだぜ☆（＾～＾）
            this.SetRowNumbers(new List<string>()
            {
                "19", "18", "17", "16", "15", "14", "13", "12", "11", "10", "  9", "  8", "  7", "  6", "  5", "  4", "  3", "  2", "  1"
            });

            // I列がない☆（＾～＾）棋譜に I1 I11 I17 とか書かれたら字が汚くて読めなくなるのだろう☆（＾～＾）
            this.ColumnNumbers = new List<string>()
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "J", "K",
                "L", "M", "N", "O", "P", "Q", "R", "S", "T"
            }; ;
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
        /// </summary>
        public List<Stone> Stones { get; private set; }

        /// <summary>
        /// 各行番号☆（＾～＾）
        /// </summary>
        public List<string> RowNumbersNoTrim { get; private set; }
        /// <summary>
        /// 各行番号☆（＾～＾）
        /// </summary>
        public List<string> RowNumbersTrimed { get; private set; }

        /// <summary>
        /// 各列番号☆（＾～＾）
        /// </summary>
        public List<string> ColumnNumbers { get; private set; }

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

        public void SetRowNumbers(List<string> rowNumbers)
        {
            if (rowNumbers == null)
            {
                throw new ArgumentNullException(nameof(rowNumbers));
            }

            this.RowNumbersNoTrim = rowNumbers;

            // 位置調整のためのスペースが含まれていると　やっかい☆（＾～＾）
            {
                var array = this.RowNumbersNoTrim.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i].Trim();
                }

                this.RowNumbersTrimed = new List<string>(array);
            }
        }

        public void SetColumnNumbers(List<string> columnNumbers)
        {
            this.ColumnNumbers = columnNumbers;
        }
    }
}
