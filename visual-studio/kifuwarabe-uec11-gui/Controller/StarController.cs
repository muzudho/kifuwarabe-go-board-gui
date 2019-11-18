namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeUec11Gui.Model;
    using KifuwarabeUec11Gui.InputScript;

    /// <summary>
    /// 星を操作するぜ☆（＾～＾）
    /// </summary>
    public static class StarController
    {
        public static void Repaint(ApplicationObjectModel appModel, MainWindow appView)
        {
            if (appModel == null)
            {
                throw new ArgumentNullException(nameof(appModel));
            }

            if (appView == null)
            {
                throw new ArgumentNullException(nameof(appView));
            }

            var starsModel = appModel.StringLists[StarsController.OutsideName].ToTextList();

            for (var i = 0; i < HyperParameter.MaxStarCount; i++)
            {
                var starView = appView.Stars[i];
                if (i < starsModel.Count)
                {
                    starView.Visibility = Visibility.Visible;
                    var start = 0;
                    CellAddress.Parse(starsModel[i], start, appModel, (cellAddress, curr) =>
                    {
                        if (cellAddress == null)
                        {
                            return start;
                        }

                        MainWindow.PutAnythingOnNode(appView, cellAddress.ToIndex(appModel), (left, top) =>
                        {
                            // 大きさ☆（＾～＾） 黒石と間違わないぐらい小さくしないとな☆（＾～＾）
                            starView.Width = appView.board.Width / appModel.Board.GetColumnDiv() * 0.3;
                            starView.Height = appView.board.Height / appModel.Board.GetRowDiv() * 0.3;

                            Canvas.SetLeft(starView, left - starView.Width / 2);
                            Canvas.SetTop(starView, top - starView.Height / 2);
                        });

                        return curr;
                    });
                }
                else
                {
                    starView.Visibility = Visibility.Hidden;
                }
            }
        }

        public static void Initialize(BoardModel model, MainWindow view)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            for (var i = 0; i < HyperParameter.MaxStarCount; i++)
            {
                var row = i / model.ColumnSize;
                var column = i % model.ColumnSize;

                var star = new Ellipse();
                star.Name = $"star{i}";
                star.Width = 1;
                star.Height = 1;
                Panel.SetZIndex(star, (int)ZOrder.Star);

                // 黒丸で☆（＾～＾）
                star.Fill = Brushes.Black;

                // 盤☆（＾～＾）
                Canvas.SetLeft(star, 0);
                Canvas.SetTop(star, 0);
                view.Stars.Add(star);
                view.canvas.Children.Add(star);
            }
        }
    }
}
