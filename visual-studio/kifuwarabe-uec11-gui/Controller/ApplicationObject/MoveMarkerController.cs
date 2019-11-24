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
    public static class MoveMarkerController
    {
        public static void Repaint(ApplicationObjectModelWrapper appModel, MainWindow appView)
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
            var moveMarker = appView.moveMarker;

            appModel.MatchPropertyOption(
                ApplicationObjectModel.MoveRealName,
                (property) =>
                {
                    if (property.Visible)
                    {
                        moveMarker.Visibility = Visibility.Visible;

                        var start = 0;
                        CellAddress.Parse(property.ValueAsText(), start, appModel, (moveCellAddress, curr) =>
                        {
                            if (moveCellAddress == null)
                            {
                                moveMarker.Visibility = Visibility.Hidden;
                                return start;
                            }

                            MainWindow.PutAnythingOnNode(appView, moveCellAddress.ToIndex(appModel), (left, top) =>
                            {
                                // Trace.WriteLine($"this.State.LastMoveIndex | left={left} top={top}");

                                moveMarker.Width = appView.board.Width / appModel.Board.GetColumnDiv() * 0.4;
                                moveMarker.Height = appView.board.Height / appModel.Board.GetRowDiv() * 0.4;

                                Canvas.SetLeft(moveMarker, left - moveMarker.Width / 2);
                                Canvas.SetTop(moveMarker, top - moveMarker.Height / 2);
                            });

                            return curr;
                        });
                    }
                    else
                    {
                        moveMarker.Visibility = Visibility.Hidden;
                    }
                },
                () =>
                {
                    moveMarker.Visibility = Visibility.Hidden;
                });
        }
    }
}
