﻿namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeGoBoardGui.Model.Dao;
    using KifuwarabeGoBoardGui.Model.Dto;
    using KifuwarabeGoBoardGui.Model.Dto.Application.Standard;

    public static class PieceView
    {
        /// <summary>
        /// ピースの描画☆（＾～＾）
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
        /// ピースをウィンドウ・サイズに合わせようぜ☆（＾～＾）？
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

            // 交点の上に合わせるなら 0、マスの中央に合わせるなら 0.5。 
            var offsetLeftRate = 0.0;
            var offsetTopRate = 0.0;

            if (ApplicationDto.Square == appModel.GetString(ApplicationDto.PieceLocationRealName).Value)
            {
                offsetLeftRate = 0.5;
                offsetTopRate = 0.5;
            }

            // TODO レイヤー番号。
            var layerIndex = 0;

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

                            Canvas.SetLeft(piece, left - piece.Width / 2 + piece.Width * offsetLeftRate);
                            Canvas.SetTop(piece, top - piece.Height / 2 + piece.Height * offsetTopRate);
                        });
                    }
                    else
                    {
                        PieceDao.ChangeModel(appModel, new PieceDto(ColorDto.Transparent, PieceShapes.Stone), layerIndex, zShapedIndex);
                    }
                });
        }
    }
}
