﻿namespace KifuwarabeGoBoardGui.View
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeGoBoardGui.Controller.Parser;
    using KifuwarabeGoBoardGui.Model.Dto;

    /// <summary>
    /// 星を操作するぜ☆（＾～＾）
    /// </summary>
    public static class StarView
    {
        public static void Repaint(ApplicationObjectDtoWrapper appModel, MainWindow appView)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            var starsModel = appModel.GetStringList(ApplicationDto.StarsRealName).Value;

            for (var i = 0; i < HyperParameter.MaxStarCount; i++)
            {
                var starView = appView.Stars[i];
                if (i < starsModel.Count)
                {
                    starView.Visibility = Visibility.Visible;
                    var start = 0;
                    CellAddressParser.Parse(
                        starsModel[i],
                        start,
                        appModel,
                        (cellAddress, curr) =>
                        {
                            if (cellAddress == null)
                            {
                                return start;
                            }

                            appView.PutAnythingOnNode(cellAddress.ToIndex(appModel), (left, top) =>
                            {
                                // 大きさ☆（＾～＾） 黒石と間違わないぐらい小さくしないとな☆（＾～＾）
                                starView.Width = appView.board.Width / appModel.GetColumnDiv() * 0.3;
                                starView.Height = appView.board.Height / appModel.GetRowDiv() * 0.3;

                                Canvas.SetLeft(starView, left - starView.Width / 2);
                                Canvas.SetTop(starView, top - starView.Height / 2);
                            });

                            return curr;
                        },
                        ()=>
                        {
                            return start;
                        });
                }
                else
                {
                    starView.Visibility = Visibility.Hidden;
                }
            }
        }

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

            for (var i = 0; i < HyperParameter.MaxStarCount; i++)
            {
                var row = i / appModel.ColumnSize;
                var column = i % appModel.ColumnSize;

                var star = new Ellipse
                {
                    Name = $"star{i}",
                    Width = 1,
                    Height = 1
                };
                Panel.SetZIndex(star, (int)ZOrder.Star);

                // 黒丸で☆（＾～＾）
                star.Fill = Brushes.Black;

                // 盤☆（＾～＾）
                Canvas.SetLeft(star, 0);
                Canvas.SetTop(star, 0);
                appView.Stars.Add(star);
                appView.canvas.Children.Add(star);
            }
        }
    }
}
