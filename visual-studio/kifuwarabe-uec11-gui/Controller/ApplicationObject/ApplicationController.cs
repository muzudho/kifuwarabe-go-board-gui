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
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.PlyRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.MoveRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.BlackNameRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.BlackTimeRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.BlackHamaRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.WhiteNameRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.WhiteTimeRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.WhiteHamaRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.KomiRealName);
            PropertyController.RepaintByName(appModel, appView, ApplicationObjectModel.InfoRealName);

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
