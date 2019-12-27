namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeGoBoardGui.Model.Dao;
    using KifuwarabeGoBoardGui.Model.Dto;
    using KifuwarabeGoBoardGui.Model.Dto.Application.Standard;

    public static class MarkView
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
                appView.Marks.Add(stone);
                appView.canvas.Children.Add(stone);
            }
        }

        /// <summary>
        /// 石ならEllipse型☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="zShapedIndex"></param>
        /// <param name="piece1"></param>
        public static void Repaint(ApplicationObjectDtoWrapper appModel, int layerIndex, int zShapedIndex, Shape piece1)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (piece1 == null)
            {
                throw new ArgumentNullException(nameof(piece1));
            }

            // ビュー☆（＾～＾）
            {
                if (zShapedIndex < appModel.Board.Layers[layerIndex].Pieces.Count)
                {
                    var piece2 = appModel.Board.Layers[layerIndex].Pieces[zShapedIndex];
                    if (ColorDto.Transparent.Equals(piece2.Color))
                    {
                        // 透明☆（＾～＾）
                        piece1.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        piece1.Fill = new SolidColorBrush(Color.FromArgb(piece2.Color.A, piece2.Color.R, piece2.Color.G, piece2.Color.B));
                        piece1.Stroke = new SolidColorBrush(Color.FromArgb(piece2.Color.A, (byte)(255-piece2.Color.R), (byte)(255-piece2.Color.G), (byte)(255-piece2.Color.B))); // 補色
                        piece1.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    // 範囲外☆（＾～＾）
                    piece1.Visibility = Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// 石をウィンドウ・サイズに合わせようぜ☆（＾～＾）？
        /// </summary>
        public static void FitSizeToWindow(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            // TODO レイヤー番号。
            var layerIndex = 0;

            for (var zShapedIndex = 0; zShapedIndex < HyperParameter.MaxCellCount; zShapedIndex++)
            {
                var mark = appView.Marks[zShapedIndex];
                if (zShapedIndex < appModel.GetCellCount())
                {
                    appView.PutAnythingOnNode(zShapedIndex, (left, top) =>
                    {
                        // 大きさ☆（＾～＾）
                        mark.Width = appView.board.Width / appModel.GetColumnDiv() * 0.8;
                        mark.Height = appView.board.Height / appModel.GetRowDiv() * 0.8;

                        Canvas.SetLeft(mark, left - mark.Width / 2);
                        Canvas.SetTop(mark, top - mark.Height / 2);
                    });
                }
                else
                {
                    PieceDao.ChangeModel(appModel, new PieceDto(ColorDto.Transparent, PieceShapes.Stone), layerIndex, zShapedIndex);
                }
            }
        }
    }
}
