namespace KifuwarabeGoBoardGui.View
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class TileColumnNumbersView
    {
        /// <summary>
        /// 線の列番号☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="appView"></param>
        public static void Repaint(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (null == appView)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (appModel.HasStringList(ApplicationDto.TileColumnNumbersRealName))
            {
                var boardLeftTop = BoardView.GetBoardLeftTop(appView);

                // マスの中央に合わせるなら 0.5。 
                var offsetLeftRate = 0.25;
                boardLeftTop.Offset(BoardView.GetLabelWidth(appModel, appView) * offsetLeftRate, 0);

                XColumnNumberView.Repaint(appModel, appView, boardLeftTop, appModel.GetStringList(ApplicationDto.TileColumnNumbersRealName), System.Windows.Media.Brushes.Orange, appView.TileColumnLabels);
            }
        }
    }
}
