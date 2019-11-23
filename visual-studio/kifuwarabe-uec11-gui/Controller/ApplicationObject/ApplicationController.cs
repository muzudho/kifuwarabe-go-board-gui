namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Diagnostics;
    using KifuwarabeUec11Gui.Model;

    public static class ApplicationController
    {
        public static void RepaintAllViews(ApplicationObjectModelWrapper appModel, MainWindow appView)
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
            appView.DispatchTimer.Interval = TimeSpan.FromMilliseconds(appModel.GetNumber(ApplicationObjectModel.IntervalMsecRealName).Value);
            // Trace.WriteLine($"interval-msec: {model.State.IntervalMsec}");

            // 列番号
            ColumnNumberController.Repaint(appModel, appView);

            // 行番号
            RowNumberController.Repaint(appModel, appView);


            // 石
            for (int index = 0; index < HyperParameter.MaxCellCount; index++)
            {
                StoneController.Repaint(appModel, appView, index);
            }

            // TODO UIウィジェット
            {
                var names = new RealName[]
                {
                    ApplicationObjectModel.Top1RealName,
                    ApplicationObjectModel.Top2RealName,
                    ApplicationObjectModel.Right1RealName,
                    ApplicationObjectModel.Right2RealName,
                    ApplicationObjectModel.Right3RealName,
                    ApplicationObjectModel.Left1RealName,
                    ApplicationObjectModel.Left2RealName,
                    ApplicationObjectModel.Left3RealName,
                    ApplicationObjectModel.Left4RealName,
                    ApplicationObjectModel.InfoRealName,
                };

                foreach (var name in names)
                {
                    if (appModel.ContainsKeyOfProperty(name))
                    {
                        PropertyController.RepaintByName(appModel, appView, name);
                    }
                    else
                    {
                        // モデルに入ってなければ、非表示にするぜ☆（＾～＾）
                        PropertyController.InvisibleNoModel(appView, name, (errMsg)=>
                        {
                            Trace.WriteLine(errMsg);
                        });
                    }
                }
            }

            // 画面のサイズに合わせて再描画しようぜ☆（＾～＾）
            appView.FitSizeToWindow();

            // 星
            StarController.Repaint(appModel, appView);

            // 着手マーカー
            LastMoveMarkerController.Repaint(appModel, appView);

            appView.InvalidateVisual();
        }
    }
}
