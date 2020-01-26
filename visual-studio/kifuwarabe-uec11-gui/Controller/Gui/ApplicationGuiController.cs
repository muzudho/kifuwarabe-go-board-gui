namespace KifuwarabeGoBoardGui.Controller.Gui
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using KifuwarabeGoBoardGui.Model.Dto;
    using KifuwarabeGoBoardGui.View;

    public static class ApplicationGuiController
    {
        public static void RepaintAllViews(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            if (null == appModel)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (null == appView)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            // 線の列番号
            {
                var boardLeftTop = BoardView.GetBoardLeftTop(appView);

                // 交点の上に合わせるなら 0、マスの中央に合わせるなら 0.5。 
                var offsetLeftRate = 0.0;

                if (ApplicationDto.Square == appModel.GetString(ApplicationDto.PieceLocationRealName).Value)
                {
                    offsetLeftRate = 0.5;
                }

                boardLeftTop.Offset(BoardView.GetLabelWidth(appModel, appView) * offsetLeftRate, 0);

                ColumnNumberView.Repaint(appModel, appView, boardLeftTop, appModel.GetStringList(ApplicationDto.LineColumnNumbersRealName));
            }

            // 線の行番号
            {
                var boardLeftTop = BoardView.GetBoardLeftTop(appView);

                // 交点の上に合わせるなら 0、マスの中央に合わせるなら 0.5。 
                var offsetTopRate = 0.0;

                if (ApplicationDto.Square == appModel.GetString(ApplicationDto.PieceLocationRealName).Value)
                {
                    offsetTopRate = 0.5;
                }

                boardLeftTop.Offset(0, BoardView.GetLabelHeight(appModel, appView) * offsetTopRate);

                RowNumberView.Repaint(appModel, appView, boardLeftTop, appModel.GetStringList(ApplicationDto.LineRowNumbersRealName));
            }

            // 石。
            {
                // TODO レイヤー番号。
                var layerIndex = 0;
                appView.PieceBoard.ForeachPiace(
                    (piece, zShapedIndex) =>
                    {
                        PieceView.Repaint(appModel, layerIndex, zShapedIndex, piece);
                    });
            }

            // マーク
            {
                // TODO レイヤー番号。
                var layerIndex = 1;
                for (int zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
                {
                    var mark = appView.Marks[zShapedIndex];
                    MarkView.Repaint(appModel, layerIndex, zShapedIndex, mark);
                }
            }

            // UIウィジェット
            {
                var names = new RealName[]
                {
                    ApplicationDto.Top1RealName,
                    ApplicationDto.Top2RealName,
                    ApplicationDto.Right1RealName,
                    ApplicationDto.Right2RealName,
                    ApplicationDto.Right3RealName,
                    ApplicationDto.Left1RealName,
                    ApplicationDto.Left2RealName,
                    ApplicationDto.Left3RealName,
                    ApplicationDto.Left4RealName,
                    ApplicationDto.InfoRealName,
                };

                foreach (var realName in names)
                {
                    if (appModel.ContainsKeyOfProperty(realName))
                    {
                        // モデルにあるなら、再描画処理をするぜ☆（＾～＾）
                        PropertyView.RepaintByName(appModel, appView, realName);
                    }
                    else
                    {
                        // モデルにないなら、非表示処理をするぜ☆（＾～＾）
                        ApplicationView.MatchCanvasBy(
                            appView,
                            realName,
                            (propView) =>
                            {
                                // ビューがあるのに、モデルがないなら、ビューを非表示にするぜ☆（＾～＾）
                                propView.Visibility = Visibility.Hidden;
                            },
                            (err) =>
                            {
                                // ビューが無いなら非表示にもできん☆（＾～＾）
                                Trace.WriteLine(err);
                            });
                    }
                }
            }

            // 画面のサイズに合わせて再描画しようぜ☆（＾～＾）
            appView.FitSizeToWindow();

            // 星
            StarView.Repaint(appModel, appView);

            // 着手マーカー
            MoveMarkerView.Repaint(appModel, appView);

            // 情報ビュー
            InfoView.Repaint(appModel, appView);

            appView.InvalidateVisual();
        }
    }
}
