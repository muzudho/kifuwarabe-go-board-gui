namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.Model;

    public static class RowNumberController
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

            // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
            // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
            var shortenEdge = System.Math.Min(view.grid.RenderSize.Width, view.grid.RenderSize.Height) * 0.95;

            // センターを求めようぜ☆（＾～＾）
            var centerX = view.grid.RenderSize.Width / 2;
            var centerY = view.grid.RenderSize.Height / 2;

            // 盤☆（＾～＾）
            var boardLeft = centerX - shortenEdge / 2;
            var boardTop = centerY - shortenEdge / 2;

            var columnInterval = view.board.Width / model.Board.GetColumnDiv();
            var rowInterval = view.board.Height / model.Board.GetRowDiv();
            var paddingLeft = view.board.Width * 0.05;
            var paddingTop = view.board.Height * 0.05;

            for (var row = 0; row < HyperParameter.MaxRowSize; row++)
            {
                var rowNumbers = model.Properties[RowNumbersController.OutsideName].ToTextList();
                if (rowNumbers.Count <= row || model.Board.RowSize <= row)
                {
                    // 範囲外アクセス。
                    var label = view.RowLabels[row];
                    label.Visibility = Visibility.Hidden;
                }
                else
                {
                    var label = view.RowLabels[row];
                    label.Content = rowNumbers[row];
                    label.Visibility = Visibility.Visible;
                    label.FontSize = columnInterval * 0.9;
                    label.Width = columnInterval * 1.8;
                    label.Height = rowInterval * 1.8;
                    // 盤☆（＾～＾）
                    Canvas.SetLeft(label, boardLeft + paddingLeft - label.Width / 2 + columnInterval * 0);
                    Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * row);
                }
            }
        }

        public static void Initialize(BoardModel model, MainWindow view)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            for (var row = 0; row < HyperParameter.MaxRowSize; row++)
            {
                var number = row + 1;
                var label = new Label();
                label.Name = $"rowLabel{number}";
                Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                view.RowLabels.Add(label);
                view.canvas.Children.Add(label);
            }
        }
    }
}
