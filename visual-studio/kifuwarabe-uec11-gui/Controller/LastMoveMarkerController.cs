namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 最後の着手のマーカー☆（＾～＾）
    /// </summary>
    public static class LastMoveMarkerController
    {
        public static string OutsideName => "move";

        public static void Repaint(ApplicationObjectModel appModel, MainWindow appView)
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
            var lastMoveMarker = appView.lastMoveMarker;

            var property = appModel.ReadProperty(OutsideName);

            if (property.Visible)
            {
                lastMoveMarker.Visibility = Visibility.Visible;

                var start = 0;
                CellAddress.Parse(property.ToText(), start, appModel, (moveCellAddress, curr) =>
                {
                    if (moveCellAddress == null)
                    {
                        lastMoveMarker.Visibility = Visibility.Hidden;
                        return start;
                    }

                    MainWindow.PutAnythingOnNode(appView, moveCellAddress.ToIndex(appModel), (left, top) =>
                    {
                            // Trace.WriteLine($"this.State.LastMoveIndex | left={left} top={top}");

                            lastMoveMarker.Width = appView.board.Width / appModel.Board.GetColumnDiv(appModel.Metrics) * 0.4;
                        lastMoveMarker.Height = appView.board.Height / appModel.Board.GetRowDiv(appModel.Metrics) * 0.4;

                        Canvas.SetLeft(lastMoveMarker, left - lastMoveMarker.Width / 2);
                        Canvas.SetTop(lastMoveMarker, top - lastMoveMarker.Height / 2);
                    });

                    return curr;
                });
            }
            else
            {
                lastMoveMarker.Visibility = Visibility.Hidden;
            }
        }
    }
}
