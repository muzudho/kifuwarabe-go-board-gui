namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeGoBoardGui.Model.Dto;

    public delegate void SomeViewCallback(Canvas canvas);
    public delegate void NoneViewCallback(string err);

    public static class ApplicationView
    {
        public static void MatchCanvasBy(
            MainWindow appView,
            RealName realName,
            SomeViewCallback someCallback,
            NoneViewCallback noneCallback
        )
        {
            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (realName == null)
            {
                throw new ArgumentNullException(nameof(realName));
            }

            if (someCallback == null)
            {
                throw new ArgumentNullException(nameof(someCallback));
            }

            if (noneCallback == null)
            {
                throw new ArgumentNullException(nameof(noneCallback));
            }

            // UIオブジェクトを検索するぜ☆（＾～＾）
            var tagName = $"{realName.Value}Canvas";
            Canvas propView = (Canvas)appView.FindName(tagName);
            if (propView == null)
            {
                noneCallback($"Warning         | {tagName} tag is not found in xaml.");
            }
            else
            {
                someCallback(propView);
            }
        }

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

            // 列番号
            ColumnNumberView.Repaint(appModel, appView);

            // 行番号
            RowNumberView.Repaint(appModel, appView);


            // 石
            for (int zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var stone = appView.Stones[zShapedIndex];
                StoneView.Repaint(appModel, zShapedIndex, stone);
            }

            // マーク
            for (int zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var mark = appView.Marks[zShapedIndex];
                MarkView.Repaint(appModel, zShapedIndex, mark);
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
