namespace KifuwarabeUec11Gui.Model
{
    using System.Collections.Generic;

    public class BoardModel
    {
        public BoardModel()
        {
            this.RowSize = 19;
            this.ColumnSize = 19;
            this.Stones = new List<Stone>();

            var plain = this.RowSize * this.ColumnSize;
            for (int i = 0; i < plain; i++)
            {
                // 初期値は 空点 で☆（＾～＾）
                this.Stones.Add(Stone.None);
            }
        }

        /// <summary>
        /// デフォルトでは 19路盤☆（＾～＾）
        /// </summary>
        public int RowSize { get; set; }

        /// <summary>
        /// デフォルトでは 19路盤☆（＾～＾）
        /// </summary>
        public int ColumnSize { get; set; }

        /// <summary>
        /// 置いている石☆（＾～＾）
        /// TODO JSONをデシリアライズできる方法が分かれば private アクセスにしたいが……☆（＾～＾）
        /// </summary>
        public List<Stone> Stones { get; set; }
    }
}
