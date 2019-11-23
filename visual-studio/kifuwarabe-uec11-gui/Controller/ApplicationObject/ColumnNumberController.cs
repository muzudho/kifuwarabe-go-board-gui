namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 列番号☆（＾～＾）
    /// </summary>
    public static class ColumnNumberController
    {
        public static void Repaint(ApplicationObjectModelWrapper model, MainWindow view)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            var columnNumbers = model.GetStringList(ApplicationObjectModel.ColumnNumbersRealName).Value;

            for (var column = 0; column < HyperParameter.MaxColumnSize; column++)
            {
                var label = view.ColumnLabels[column];
                if (columnNumbers.Count <= column || model.Board.ColumnSize <= column)
                {
                    // 範囲外アクセス。
                    label.Visibility = Visibility.Hidden;
                }
                else
                {
                    label.Content = columnNumbers[column];

                    // 昔でいう呼び方で Client area は WPF では grid.RenderSize らしい（＾ｑ＾）
                    // 短い方の一辺を求めようぜ☆（＾～＾）ぴったり枠にくっつくと窮屈なんで 0.95 掛けで☆（＾～＾）
                    var shortenEdge = System.Math.Min(view.grid.RenderSize.Width, view.grid.RenderSize.Height) * 0.95;

                    // センターを求めようぜ☆（＾～＾）
                    var centerX = view.grid.RenderSize.Width / 2;
                    var centerY = view.grid.RenderSize.Height / 2;

                    // 盤☆（＾～＾）
                    var boardLeft = centerX - shortenEdge / 2;
                    var boardTop = centerY - shortenEdge / 2;
                    var paddingLeft = view.board.Width * 0.05;
                    var paddingTop = view.board.Height * 0.05;
                    var columnInterval = view.board.Width / model.Board.GetColumnDiv();
                    var rowInterval = view.board.Height / model.Board.GetRowDiv();

                    label.Visibility = Visibility.Visible;
                    label.FontSize = columnInterval * 0.9;
                    label.Width = columnInterval * 1.8;
                    label.Height = rowInterval * 1.8;
                    // 文字位置の調整は　良い方法がないので勘で調整☆（＾～＾）
                    Canvas.SetLeft(label, boardLeft + paddingLeft * 1.05 - label.Width / 3 + columnInterval * 1.01 * (column + MainWindow.SignLen));
                    Canvas.SetTop(label, boardTop + paddingTop - label.Height / 2 + rowInterval * model.Board.RowSize);
                }
            }
        }

        public static void Initialize(MainWindow view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            for (var column = 0; column < HyperParameter.MaxColumnSize; column++)
            {
                var label = new Label();
                label.Name = $"columnLabel{column + 1}";
                Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                // label.Content = (char)(65 + (column < 8 ? column : column + 1)); // 65はAsciiCodeのA。コンピューター囲碁では I は抜く慣習。
                view.ColumnLabels.Add(label);
                view.canvas.Children.Add(label);
            }
        }
    }
}
