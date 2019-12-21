namespace KifuwarabeGoBoardGui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.InputScript;
    using KifuwarabeGoBoardGui.Model;

    /// <summary>
    /// 最後の着手のマーカー☆（＾～＾）
    /// </summary>
    public static class MoveMarkerViewController
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
            var moveMarker = appView.moveMarker;

            appModel.MatchPropertyOption(
                ApplicationDto.MoveRealName,
                (property) =>
                {
                    if (property.Visible)
                    {
                        moveMarker.Visibility = Visibility.Visible;

                        var start = 0;
                        CellAddressParser.Parse(
                            property.ValueAsText(),
                            start,
                            appModel,
                            (moveCellAddress, curr) =>
                            {
                                if (moveCellAddress == null)
                                {
                                    moveMarker.Visibility = Visibility.Hidden;
                                    return start;
                                }

                                appView.PutAnythingOnNode(moveCellAddress.ToIndex(appModel), (left, top) =>
                                {
                                    // Trace.WriteLine($"this.State.LastMoveIndex | left={left} top={top}");

                                    moveMarker.Width = appView.board.Width / appModel.GetColumnDiv() * 0.4;
                                    moveMarker.Height = appView.board.Height / appModel.GetRowDiv() * 0.4;

                                    Canvas.SetLeft(moveMarker, left - moveMarker.Width / 2);
                                    Canvas.SetTop(moveMarker, top - moveMarker.Height / 2);
                                });

                                return curr;
                            },
                            ()=>
                            {
                                // パース失敗☆（＾～＾）
                                return start;
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
