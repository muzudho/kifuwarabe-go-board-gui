namespace KifuwarabeUec11Gui.Controller
{
    using System;
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
            appView.DispatchTimer.Interval = TimeSpan.FromMilliseconds(appModel.Numbers[ApplicationObjectModel.IntervalMsecOutsideName].Value);
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
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Top2OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Top1OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Right3OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Right2OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Right1OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Left1OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Left2OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Left3OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.Left4OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.InfoOutsideName);

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
