namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 列番号☆（＾～＾）
    /// </summary>
    public static class ColumnNumberViewController
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

            var columnNumbers = appModel.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).Value;

            for (var column = 0; column < HyperParameter.MaxColumnSize; column++)
            {
                var label = appView.ColumnLabels[column];
                if (columnNumbers.Count <= column || appModel.Board.ColumnSize <= column)
                {
                    // 範囲外アクセス。
                    label.Visibility = Visibility.Hidden;
                }
                else
                {
                    label.Content = columnNumbers[column];

                    // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
                    // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
                    var shortenEdge = System.Math.Min(appView.grid.RenderSize.Width, appView.grid.RenderSize.Height) * 0.95;

                    // センターを求めようぜ☆（＾～＾）
                    var centerX = appView.grid.RenderSize.Width / 2;
                    var centerY = appView.grid.RenderSize.Height / 2;

                    // 盤☆（＾～＾）
                    var boardLeft = centerX - shortenEdge / 2;
                    var boardTop = centerY - shortenEdge / 2;
                    var paddingLeft = appView.board.Width * 0.05;
                    var paddingTop = appView.board.Height * 0.05;
                    var columnInterval = appView.board.Width / appModel.Board.GetColumnDiv();
                    var rowInterval = appView.board.Height / appModel.Board.GetRowDiv();

                    label.Visibility = Visibility.Visible;
                    label.FontSize = columnInterval * 0.9;
                    label.Width = columnInterval * 1.8;
                    label.Height = rowInterval * 1.8;
                    // 文字位置の調整は　良い方法がないので勘で調整☆（＾～＾）
                    Canvas.SetLeft(label, boardLeft + paddingLeft * 1.05 - label.Width / 3 + columnInterval * 1.01 * (column + MainWindow.SignLen));
                    Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * appModel.Board.RowSize);
                }
            }
        }

        public static void Initialize(MainWindow appView)
        {
            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            for (var column = 0; column < HyperParameter.MaxColumnSize; column++)
            {
                var label = new Label();
                label.Name = $"columnLabel{column + 1}";
                Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                appView.ColumnLabels.Add(label);
                appView.canvas.Children.Add(label);
            }
        }
    }
}
