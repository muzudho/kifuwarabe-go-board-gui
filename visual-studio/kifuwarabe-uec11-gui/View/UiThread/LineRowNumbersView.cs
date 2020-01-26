namespace KifuwarabeGoBoardGui.View
{
    using System;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class LineRowNumbersView
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

            if (appModel.HasStringList(ApplicationDto.LineRowNumbersRealName))
            {
                var boardLeftTop = BoardView.GetBoardLeftTop(appView);

                /*
                // 交点の上に合わせるなら 0、マスの中央に合わせるなら 0.5。 
                var offsetTopRate = 0.0;

                if (ApplicationDto.Square == appModel.GetString(ApplicationDto.PieceLocationRealName).Value)
                {
                    offsetTopRate = 0.5;
                }

                boardLeftTop.Offset(0, BoardView.GetLabelHeight(appModel, appView) * offsetTopRate);
                 */

                XRowNumberView.Repaint(appModel, appView, boardLeftTop, appModel.GetStringList(ApplicationDto.LineRowNumbersRealName), System.Windows.Media.Brushes.Black, appView.LineRowLabels);
            }
        }
    }
}
