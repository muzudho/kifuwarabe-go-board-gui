namespace KifuwarabeGoBoardGui.Controller
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeGoBoardGui.Model;

    public delegate void SomeViewCallback(Canvas canvas);
    public delegate void NoneViewCallback(string err);

    public static class ApplicationViewController
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

            // インターバル・ミリ秒☆（＾～＾）
            // appView.DispatchTimer.Interval = TimeSpan.FromMilliseconds(appModel.GetNumber(ApplicationObjectModel.IntervalMsecRealName).Value);
            // Trace.WriteLine($"interval-msec: {model.State.IntervalMsec}");

            // 列番号
            ColumnNumberViewController.Repaint(appModel, appView);

            // 行番号
            RowNumberViewController.Repaint(appModel, appView);


            // 石
            for (int zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var stone = appView.Stones[zShapedIndex];
                StoneViewController.Repaint(appModel, zShapedIndex, stone);
            }

            // マーク
            for (int zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var mark = appView.Marks[zShapedIndex];
                MarkViewController.Repaint(appModel, zShapedIndex, mark);
            }

            // TODO UIウィジェット
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
                        PropertyViewController.RepaintByName(appModel, appView, realName);
                    }
                    else
                    {
                        // モデルにないなら、非表示処理をするぜ☆（＾～＾）
                        ApplicationViewController.MatchCanvasBy(
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
            StarViewController.Repaint(appModel, appView);

            // 着手マーカー
            MoveMarkerViewController.Repaint(appModel, appView);

            appView.InvalidateVisual();
        }
    }
}
