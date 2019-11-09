namespace KifuwarabeUec11Gui
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
        public static void Repaint(ApplicationObjectModel model, MainWindow view)
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
                var starView = view.Stars[i];
                var starsModel = model.Properties[StarsController.OutsideName].ToTextList();
                if (i < starsModel.Count)
                {
                    starView.Visibility = Visibility.Visible;
                    var (cellAddress, next) = CellAddress.Parse(starsModel[i], 0, model);
                    if (cellAddress != null)
                    {
                        MainWindow.PutAnythingOnNode(view, cellAddress.ToIndex(model), (left, top) =>
                        {
                            // 大きさ☆（＾～＾） 黒石と間違わないぐらい小さくしないとな☆（＾～＾）
                            starView.Width = view.board.Width / model.Board.GetColumnDiv() * 0.3;
                            starView.Height = view.board.Height / model.Board.GetRowDiv() * 0.3;

                            Canvas.SetLeft(starView, left - starView.Width / 2);
                            Canvas.SetTop(starView, top - starView.Height / 2);
                        });
                    }
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

            if (view==null)
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
