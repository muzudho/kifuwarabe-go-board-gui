namespace KifuwarabeUec11Gui.Controller
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using KifuwarabeUec11Gui.Model;

    /// <summary>
    /// 石を操作するぜ☆（＾～＾）
    /// 
    /// こっちを設定して、あっちを設定して、また　こっちに戻ってきて設定して、というような
    /// 無限ループしないようにセットするのもコントローラーのメリットだぜ☆（＾～＾）
    /// </summary>
    public static class StoneController
    {
        public static void Repaint(ApplicationObjectModel model, MainWindow view, int zShapedIndex)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // ビュー☆（＾～＾）
            {
                var stone = view.GetStone(zShapedIndex);

                if (zShapedIndex < model.Board.Stones.Count)
                {
                    switch (model.Board.Stones[zShapedIndex])
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
        /// 黒石に変えようぜ☆（＾～＾）
        /// </summary>
        public static void ChangeColorToBlack(ApplicationObjectModel model, MainWindow view, int zShapedIndex)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // モデル☆（＾～＾）
            {
                model.Board.SetStone(zShapedIndex, Stone.Black);
            }

            // ビュー☆（＾～＾）
            {
                var stone = view.GetStone(zShapedIndex);
                stone.Fill = Brushes.Black;
                stone.Stroke = Brushes.White;
                stone.Visibility = Visibility.Visible;
            }
        }

        public static void ChangeColorToWhite(ApplicationObjectModel model, MainWindow view, int zShapedIndex)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // モデル☆（＾～＾）
            {
                model.Board.SetStone(zShapedIndex, Stone.White);
            }

            // ビュー☆（＾～＾）
            {
                var stone = view.GetStone(zShapedIndex);
                stone.Fill = Brushes.White;
                stone.Stroke = Brushes.Black;
                stone.Visibility = Visibility.Visible;
            }
        }

        public static void ChangeColorToSpace(ApplicationObjectModel model, MainWindow view, int zShapedIndex)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            // モデル☆（＾～＾）
            {
                model.Board.SetStone(zShapedIndex, Stone.None);
            }

            // ビュー☆（＾～＾）
            {
                var stone = view.GetStone(zShapedIndex);
                stone.Visibility = Visibility.Hidden;
            }
        }
    }
}
