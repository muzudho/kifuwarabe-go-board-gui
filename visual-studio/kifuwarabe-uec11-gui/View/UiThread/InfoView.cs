namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model.Dto;

    /// <summary>
    /// 情報ビュー☆（＾～＾）指し手とか表示したいときに使うやつだぜ☆（＾～＾）
    /// </summary>
    public static class InfoView
    {
        public static void Repaint(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            // Trace.WriteLine($"state.LastMoveIndex | {model.LastMoveIndex}");
            var infoLabel = appView.infoValue;
            // var infoContent = appView.infoValue.Content;

            appModel.MatchPropertyOption(
                ApplicationDto.InfoRealName,
                (property) =>
                {
                    if (property.Visible)
                    {
                        infoLabel.Visibility = Visibility.Visible;
                        infoLabel.Content = property.ValueAsText();
                    }
                    else
                    {
                        infoLabel.Visibility = Visibility.Hidden;
                    }
                },
                () =>
                {
                    infoLabel.Visibility = Visibility.Hidden;
                });
        }
    }
}
