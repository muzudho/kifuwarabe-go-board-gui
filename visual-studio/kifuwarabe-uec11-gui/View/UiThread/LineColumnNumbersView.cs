namespace KifuwarabeGoBoardGui.View
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class LineColumnNumbersView
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

            if (appModel.HasStringList(ApplicationDto.LineColumnNumbersRealName))
            {
                var boardLeftTop = BoardView.GetBoardLeftTop(appView);

                /*
                // 交点の上に合わせるなら 0、マスの中央に合わせるなら 0.5。 
                var offsetLeftRate = 0.0;

                if (ApplicationDto.Square == appModel.GetString(ApplicationDto.PieceLocationRealName).Value)
                {
                    offsetLeftRate = 0.5;
                }

                boardLeftTop.Offset(BoardView.GetLabelWidth(appModel, appView) * offsetLeftRate, 0);
                 */

                XColumnNumberView.Repaint(appModel, appView, boardLeftTop, appModel.GetStringList(ApplicationDto.LineColumnNumbersRealName), System.Windows.Media.Brushes.Black, appView.LineColumnLabels);
            }
        }
    }
}
