﻿namespace KifuwarabeUec11Gui
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.Output;

    public static class RowNumberController
    {
        public static void Repaint(BoardModel model, MainWindow view)
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

            var columnInterval = view.board.Width / model.GetColumnDiv();
            var rowInterval = view.board.Height / model.GetRowDiv();
            var paddingLeft = view.board.Width * 0.05;
            var paddingTop = view.board.Height * 0.05;

            for (var row = 0; row < HyperParameter.MaxRowSize; row++)
            {
                if (row < view.BoardModel.RowSize)
                {
                    // 逆さにするぜ☆（＾～＾）
                    var label = view.RowLabels[view.BoardModel.GetRowLastO0() - row];

                    label.Visibility = Visibility.Visible;
                    label.FontSize = columnInterval * 0.9;
                    label.Width = columnInterval * 1.8;
                    label.Height = rowInterval * 1.8;
                    // 盤☆（＾～＾）
                    Canvas.SetLeft(label, boardLeft + paddingLeft - label.Width / 2 + columnInterval * 0);
                    Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * row);
                }
                else
                {
                    var label = view.RowLabels[row];
                    label.Visibility = Visibility.Hidden;
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
                // var number = model.RowSize - row;
                var label = new Label();
                label.Name = $"rowLabel{number}";
                Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                if (9 < number)
                {
                    label.Content = number;
                }
                else
                {
                    // 文字位置の調整がうまく行かないので勘で調整☆（＾～＾）
                    label.Content = $"  {number}";
                }
                view.RowLabels.Add(label);
                view.canvas.Children.Add(label);
            }
        }
    }
}
