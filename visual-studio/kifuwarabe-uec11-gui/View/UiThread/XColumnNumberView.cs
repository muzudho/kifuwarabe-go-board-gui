namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using KifuwarabeGoBoardGui.Model.Dto;

    /// <summary>
    /// 列番号☆（＾～＾）
    /// 交点に合わせるタイプと、マスに合わせるタイプの２通りがあるぜ☆（＾～＾）
    /// </summary>
    public static class XColumnNumberView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="appView"></param>
        /// <param name="boardLeftTop">盤の左上座標</param>
        /// <param name="columnNumbersStringList"></param>
        /// <param name="foregroundBrush"></param>
        /// <param name="columnLabels"></param>
        public static void Repaint(ApplicationObjectDtoWrapper appModel, MainWindow appView, Point boardLeftTop, PropertyStringList columnNumbersStringList, System.Windows.Media.Brush foregroundBrush, List<Label> columnLabels)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            if (columnNumbersStringList == null)
            {
                throw new ArgumentNullException(nameof(columnNumbersStringList));
            }

            if (columnLabels == null)
            {
                throw new ArgumentNullException(nameof(columnLabels));
            }

            var columnNumbers = columnNumbersStringList.Value;

            for (var column = 0; column < columnLabels.Count; column++)
            {
                var label = columnLabels[column];
                if (columnNumbers.Count <= column || appModel.ColumnSize <= column)
                {
                    // 範囲外アクセス。
                    label.Visibility = Visibility.Hidden;
                }
                else
                {
                    label.Content = columnNumbers[column];

                    // 盤☆（＾～＾）
                    var paddingLeft = appView.board.Width * 0.05;
                    var paddingTop = appView.board.Height * 0.05;
                    var columnInterval = appView.board.Width / appModel.GetColumnDiv();
                    var rowInterval = appView.board.Height / appModel.GetRowDiv();

                    label.Visibility = Visibility.Visible;
                    // 0.9倍がちょうどいいんだが☆（＾～＾）
                    label.FontSize = columnInterval * 0.65;
                    label.Width = BoardView.GetLabelWidth(appModel, appView);
                    label.Height = rowInterval * 1.8;
                    label.Foreground = foregroundBrush;

                    // 文字位置の調整は　良い方法がないので勘で調整☆（＾～＾）
                    Canvas.SetLeft(label, boardLeftTop.X + paddingLeft * 1.05 - label.Width / 3 + columnInterval * 1.01 * (column + MainWindow.SignLen));
                    Canvas.SetTop(label, boardLeftTop.Y + paddingTop - label.Height / 2 + rowInterval * appModel.RowSize);
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
                // 面の列
                if (column < HyperParameter.MaxColumnSize - 1)
                {
                    var label = new Label
                    {
                        Name = $"tileColumnLabel{column + 1}"
                    };
                    Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                    appView.TileColumnLabels.Add(label);
                    appView.canvas.Children.Add(label);
                }

                // 線の列
                {
                    var label = new Label
                    {
                        Name = $"lineColumnLabel{column + 1}"
                    };
                    Panel.SetZIndex(label, (int)ZOrder.LineNumber);
                    appView.LineColumnLabels.Add(label);
                    appView.canvas.Children.Add(label);
                }
            }
        }
    }
}
