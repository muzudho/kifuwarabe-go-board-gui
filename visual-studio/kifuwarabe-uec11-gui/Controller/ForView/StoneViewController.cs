namespace KifuwarabeGoBoardGui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeGoBoardGui.Model;

    public static class StoneViewController
    {
        /// <summary>
        /// 黒石を描いて非表示にして持っておこうぜ☆（＾～＾）？
        /// </summary>
        public static void Initialize(ApplicationObjectModelWrapper appModel, MainWindow appView)
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
                var row = i / appModel.Board.ColumnSize;
                var column = i % appModel.Board.ColumnSize;

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
        public static void Repaint(ApplicationObjectModelWrapper appModel, int zShapedIndex, Shape stone)
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
                if (zShapedIndex < appModel.Board.Stones.Count)
                {
                    switch (appModel.Board.Stones[zShapedIndex])
                    {
                        case Stone.Black:
                            stone.Fill = Brushes.Black;
                            stone.Stroke = Brushes.White;
                            stone.Visibility = Visibility.Visible;
                            break;
                        case Stone.White:
                            stone.Fill = Brushes.White;
                            stone.Stroke = Brushes.Black;
                            stone.Visibility = Visibility.Visible;
                            break;
                        case Stone.None:
                            stone.Visibility = Visibility.Hidden;
                            break;
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
        public static void FitSizeToWindow(ApplicationObjectModelWrapper appModel, MainWindow appView)
        {
            for (var zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var stone = appView.Stones[zShapedIndex];
                if (zShapedIndex < appModel.Board.GetCellCount())
                {
                    appView.PutAnythingOnNode(zShapedIndex, (left, top) =>
                    {
                        // 大きさ☆（＾～＾）
                        stone.Width = appView.board.Width / appModel.Board.GetColumnDiv() * 0.8;
                        stone.Height = appView.board.Height / appModel.Board.GetRowDiv() * 0.8;

                        Canvas.SetLeft(stone, left - stone.Width / 2);
                        Canvas.SetTop(stone, top - stone.Height / 2);
                    });
                }
                else
                {
                    StoneModelController.ChangeModelToSpace(appModel, zShapedIndex);
                }
            }
        }
    }
}
