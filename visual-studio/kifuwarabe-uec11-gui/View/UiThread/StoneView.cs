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
                        StoneDao.ChangeModel(appModel, ColorDto.Transparent, zShapedIndex);
                    }
                });
        }
    }
}
