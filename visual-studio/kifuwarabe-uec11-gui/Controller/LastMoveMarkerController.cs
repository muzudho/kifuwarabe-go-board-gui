namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.Output;

    /// <summary>
    /// 最後の着手のマーカー☆（＾～＾）
    /// </summary>
    public static class LastMoveMarkerController
    {
        public static void Repaint(State model, MainWindow view)
        {
            if (model==null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            Trace.WriteLine($"state.LastMoveIndex | {model.LastMoveIndex}");
            var lastMoveMarker = view.lastMoveMarker;

            if (-1 < model.LastMoveIndex)
            {
                var board = view.board;

                lastMoveMarker.Visibility = Visibility.Visible;
                MainWindow.PutAnythingOnNode(view, model.LastMoveIndex, (left, top) =>
                {
                    Trace.WriteLine($"this.State.LastMoveIndex | left={left} top={top}");

                    lastMoveMarker.Width = board.Width / MainWindow.BoardDiv * 0.4;
                    lastMoveMarker.Height = board.Height / MainWindow.BoardDiv * 0.4;

                    Canvas.SetLeft(lastMoveMarker, left - lastMoveMarker.Width / 2);
                    Canvas.SetTop(lastMoveMarker, top - lastMoveMarker.Height / 2);
                });
            }
            else
            {
                lastMoveMarker.Visibility = Visibility.Hidden;
            }
        }
    }
}
