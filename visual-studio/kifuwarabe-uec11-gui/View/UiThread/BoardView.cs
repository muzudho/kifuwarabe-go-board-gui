namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Windows;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class BoardView
    {
        /// <summary>
        /// 盤の左上座標。
        /// </summary>
        /// <param name="appView"></param>
        /// <returns></returns>
        public static Point GetBoardLeftTop(MainWindow appView)
        {
            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(appView.grid.RenderSize.Width, appView.grid.RenderSize.Height) * 0.95;

            // センターを求めようぜ☆（＾～＾）
            var centerX = appView.grid.RenderSize.Width / 2;
            var centerY = appView.grid.RenderSize.Height / 2;

            return new Point(centerX - shortenEdge / 2, centerY - shortenEdge / 2);
        }

        public static double GetLabelWidth(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            var columnInterval = appView.board.Width / appModel.GetColumnDiv();
            return columnInterval * 1.8;
        }

        public static double GetLabelHeight(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            var rowInterval = appView.board.Height / appModel.GetRowDiv();
            return rowInterval * 1.8; ;
        }
    }
}
