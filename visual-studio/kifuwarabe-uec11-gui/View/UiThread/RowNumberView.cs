namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class RowNumberView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="appView"></param>
        /// <param name="boardLeftTop">盤の左上座標</param>
        public static void Repaint(ApplicationObjectDtoWrapper appModel, MainWindow appView, Point boardLeftTop)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            // 盤☆（＾～＾）
            var columnInterval = appView.board.Width / appModel.GetColumnDiv();
            var rowInterval = appView.board.Height / appModel.GetRowDiv();
            var paddingLeft = appView.board.Width * 0.05;
            var paddingTop = appView.board.Height * 0.05;

            for (var row = 0; row < HyperParameter.MaxRowSize; row++)
            {
                var rowNumbers = appModel.GetStringList(ApplicationDto.LineRowNumbersRealName).Value;
                if (rowNumbers.Count <= row || appModel.RowSize <= row)
                {
                    // 範囲外アクセス。
                    var label = appView.RowLabels[row];
                    label.Visibility = Visibility.Hidden;
                }
                else
                {
                    var label = appView.RowLabels[row];
                    label.Content = rowNumbers[row];
                    label.Visibility = Visibility.Visible;
                    label.FontSize = columnInterval * 0.9;
                    label.Width = columnInterval * 1.8;
                    label.Height = rowInterval * 1.8;

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
                var number = row + 1;
                var label = new Label
                {
                    Name = $"rowLabel{number}"
                };
                Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                appView.RowLabels.Add(label);
                appView.canvas.Children.Add(label);
            }
        }
    }
}
