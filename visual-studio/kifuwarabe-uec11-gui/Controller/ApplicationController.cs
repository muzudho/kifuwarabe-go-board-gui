namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using KifuwarabeUec11Gui.Model;

    public static class ApplicationController
    {
        public static void RepaintAllViews(ApplicationObjectModel appModel, MainWindow appView)
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
            if (int.TryParse(appModel.Properties[ApplicationObjectModel.IntervalMsecOutsideName].ToText(), out int outValue))
            {
                appView.DispatchTimer.Interval = TimeSpan.FromMilliseconds(outValue);
                // Trace.WriteLine($"interval-msec: {model.State.IntervalMsec}");
            }

            // 列番号
            ColumnNumberController.Repaint(appModel, appView);

            // 行番号
            RowNumberController.Repaint(appModel, appView);

            // 星
            StarController.Repaint(appModel, appView);

            // 石
            for (int index = 0; index < HyperParameter.MaxCellCount; index++)
            {
                StoneController.Repaint(appModel, appView, index);
            }

            // 着手マーカー
            LastMoveMarkerController.Repaint(appModel, appView);

            // TODO UIウィジェット
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.PlyOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, LastMoveMarkerController.OutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.BlackNameOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.BlackTimeOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.BlackHamaOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.WhiteNameOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.WhiteTimeOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.WhiteHamaOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.KomiOutsideName);
            PropertyController.RepaintByOutsideName(appModel, appView, ApplicationObjectModel.InfoOutsideName);

            // 画面のサイズに合わせて再描画しようぜ☆（＾～＾）
            appView.FitSizeToWindow();
            appView.InvalidateVisual();
        }
    }
}
