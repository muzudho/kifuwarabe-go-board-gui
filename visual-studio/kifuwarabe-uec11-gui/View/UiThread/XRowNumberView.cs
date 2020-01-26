namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class XRowNumberView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="appView"></param>
        /// <param name="boardLeftTop">盤の左上座標</param>
        /// <param name="rowNumbersStringList"></param>
        /// <param name="foregroundBrush"></param>
        /// <param name="rowLabels"></param>
        public static void Repaint(ApplicationObjectDtoWrapper appModel, MainWindow appView, Point boardLeftTop, PropertyStringList rowNumbersStringList, System.Windows.Media.Brush foregroundBrush, List<Label> rowLabels)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (rowNumbersStringList == null)
            {
                throw new ArgumentNullException(nameof(rowNumbersStringList));
            }

            if (rowLabels == null)
            {
                throw new ArgumentNullException(nameof(rowLabels));
            }

            var rowNumbers = rowNumbersStringList.Value;

            // 盤☆（＾～＾）
            var columnInterval = appView.board.Width / appModel.GetColumnDiv();
            var rowInterval = appView.board.Height / appModel.GetRowDiv();
            var paddingLeft = appView.board.Width * 0.05;
            var paddingTop = appView.board.Height * 0.05;

            for (var row = 0; row < rowLabels.Count; row++)
            {
                var label = rowLabels[row];

                if (rowNumbers.Count <= row || appModel.RowSize <= row)
                {
                    // 範囲外アクセス。
                    label.Visibility = Visibility.Hidden;
                }
                else
                {
                    label.Content = rowNumbers[row];
                    label.Visibility = Visibility.Visible;
                    // 0.9倍がちょうどいいんだが☆（＾～＾）
                    label.FontSize = columnInterval * 0.65;
                    label.Width = columnInterval * 1.8;
                    label.Height = rowInterval * 1.8;
                    label.Foreground = foregroundBrush;

                    // 盤☆（＾～＾）
                    Canvas.SetLeft(label, boardLeftTop.X + paddingLeft - label.Width / 2 + columnInterval * 0);
                    Canvas.SetTop(label, boardLeftTop.Y + paddingTop - label.Height / 2 + rowInterval * row);
                }
            }
        }

        public static void Initialize(MainWindow appView)
        {
            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            for (var row = 0; row < HyperParameter.MaxRowSize; row++)
            {
                // 面の行
                if (row < HyperParameter.MaxRowSize - 1)
                {
                    var number = row + 1;
                    var label = new Label
                    {
                        Name = $"tileRowLabel{number}"
                    };
                    Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                    appView.TileRowLabels.Add(label);
                    appView.canvas.Children.Add(label);
                }

                // 線の行
                {
                    var number = row + 1;
                    var label = new Label
                    {
                        Name = $"lineRowLabel{number}"
                    };
                    Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                    appView.LineRowLabels.Add(label);
                    appView.canvas.Children.Add(label);
                }
            }
        }
    }
}
