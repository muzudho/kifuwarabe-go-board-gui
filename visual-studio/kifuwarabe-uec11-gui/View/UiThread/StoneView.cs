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

    public static class StoneView
    {
        /// <summary>
        /// 石ならEllipse型☆（＾～＾）
        /// </summary>
        /// <param name="appModel"></param>
        /// <param name="zShapedIndex"></param>
        /// <param name="piece1"></param>
        public static void Repaint(ApplicationObjectDtoWrapper appModel, int zShapedIndex, Shape piece1)
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
                if (zShapedIndex < appModel.Board.Layer1.Count)
                {
                    var piece2 = appModel.Board.Layer1[zShapedIndex];
                    if (ColorDto.Transparent.Equals(piece2.Color))
                    {
                        // 透明☆（＾～＾）
                        piece1.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        piece1.Fill = new SolidColorBrush(Color.FromArgb(piece2.Color.A, piece2.Color.R, piece2.Color.G, piece2.Color.B));
                        piece1.Stroke = new SolidColorBrush(Color.FromArgb(piece2.Color.A, (byte)(255 - piece2.Color.R), (byte)(255 - piece2.Color.G), (byte)(255 - piece2.Color.B))); // 補色
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
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            appView.PieceBoard.ForeachPiace(
                (piece, zShapedIndex) =>
                {
                    if (zShapedIndex < appModel.GetCellCount())
                    {
                        appView.PutAnythingOnNode(zShapedIndex, (left, top) =>
                        {
                            // 大きさ☆（＾～＾）
                            piece.Width = appView.board.Width / appModel.GetColumnDiv() * 0.8;
                            piece.Height = appView.board.Height / appModel.GetRowDiv() * 0.8;

                            Canvas.SetLeft(piece, left - piece.Width / 2);
                            Canvas.SetTop(piece, top - piece.Height / 2);
                        });
                    }
                    else
                    {
                        StoneDao.ChangeModel(appModel, new PieceDto(ColorDto.Transparent, PieceShapes.Stone), zShapedIndex);
                    }
                });
        }
    }
}
