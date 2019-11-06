namespace KifuwarabeUec11Gui
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using KifuwarabeUec11Gui.Output;
    using KifuwarabeUec11Gui.InputScript;

    /// <summary>
    /// 星を操作するぜ☆（＾～＾）
    /// </summary>
    public static class StarController
    {
        public static void Repaint(BoardModel model, MainWindow view)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            string[] starSigns;

            if (model.RowSize == 19 && model.ColumnSize == 19)
            {
                // 19路盤。囲碁盤☆（＾～＾）
                starSigns = new string[] { "D16", "K16", "Q16", "D10", "K10", "Q10", "D4", "K4", "Q4" };
            }
            else if (model.RowSize == 15 && model.ColumnSize == 15)
            {
                // 15道盤。連珠盤☆（＾～＾）
                starSigns = new string[] { "D4", "M4", "H8", "D12", "M12" };
            }
            else if (model.RowSize == 13 && model.ColumnSize == 13)
            {
                // 13路盤。囲碁盤☆（＾～＾）
                starSigns = new string[] { "D4", "G7", "K4", "D10", "K10" };
            }
            else if (model.RowSize == 10 && model.ColumnSize == 10)
            {
                // 10路盤。将棋盤☆（＾～＾）
                starSigns = new string[] { "D4", "G4", "D7", "G7" };
            }
            else if (model.RowSize == 9 && model.ColumnSize == 9)
            {
                // 9路盤。囲碁盤☆（＾～＾）
                starSigns = new string[] { "E5" };
            }
            else
            {                
                starSigns = Array.Empty<string>();
            }

            for (var i = 0; i < HyperParameter.MaxStarCount; i++)
            {
                var star = view.Stars[i];
                if (i < starSigns.Length)
                {
                    star.Visibility = Visibility.Visible;
                    var (cellAddress, next) = CellAddress.Parse(starSigns[i], 0, view.BoardModel);
                    if (cellAddress != null)
                    {
                        MainWindow.PutAnythingOnNode(view, cellAddress.ToIndex(view.BoardModel), (left, top) =>
                        {
                            // 大きさ☆（＾～＾） 黒石と間違わないぐらい小さくしないとな☆（＾～＾）
                            star.Width = view.board.Width / view.BoardModel.GetColumnDiv() * 0.3;
                            star.Height = view.board.Height / view.BoardModel.GetRowDiv() * 0.3;

                            Canvas.SetLeft(star, left - star.Width / 2);
                            Canvas.SetTop(star, top - star.Height / 2);
                        });
                    }
                }
                else
                {
                    star.Visibility = Visibility.Hidden;
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
