namespace KifuwarabeGoBoardGui.View
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class TileRowNumbersView
    {
        /// <summary>
        /// 線の行番号☆（＾～＾）
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

            if (appModel.HasStringList(ApplicationDto.TileRowNumbersRealName))
            {
                var boardLeftTop = BoardView.GetBoardLeftTop(appView);

                // マスの中央に合わせるなら 0.5。 
                var offsetTopRate = 0.25;
                boardLeftTop.Offset(0, BoardView.GetLabelHeight(appModel, appView) * offsetTopRate);

                XRowNumberView.Repaint(appModel, appView, boardLeftTop, appModel.GetStringList(ApplicationDto.TileRowNumbersRealName), System.Windows.Media.Brushes.Orange, appView.TileRowLabels);
            }
        }
    }
}
