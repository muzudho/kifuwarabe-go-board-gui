namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeGoBoardGui.Model.Dao;
    using KifuwarabeGoBoardGui.Model.Dto;

    public static class StoneView
    {
        /// <summary>
        /// 黒石を描いて非表示にして持っておこうぜ☆（＾～＾）？
        /// </summary>
        public static void Initialize(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            for (var i = 0; i < HyperParameter.MaxCellCount; i++)
            {
                var row = i / appModel.ColumnSize;
                var column = i % appModel.ColumnSize;

                var stone = new Ellipse();
                stone.Name = $"stone{i}";
                stone.Width = 10;
                stone.Height = 10;
                stone.StrokeThickness = 1.5;
                stone.Visibility = Visibility.Hidden;
                Panel.SetZIndex(stone, (int)ZOrder.Stone);

                // とりあえず黒石にして作っておこうぜ☆（＾～＾）
                stone.Fill = Brushes.Black;
                stone.Stroke = Brushes.White;

                // 盤☆（＾～＾）
                Canvas.SetLeft(stone, 0);
                Canvas.SetTop(stone, 0);
                appView.Stones.Add(stone);
                appView.canvas.Children.Add(stone);
            }
        }

        /// <summary>
        /// 石ならEllipse型☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="zShapedIndex"></param>
        /// <param name="stone"></param>
        public static void Repaint(ApplicationObjectDtoWrapper appModel, int zShapedIndex, Shape stone)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (stone == null)
            {
                throw new ArgumentNullException(nameof(stone));
            }

            // ビュー☆（＾～＾）
            {
                if (zShapedIndex < appModel.Board.Colors.Count)
                {
                    var color = appModel.Board.Colors[zShapedIndex];
                    if (ColorDto.Transparent.Equals(color))
                    {
                        // 透明☆（＾～＾）
                        stone.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        stone.Fill = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
                        stone.Stroke = new SolidColorBrush(Color.FromArgb(color.A, (byte)(255 - color.R), (byte)(255 - color.G), (byte)(255 - color.B))); // 補色
                        stone.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    // 範囲外☆（＾～＾）
                    stone.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// 石をウィンドウ・サイズに合わせようぜ☆（＾～＾）？
        /// </summary>
        public static void FitSizeToWindow(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            for (var zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var stone = appView.Stones[zShapedIndex];
                if (zShapedIndex < appModel.GetCellCount())
                {
                    appView.PutAnythingOnNode(zShapedIndex, (left, top) =>
                    {
                        // 大きさ☆（＾～＾）
                        stone.Width = appView.board.Width / appModel.GetColumnDiv() * 0.8;
                        stone.Height = appView.board.Height / appModel.GetRowDiv() * 0.8;

                        Canvas.SetLeft(stone, left - stone.Width / 2);
                        Canvas.SetTop(stone, top - stone.Height / 2);
                    });
                }
                else
                {
                    StoneDao.ChangeModelToSpace(appModel, zShapedIndex);
                }
            }
        }
    }
}
