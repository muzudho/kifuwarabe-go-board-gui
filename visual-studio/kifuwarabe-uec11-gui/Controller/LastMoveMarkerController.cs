namespace KifuwarabeUec11Gui
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.InputScript;
    using KifuwarabeUec11Gui.InputScript.InternationalGo;
    using KifuwarabeUec11Gui.InputScript.Translator;
    using KifuwarabeUec11Gui.Output;

    /// <summary>
    /// 最後の着手のマーカー☆（＾～＾）
    /// </summary>
    public static class LastMoveMarkerController
    {
        public static void SetIndex(State model, MainWindow view, int zShapedIndex)
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
            model.LastMoveIndex = zShapedIndex;

            // 表示するときは 国際囲碁式 のように上下逆にひっくり返そうぜ☆（＾～＾）
            view.lastMoveValue.Content = InternationalCellAddress.FromIndex(zShapedIndex, view.BoardModel).ToDisplay(view.BoardModel);
        }

        public static void SetAddress(State model, MainWindow view, CellAddress cellAddress)
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

            // インデックスは Z字式 で出てくるぜ☆（＾～＾）
            // Trace.WriteLine($"Move            | cellAddress.ToIndex() = {cellAddress.ToIndex()}");
            // Trace.WriteLine($"Move            | Convert = {ZShapedToInternational.ConvertIndex(cellAddress.ToIndex())}");
            // 上下逆にひっくり返そうぜ☆（＾～＾）
            model.LastMoveIndex = ZShapedToInternational.ConvertIndex(cellAddress.ToIndex(view.BoardModel), view.BoardModel);
            view.lastMoveValue.Content = cellAddress.ToDisplay(view.BoardModel);
        }

        public static void Repaint(State model, MainWindow view)
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

            if (-1 < model.LastMoveIndex)
            {
                var board = view.board;

                lastMoveMarker.Visibility = Visibility.Visible;
                MainWindow.PutAnythingOnNode(view, model.LastMoveIndex, (left, top) =>
                {
                    // Trace.WriteLine($"this.State.LastMoveIndex | left={left} top={top}");

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
