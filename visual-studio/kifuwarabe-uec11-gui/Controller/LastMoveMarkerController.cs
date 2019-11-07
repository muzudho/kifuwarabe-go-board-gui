namespace KifuwarabeUec11Gui
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
        public static void Repaint(ApplicationObjectModel model, MainWindow view)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // Trace.WriteLine($"state.LastMoveIndex | {model.LastMoveIndex}");
            var lastMoveMarker = view.lastMoveMarker;

            if (model.State.Move.Visible)
            {
                lastMoveMarker.Visibility = Visibility.Visible;

                var (moveCellAddress, next) = CellAddress.Parse(model.State.Move.Value, 0, model);

                MainWindow.PutAnythingOnNode(view, moveCellAddress.ToIndex(model), (left, top) =>
                {
                    // Trace.WriteLine($"this.State.LastMoveIndex | left={left} top={top}");

                    lastMoveMarker.Width = view.board.Width / model.Board.GetColumnDiv() * 0.4;
                    lastMoveMarker.Height = view.board.Height / model.Board.GetRowDiv() * 0.4;

                    Canvas.SetLeft(lastMoveMarker, left - lastMoveMarker.Width / 2);
                    Canvas.SetTop(lastMoveMarker, top - lastMoveMarker.Height / 2);
                });
            }
            else
            {
                lastMoveMarker.Visibility = Visibility.Hidden;
            }
        }

        public static void SetIndex(ApplicationObjectModel model, MainWindow view, int zShapedIndex)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // 内部的には インデックスは Z字式 で持てだぜ☆（＾～＾）
            var text = CellAddress.FromIndex(zShapedIndex, model).ToDisplayTrimed(model);
            model.State.Move.Value = text;
            view.lastMoveValue.Content = text;
        }

        public static void SetAddress(ApplicationObjectModel model, MainWindow view, CellAddress cellAddress)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            if (cellAddress == null)
            {
                throw new ArgumentNullException(nameof(cellAddress));
            }

            var text = cellAddress.ToDisplayTrimed(model);
            model.State.Move.Value = text;
            view.lastMoveValue.Content = text;
        }
    }
}
