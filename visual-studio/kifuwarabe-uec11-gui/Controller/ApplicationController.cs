namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using KifuwarabeUec11Gui.Model;

    public static class ApplicationController
    {
        public static void RepaintAllViews(ApplicationObjectModelForCSharp appModel, MainWindow appView)
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
            appView.DispatchTimer.Interval = TimeSpan.FromMilliseconds(appModel.Numbers[ApplicationObjectModelForCSharp.IntervalMsecOutsideName].Value);
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
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.PlyOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, LastMoveMarkerController.OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.BlackNameOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.BlackTimeOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.BlackHamaOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.WhiteNameOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.WhiteTimeOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.WhiteHamaOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.KomiOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModelForCSharp.InfoOutsideName);

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
